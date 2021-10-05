using System;

namespace ConsoleOnlineStore
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayLoginWindow();
        }

        static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        static void DisplayLoginWindow()
        {
            while (true)
            {
                Console.WriteLine("1. Войти в свой аккаунт\n2. Пройти регистрацию.");
                
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                Console.Clear();
                
                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    
                    LoginInStore loginInStore = new LoginInStore();

                    loginInStore.Notify += PrintMessage;
            
                    loginInStore.Join();
                    
                    break;
                }
                
                if(key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                
                    Registration registration = new Registration();

                    registration.Notify += PrintMessage;
            
                    registration.NewUser(AddOrTakeFormJson.TakeUsersForCheck());
            
                    AddOrTakeFormJson.AddNewUser(registration);
                }
            }
        }
    }
}