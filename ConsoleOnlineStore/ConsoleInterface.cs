using System;

namespace ConsoleOnlineStore
{
    public static class ConsoleInterface
    {
        public static void DisplayLoginWindow()
        {
            Console.WriteLine("1. Войти в свой аккаунт\n" +
                              "2. Пройти регистрацию.");
            
            while (true)
            {
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
                    Console.Clear();
                    DisplayBasket();
                    break;
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
            
            for (int i = 0; i < JsonStorage.Products.Count; i++)
            {
                Console.WriteLine($"Номер товара: {i + 1}");
                Console.WriteLine($"Название: {JsonStorage.Products[i].Name}");
                Console.WriteLine($"Описание: {JsonStorage.Products[i].Description}");
                Console.WriteLine($"Количество: {JsonStorage.Products[i].Quantity}");
                Console.WriteLine($"Цена: {JsonStorage.Products[i].Price}");
                Console.WriteLine();
            }
            
            Console.WriteLine("1. Добавить предмет в корзину.\n" +
                              "2. Выйти в меню");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    DisplayAddToBasket();
                }
                
                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    DisplayMainWindow();
                    break;
                }
            }
        }

        private static void DisplayAddToBasket()
        {
            Console.Write("Укажите номер товара: ");
            Console.Write("Укажите количество товара: ");

            if (int.TryParse(Console.ReadLine(), out int num) && int.TryParse(Console.ReadLine(), out int count))
            {
                if (Basket.AddToBasket(num, count))
                {
                    Console.Clear();
                    Console.WriteLine("Вы указали неверный номер товара или его количество, просмотрите каталог и попробуйте ещё раз\n");
                    DisplayCatalog();
                }
                
                Console.WriteLine($"Вы хотете добавить товар под номером: {num}");
                Console.WriteLine($"Название: {JsonStorage.Products[num - 1].Name}");
                Console.WriteLine($"Описание: {JsonStorage.Products[num - 1].Description}");
                Console.WriteLine($"Количество: {count}");
                Console.WriteLine($"Цена за штуку: {JsonStorage.Products[num - 1].Price}");

                Console.WriteLine("1. Добавить товар в корзину.\n" +
                                  "2. Указать другой номер товара.\n" +
                                  "3. Вернуться в каталог.\n");
                
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.D1)
                    {
                        Console.Clear();

                        Basket.productsInBasket.Add(JsonStorage.Products[num - 1]);
                        Basket.productsInBasket[^1].Quantity = count;
                        
                        Console.WriteLine("Вы успешно добавили товар в корзину!\n");
                        DisplayCatalog();
                    }
                
                    if (key.Key == ConsoleKey.D2)
                    {
                        Console.Clear();
                        DisplayAddToBasket();
                        break;
                    }
                    
                    if (key.Key == ConsoleKey.D3)
                    {
                        Console.Clear();
                        DisplayCatalog();
                        break;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Вы указали Не число, просмотрите каталог и попробуйте ещё раз.\n");
                DisplayCatalog();
            }
        }

        private static void DisplayBasket()
        {
            Console.WriteLine("1. Произвести покупку\n" +
                              "2. Очистить корзину\n" +
                              "3. Покинуть корзину\n");

            Console.WriteLine($"К оплате за все товары: {Basket.ProductPrice()}\n");
            
            if (Basket.productsInBasket.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста!");
            }
            else
            {
                Basket.PrintBasket();
            }
            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();

                    Console.WriteLine("Подтвердить покупку товара?\n" +
                                      "1. Да.\n" +
                                      "2. Нет");

                    while (true)
                    {
                        ConsoleKeyInfo key2 = Console.ReadKey(true);

                        if (key2.Key == ConsoleKey.D1)
                        {
                            Console.Clear();

                            Console.WriteLine("Поздравлем с успешной покупкой!\n");
                            
                            Basket.productsInBasket.Clear();
                            
                            DisplayMainWindow();
                        }

                        if (key2.Key == ConsoleKey.D2)
                        {
                            Console.Clear();
                            DisplayBasket();
                        }
                    }
                }
                
                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    
                    Basket.productsInBasket.Clear();
                    
                    DisplayBasket();
                    break;
                }

                if (key.Key == ConsoleKey.D3)
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
                    
                    Console.Clear();
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
                        Console.WriteLine($"Приветствуем {user.Name}!\n");
                        DisplayMainWindow();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Вы указали неверный логин или пароль, попробуйте ещё раз!\n");
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