using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CEM.ProgDec.BL.Models;
using CEM.ProgDec.PL;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CEM.ProgDec.BL
{
    public class LoginFailureExecption : Exception
    {
        public LoginFailureExecption(): base("Cannot log in with these credentials. Your IP has been saved.") 
        {
            
        }

        public LoginFailureExecption(string message) : base(message)
        {
            
        }
    }

    public static class UserManager
    {
         private static string GetHash(string password)
        {
            using(var hasher = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public static int DeleteAll()
        {
            return 0;
        }

        public static int Insert(User user, bool rollback = false)
        {
            return 0;
        }

        public static bool Login(User user)
        {
            return false;
        }

        public static void Seed()
        {

        }
    }
}
