using System;
using System.Collections.Generic;

namespace ConsoleOnlineStore
{
    public class PurchaseHistory
    {
        public static string Login { get; set; }

        private readonly List<ProductHistory> _productHistories;

        public PurchaseHistory()
        {
            _productHistories = JsonStorage.GetPurchaseHistory();
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

        public List<ProductHistory> SelectPurchaseHistory()
        {
            if (_productHistories is null)
            {
                return new List<ProductHistory>();
            }

            List<ProductHistory> productHistories = new();
            
            for (int i = _productHistories.Count - 1; i >= 0; i--)
            {
                if (_productHistories[i].Login == Login)
                {
                    productHistories.Add(_productHistories[i]);
                }
            }

            return productHistories;
        }
    }
}
