using System;
using System.IO;
using Newtonsoft.Json;

namespace TruckingLogin
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CheckForIdFile();
            
            Console.WriteLine("\n\n\n\t\t\t\t\t1.Create Account \n\t\t\t\t\t2.Login");
            Console.Write("\t\t\t\t\tChoice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    CreateAccount();
                    break;
                case "2":
                    Console.Clear();
                    Login();
                    break;
            }
            Login();
        }

        private static void CheckForIdFile()
        {
            if (!File.Exists("NumberOfTruckers.txt"))
            {
                File.WriteAllText("NumberOfTruckers.txt", "0");
            }

            return;
        }

        private static void CreateAccount()
        {
            
            int currentId = Convert.ToInt32(File.ReadAllText("NumberOfTruckers.txt"));

            int newId = currentId + 1;
            Console.WriteLine("\n\n\n\t\t\t\t\tYou are creating an account as trucker id: " + newId);
            Console.WriteLine("\t\t\t\t\tEnter a password for your account (9 characters): ");
            Console.Write("\t\t\t\t\tPassword: ");
            string password = Console.ReadLine();
            if(password.Length < 9)
            {
                Console.WriteLine("Password must be 9 characters long");
                CreateAccount();
            }
            Console.Write("\t\t\t\t\tEnter your name: ");
            string name = Console.ReadLine();
            
            File.WriteAllText("NumberOfTruckers.txt", newId.ToString());
            TruckingLogin trucker = new TruckingLogin(newId, password, name);
            var json = JsonConvert.SerializeObject(trucker);
            File.WriteAllText(newId + ".json", json);
            Main(null);
        }

        public static void Login()
        {
            Console.Write("\n\n\n\t\t\t\t\tEnter ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("\n\t\t\t\t\tEnter Password: ");
            string password = Console.ReadLine();
            if (File.Exists(id + ".json"))
            {
                var json = File.ReadAllText(id + ".json");
                TruckingLogin trucker = JsonConvert.DeserializeObject<TruckingLogin>(json);
                if (trucker.password == password)
                {
                    DoLoggedInScreen(trucker);
                }
                else
                {
                    Console.WriteLine("\t\t\t\t\tIncorrect Password");
                    Login();
                }
            }
            
            }

        private static void DoLoggedInScreen(TruckingLogin trucker)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\t\t\t\t\t\tWelcome " + trucker.Name);
            Console.WriteLine("\t\t\t\t\t1.View Orders \n\t\t\t\t\t2.Add Order \n\t\t\t\t\t3.Logout");
            Console.Write("\t\t\t\t\tChoice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewOrders(trucker);
                    break;
                case "2":
                    AddOrder(trucker);
                    break;
                case "3":
                    Main(null);
                    break;
            }
        }

        private static void AddOrder(TruckingLogin trucker)
        {
            Console.Clear();
            Console.Write("\n\n\n\t\t\t\t\tEnter Order Address: ");
            string address = Console.ReadLine();
            
            Console.Write("\t\t\t\t\tEnter Order Weight (lb.): ");
            int weight = Convert.ToInt32(Console.ReadLine());
            
            Console.Write("\t\t\t\t\tEnter Order Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            
            Order order = new Order(address, weight, quantity);
            trucker.AddNewOrder(order);
            var json = JsonConvert.SerializeObject(trucker);
            
            File.WriteAllText(trucker.id + ".json", json);
            
            DoLoggedInScreen(trucker);
        }

        private static void ViewOrders(TruckingLogin trucker)
        {
            Console.WriteLine("\n\n\n\t\t\t\t\tOrders for " + trucker.Name);
            foreach (var order in trucker.Orders)
            {
                Console.WriteLine("\t\t\t\t\tAddress:" + order.Address + ". \n\t\t\t\t\tWeight (lb.): " + order.Weight + " \n\t\t\t\t\tQuantity: " + order.Quantity + "\n\t\t\t\t\t---------------------");
            }
            Console.WriteLine("\n\t\t\t\t\tPress any key to go back");
            Console.ReadKey();
            DoLoggedInScreen(trucker);
        }
    }
}