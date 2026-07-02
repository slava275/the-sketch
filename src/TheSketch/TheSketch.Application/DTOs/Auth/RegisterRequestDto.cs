using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Application.DTOs.Auth;

public record RegisterRequestDto(string Email, string Password, string ConfirmPassword);
public record LoginRequestDto(string Email, string Password);

public record AuthResponseDto(string Email, string Token, string Role);
