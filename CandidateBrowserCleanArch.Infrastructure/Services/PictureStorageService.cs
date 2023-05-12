using CandidateBrowserCleanArch.Application;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Infrastructure;

public class PictureStorageService : IPictureStorageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PictureStorageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<string> GetPicture(string fileName)
    {
        var url = new Uri($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}");
        return  $"{url}profileImages/{fileName}"; ;
    }
}
