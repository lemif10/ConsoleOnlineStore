using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class LoginInStore
    {
        public bool Join(List<User> users, User user)
        {
            if (users is null) return false;

            foreach (User item in users)
            {
                if (item.Login == user.Login)
                {
                    if (item.Password == user.Password)
                    {
                        user.Name = item.Name;
                        return true;
                    }

                    return false;
                }

            }

            return false;
        }
    }
}