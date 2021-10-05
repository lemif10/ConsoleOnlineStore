using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class LoginInStore : LoginData
    {
        public delegate void LoginHandling(string message);

        public event LoginHandling Notify;
        
        public void Join()
        {
            Console.Write("Введите логин: ");
            Login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            Password = Console.ReadLine();

            if (CheckLoginAndPassword(AddOrTakeFormJson.TakeUsersForCheck()))
            {
                Console.Clear();
                Notify?.Invoke($"Приветствуем в нашем магазине, {Name}!");
            }
            else
            {
                Notify?.Invoke("Вы неверно указали логин или пароль. Попробуйте ещё раз");
                Join();
            }
        }

        private bool CheckLoginAndPassword(List<LoginInStore> users)
        {
            if (users is null)
            {
                return false;
            }

            foreach (LoginInStore user in users)
            {
                if (user.Login == Login)
                {
                    if (user.Password == Password)
                    {
                        Name = user.Name;
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }
    }
}