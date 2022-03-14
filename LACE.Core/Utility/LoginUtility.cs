using Nedesk.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace LACE.Core.Utility
{
    public static class LoginUtility
    {
        public static string EncryptPassword(string password)
        {
            StringBuilder sb = new();

            HMACSHA512 hash = new HMACSHA512(Encoding.UTF8.GetBytes("<NEDESK>"));

            byte[] buffer = Encoding.UTF8.GetBytes(password);
            byte[] secretBuffer = Encoding.UTF8.GetBytes("<NEDESK>");

            byte[] hashPassword = hash.ComputeHash(buffer);
            byte[] hashSecret = hash.ComputeHash(secretBuffer);

            foreach(byte b in hashPassword)
            {
                sb.Append(b.ToString("X2"));
            }

            foreach(byte b in hashSecret)
            {
                sb.Append(b.ToString("X2"));
            }

            byte[] salt = hash.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));

            sb.Clear();

            foreach(byte b in salt)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string GenerateSession(IAuthUser authUser)
        {
            StringBuilder sb = new();

            HMACMD5 hash = new HMACMD5(Encoding.UTF8.GetBytes("<NEDESK>"));

            string password = $"{authUser.Name}├┼╚╩╔╦8,◙þþ{DateTime.Now}";

            byte[] buffer = Encoding.UTF8.GetBytes(password);
            byte[] secretBuffer = Encoding.UTF8.GetBytes("<NEDESK>");

            byte[] hashPassword = hash.ComputeHash(buffer);
            byte[] hashSecret = hash.ComputeHash(secretBuffer);

            foreach (byte b in hashPassword)
            {
                sb.Append(b.ToString("X2"));
            }

            foreach (byte b in hashSecret)
            {
                sb.Append(b.ToString("X2"));
            }

            byte[] salt = hash.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));

            sb.Clear();

            foreach (byte b in salt)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

    }
}
