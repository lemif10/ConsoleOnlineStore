using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class Basket
    {
        public static string Login { get; set; }
        
        public readonly List<Product> Products;
        
        public static readonly List<Product> ProductsInBasket = new List<Product>();

        public Basket()
        {
            Products = JsonStorage.GetProduct();
        }
        
        public bool AddToBasket(string index, string quantity)
        {
            if (int.TryParse(index, out int ind) && int.TryParse(quantity, out int quan))
            {
                if (ind < 0 || ind >= Products.Count  || quan <= 0 ||
                    quan > Products[ind - 1].Quantity)
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
                Console.WriteLine($"Цена: {ProductsInBasket[i].Price}");
                Console.WriteLine();
            }
        }
        
        public decimal ProductPrice()
        {
            decimal price = 0;

            foreach (Product product in ProductsInBasket)
            {
                price += product.Price * product.Quantity;

                product.Price *= product.Quantity;
            }

            return price;
        }

        public List<PurchaseHistory> MakePurchaseHistory()
        {
            List<PurchaseHistory> purchaseHistories = new List<PurchaseHistory>();

            PurchaseHistory purchaseHistory = new PurchaseHistory();
            
            foreach (Product product in ProductsInBasket)
            {
                purchaseHistory.Login = Login;
                purchaseHistory.Name = product.Name;
                purchaseHistory.Description = product.Description;
                purchaseHistory.Quantity = product.Quantity;
                purchaseHistory.Price = product.Price;
                purchaseHistories.Add(purchaseHistory);
            }

            return purchaseHistories;
        }

        public List<Product> SoldQuantity()
        {
            foreach (Product product in Products)
            {
                foreach (Product productInBasket in ProductsInBasket)
                {
                    if (product.Name == productInBasket.Name)
                    {
                        product.Quantity -= productInBasket.Quantity;
                    }
                }
            }

            return Products;
        }
    }
}