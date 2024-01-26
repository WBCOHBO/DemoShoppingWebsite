using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DemoShoppingWebsite.Models
{
    public partial class dbShoppingCarAzureEntities
    {
        //讓dbShoppingCarAzureEntities的base("name=dbShoppingCarAzureEntities")不直接對應Web.config內<connectionStrings>標籤下的<add name="dbShoppingCarAzureEntities".../>內容。
        public dbShoppingCarAzureEntities(string cnStr) : base(cnStr)
        {
            
        }
    }

    public class EncryptService
    {
        //可自行替換key與iv內容
        static string keystr = "01234567";
        static string ivstr = "abcdefgh";

        public static string EncryptBase64(string SourceStr)
        {
            string encrypt = string.Empty;

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(keystr));
            byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(ivstr));
            aes.Key = key;
            aes.IV = iv;

            byte[] dataByteArray = Encoding.UTF8.GetBytes(SourceStr);
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }

        public static string DecryptBase64(string EncryptStr)
        {
            string decrypt = string.Empty;
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(keystr));
            byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(ivstr));
            aes.Key = key;
            aes.IV = iv;
            byte[] dataByteArray = Convert.FromBase64String(EncryptStr);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return decrypt;
        }

        public class ConnectStringService
        {
            public static dbShoppingCarAzureEntities CreateDBContext()
            {
                var db_ConnStr = ConfigurationManager.AppSettings["db_ConnStr"];
                var db_Pwd = EncryptService.DecryptBase64(ConfigurationManager.AppSettings["db_Pwd"]);

                var db_ConnStrFull = $@"{db_ConnStr}password={db_Pwd}""";

                return new dbShoppingCarAzureEntities(db_ConnStrFull);
            }
        }
    }
}