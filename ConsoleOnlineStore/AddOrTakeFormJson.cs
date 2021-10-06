using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleOnlineStore
{
    public static class AddOrTakeFormJson
    {
        public static void AddNewUser(Registration registration)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login&Password.json");

            List<Registration> registrations = new List<Registration>();
            
            if (File.Exists("Login&Password.json"))
            {
                registrations = JsonConvert.DeserializeObject<List<Registration>>(File.ReadAllText("Login&Password.json"))
                                ?? new List<Registration>();
            }
            
            registrations.Add(registration);
            
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(JsonConvert.SerializeObject(registrations, Formatting.Indented));
            sw.Close();
        }

        public static List<LoginInStore> TakeUsersForCheck()
        {
            if (!File.Exists("Login&Password.json"))
            {
                List<LoginInStore> user = new List<LoginInStore>();
                
                return user;
            }
            
            return JsonConvert.DeserializeObject<List<LoginInStore>>(
                File.ReadAllText("Login&Password.json"));
        }
        
        public static List<Goods> TakeGoods()
            => JsonConvert.DeserializeObject<List<Goods>>(File.ReadAllText("Goods.json"));
    }
}