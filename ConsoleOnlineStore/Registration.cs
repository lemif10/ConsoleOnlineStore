using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class Registration
    {
        public bool NewUser(List<User> users, User user)
        {
            foreach (User item in users)
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