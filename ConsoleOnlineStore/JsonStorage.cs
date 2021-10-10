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

        private const string PathHistory = "";
        
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

        public static List<Product> GetProduct()
        {
            return JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PathProduct));
        }

        public static void AddNewPurchaseHistory(List<Product> products)
        {
            List<Product> basketHistory = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PathLogin))
                               ?? new List<Product>();

            foreach (Product product in products)
            {
                basketHistory.Add(product);
            }
            
            using (StreamWriter streamWriter = new StreamWriter(PathLogin))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(basketHistory, Formatting.Indented));
            }
        }
    }
}