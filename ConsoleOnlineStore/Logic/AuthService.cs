using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ConsoleOnlineStore.Models;
using ConsoleOnlineStore.Services;

namespace ConsoleOnlineStore.Logic
{
    public class AuthService
    {
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

        public string GetHash(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
    }
}
