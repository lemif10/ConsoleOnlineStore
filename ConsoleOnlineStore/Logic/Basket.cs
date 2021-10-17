﻿using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace ConsoleOnlineStore
{
    public class Basket
    {
        private static Timer _timer;

        private readonly List<Product> _products;
        
        public static readonly List<Product> productsInBasket = new();

        private readonly IConfiguration _configuration;

        public Basket()
        {
            _products = JsonStorage.GetProducts();

            _configuration = new ConfigurationBuilder().AddJsonFile("Content/appsettings.json").Build();
        }

        public void SetTimer()
        {
            _timer = new Timer(TimeOut, _configuration["Timer:State"], TimeSpan.FromMinutes(double.Parse(_configuration["Timer:StartAfterMinutes"])),
                TimeSpan.FromSeconds(double.Parse(_configuration["Timer:Period"])));
        }

        private void TimeOut(object state)
        {
            Console.Write(state);
            
            productsInBasket.Clear();
            
            _timer.Dispose();
        }

        public void DisposeTimer()
        {
            _timer.Dispose();
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
            for (int i = 0; i < productsInBasket.Count; i++)
            {
                Console.WriteLine($"Товар номер: {i + 1}");
                Console.WriteLine($"Название: {productsInBasket[i].Name}");
                Console.WriteLine($"Описание: {productsInBasket[i].Description}");
                Console.WriteLine($"Количество: {productsInBasket[i].Quantity}");
                Console.WriteLine($"Цена за весь товар: {productsInBasket[i].Price}");
                Console.WriteLine();
            }
        }
        
        public decimal ProductPrice()
        {
            decimal price = 0;

            foreach (Product product in _products)
            {
                foreach (Product productInBasket in productsInBasket)
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
                foreach (Product productInBasket in productsInBasket)
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
