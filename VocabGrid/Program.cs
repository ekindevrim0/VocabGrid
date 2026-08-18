using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VocabGrid.Data;
using VocabGrid.Interfaces;
using VocabGrid.Repositories;
using VocabGrid.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "VocabGrid API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token değerinizi girin."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    // Development: open for local Flutter/web testing.
    // Production: set Cors:AllowedOrigins in config (comma-separated).
    options.AddPolicy("AppCors", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
            return;
        }

        var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                      ?? Array.Empty<string>();
        if (origins.Length == 0)
        {
            policy.SetIsOriginAllowed(_ => false);
            return;
        }

        policy.WithOrigins(origins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();

// UseExceptionHandler, govdesiz bir 500 yerine ProblemDetails uretsin diye.
builder.Services.AddProblemDetails();

// Kimlik uc noktalarinda hiz siniri. Parola deneyen tek yollar bunlar;
// sinir yokken bir parola kaba kuvvetle denenebilirdi. Bolum anahtari IP:
// e-postaya gore bolmek, saldirganin baskasinin hesabini kilitlemesine
// izin verirdi.
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy(RateLimitPolicies.Credentials, context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(5),
                QueueLimit = 0
            }));

    // Kayit ve posta gonderen uc noktalarda sinir daha gevsek: burada
    // korunan bir sir degil, kayit ve e-posta kuyrugunun kotuye kullanimi.
    options.AddPolicy(RateLimitPolicies.Registration, context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(15),
                QueueLimit = 0
            }));
});

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "Jwt:Key yapılandırması eksik. User Secrets veya ortam değişkeni ile ayarlayın; hard-coded secret kullanmayın.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Yakalanmayan bir istisna aksi halde geliştirme sayfasını, yani yığın izini
// ve kaynak parçalarını istemciye gönderiyordu. Bu, ortamdan bağımsız olarak
// RFC 9457 ProblemDetails döndürür; ayrıntı sunucu günlüğünde kalır.
app.UseExceptionHandler();

app.UseCors("AppCors");

// Kimlik doğrulamadan önce: sınıra takılan istek, parola karşılaştırma
// maliyetine hiç girmeden reddedilmeli.
app.UseRateLimiter();

var webRoot = Path.Combine(app.Environment.ContentRootPath, "wwwroot");
Directory.CreateDirectory(Path.Combine(webRoot, "uploads"));
app.Environment.WebRootPath ??= webRoot;
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
