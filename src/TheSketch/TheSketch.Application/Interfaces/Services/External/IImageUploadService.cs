using System;
using System.Collections.Generic;
using System.Text;

namespace TheSketch.Application.Interfaces.Services.External;

public interface IImageUploadService
{
    Task<string> UploadImageAsync(Stream stream, string fileName);
}
