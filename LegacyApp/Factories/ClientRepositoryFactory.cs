namespace LegacyApp;

public class ClientRepositoryFactory
{
    public static IClientRepository CreateClientRepository()
    {
        IDatabaseAccess<Client> databaseAccess = new ClientDataBaseAccess();
        return new ClientRepository(databaseAccess);
    }
}