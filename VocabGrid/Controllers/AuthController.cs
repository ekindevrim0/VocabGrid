using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
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
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Email and Password are required.");
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
            Username = request.Email.Split('@')[0],
            Email = request.Email.ToLower(),
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
            User = new { user.Id, user.Username, user.Email }
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Email and Password are required.");
        }

        var userRepository = _unitOfWork.Repository<User>();

        var users = await userRepository.FindAsync(u => u.Email.ToLower() == request.Email.ToLower());
        var user = users.FirstOrDefault();

        if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Invalid credentials.");
        }

        string token = CreateToken(user);

        return Ok(new
        {
            Message = "Login successful.",
            Token = token,
            User = new { user.Id, user.Username, user.Email }
        });
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleAuth([FromBody] GoogleAuthDto dto)
    {
        if (string.IsNullOrEmpty(dto.GoogleId) || string.IsNullOrEmpty(dto.Email))
        {
            return BadRequest("Google ID and Email are required.");
        }

        var userRepository = _unitOfWork.Repository<User>();

        var existingUsersByGoogleId = await userRepository.FindAsync(u => u.GoogleId == dto.GoogleId);
        var user = existingUsersByGoogleId.FirstOrDefault();

        if (user == null)
        {
            var existingUsersByEmail = await userRepository.FindAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            user = existingUsersByEmail.FirstOrDefault();

            if (user != null)
            {
                user.GoogleId = dto.GoogleId;
                userRepository.Update(user);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                user = new User
                {
                    Username = string.IsNullOrWhiteSpace(dto.Name) ? dto.Email.Split('@')[0] : dto.Name,
                    Email = dto.Email.ToLower(),
                    GoogleId = dto.GoogleId,
                    PasswordHash = Array.Empty<byte>(),
                    PasswordSalt = Array.Empty<byte>()
                };

                await userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();
            }
        }

        string token = CreateToken(user);

        return Ok(new
        {
            Message = "Google authentication successful.",
            Token = token,
            User = new { user.Id, user.Username, user.Email, user.GoogleId }
        });
    }

    [HttpPost("apple")]
    public async Task<IActionResult> AppleAuth([FromBody] AppleAuthDto dto)
    {
        if (string.IsNullOrEmpty(dto.AppleId) || string.IsNullOrEmpty(dto.Email))
        {
            return BadRequest("Apple ID and Email are required.");
        }

        var userRepository = _unitOfWork.Repository<User>();

        var existingUsersByAppleId = await userRepository.FindAsync(u => u.AppleId == dto.AppleId);
        var user = existingUsersByAppleId.FirstOrDefault();

        if (user == null)
        {
            var existingUsersByEmail = await userRepository.FindAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            user = existingUsersByEmail.FirstOrDefault();

            if (user != null)
            {
                user.AppleId = dto.AppleId;
                userRepository.Update(user);
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                user = new User
                {
                    Username = string.IsNullOrWhiteSpace(dto.Name) ? dto.Email.Split('@')[0] : dto.Name,
                    Email = dto.Email.ToLower(),
                    AppleId = dto.AppleId,
                    PasswordHash = Array.Empty<byte>(),
                    PasswordSalt = Array.Empty<byte>()
                };

                await userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();
            }
        }

        string token = CreateToken(user);

        return Ok(new
        {
            Message = "Apple authentication successful.",
            Token = token,
            User = new { user.Id, user.Username, user.Email, user.AppleId }
        });
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username ?? string.Empty)
        };

        var keySecret = _configuration["Jwt:Secret"] ?? "SuperSecretKeyForVocabGridApplication2026";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}