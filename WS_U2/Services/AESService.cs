using System.Security.Cryptography;
using System.Text;
using WS_U2.Interfaces;

namespace WS_U2.Services
{
    public class AESService : IAESService
    {
        public string GenerateEncryptedUsername(int length)
        {
            IPService ipService = new();
            var ip = ipService.GetIPAsync();
            byte[] encryptedUsername;

            using (Aes aes = Aes.Create())
            {
                string key = "bm90c29zZWN1cmU=";
                string iv = "ZXJ1Y2Vzb3N0b24=";

                aes.Key = Encoding.ASCII.GetBytes(key);
                aes.IV = Encoding.ASCII.GetBytes(iv);

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(ip);
                        }
                        encryptedUsername = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedUsername).Substring(0, length);
        }
    }
}