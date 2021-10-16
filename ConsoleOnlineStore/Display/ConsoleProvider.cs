﻿using System;

namespace ConsoleOnlineStore
{
    public static class ConsoleProvider
    {
        public static void DisplayAuthWindow()
        {
            Console.WriteLine("1. Войти в свой аккаунт\n" +
                              "2. Пройти регистрацию.");
            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                { 
                    case ConsoleKey.D1:
                        Console.Clear();
                        DisplayLoginWindow();
                        return;
                        
                    case ConsoleKey.D2:
                        Console.Clear();
                        DisplayRegistrationWindow();
                        return;
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

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        DisplayCatalog();
                        return;
                    
                    case ConsoleKey.D2:
                        Console.Clear();
                        DisplayBasket();
                        return;
                        
                    case ConsoleKey.D3:
                        Console.Clear();
                        DisplayPurchaseHistory();
                        return;
                        
                    case ConsoleKey.D4:
                        Console.Clear();
                        DisplayAuthWindow();
                        return;
                }
            }
        }

        private static void DisplayCatalog()
        {
            Catalog catalog = new Catalog();

            catalog.PrintCatalog();

            Console.WriteLine("1. Добавить предмет в корзину.\n" +
                              "2. Следующая страница\n" +
                              "3. Предыдущая страница\n" +
                              "4. Выбрать страницу\n" +
                              "5. Выйти в меню\n");

            Console.WriteLine($"Текущая страница {Catalog.Page}," +
                              $" последняя страница: {Math.Ceiling(catalog.products.Count / (Catalog.pagination * 1.0))}");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        DisplayAddToBasket();
                        return;
                    
                    case ConsoleKey.D2:
                        Console.Clear();
                        
                        Catalog.Page++;
                        
                        if (Catalog.Page * Catalog.pagination - (Catalog.pagination - 1) > catalog.products.Count)
                        {
                            Console.WriteLine("Невозможно отобразить следующую страницу!\n");
                            Catalog.Page--;
                            DisplayCatalog();
                        }
                        
                        DisplayCatalog();
                        return;

                    case ConsoleKey.D3:
                        Console.Clear();
                        
                        Catalog.Page--;
                        
                        if (Catalog.Page < 1)
                        {
                            Console.WriteLine("Невозможно отобразить предыидущую страницу!\n");
                            Catalog.Page++;
                            DisplayCatalog();
                        }
                        
                        DisplayCatalog();
                        return;
                        
                    case ConsoleKey.D4:
                        Console.Clear();

                        Console.Write("Укажите страницу для перехода: ");
                        if (int.TryParse(Console.ReadLine(), out int page))
                        {
                            Console.Clear();

                            if (page < 1 || page * Catalog.pagination - (Catalog.pagination - 1) > catalog.products.Count)
                            {
                                Console.WriteLine("Невозможно отобразить выбранную страницу!\n");
                                DisplayCatalog();
                            }

                            Catalog.Page = page;
                            DisplayCatalog();
                        }

                        Console.Clear();
                        Console.WriteLine("Вы неверно указали страницу, попробуйте ещё раз!\n");
                        DisplayCatalog();
                        return;

                    case ConsoleKey.D5:
                        Console.Clear();
                        DisplayMainWindow();
                        return;
                }
            }
        }

        private static void DisplayAddToBasket()
        {
            Console.Write("Укажите номер товара: ");
            string num = Console.ReadLine();
            Console.Write("Укажите количество товара: ");
            string quantity = Console.ReadLine();

            Catalog catalog = new Catalog();
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
                Console.WriteLine($"Название: {catalog.products[int.Parse(num) - 1].Name}"); 
                Console.WriteLine($"Описание: {catalog.products[int.Parse(num) - 1].Description}"); 
                Console.WriteLine($"Количество: {int.Parse(quantity)}");
                Console.WriteLine($"Цена за штуку: {catalog.products[int.Parse(num) - 1].Price}");
                Console.WriteLine();
            }

            Console.WriteLine("1. Добавить товар в корзину.\n" +
                              "2. Указать другой номер товара.\n" +
                              "3. Вернуться в каталог.");
            while (true) 
            { 
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();

                        if (Basket.productsInBasket.Count == 0)
                        {
                            basket.SetTimer();
                        }
                        
                        Basket.productsInBasket.Add(catalog.products[int.Parse(num ?? string.Empty) - 1]); 
                        Basket.productsInBasket[^1].Quantity = int.Parse(quantity ?? string.Empty);

                        Console.WriteLine("Вы успешно добавили товар в корзину!\n"); 
                    
                        DisplayCatalog();
                        break;
                    
                    case ConsoleKey.D2:
                        Console.Clear(); 
                        DisplayAddToBasket();
                        return;
                        
                    case ConsoleKey.D3:
                        Console.Clear(); 
                        DisplayCatalog(); 
                        return;
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
            
            if (Basket.productsInBasket.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста!");
            }
            else
            {
                basket.PrintBasket();
            }
            
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();

                        Console.WriteLine("Подтвердить покупку товара?\n" +
                                          "1. Да.\n" +
                                          "2. Нет");

                        while (true)
                        {
                            ConsoleKeyInfo key2 = Console.ReadKey(true);

                            switch (key2.Key)
                            {
                                case ConsoleKey.D1:
                                    Console.Clear();
                                    
                                    if (Basket.productsInBasket.Count == 0)
                                    {
                                        Console.WriteLine("Ваша корзина пуста, вы не можете провести оплату!");
                                        
                                        DisplayBasket();
                                    }
                                    
                                    PurchaseHistory purchaseHistory = new();

                                    JsonStorage.NewProductsQuantity(basket.SoldQuantity());

                                    JsonStorage.AddNewPurchaseHistory(
                                        purchaseHistory.MakePurchaseHistory(Basket.productsInBasket));
                                    
                                    Basket.productsInBasket.Clear();
                                    basket.DisposeTimer();
                                    
                                    Console.WriteLine("Поздравлем с успешной покупкой!\n");

                                    DisplayMainWindow();
                                    return;
                                
                                case ConsoleKey.D2:
                                    Console.Clear();
                                    DisplayBasket();
                                    return;
                            }
                        }
                        
                    case ConsoleKey.D2:
                        Console.Clear();
                    
                        Basket.productsInBasket.Clear();
                    
                        DisplayBasket();
                        return;
                        
                    case ConsoleKey.D3:
                        Console.Clear();
                        DisplayMainWindow();
                        return;
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

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        DisplayMainWindow();
                        return;
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
            
            if (user.Login?.Length <= 2)
            {
                Console.WriteLine("Вы указали слишком короткий логин, попробуйте ещё раз!");
                DisplayRegistrationWindow();
            }
            
            Console.Write("Укажите ваш пароль: ");
            user.Password = Console.ReadLine();
            
            if (user.Password?.Length <= 3)
            {
                Console.WriteLine("Вы указали слишком короткий пароль, попробуйте ещё раз!");
                DisplayRegistrationWindow();
            }
            
            Console.Write("Укажите ваше имя: ");
            user.Name = Console.ReadLine();
            
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

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        
                        if (authService.AddNewUser(user))
                        {
                            Console.WriteLine("Данный логин уже занят, попробуйте другой!\n");
                            DisplayRegistrationWindow();
                        }

                        user.Password = authService.GetHash(user.Password);
                        
                        JsonStorage.AddNewUser(user);
                        
                        Console.WriteLine("Регистрация прошла успешно! Теперь вы можете войти в свой аккаунт.\n");
                        DisplayAuthWindow();
                        return;
                    
                    case ConsoleKey.D2:
                        Console.Clear();
                        DisplayRegistrationWindow();
                        return;
                        
                    case ConsoleKey.D3:
                        Console.Clear();
                        DisplayAuthWindow();
                        return;
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

                switch (key.Key)
                {
                    case ConsoleKey.D1:

                        user.Password = authService.GetHash(user.Password);
                        
                        if (authService.Login(user))
                        {
                            Console.Clear();
                            Console.WriteLine($"Приветствуем {user.Name}!\n");
                            DisplayMainWindow();
                        }

                        Console.Clear();
                        Console.WriteLine("Вы указали неверный логин или пароль, попробуйте ещё раз!\n");
                        DisplayLoginWindow();
                        return;
                    
                    case ConsoleKey.D2:
                        Console.Clear();
                        DisplayLoginWindow();
                        return;
                        
                    case ConsoleKey.D3:
                        Console.Clear();
                        DisplayAuthWindow();
                        return;
                }
            }
        }

        private static void DisplayFindWindow()
        {
            
        }
    }
}
