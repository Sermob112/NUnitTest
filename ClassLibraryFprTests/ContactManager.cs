using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForTests
{
    public class ContactManager
    {
        private readonly List<Client> _clients = new();

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public Client SearchClient(string name)
        {
            return _clients.FirstOrDefault(c => c.Name == name);
        }

        public List<Client> GetClientList()
        {
            return _clients;
        }
    }
}
