using LegacyApp.Data;
using LegacyApp.Data.Interfaces;
using LegacyApp.Models;

namespace LegacyAppTest
{
    public class ClientRepositoryTest
    {
        //Mocks
        private class MockClientRepository : IDatabaseAccess<Client>
        {
            private readonly Dictionary<int, Client> _mockClients;
            private readonly bool _shouldThrow;

            public MockClientRepository(Dictionary<int, Client> clients, bool shouldThrow = false)
            {
                _mockClients = clients;
                _shouldThrow = shouldThrow;
            }

            public Client GetById(int clientId)
            {
                if (_shouldThrow)
                {
                    throw new ArgumentException($"User with id {clientId} does not exist in database");
                }

                if (_mockClients.TryGetValue(clientId, out var client))
                {
                    return client;
                }

                throw new ArgumentException($"User with id {clientId} does not exist in database");
            }
        }

        //Tests
        [Fact]
        public void GetByIdReturnsClientWhenExistsInDatabase()
        {
            var mockClients = new Dictionary<int, Client>
            {
                { 1, new Client { ClientId = 1, Name = "Test Client" } }
            };
            var mockDatabaseAccess = new MockClientRepository(mockClients);
            var clientRepository = new ClientRepository(mockDatabaseAccess);

            var client = clientRepository.GetById(1);

            Assert.NotNull(client);
            Assert.Equal("Test Client", client.Name);
        }

        [Fact]
        public void GetByIdThrowsExceptionWhenNoClientInDatabase()
        {
            var mockClients = new Dictionary<int, Client>();
            var mockDatabaseAccess = new MockClientRepository(mockClients, shouldThrow: true);
            var clientRepository = new ClientRepository(mockDatabaseAccess);

            Assert.Throws<ArgumentException>(() => clientRepository.GetById(123));
        }
    }
}