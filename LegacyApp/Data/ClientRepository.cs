namespace LegacyApp
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDatabaseAccess<Client> _databaseAccess;

        public ClientRepository(IDatabaseAccess<Client> databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }
        
        /// <summary>
        /// Simulating fetching a client from remote database
        /// </summary>
        /// <returns>Returning client object</returns>
        public Client GetById(int clientId)
        {
            return _databaseAccess.GetById(clientId);
        }
    }
}