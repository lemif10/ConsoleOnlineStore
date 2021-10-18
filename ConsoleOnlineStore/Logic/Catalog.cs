using System;
using System.Collections.Generic;
using ConsoleOnlineStore.Models;
using ConsoleOnlineStore.Services;

namespace ConsoleOnlineStore.Logic
{
    public class Catalog
    {
        public const int pagination = 3;
        public readonly List<Product> Products;

        public static int Page { get; set; } = 1;
        
        public Catalog()
        {
            Products = JsonStorage.GetProducts();
        }
        
        public void PrintCatalog()
        {
            for (int i = (Page - 1) * pagination; i < Page * pagination && i < Products.Count; i++)
            {   Console.WriteLine($"Номер товара: {i + 1}"); 
                Console.WriteLine($"Название: {Products[i].Name}"); 
                Console.WriteLine($"Описание: {Products[i].Description}"); 
                Console.WriteLine($"Количество: {Products[i].Quantity}"); 
                Console.WriteLine($"Цена за штуку: {Products[i].Price}"); 
                Console.WriteLine();
            }
        }

        public int FindProduct(string productName)
        {
            productName = productName.ToLower();

            for (int i = 0; i < Products.Count; i++)
            {
                if (productName == Products[i].Name.ToLower())
                {
                    return i;
                }
            }

            return -1;
        }
    }
}