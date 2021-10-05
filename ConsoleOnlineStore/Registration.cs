using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class Registration : LoginData
    {
        public delegate void RegistrationHandling(string message);
        
        public event RegistrationHandling Notify;
        
        public void NewUser(List<LoginInStore> users)
        {
            Console.Write("Укажите ваш логин: ");
            Login = Console.ReadLine();
            
            Console.Write("Укажите ваш пароль: ");
            Password = Console.ReadLine();
            
            Console.Write("Укажите ваше имя: ");
            Name = Console.ReadLine();
            
            Console.WriteLine("1. Подтвердить регистрацию.\n2. Пройти регистрацию заново.");
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            if (key.Key == ConsoleKey.D1)
            {
                if (users != null)
                {
                    foreach (LoginInStore user in users)
                    {
                        if (user.Login == Login)
                        {
                            Console.Clear();
                            Notify?.Invoke("Этот логин занят, попробуйте ввести другой!");
                            NewUser(AddOrTakeFormJson.TakeUsersForCheck());
                        }
                    }
                }
                
                Console.Clear();
                Notify?.Invoke("Регистрация прошла успешно!");
            }
            else if(key.Key == ConsoleKey.D2)
            {
                Console.Clear();
                NewUser(AddOrTakeFormJson.TakeUsersForCheck());
            }
        }
    }
}