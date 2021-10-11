using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleOnlineStore
{
    public static class JsonStorage
    {
        private const string PathLogin = 
            @"C:\Users\RedmiBook\RiderProjects\ConsoleOnlineStore\ConsoleOnlineStore\Login&Password.json";

        private const string PathProduct =
            @"C:\Users\RedmiBook\RiderProjects\ConsoleOnlineStore\ConsoleOnlineStore\Products.json";

        private const string PathHistory = @"C:\Users\RedmiBook\RiderProjects\ConsoleOnlineStore\ConsoleOnlineStore\PurchaseHistory.json";
        
        public static void AddNewUser(User user)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathLogin))
                               ?? new List<User>();
            
            users.Add(user);

            using (StreamWriter streamWriter = new StreamWriter(PathLogin))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(users, Formatting.Indented));
            }
        }

        public static List<User> GetUser()
        {
            return JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathLogin));
        }

        public static void NewProductsQuantity(List<Product> newQuantity)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PathLogin))
                               ?? new List<Product>();
            
            products.Clear();

            foreach (Product product in newQuantity)
            {
                products.Add(product);
            }

            using (StreamWriter streamWriter = new StreamWriter(PathProduct))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));
            }
        }
        
        public static List<Product> GetProduct()
        {
            return JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PathProduct));
        }

        public static void AddNewPurchaseHistory(List<ProductHistory> productHistories)
        {
            List<ProductHistory> basketHistory = JsonConvert.DeserializeObject<List<ProductHistory>>(File.ReadAllText(PathHistory))
                               ?? new List<ProductHistory>();

            foreach (ProductHistory productHistory in productHistories)
            {
                basketHistory.Add(productHistory);
            }
            
            using (StreamWriter streamWriter = new StreamWriter(PathHistory))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(basketHistory, Formatting.Indented));
            }
        }

        public static List<ProductHistory> GetPurchaseHistory()
        {
            return JsonConvert.DeserializeObject<List<ProductHistory>>(File.ReadAllText(PathHistory));
        }
    }
}