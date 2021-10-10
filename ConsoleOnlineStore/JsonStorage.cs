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

        public static List<Product> GetProduct()
        {
            return JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(PathProduct));
        }

        public static void AddNewPurchaseHistory(List<PurchaseHistory> purchaseHistories)
        {
            List<PurchaseHistory> basketHistory = JsonConvert.DeserializeObject<List<PurchaseHistory>>(File.ReadAllText(PathHistory))
                               ?? new List<PurchaseHistory>();

            foreach (PurchaseHistory purchaseHistory in purchaseHistories)
            {
                basketHistory.Add(purchaseHistory);
            }
            
            using (StreamWriter streamWriter = new StreamWriter(PathHistory))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(basketHistory, Formatting.Indented));
            }
        }

        public static List<PurchaseHistory> GetPurchaseHistory()
        {
            return JsonConvert.DeserializeObject<List<PurchaseHistory>>(File.ReadAllText(PathLogin));
        }
    }
}