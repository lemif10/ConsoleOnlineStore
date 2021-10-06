using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleOnlineStore
{
    public static class JsonSerializer
    {
        public static void AddNewUser(Registration registration)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login&Password.json");

            var registrations = new List<Registration>();

            if (File.Exists("Login&Password.json"))
                registrations =
                    JsonConvert.DeserializeObject<List<Registration>>(File.ReadAllText("Login&Password.json"))
                    ?? new List<Registration>();

            registrations.Add(registration);

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

        public static List<Goods> TakeGoods()
        {
            return JsonConvert.DeserializeObject<List<Goods>>(File.ReadAllText("Goods.json"));
        }
    }
}