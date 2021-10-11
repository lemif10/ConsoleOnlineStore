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

            ProductHistory productHistory = new ProductHistory();

            foreach (Product product in productsInBasket)
            {
                productHistory.Login = Login;
                productHistory.Name = product.Name;
                productHistory.Description = product.Description;
                productHistory.Quantity = product.Quantity;
                productHistory.Price = product.Price;
                productHistories.Add(productHistory);
            }

            return productHistories;
        }

        public List<Product> SelectPurchaseHistory()
        {
            if (_productHistories is null)
            {
                return new List<Product>();
            }

            List<Product> products = new();

            Product product = new();

            for (int i = _productHistories.Count - 1; i >= 0; i--)
            {
                if (_productHistories[i].Login == Login)
                {
                    product.Name = _productHistories[i].Name;
                    product.Description = _productHistories[i].Description;
                    product.Quantity = _productHistories[i].Quantity;
                    product.Price = _productHistories[i].Price;
                    products.Add(product);
                    product = new();
                }
            }

            return products;
        }
    }
}