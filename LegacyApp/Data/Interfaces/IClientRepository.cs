using LegacyApp.Models;

namespace LegacyApp.Data.Interfaces;

public interface IClientRepository
{
    Client GetById(int clientId);
}