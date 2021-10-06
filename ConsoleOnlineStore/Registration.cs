using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class Registration
    {
        public delegate void RegistrationHandling(string message);

        public event RegistrationHandling Notify;

        public User NewUser(List<User> users, User user)
        {
            Console.Write("Укажите ваш логин: ");
            user.Login = Console.ReadLine();

            Console.Write("Укажите ваш пароль: ");
            user.Password = Console.ReadLine();

            Console.Write("Укажите ваше имя: ");
            user.Name = Console.ReadLine();

            Console.WriteLine("1. Подтвердить регистрацию.\n2. Пройти регистрацию заново.");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.D1)
            {
                if (users != null)
                    foreach (User item in users)
                        if (user.Login == item.Login)
                        {
                            Console.Clear();
                            Notify?.Invoke("Этот логин занят, попробуйте ввести другой!");
                            NewUser(JsonSerializer.GetUser(), user);
                        }

                Console.Clear();
                Notify?.Invoke("Регистрация прошла успешно!");
                return user;
            }
            else if (key.Key == ConsoleKey.D2)
            {
                Console.Clear();
                return NewUser(users, user);
            }
            
            return NewUser(users, user);
        }
    }
}