using FSL.BlazorCookies.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FSL.BlazorCookies.Provider
{
    public sealed class DESKeyCryptographyProvider : ICryptographyProvider
    {
        private readonly MySettingsConfiguration _configuration;
        private readonly byte[] _iv = { 12, 34, 56, 78, 90, 102, 114, 126 };

        public DESKeyCryptographyProvider(
            MySettingsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DeCrypt(
            string info)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs; byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                var conteudo = info.Replace(" ", "+").Replace("[i]", "=").Replace("[e]", "&").Replace("[m]", "+").Replace("[n]", "-");

                input = new byte[conteudo.Length];
                input = Convert.FromBase64String(conteudo);

                var key = Encoding.UTF8.GetBytes(_configuration.CryptographicKey.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateDecryptor(key, _iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Encrypt(
            string info)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs; byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();
                input = Encoding.UTF8.GetBytes(info);

                var key = Encoding.UTF8.GetBytes(_configuration.CryptographicKey.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateEncryptor(key, _iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray()).Replace("=", "[i]").Replace("&", "[e]").Replace("+", "[m]").Replace("-", "[n]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
