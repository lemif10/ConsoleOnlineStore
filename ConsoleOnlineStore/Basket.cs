using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public static class Basket
    {
        public static List<Product> productsInBasket;
        
        public static bool AddToBasket(int index, int count)
        {
            if (count >= 0 && count <= JsonStorage.Products[index - 1].Quantity
                           && index >= 0 && JsonStorage.Products.Count > index)
            {
                return true;
            }

            return false;
        }

        public static void PrintBasket()
        {
            for (int i = 0; i < productsInBasket.Count; i++)
            {
                Console.WriteLine($"Товар номер: {i + 1}");
                Console.WriteLine($"Название: {productsInBasket[i].Name}");
                Console.WriteLine($"Описание: {productsInBasket[i].Description}");
                Console.WriteLine($"Количество: {productsInBasket[i].Quantity}");
                Console.WriteLine($"Цена: {productsInBasket[i].Price}");
                Console.WriteLine();
            }
        }
        
        public static decimal ProductPrice()
        {
            decimal price = 0;

            foreach (Product product in productsInBasket)
            {
                price += product.Price * product.Quantity;
            }

            return price;
        }
    }
}