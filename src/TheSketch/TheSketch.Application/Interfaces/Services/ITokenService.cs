using System;
using System.Collections.Generic;
using System.Text;
using TheSketch.Domain.Entities;

namespace TheSketch.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateJwtToken(User user);
}
