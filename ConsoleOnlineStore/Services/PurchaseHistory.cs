using ConsoleOnlineStore.Models;
using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore.Services
{
    public class PurchaseHistory
    {
        public static string Login { get; set; }

        public readonly List<ProductHistory> ProductHistories;

        public PurchaseHistory()
        {
            ProductHistories = JsonStorage.GetPurchaseHistory();
        }

        public static List<ProductHistory> CreatePurchaseHistory(List<Product> productsInBasket)
        {
            List<ProductHistory> productHistories = new();

            foreach (Product product in productsInBasket)
            {
                ProductHistory productHistory = new()
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
    }
}
