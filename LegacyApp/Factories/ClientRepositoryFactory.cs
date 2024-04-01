using LegacyApp.Data;
using LegacyApp.Data.Interfaces;
using LegacyApp.Models;

namespace LegacyApp.Factories;

public class ClientRepositoryFactory
{
    public static IClientRepository CreateClientRepository()
    {
        IDatabaseAccess<Client> databaseAccess = new ClientDataBaseAccess();
        return new ClientRepository(databaseAccess);
    }
}