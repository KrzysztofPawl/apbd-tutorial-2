namespace LegacyApp;

public class UserCreditServiceFactory
{
    public static IUserCreditService Create()
    {
        //Pick Database Here
        ICreditDatabase creditDatabase = new CreditDatabase();
        return new UserCreditService(creditDatabase);
    }
}