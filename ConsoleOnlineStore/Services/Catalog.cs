using ConsoleOnlineStore.Models;
using System.Collections.Generic;

namespace ConsoleOnlineStore.Services
{
    public class Catalog
    {
        public const int Pagination = 3;
        public readonly List<Product> Products;

        public static int Page { get; set; } = 1;

        public Catalog()
        {
            Products = JsonStorage.GetProducts();
        }

        public int FindProduct(string productName)
        {
            productName = productName.ToLower();

            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Name.ToLower().Contains(productName))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}