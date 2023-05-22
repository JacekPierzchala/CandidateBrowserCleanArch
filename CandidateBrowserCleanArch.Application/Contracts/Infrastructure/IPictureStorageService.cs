using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public interface IPictureStorageService
{
    Task<string> GetPicture(string fileName);
    Task<string> UploadPicture(string pictureData, string fileName, string fileNameOld);
}
