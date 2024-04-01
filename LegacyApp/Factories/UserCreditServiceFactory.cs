using LegacyApp.Data;
using LegacyApp.Data.Interfaces;
using LegacyApp.Services;

namespace LegacyApp.Factories;

public class UserCreditServiceFactory
{
    public static IUserCreditService Create()
    {
        //Pick Database Here
        ICreditDatabase creditDatabase = new CreditDatabase();
        return new UserCreditService(creditDatabase);
    }
}