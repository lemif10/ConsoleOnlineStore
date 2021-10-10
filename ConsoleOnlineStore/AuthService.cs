using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class AuthService
    {
        private readonly List<User> _users;

        public AuthService()
        {
            _users = JsonStorage.GetUser();
        }
        
        public bool Join(User user)
        {
            if (_users is null)
            {
                return false;
            }

            foreach (User item in _users)
            {
                if (item.Login == user.Login)
                {
                    if (item.Password == user.Password)
                    {
                        user.Name = item.Name;
                        Basket.Login = item.Login;
                        return true;
                    }

                    return false;
                }

            }

            return false;
        }
        
        public bool NewUser(User user)
        {
            if (_users is null)
            {
                return true;
            }
            
            foreach (User item in _users)
            {
                if (item.Login == user.Login)
                {
                    return false;
                }
            }

            return true;
        }
    }
}