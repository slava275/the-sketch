using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Application.DTOs.Auth;

namespace TheSketch.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
}
