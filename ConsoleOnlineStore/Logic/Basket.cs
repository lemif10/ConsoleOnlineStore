using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleOnlineStore.Models;
using ConsoleOnlineStore.Provider;
using ConsoleOnlineStore.Services;
using Microsoft.Extensions.Configuration;

namespace ConsoleOnlineStore.Logic
{
    public class Basket
    {
        private static Timer _timer;

        private static int _seconds;

        private readonly List<Product> _products;

        private readonly IConfiguration _configuration;
        
        public static readonly List<Product> ProductsInBasket = new();

        public Basket()
        {
            _products = JsonStorage.GetProducts();

            _configuration = new ConfigurationBuilder().AddJsonFile("Content/appsettings.json").Build();
        }

        public void SetTimer()
        {
            _timer = new Timer(GetTime, _configuration["Timer:State"], TimeSpan.FromMinutes(double.Parse(_configuration["Timer:StartAfterMinutes"])),
                TimeSpan.FromSeconds(double.Parse(_configuration["Timer:PeriodSeconds"])));

            _seconds = int.Parse(_configuration["Timer:TimeOutAfterSeconds"]);
        }

        private void GetTime(object state)
        {
            if (_seconds == 0)
            {
                ConsoleProvider.SetTitleName();
                Console.WriteLine("\nВремя ожидания покупки вышло, ваша корзина пуста!");
                ResetBasket();
                return;
            }
            
            Console.Title = $"{state}{_seconds / 60:D2}:{_seconds % 60:D2}";

            _seconds--;
        }

        public static void ResetBasket()
        {
            ProductsInBasket.Clear();
            _timer.Dispose();
            ConsoleProvider.SetTitleName();
        }
        
        public bool AddToBasket(string index, string quantity)
        {
            if (int.TryParse(index, out int ind) && int.TryParse(quantity, out int quan))
            {
                if (ind <= 0 || ind > _products.Count  || quan <= 0 ||
                    quan > _products[ind - 1].Quantity)
                {
                    return false;
                }
                
                return true;
            }

            return false;
        }

        public void PrintBasket()
        {
            for (int i = 0; i < ProductsInBasket.Count; i++)
            {
                Console.WriteLine($"Товар номер: {i + 1}");
                Console.WriteLine($"Название: {ProductsInBasket[i].Name}");
                Console.WriteLine($"Описание: {ProductsInBasket[i].Description}");
                Console.WriteLine($"Количество: {ProductsInBasket[i].Quantity}");
                Console.WriteLine($"Цена за весь товар: {ProductsInBasket[i].Price}\n");
            }
        }
        
        public decimal ProductPrice()
        {
            decimal price = 0;

            foreach (Product product in _products)
            {
                foreach (Product productInBasket in ProductsInBasket)
                {
                    if (productInBasket.Name == product.Name)
                    {
                        price += product.Price * productInBasket.Quantity;

                        productInBasket.Price = product.Price * productInBasket.Quantity;
                    }
                }
            }
            
            return price;
        }

        public List<Product> SoldQuantity()
        {
            foreach (Product product in _products)
            {
                foreach (Product productInBasket in ProductsInBasket)
                {
                    if (product.Name == productInBasket.Name)
                    {
                        product.Quantity -= productInBasket.Quantity;
                    }
                }
            }

            return _products;
        }
    }
}
