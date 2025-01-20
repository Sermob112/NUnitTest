using NUnit.Framework;
using System.Collections.Generic;

namespace CRMSystem.Tests
{
    [TestFixture]
    public class ClientSearchTests
    {
        private List<string> _clients;

        [SetUp]
        public void SetUp()
        {
            // Инициализируем тестовые данные
            _clients = new List<string>
            {
                "Alpha Tech",
                "Beta Corp",
                "Gamma Solutions",
                "Delta Systems"
            };
        }

        [Test]
        public void FindClients_ExactMatch_ReturnsClient()
        {
            // Arrange
            var query = "Alpha Tech";

            // Act
            var result = FindClients(query);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Alpha Tech"));
        }

        [Test]
        public void FindClients_PartialMatch_ReturnsMatchingClients()
        {
            // Arrange
            var query = "Tech";

            // Act
            var result = FindClients(query);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Alpha Tech"));
        }

        [Test]
        public void FindClients_NoMatch_ReturnsEmptyList()
        {
            // Arrange
            var query = "Zeta";

            // Act
            var result = FindClients(query);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindClients_EmptyQuery_ReturnsAllClients()
        {
            // Arrange
            var query = "";

            // Act
            var result = FindClients(query);

            // Assert
            Assert.That(result.Count, Is.EqualTo(_clients.Count));
        }

        private List<string> FindClients(string query)
        {
            if (string.IsNullOrEmpty(query))
                return _clients;

            return _clients.FindAll(client => client.Contains(query));
        }
    }
}
