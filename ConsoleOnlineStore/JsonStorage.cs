using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleOnlineStore
{
    public static class JsonStorage
    {
        public static void AddNewUser(User user)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login&Password.json");

            List<User> registrations = new List<User>();

            if (File.Exists("Login&Password.json"))
            {
                registrations =
                JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Login&Password.json"))
                ?? new List<User>(); 
            }

            registrations.Add(user);

            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(registrations, Formatting.Indented));
            }
        }

        public static List<User> GetUser()
        {
            if (!File.Exists("Login&Password.json"))
            {
                var user = new List<User>();

                return user;
            }

            return JsonConvert.DeserializeObject<List<User>>(
                File.ReadAllText("Login&Password.json"));
        }

        public static List<Goods> GetGoods()
        {
            return JsonConvert.DeserializeObject<List<Goods>>(File.ReadAllText("Goods.json"));
        }
    }
}