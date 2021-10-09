using System;

namespace ConsoleOnlineStore
{
    public static class ConsoleInterface
    {
        public static void DisplayLoginWindow()
        {
            while (true)
            {
                Console.WriteLine("1. Войти в свой аккаунт\n" +
                                  "2. Пройти регистрацию.");

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    DisplayJoinWindow();
                    break;
                }

                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    DisplayRegistrationWindow();
                    break;
                }
            }
        }

        private static void DisplayMainWindow()
        {
            Console.WriteLine(
                    "1. Посмотреть каталог\n" +
                    "2. Посмотреть корзину.\n" +
                    "3. Посмотреть историю покупок\n" +
                    "4. Выйти из аккаунта");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    DisplayCatalog();
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
                    break;
                }
            }
        }

        private static void DisplayCatalog()
        {
            Console.WriteLine("1. Добавить предмет в корзину.\n" +
                              "2. Выйти в меню\n");
            
            Catalog catalog = new Catalog();
            
            catalog.ShowGoods();
            
            Console.WriteLine("1. Добавить предмет в корзину.\n" +
                              "2. Выйти в меню\n");
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
            AuthService authService = new AuthService();

            User user = new User();

            Console.WriteLine("Регистрация:");
            
            Console.Write("Укажите ваш логин: ");
            user.Login = Console.ReadLine();
            
            Console.Write("Укажите ваш пароль: ");
            user.Password = Console.ReadLine();
            
            Console.Write("Укажите ваше имя: ");
            user.Name = Console.ReadLine();
            
            Console.WriteLine("\n1. Подтвердить регистрацию.\n" +
                              "2. Пройти регистрацию заново.\n" +
                              "3. Покинуть окно регистрации");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    if (!authService.NewUser(user))
                    {
                        Console.Clear();
                        Console.WriteLine("Данный логин уже занят, попробуйте другой!\n");
                        DisplayRegistrationWindow();
                    }
                    
                    JsonStorage.AddNewUser(user);
                    
                    Console.WriteLine("Регистрация прошла успешно! Теперь вы можете войти в свой аккаунт.\n");
                    DisplayLoginWindow();
                    break;
                }

                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    DisplayRegistrationWindow();
                    break;
                }

                if (key.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    DisplayLoginWindow();
                    break;
                }
            }
        }

        private static void DisplayJoinWindow()
        {
            AuthService authService = new AuthService();
            
            User user = new User();
            
            Console.Write("Введите логин: ");
            user.Login = Console.ReadLine();
            
            Console.Write("Введите пароль: ");
            user.Password = Console.ReadLine();

            Console.WriteLine("\n1. Подтвердить вход.\n" +
                              "2. Указать другой логин или пароль.\n" +
                              "3. Покинуть окно входа в акканут.");
            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    if (authService.Join(user))
                    {
                        Console.Clear();
                        Console.WriteLine($"Приветствуем {user.Name}!");
                        DisplayMainWindow();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Вы указали неверный логин или пароль, попробуйте ещё раз!");
                        DisplayJoinWindow();
                        break;
                    }
                }

                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    DisplayJoinWindow();
                    break;
                }

                if (key.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    DisplayLoginWindow();
                    break;
                }
            }
        }
    }
}