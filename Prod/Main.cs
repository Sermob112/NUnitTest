using ClassLibraryForTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NunitTest
{
    public class Program
    {
        public static void Main()
        {
            var clientManager = new ClientManager();
            clientManager.AddClient(new Client { Name = "Company A", HasContract = true, City = "New York", ContactName = "John Doe", IsCurrent = true });
            clientManager.AddClient(new Client { Name = "Company B", HasContract = false, City = "Los Angeles", ContactName = "Jane Smith", IsCurrent = false });

            var categorized = clientManager.CategorizeClients(clientManager.SearchClients());
            Console.WriteLine("Current Clients:");
            foreach (var client in categorized["Current"])
            {
                Console.WriteLine(client.Name);
            }

            Console.WriteLine("Potential Clients:");
            foreach (var client in categorized["Potential"])
            {
                Console.WriteLine(client.Name);
            }

            var contactManager = new ContactManager();
            contactManager.AddClient(new Client { Name = "Company C", IsCurrent = true });
            contactManager.AddClient(new Client { Name = "Company D", IsCurrent = false });

            Console.WriteLine("Search result: " + contactManager.SearchClient("Company C")?.Name);
        }
    }
}
