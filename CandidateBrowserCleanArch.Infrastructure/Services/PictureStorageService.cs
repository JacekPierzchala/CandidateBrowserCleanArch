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
    private readonly IWebHostEnvironment _webHostEnvironment;

    private  Uri _url=> new Uri($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}");

    public PictureStorageService(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<string> GetPicture(string fileName)
    {
        return  $"{_url}profileImages/{fileName}"; 
    }

    public async Task<string> UploadPicture(string pictureData,string fileName, string fileNameOld)
    {
        if (!string.IsNullOrEmpty(pictureData))
        {
            var newFile=await CreateFile(pictureData, fileName);
            var picName = Path.GetFileName(fileNameOld);

            var path = $"{_webHostEnvironment.WebRootPath}\\profileImages\\{fileNameOld}";
            if (File.Exists(path) && fileNameOld!= "avatar.png")
            {
                File.Delete(path);
            }
            return  newFile;
        }
        return string.Empty;
    }

    private async Task<string> CreateFile(string imageBase64, string imageName)
    {
        var ext = Path.GetExtension(imageName);
        var fileName = $"{Guid.NewGuid()}{ext}";

        var path =$"{_webHostEnvironment.WebRootPath}\\profileImages\\{fileName}";

        byte[] image = Convert.FromBase64String(imageBase64);

        var fileStream = File.Create(path);
        await fileStream.WriteAsync(image, 0, image.Length);
        fileStream.Close();

        return fileName;
    }
}
