using TheSketch.Application.DTOs.Auth;
using TheSketch.Application.Interfaces.Repositories;
using TheSketch.Application.Interfaces.Services;
using TheSketch.Domain.Entities;
using TheSketch.Domain.Exceptions;

namespace TheSketch.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;
    private readonly ITokenService tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UserException($"User with email {request.Email} does not exist.");
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new UserException("Invalid password.");
        }

        var token = tokenService.GenerateJwtToken(user);
        return new AuthResponseDto(user.Email, token, user.Role);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new UserException($"User with email {request.Email} already exists.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = "User"
        };

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        var token = tokenService.GenerateJwtToken(user);

        return new AuthResponseDto(user.Email, token, user.Role);
    }
}
