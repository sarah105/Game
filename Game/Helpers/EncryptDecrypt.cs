using System;
using System.Text;

namespace Game.Helpers
{
    public class EncryptDecrypt
    {
        private readonly string key = "@key#?svfzz@bf$bza";
        public string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            //password += key;
            //var enhancedHashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);//hashType: HashType.SHA384 default. max hash string with length 56byte
            password = BCrypt.Net.BCrypt.HashPassword(password);
            return password;
        }

        public bool CheckPassword(string encryptPassword, string userPassword)
        {
            if (string.IsNullOrEmpty(encryptPassword) || string.IsNullOrEmpty(userPassword)) 
                return false;
            //var validatePassword = BCrypt.Net.BCrypt.EnhancedVerify(userPassword+key, encryptPassword);
            return BCrypt.Net.BCrypt.Verify(userPassword, encryptPassword);
        }

        //public string Encrypt2(string password)
        //{
        //    if (string.IsNullOrEmpty(password)) return "";
        //    password += key;
        //    var passwordBytes = Encoding.UTF8.GetBytes(password);
        //    return Convert.ToBase64String(passwordBytes);
        //}
        //public string Decrypt(string encodeData)
        //{
        //    if (string.IsNullOrEmpty(encodeData)) return "";
        //    var encodeBytes = Convert.FromBase64String(encodeData);
        //    var result = Encoding.UTF8.GetString(encodeBytes);
        //    result = result.Substring(0, result.Length - key.Length);
        //    return result;
        //}
    }
}
