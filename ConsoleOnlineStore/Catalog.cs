using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public static class Catalog
    {
        public static void ShowGoods(List<Goods> goods)
        {
            foreach (Goods product in goods)
            {
                Console.WriteLine($"Название: {product.Name}");
                Console.WriteLine($"Описание: {product.Description}");
                Console.WriteLine($"Количество: {product.Quantity}");
                Console.WriteLine($"Цена: {product.Price}");
                Console.WriteLine();
            }
        }
    }
}