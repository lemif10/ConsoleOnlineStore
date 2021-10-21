using ConsoleOnlineStore.Models;
using ConsoleOnlineStore.Services;
using System;

namespace ConsoleOnlineStore.Provider
{
    public static class ConsoleProvider
    {
        public static void SetTitleName()
        {
            Console.Title = "OnlineStore";
        }

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
                    "1. Посмотреть каталог.\n" +
                    "2. Посмотреть корзину.\n" +
                    "3. Поиск товара по названию.\n" +
                    "4. Посмотреть историю покупок.\n" +
                    "5. Выйти из аккаунта");

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
                        DisplayFindWindow();
                        return;

                    case ConsoleKey.D4:
                        Console.Clear();
                        DisplayPurchaseHistory();
                        return;

                    case ConsoleKey.D5:
                        Console.Clear();

                        if (Basket.ProductsInBasket.Count == 0)
                        {
                            DisplayAuthWindow();
                        }

                        Basket.ResetBasket();
                        DisplayAuthWindow();
                        return;
                }
            }
        }

        private static void DisplayCatalog()
        {
            Catalog catalog = new Catalog();

            for (int i = (Catalog.Page - 1) * Catalog.Pagination;
                i < Catalog.Page * Catalog.Pagination && i < catalog.Products.Count; i++)
            {
                Console.WriteLine($"Номер товара: {i + 1}");
                Console.WriteLine($"Название: {catalog.Products[i].Name}");
                Console.WriteLine($"Описание: {catalog.Products[i].Description}");
                Console.WriteLine($"Количество: {catalog.Products[i].Quantity}");
                Console.WriteLine($"Цена за штуку: {catalog.Products[i].Price}\n");
            }

            Console.WriteLine("1. Добавить предмет в корзину.\n" +
                              "2. Следующая страница\n" +
                              "3. Предыдущая страница\n" +
                              "4. Выбрать страницу\n" +
                              "5. Выйти в меню\n");

            Console.WriteLine($"Текущая страница {Catalog.Page}," +
                              $" последняя страница: {Math.Ceiling(catalog.Products.Count / (Catalog.Pagination * 1.0))}");

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

                        if (Catalog.Page * Catalog.Pagination - (Catalog.Pagination - 1) > catalog.Products.Count)
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

                            if (page < 1 || page * Catalog.Pagination - (Catalog.Pagination - 1) > catalog.Products.Count)
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
            string index = Console.ReadLine();

            Console.Write("Укажите количество товара: ");
            string quantity = Console.ReadLine();

            Basket basket = new();

            if (index is null or "" || quantity is null or "")
            {
                Console.Clear();
                Console.WriteLine("Вы не указали номер товара и количество товара, попробуйте ещё раз!");
                DisplayAddToBasket();
            }

            Console.WriteLine("\n1. Добавить товар в корзину.\n" +
                              "2. Указать другой номер товара.\n" +
                              "3. Вернуться в каталог.");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();

                        if (basket.AddProduct(index, quantity))
                        {
                            Console.WriteLine("Вы успешно добавили товар в корзину!\n");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось добавить товар в корзину, просмотрите каталог ещё раз и укажите нужное значение!");
                            DisplayCatalog();
                        }

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
            Basket basket = new();

            Console.WriteLine($"К оплате за все товары: {basket.ProductPrice()}\n");

            if (Basket.ProductsInBasket.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста!\n");
            }
            else
            {
                for (int i = 0; i < Basket.ProductsInBasket.Count; i++)
                {
                    Console.WriteLine($"Товар номер: {i + 1}");
                    Console.WriteLine($"Название: {Basket.ProductsInBasket[i].Name}");
                    Console.WriteLine($"Описание: {Basket.ProductsInBasket[i].Description}");
                    Console.WriteLine($"Количество: {Basket.ProductsInBasket[i].Quantity}");
                    Console.WriteLine($"Цена за весь товар: {Basket.ProductsInBasket[i].Price}\n");
                }
            }

            Console.WriteLine("1. Произвести покупку\n" +
                              "2. Очистить корзину\n" +
                              "3. Покинуть корзину");

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

                                    if (Basket.ProductsInBasket.Count == 0)
                                    {
                                        Console.WriteLine("Ваша корзина пуста, вы не можете провести оплату!\n");

                                        DisplayBasket();
                                    }

                                    JsonStorage.ChangeProductsQuantity(basket.SoldQuantity());

                                    JsonStorage.AddNewPurchaseHistory(
                                        PurchaseHistory.CreatePurchaseHistory(Basket.ProductsInBasket));

                                    Basket.ResetBasket();

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

                        if (Basket.ProductsInBasket.Count == 0)
                        {
                            Console.WriteLine("Ваша корзина и так пуста!\n");
                            DisplayBasket();
                        }

                        Basket.ResetBasket();

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

            PurchaseHistory purchaseHistory = new PurchaseHistory();

            if (purchaseHistory.ProductHistories is null)
            {
                Console.Clear();
                Console.WriteLine("Ваша история покупок пока что пуста, купите что-то и заходите!\n");
                DisplayMainWindow();
            }
            else
            {
                for (int i = purchaseHistory.ProductHistories.Count - 1; i >= 0; i--)
                {
                    if (purchaseHistory.ProductHistories[i].Login == PurchaseHistory.Login)
                    {
                        Console.WriteLine($"Название: {purchaseHistory.ProductHistories[i].Name}");
                        Console.WriteLine($"Описание: {purchaseHistory.ProductHistories[i].Description}");
                        Console.WriteLine($"Количество: {purchaseHistory.ProductHistories[i].Quantity}");
                        Console.WriteLine($"Цена покупки: {purchaseHistory.ProductHistories[i].Price}");
                        Console.WriteLine($"Дата покупки: {purchaseHistory.ProductHistories[i].Date.ToShortDateString()}\n");
                    }
                }
            }



            Console.WriteLine("1. Покинуть историю покупок");

            Console.SetCursorPosition(0, 0);

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
            AuthService authService = new();

            User user = new();

            Console.WriteLine("Регистрация:");

            Console.Write("Укажите ваш логин: ");
            user.Login = Console.ReadLine();

            if (user.Login?.Length < AuthService.MinLength)
            {
                Console.Clear();
                Console.WriteLine($"Вы указали слишком короткий логин, минимальная длинна - {AuthService.MinLength}, попробуйте ещё раз!\n");
                DisplayRegistrationWindow();
            }

            Console.Write("Укажите ваш пароль: ");
            user.Password = GetSecurePassword();

            if (user.Password?.Length < AuthService.MinLength)
            {
                Console.Clear();
                Console.WriteLine($"Вы указали слишком короткий пароль, минимальная длинна - {AuthService.MinLength}, попробуйте ещё раз!\n");
                DisplayRegistrationWindow();
            }

            Console.Write("Повторно введите пароль: ");
            string repeatPassword = GetSecurePassword();

            if (user.Password != repeatPassword)
            {
                Console.Clear();
                Console.WriteLine("Вы неверно повторили пароль, попробуйте ещё раз!\n");
                DisplayRegistrationWindow();
            }

            Console.Write("Укажите ваше имя: ");
            user.Name = Console.ReadLine();

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

                        user.Password = AuthService.GetHash(user.Password);

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
            AuthService authService = new();

            User user = new();

            Console.Write("Введите логин: ");
            user.Login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            user.Password = GetSecurePassword();

            Console.WriteLine("\n1. Подтвердить вход.\n" +
                              "2. Указать другой логин или пароль.\n" +
                              "3. Покинуть окно входа в акканут.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();

                        user.Password = AuthService.GetHash(user.Password);

                        if (authService.Login(user))
                        {
                            Console.WriteLine($"Приветствуем {user.Name}!\n");
                            DisplayMainWindow();
                        }

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
            Console.Write("Укажите название товара: ");
            string productName = Console.ReadLine();

            Catalog catalog = new();

            int productIndex = catalog.FindProduct(productName);

            if (productIndex != -1)
            {
                Console.WriteLine($"\nНазвание: {catalog.Products[productIndex].Name}");
                Console.WriteLine($"Описание: {catalog.Products[productIndex].Description}");
                Console.WriteLine($"Количество: {catalog.Products[productIndex].Quantity}");
                Console.WriteLine($"Цена за штуку: {catalog.Products[productIndex].Price}\n");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Мы не смогли найти товар который вы искали, нужно полное совпадение названия товара!\n");
                DisplayMainWindow();
            }

            Basket basket = new();

            Console.Write("Укажите количество товара: ");
            string quantity = Console.ReadLine();

            Console.WriteLine("\n1. Добавить товар в корзину.\n" +
                              "2. Найти другой товар\n" +
                              "3. Вернуться в главное меню.");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();

                        if (basket.AddProduct((productIndex + 1).ToString(), quantity))
                        {
                            Console.WriteLine("Вы успешно добавили товар в корзину!\n");
                        }
                        else
                        {
                            Console.WriteLine("Вы указали неверное количество товара!\n");
                            DisplayFindWindow();
                        }

                        DisplayMainWindow();
                        return;

                    case ConsoleKey.D2:
                        Console.Clear();
                        DisplayFindWindow();
                        return;

                    case ConsoleKey.D3:
                        Console.Clear();
                        DisplayMainWindow();
                        return;
                }
            }
        }

        private static string GetSecurePassword()
        {
            string securePassword = string.Empty;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key is ConsoleKey.Backspace or ConsoleKey.Spacebar or ConsoleKey.Delete)
                {
                    continue;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                securePassword += key.KeyChar;
                Console.Write("*");
            }

            Console.WriteLine();

            return securePassword.Trim();
        }
    }
}
