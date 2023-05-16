using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace CandidateBrowserCleanArch.Identity;

internal class EncryptService : IEncryptService
{
    private readonly IConfiguration _configuration;
    

    public EncryptService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> DecryptAsync(string encrypted)
    {

        string decrypted = null;
        byte[] cipher = Convert.FromBase64String(encrypted);

        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(_configuration["EncodingKey"]);
           // aes.Key = Convert.FromBase64String(_configuration["Encoding:EncodingKey"]);
            aes.IV = Convert.FromBase64String(_configuration["EncodingKey"]);
           // aes.IV = Convert.FromBase64String(_configuration["Encoding:EncodingKey"]);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform dec = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipher))
            {
                using (CryptoStream cs = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        decrypted = sr.ReadToEnd();
                    }
                }
            }
        }

        return decrypted;

    }
    

public async Task<string> EncryptAsync(string plainText)
{
        byte[] encrypted;

        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(_configuration["EncodingKey"]);
            aes.IV = Convert.FromBase64String(_configuration["EncodingKey"]);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform enc = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    encrypted = ms.ToArray();
                }
            }
        }

        return Convert.ToBase64String(encrypted);

    }
}

