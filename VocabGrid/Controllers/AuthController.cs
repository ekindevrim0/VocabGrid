using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private const string AppleIssuer = "https://appleid.apple.com";
    private const string AppleOpenIdConfigurationUrl =
        "https://appleid.apple.com/.well-known/openid-configuration";

    private static readonly ConfigurationManager<OpenIdConnectConfiguration> AppleConfigurationManager =
        new(
            AppleOpenIdConfigurationUrl,
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever { RequireHttps = true });

    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var userRepository = _unitOfWork.Repository<User>();

        var existingUser = await userRepository.FindAsync(u => u.Email.ToLower() == request.Email.ToLower());
        if (existingUser.Any())
        {
            return BadRequest("User with this email already exists.");
        }

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Username = request.Email.Split('@')[0],
            Email = request.Email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        string token = CreateToken(user);

        return Ok(new
        {
            Message = "Registration successful.",
            Token = token,
            User = new { user.Id, user.FirstName, user.LastName, user.Username, user.Email }
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Email and Password are required.");
        }

        var userRepository = _unitOfWork.Repository<User>();

        var users = await userRepository.FindAsync(u => u.Email.ToLower() == request.Email.ToLower());
        var user = users.FirstOrDefault();

        if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Unauthorized("Invalid credentials.");
        }

        string token = CreateToken(user);

        return Ok(new
        {
            Message = "Login successful.",
            Token = token,
            User = new { user.Id, user.FirstName, user.LastName, user.Username, user.Email }
        });
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleAuth([FromBody] GoogleAuthDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.IdToken))
        {
            return BadRequest("Google IdToken is required.");
        }

        var clientId = _configuration["Authentication:Google:ClientId"];
        if (string.IsNullOrWhiteSpace(clientId))
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                "Google authentication is not configured. Set Authentication:Google:ClientId.");
        }

        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(
                dto.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                });
        }
        catch (InvalidJwtException)
        {
            return Unauthorized("Invalid Google IdToken.");
        }

        var googleId = payload.Subject;
        var email = payload.Email?.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("Google account email is required.");
        }

        var displayName = payload.Name ?? string.Empty;
        var user = await FindOrLinkSocialUserAsync(
            googleId: googleId,
            appleId: null,
            email: email,
            displayName: displayName);

        string token = CreateToken(user);

        return Ok(new
        {
            Message = "Google authentication successful.",
            Token = token,
            User = new { user.Id, user.FirstName, user.LastName, user.Username, user.Email, user.GoogleId }
        });
    }

    [HttpPost("apple")]
    public async Task<IActionResult> AppleAuth([FromBody] AppleAuthDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.IdToken))
        {
            return BadRequest("Apple IdToken is required.");
        }

        var clientId = _configuration["Authentication:Apple:ClientId"];
        if (string.IsNullOrWhiteSpace(clientId))
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                "Apple authentication is not configured. Set Authentication:Apple:ClientId.");
        }

        // Apple identity must come from a JWKS-validated IdToken, never from client-supplied AppleId/Email alone.
        ClaimsPrincipal principal;
        try
        {
            principal = await ValidateAppleIdTokenAsync(dto.IdToken, clientId);
        }
        catch (SecurityTokenException)
        {
            return Unauthorized("Invalid Apple IdToken.");
        }
        catch (Exception)
        {
            return Unauthorized("Apple IdToken validation failed.");
        }

        var appleId = principal.FindFirstValue("sub")
                      ?? principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = (principal.FindFirstValue(ClaimTypes.Email)
                     ?? principal.FindFirstValue("email"))
                    ?.Trim()
                    .ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(appleId))
        {
            return Unauthorized("Apple subject claim is missing.");
        }

        // Apple may omit email on subsequent logins; require it for first-time account creation.
        var userRepository = _unitOfWork.Repository<User>();
        var existingByApple = (await userRepository.FindAsync(u => u.AppleId == appleId)).FirstOrDefault();
        if (existingByApple == null && string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("Apple account email is required for first-time sign-in.");
        }

        var displayName = string.IsNullOrWhiteSpace(dto.Name) ? string.Empty : dto.Name.Trim();
        var user = await FindOrLinkSocialUserAsync(
            googleId: null,
            appleId: appleId,
            email: email ?? existingByApple!.Email,
            displayName: displayName);

        string token = CreateToken(user);

        return Ok(new
        {
            Message = "Apple authentication successful.",
            Token = token,
            User = new { user.Id, user.FirstName, user.LastName, user.Username, user.Email, user.AppleId }
        });
    }
