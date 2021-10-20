using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ConsoleOnlineStore.Models;
using ConsoleOnlineStore.Services;
using System.Security;

namespace ConsoleOnlineStore.Logic
{
    public class AuthService
    {
        public const int MinLenght = 3;
        
        private readonly List<User> _users;

        public AuthService()
        {
            _users = JsonStorage.GetUsers();
        }
        
        public bool Login(User user)
        {
            if (_users is null)
            {
                return false;
            }

            foreach (User item in _users)
            {
                if (item.Login == user.Login && item.Password == user.Password)
                {
                    user.Name = item.Name;
                    PurchaseHistory.Login = item.Login;
                    return true;
                }
            }

            return false;
        }

        public bool AddNewUser(User user)
        {
            if (_users is null)
            {
                return false;
            }
            
            foreach (User item in _users)
            {
                if (item.Login == user.Login)
                {
                    return true;
                }
            }

            return false;
        }

        public string SecurePassword()
        {
            string securePwd = String.Empty;
            ConsoleKeyInfo key;
            
            do 
            {
                key = Console.ReadKey(true);

                securePwd += key.KeyChar;
                Console.Write("*");
            } 
            
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return securePwd.Trim();
        }
        
        public string GetHash(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
        
    }
}
