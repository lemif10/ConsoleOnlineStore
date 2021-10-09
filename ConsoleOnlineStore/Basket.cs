using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class Basket
    {
        private List<Product> productsInBasket = new List<Product>();
        
        public void AddToBasket(int index)
        {
            productsInBasket.Add(JsonStorage.Products[index - 1]);
        }
    }
}