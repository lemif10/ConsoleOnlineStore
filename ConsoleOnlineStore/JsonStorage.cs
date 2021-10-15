using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleOnlineStore
{
    public static class JsonStorage
    {
        private static string PathUser {get;} = Path.Combine(Directory.GetCurrentDirectory(), "Users.json");
        
        private static string PathProduct {get;} = Path.Combine(Directory.GetCurrentDirectory(), "products.json");

        private static string PathHistory {get;} = Path.Combine(Directory.GetCurrentDirectory(), "PurchaseHistory.json");
        
        public static void AddNewUser(User user)
        {
            if (!File.Exists(PathUser))
            {
                File.Create(PathUser);
            }
            
            List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathUser))
                               ?? new List<User>();
            
            users.Add(user);

            using (StreamWriter streamWriter = new StreamWriter(PathUser))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(users, Formatting.Indented));
            }
        }

        public static List<User> GetUsers()
        {
            if (!File.Exists(PathUser))
            {
                File.Create(PathUser);
            }
            
            return JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(PathUser));
        }

        public static void NewProductsQuantity(List<Product> newQuantity)
        {
            if (!File.Exists(PathProduct))
            {
                File.Create(PathProduct);
            }
            
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PathProduct))
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
        
        public static List<Product> GetProducts()
        {
            if (!File.Exists(PathProduct))
            {
                File.Create(PathProduct);
            }
            
            return JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PathProduct));
        }

        public static void AddNewPurchaseHistory(List<ProductHistory> productHistories)
        {
            if (!File.Exists(PathHistory))
            {
                File.Create(PathHistory);
            }
            
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
            if (!File.Exists(PathHistory))
            {
                File.Create(PathHistory);
            }
            
            return JsonConvert.DeserializeObject<List<ProductHistory>>(File.ReadAllText(PathHistory));
        }
    }
}
