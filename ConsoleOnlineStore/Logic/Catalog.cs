using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class Catalog
    {
        public const int pagination = 3;
        public readonly List<Product> products;

        public static int Page { get; set; } = 1;
        
        public Catalog()
        {
            products = JsonStorage.GetProducts();
        }
        
        public void PrintCatalog()
        {
            for (int i = (Page - 1) * pagination; i < Page * pagination && i < products.Count; i++)
            {   Console.WriteLine($"Номер товара: {i + 1}"); 
                Console.WriteLine($"Название: {products[i].Name}"); 
                Console.WriteLine($"Описание: {products[i].Description}"); 
                Console.WriteLine($"Количество: {products[i].Quantity}"); 
                Console.WriteLine($"Цена за штуку: {products[i].Price}"); 
                Console.WriteLine();
            }
        }
    }
}