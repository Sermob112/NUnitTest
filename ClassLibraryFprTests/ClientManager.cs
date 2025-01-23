using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForTests
{
    public class ClientManager
    {
        private readonly List<Client> _clients = new();

        public void AddClient(Client client) => _clients.Add(client);
        public Dictionary<string, List<Client>> CategorizeClients(List<Client> clients)
        {
            var categorizedClients = new Dictionary<string, List<Client>>
        {
            { "Current", new List<Client>() },
            { "Potential", new List<Client>() }
        };

            foreach (var client in clients)
            {
                if (client.HasContract)
                    categorizedClients["Current"].Add(client);
                else
                    categorizedClients["Potential"].Add(client);
            }

            return categorizedClients;
        }

        public List<Client> SearchClients(string name = null, string city = null, string contactName = null)
        {
            return _clients.Where(c =>
                (string.IsNullOrEmpty(name) || c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(city) || c.City.Contains(city, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(contactName) || c.ContactName.Contains(contactName, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }
}
