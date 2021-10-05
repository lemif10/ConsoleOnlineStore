using System;

namespace ConsoleOnlineStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Registration registration = new Registration();
            
            registration.NewUser();
            
            AddOrTakeFormJson.AddNewUser(registration);
            
            LoginInStore loginInStore = new LoginInStore();

            loginInStore.Notify += PrintMessage;
            
            loginInStore.Join();
        }

        static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}