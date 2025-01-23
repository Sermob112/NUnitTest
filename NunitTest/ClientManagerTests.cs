using ClassLibraryForTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NunitTests
{
    public class ClientManagerTests
    {
        [Test]
        public void CategorizeClients_ShouldSeparateCurrentAndPotentialClients()
        {
            
            var clients = new List<Client>
            {
                new Client { Name = "Company A", HasContract = true },
                new Client { Name = "Company B", HasContract = false },
                new Client { Name = "Company C", HasContract = true },
                new Client { Name = "Company D", HasContract = false }
            };

            var clientManager = new ClientManager();

           
            var result = clientManager.CategorizeClients(clients);

            
            Assert.That(result["Current"].Count, Is.EqualTo(2), "Current clients count should be 2");
            Assert.That(result["Potential"].Count, Is.EqualTo(2), "Potential clients count should be 2");


            Assert.That(result["Current"].Exists(c => c.Name == "Company A"), "Company A should be a current client");
            Assert.That(result["Current"].Exists(c => c.Name == "Company C"), "Company C should be a current client");
            Assert.That(result["Potential"].Exists(c => c.Name == "Company B"), "Company B should be a potential client");
            Assert.That(result["Potential"].Exists(c => c.Name == "Company D"), "Company D should be a potential client");
        }

        public void ContactManager_ShouldSupportContinuousClientContact()
        {
            
            var contactManager = new ContactManager();

            var client1 = new Client { Name = "Company A", IsCurrent = true };
            var client2 = new Client { Name = "Company B", IsCurrent = false };

           
            contactManager.AddClient(client1);
            contactManager.AddClient(client2);

            var allClients = contactManager.GetClientList();
            var searchResult = contactManager.SearchClient("Company A");

           
            Assert.That(allClients.Count, Is.EqualTo(2), "The system should have 2 clients.");
            Assert.That(allClients, Has.Exactly(1).Matches<Client>(c => c.Name == "Company A"), "Company A should be in the client list.");
            Assert.That(searchResult, Is.Not.Null, "Search should return a result for an existing client.");
            Assert.That(searchResult.Name, Is.EqualTo("Company A"), "Search result should match the client's name.");
        }


    }
}

