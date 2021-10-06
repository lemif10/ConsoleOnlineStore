using System;

namespace ConsoleOnlineStore
{
    class Program
    {
        static void Main()
        {
            DisplayLoginWindow();
            
            DisplayMainWindow();
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
                
                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                
                    Registration registration = new Registration();

                    registration.Notify += PrintMessage;
            
                    registration.NewUser(AddOrTakeFormJson.TakeUsersForCheck());
            
                    AddOrTakeFormJson.AddNewUser(registration);
                }
            }
        }

        static void DisplayMainWindow()
        {
            while (true)
            {
                Console.WriteLine("1. Посмотреть каталог\n2. Посмотреть корзину.\n3. Посмотреть историю покупок\n4. Выйти из аккаунта");
                
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                Console.Clear();
                
                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    Catalog.ShowGoods(AddOrTakeFormJson.TakeGoods());
                    break;
                }
                
                if (key.Key == ConsoleKey.D2)
                {

                }
                
                if (key.Key == ConsoleKey.D3)
                {

                }
                
                if (key.Key == ConsoleKey.D4)
                {
                    Console.Clear();
                    Main();
                }
            }
        }
    }
}