using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity.Interfaces;

internal interface IEncryptService
{
    Task<string> EncryptAsync(string plainText);

    Task<string> DecryptAsync(string ciphertext);

}

