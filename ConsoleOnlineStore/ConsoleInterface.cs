using System;

namespace ConsoleOnlineStore
{
    public static class ConsoleInterface
    {
        public static void DisplayAuthWindow()
        {
            Console.WriteLine("1. Войти в свой аккаунт\n" +
                              "2. Пройти регистрацию.");
            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    DisplayLoginWindow();
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
                    Console.Clear();
                    DisplayPurchaseHistory();
                    break;
                }
                
                if (key.Key == ConsoleKey.D4)
                {
                    Console.Clear();
                    DisplayAuthWindow();
                    break;
                }
            }
        }

        private static void DisplayCatalog()
        {
            Console.WriteLine("1. Добавить предмет в корзину.\n" +
                              "2. Выйти в меню\n");

            Basket basket = new Basket();
            
            for (int i = 0; i < basket.Products.Count; i++)
            {
                Console.WriteLine($"Номер товара: {i + 1}");
                Console.WriteLine($"Название: {basket.Products[i].Name}");
                Console.WriteLine($"Описание: {basket.Products[i].Description}");
                Console.WriteLine($"Количество: {basket.Products[i].Quantity}");
                Console.WriteLine($"Цена за штуку: {basket.Products[i].Price}");
                Console.WriteLine();
            }

            Console.WriteLine("1. Добавить предмет в корзину.\n" +
                              "2. Выйти в меню");

            Console.SetCursorPosition(0, 0);

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
            string num = Console.ReadLine();
            Console.Write("Укажите количество товара: ");
            string quantity = Console.ReadLine();

            Basket basket = new Basket();
            
            if (num == "" || quantity == "" || num is null || quantity is null)
            {
                Console.Clear();
                Console.WriteLine("Вы не указали номер товара или его количество, попробуйте ещё раз");
                DisplayAddToBasket();
            }
            else if(!basket.AddToBasket(num, quantity))
            {
                Console.Clear();
                Console.WriteLine("Вы указали неверный номер товара или его количество, просмотрите каталог ещё раз!\n");
                DisplayCatalog();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Вы хотете добавить товар под номером: {num}");
                Console.WriteLine($"Название: {basket.Products[int.Parse(num) - 1].Name}"); 
                Console.WriteLine($"Описание: {basket.Products[int.Parse(num) - 1].Description}"); 
                Console.WriteLine($"Количество: {int.Parse(quantity)}");
                Console.WriteLine($"Цена за штуку: {basket.Products[int.Parse(num) - 1].Price}");
                Console.WriteLine();
            }

            Console.WriteLine("1. Добавить товар в корзину.\n" +
                              "2. Указать другой номер товара.\n" +
                              "3. Вернуться в каталог.");
            while (true) 
            { 
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1) 
                { 
                    Console.Clear();
                    
                    Basket.ProductsInBasket.Add(basket.Products[int.Parse(num ?? string.Empty) - 1]); 
                    Basket.ProductsInBasket[^1].Quantity = int.Parse(quantity ?? string.Empty);

                    Console.WriteLine("Вы успешно добавили товар в корзину!\n"); 
                    
                    DisplayCatalog();
                    break;
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
        
        private static void DisplayBasket()
        {
            Console.WriteLine("1. Произвести покупку\n" +
                              "2. Очистить корзину\n" +
                              "3. Покинуть корзину\n");

            Basket basket = new Basket();
            
            Console.WriteLine($"К оплате за все товары: {basket.ProductPrice()}\n");
            
            if (Basket.ProductsInBasket.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста!");
            }
            else
            {
                basket.PrintBasket();
            }

            Console.SetCursorPosition(0, 0 );
            
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

                            PurchaseHistory purchaseHistory = new();
                            
                            JsonStorage.NewProductsQuantity(basket.SoldQuantity());
                            
                            JsonStorage.AddNewPurchaseHistory(purchaseHistory.MakePurchaseHistory(Basket.ProductsInBasket));
                            
                            Basket.ProductsInBasket.Clear();

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
                    
                    Basket.ProductsInBasket.Clear();
                    
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

        private static void DisplayPurchaseHistory()
        {
            Console.WriteLine("1. Покинуть историю покупок\n");
            
            PurchaseHistory purchaseHistory = new();

            foreach (ProductHistory product in purchaseHistory.SelectPurchaseHistory())
            {
                Console.WriteLine($"Название: {product.Name}");
                Console.WriteLine($"Описание: {product.Description}");
                Console.WriteLine($"Количество: {product.Quantity}");
                Console.WriteLine($"Цена покупки: {product.Price}");
                Console.WriteLine($"Дата покупки: {product.Date.ToShortDateString()}");
                Console.WriteLine();
            }

            Console.WriteLine("1. Покинуть историю покупок");

            Console.SetCursorPosition(0, 0);
            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
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

            if (user.Login?.Length <= 2)
            {
                Console.WriteLine("Вы указали слишком короткий логин, попробуйте ещё раз!");
                DisplayRegistrationWindow();
            }
            
            if (user.Password?.Length <= 3)
            {
                Console.WriteLine("Вы указали слишком короткий пароль, попробуйте ещё раз!");
                DisplayRegistrationWindow();
            }
            
            if (user.Name?.Length <= 1)
            {
                Console.WriteLine("Вы указали слишком короткое имя, попробуйте ещё раз!");
                DisplayRegistrationWindow();
            }
            
            Console.WriteLine("\n1. Подтвердить регистрацию.\n" +
                              "2. Пройти регистрацию заново.\n" +
                              "3. Покинуть окно регистрации");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1)
                {
                    if (authService.AddNewUser(user))
                    {
                        Console.Clear();
                        Console.WriteLine("Данный логин уже занят, попробуйте другой!\n");
                        DisplayRegistrationWindow();
                    }
                    
                    JsonStorage.AddNewUser(user);
                    
                    Console.Clear();
                    Console.WriteLine("Регистрация прошла успешно! Теперь вы можете войти в свой аккаунт.\n");
                    DisplayAuthWindow();
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
                    DisplayAuthWindow();
                    break;
                }
            }
        }

        private static void DisplayLoginWindow()
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
                    if (authService.Login(user))
                    {
                        Console.Clear();
                        Console.WriteLine($"Приветствуем {user.Name}!\n");
                        DisplayMainWindow();
                        break;
                    }
                    
                    Console.Clear();
                    Console.WriteLine("Вы указали неверный логин или пароль, попробуйте ещё раз!\n"); 
                    DisplayLoginWindow(); 
                    break;
                }

                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    DisplayLoginWindow();
                    break;
                }

                if (key.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    DisplayAuthWindow();
                    break;
                }
            }
        }
    }
}