using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ConsoleOnlineStore
{
    public static class AddOrTakeFormJson
    {
        public static void AddNewUser(Registration registration)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Login&Password.json");

            List<Registration> registrations = JsonConvert.DeserializeObject<List<Registration>>(File.ReadAllText("Login&Password.json"))
                                               ?? new List<Registration>();

            registrations.Add(registration);
            
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(JsonConvert.SerializeObject(registrations, Formatting.Indented));
            sw.Close();
        }

        public static List<LoginInStore> TakeUsersForCheck()
        {
            List<LoginInStore> users =
                JsonConvert.DeserializeObject<List<LoginInStore>>(File.ReadAllText("Login&Password.json"));

            return users;
        }
    }
}