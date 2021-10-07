using System;

namespace ConsoleOnlineStore
{
    public static class ConsoleInterface
    {
        public static void DisplayLoginWindow()
        {
            while (true)
            {
                Console.WriteLine("1. Войти в свой аккаунт\n2. Пройти регистрацию.");

                ConsoleKeyInfo key = Console.ReadKey(true);

                Console.Clear();

                if (key.Key == ConsoleKey.D1)
                {
                    DisplayJoinWindow();
                    break;
                }

                if (key.Key == ConsoleKey.D2)
                {
                    DisplayRegistrationWindow();
                    break;
                }
            }
        }

        private static void DisplayMainWindow()
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
                    DisplayCatalog();
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

        private static void DisplayCatalog()
        {
            Catalog catalog = new Catalog();

            Console.WriteLine("1. Добавить предмет в корзину. 2. Выйти в меню");
            
            Console.WriteLine();
            
            catalog.ShowGoods(JsonSerializer.TakeGoods());

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    
                }

                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    DisplayMainWindow();
                    break;
                }
            }
        }
        
        private static void DisplayRegistrationWindow()
        {
            Registration registration = new Registration();

            User user = new User();

            Console.WriteLine("Регистрация:");
            
            Console.Write("Укажите ваш логин: ");
            user.Login = Console.ReadLine();
            
            Console.Write("Укажите ваш пароль: ");
            user.Password = Console.ReadLine();
            
            Console.Write("Укажите ваше имя: ");
            user.Name = Console.ReadLine();

            if (!registration.NewUser(JsonSerializer.GetUser(), user))
            {
                Console.Clear();
                Console.WriteLine("Данный логин уже занят, попробуйте другой!");
                DisplayRegistrationWindow();
            }
            
            JsonSerializer.AddNewUser(user);

            while (true)
            {
                Console.WriteLine("1. Подтвердить регистрацию.\n2. Пройти регистрацию заново.");

                ConsoleKeyInfo key = Console.ReadKey(true);

                Console.Clear();

                if (key.Key == ConsoleKey.D1)
                {
                    DisplayLoginWindow();
                    break;
                }

                if (key.Key == ConsoleKey.D2)
                {
                    DisplayRegistrationWindow();
                    break;
                }
            }
        }

        private static void DisplayJoinWindow()
        {
            LoginInStore loginInStore = new LoginInStore();
            
            User user = new User();
            
            Console.Write("Введите логин: ");
            user.Login = Console.ReadLine();
            
            Console.Write("Введите пароль: ");
            user.Password = Console.ReadLine();
            
            if (loginInStore.Join(JsonSerializer.GetUser(), user))
            {
                Console.Clear();
                DisplayMainWindow();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Вы указали неверный логин или пароль, попробуйте ещё раз!");
                DisplayJoinWindow();
            }
        }
    }
}