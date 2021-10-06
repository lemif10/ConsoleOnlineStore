using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class LoginInStore
    {
        public delegate void LoginHandling(string message);

        public event LoginHandling Notify;

        public void Join(User user)
        {
            Console.Write("Введите логин: ");
            user.Login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            user.Password = Console.ReadLine();

            if (CheckLoginAndPassword(JsonSerializer.GetUser(), user))
            {
                Console.Clear();
                Notify?.Invoke($"Приветствуем в нашем магазине, {user.Name}!");
            }
            else
            {
                Console.Clear();
                Notify?.Invoke("Вы неверно указали логин или пароль. Попробуйте ещё раз");
                Join(user);
            }
        }

        private bool CheckLoginAndPassword(List<User> users, User user)
        {
            if (users is null) return false;

            foreach (var item in users)
                if (item.Login == user.Login)
                {
                    if (item.Password == user.Password)
                    {
                        user.Name = item.Name;
                        return true;
                    }

                    return false;
                }

            return false;
        }
    }
}