[HttpPost("forgot-password")]
public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request, [FromServices] IEmailService emailService)
{
    if (string.IsNullOrEmpty(request.Email))
        return BadRequest("Email is required.");

    var userRepository = _unitOfWork.Repository<User>();
    var users = await userRepository.FindAsync(u => u.Email == request.Email);
    var user = users.FirstOrDefault();

    // Return Ok even if user isn't found to prevent email enumeration
    if (user == null)
        return Ok(new { Message = "If an account exists, a password reset token has been generated." });

    var resetToken = Guid.NewGuid().ToString("N");

   // Change ExpiryTime to ExpiresAt
var tokenEntity = new PasswordResetToken
{
    UserId = user.Id,
    Token = resetToken,
    ExpiresAt = DateTime.UtcNow.AddMinutes(15), // 👈 Updated here
    IsUsed = false
};

    var tokenRepository = _unitOfWork.Repository<PasswordResetToken>();
    await tokenRepository.AddAsync(tokenEntity);
    await _unitOfWork.CompleteAsync();

    await emailService.SendPasswordResetEmailAsync(user.Email, resetToken);

    return Ok(new { Message = "If an account exists, a password reset token has been generated." });
}

[HttpPost("reset-password")]
public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
{
    if (string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.NewPassword))
        return BadRequest("Token and new password are required.");

    var tokenRepository = _unitOfWork.Repository<PasswordResetToken>();
    var tokens = await tokenRepository.FindAsync(t => t.Token == request.Token);
    var resetToken = tokens.FirstOrDefault();

   // Change ExpiryTime to ExpiresAt
if (resetToken == null || resetToken.IsUsed || resetToken.ExpiresAt <= DateTime.UtcNow) // 👈 Updated here
    return BadRequest("Invalid, expired, or already used password reset token.");
    var userRepository = _unitOfWork.Repository<User>();
    var user = await userRepository.GetByIdAsync(resetToken.UserId);

    if (user == null)
        return BadRequest("Associated user not found.");

    // Update password hash/salt using your existing hashing method
    CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
    user.PasswordHash = passwordHash;
    user.PasswordSalt = passwordSalt;

    // Mark token as used
    resetToken.IsUsed = true;

    await _unitOfWork.CompleteAsync();

    return Ok(new { Message = "Password has been successfully reset." });
}
    private async Task<User> FindOrLinkSocialUserAsync(
        string? googleId,
        string? appleId,
        string email,
        string displayName)
    {
        var userRepository = _unitOfWork.Repository<User>();
        User? user = null;

        if (!string.IsNullOrWhiteSpace(googleId))
        {
            user = (await userRepository.FindAsync(u => u.GoogleId == googleId)).FirstOrDefault();
        }
        else if (!string.IsNullOrWhiteSpace(appleId))
        {
            user = (await userRepository.FindAsync(u => u.AppleId == appleId)).FirstOrDefault();
        }

        if (user == null)
        {
            user = (await userRepository.FindAsync(u => u.Email.ToLower() == email.ToLower())).FirstOrDefault();
            if (user != null)
            {
                if (!string.IsNullOrWhiteSpace(googleId))
                {
                    user.GoogleId = googleId;
                }

                if (!string.IsNullOrWhiteSpace(appleId))
                {
                    user.AppleId = appleId;
                }

                userRepository.Update(user);
                await _unitOfWork.CompleteAsync();
            }
        }

        if (user == null)
        {
            var (firstName, lastName) = SplitDisplayName(displayName, email);
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = email.Split('@')[0],
                Email = email.ToLowerInvariant(),
                GoogleId = googleId,
                AppleId = appleId,
                PasswordHash = Array.Empty<byte>(),
                PasswordSalt = Array.Empty<byte>()
            };

            await userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        return user;
    }

    private static (string FirstName, string LastName) SplitDisplayName(string displayName, string email)
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            return (email.Split('@')[0], "User");
        }

        var parts = displayName.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1)
        {
            return (parts[0], "User");
        }

        return (parts[0], parts[1]);
    }

    private static async Task<ClaimsPrincipal> ValidateAppleIdTokenAsync(string idToken, string clientId)
    {
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(idToken))
        {
            throw new SecurityTokenException("Apple IdToken is not a readable JWT.");
        }

        var appleConfig = await AppleConfigurationManager.GetConfigurationAsync(CancellationToken.None);

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AppleIssuer,
            ValidateAudience = true,
            ValidAudience = clientId,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = appleConfig.SigningKeys,
            ClockSkew = TimeSpan.FromMinutes(1)
        };

        return handler.ValidateToken(idToken, parameters, out _);
    }

    private string CreateToken(User user)
    {
        var keySecret = _configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(keySecret))
        {
            throw new InvalidOperationException(
                "Jwt:Key is missing. Configure User Secrets or environment variables.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
            new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
            new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        if (passwordHash.Length == 0 || passwordSalt.Length == 0)
        {
            return false;
        }

        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}
