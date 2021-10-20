using System;
using System.Collections.Generic;
using ConsoleOnlineStore.Models;
using ConsoleOnlineStore.Services;

namespace ConsoleOnlineStore.Logic
{
    public class PurchaseHistory
    {
        public static string Login { get; set; }

        public readonly List<ProductHistory> ProductHistories;

        public PurchaseHistory()
        {
            ProductHistories = JsonStorage.GetPurchaseHistory();
        }
        
        public List<ProductHistory> MakePurchaseHistory(List<Product> productsInBasket)
        {
            List<ProductHistory> productHistories = new List<ProductHistory>();

            foreach (Product product in productsInBasket)
            {
                ProductHistory productHistory = new ProductHistory
                {
                    Login = Login,
                    Name = product.Name,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Date = DateTime.Now.Date
                };

                productHistories.Add(productHistory);
            }

            return productHistories;
        }

        public void PrintPurchaseHistory()
        {
            for (int i = ProductHistories.Count - 1; i >= 0; i--)
            {
                if (ProductHistories[i].Login == Login)
                {
                    Console.WriteLine($"Название: {ProductHistories[i].Name}");
                    Console.WriteLine($"Описание: {ProductHistories[i].Description}");
                    Console.WriteLine($"Количество: {ProductHistories[i].Quantity}");
                    Console.WriteLine($"Цена покупки: {ProductHistories[i].Price}");
                    Console.WriteLine($"Дата покупки: {ProductHistories[i].Date.ToShortDateString()}\n");
                }
            }
        }
    }
}
