using System.Collections.Generic;

namespace ConsoleOnlineStore
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
    }
}
