using System;

namespace ConsoleOnlineStore
{
    public class Registration : LoginData
    {
        public void NewUser()
        {
            Console.Write("Укажите ваш логин: ");
            Login = Console.ReadLine();
            
            Console.Write("Укажите ваш пароль: ");
            Password = Console.ReadLine();
            
            Console.Write("Укажите ваше имя: ");
            Name = Console.ReadLine();
        }
    }
}