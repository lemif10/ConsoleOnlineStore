using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class Basket
    {
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
            }

            return price;
        }
    }
}