using System;

namespace ConsoleOnlineStore
{
    public static class ConsoleInterface
    {
        private static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayLoginWindow()
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

                    loginInStore.Join(new User());

                    break;
                }

                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();

                    Registration registration = new Registration();

                    registration.Notify += PrintMessage;

                    registration.NewUser(JsonSerializer.GetUser(), new User());

                    JsonSerializer.AddNewUser(registration);
                }
            }
        }

        public static void DisplayMainWindow()
        {
            while (true)
            {
                Console.WriteLine(
                    "1. Посмотреть каталог\n" +
                    "2. Посмотреть корзину.\n" +
                    "3. Посмотреть историю покупок\n" +
                    "4. Выйти из аккаунта");

                ConsoleKeyInfo key = Console.ReadKey(true);

                Console.Clear();

                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    Catalog.ShowGoods(JsonSerializer.TakeGoods());
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
                    DisplayLoginWindow();
                }
            }
        }
    }
}