using LegacyApp;

namespace LegacyAppTest;

//Setup
public class MockClientRepository : IClientRepository
{
    private readonly Client _clientToReturn;

    public MockClientRepository(Client clientToReturn)
    {
        _clientToReturn = clientToReturn;
    }

    public Client GetById(int clientId)
    {
        return _clientToReturn;
    }
}

public class MockUserCreditService : IUserCreditService
{
    private readonly int _creditLimit;

    public MockUserCreditService(int creditLimit = 1000)
    {
        _creditLimit = creditLimit;
    }

    public int EvaluateCustomerCreditLimit(string lastName, DateTime dateOfBirth)
    {
        return _creditLimit;
    }
}

public class UserServiceTests
{
    private const int MinimumAge = 21;

    //Helper Methods
    private static (UserService, IClientRepository, IUserCreditService) CreateUserServiceWithMocks(string clientType,
        int creditLimit = 1000)
    {
        var client = new Client { Type = clientType };
        var mockClientRepository = new MockClientRepository(client);
        var mockUserCreditService = new MockUserCreditService(creditLimit);
        var mockUserService = new UserService(mockClientRepository, mockUserCreditService);

        return (mockUserService, mockClientRepository, mockUserCreditService);
    }

    private static (string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        GetMockUserData(
            string firstName = "John",
            string lastName = "Doe",
            string email = "john@wp.com",
            DateTime? dateOfBirth = null,
            int clientId = 1)
    {
        dateOfBirth ??= new DateTime(1995, 1, 1);
        return (firstName, lastName, email, dateOfBirth.Value, clientId);
    }

    //Tests
    [Fact]
    public void AddUserShouldReturnFalseWhenEmailWithoutAtAndDot()
    {
        var (testObj, _, _) = CreateUserServiceWithMocks("NormalClient");
        var (firstName, lastName, email, dateOfBirth, clientId) = GetMockUserData(email: "Invalid");
        var result = testObj.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        Assert.False(result);
    }

    [Fact]
    public void AddUserShouldReturnFalseWhenAgeBelow21()
    {
        var (testObj, _, _) = CreateUserServiceWithMocks("NormalClient");
        var (firstName, lastName, email, dateOfBirth, clientId) =
            GetMockUserData(dateOfBirth: new DateTime(2010, 1, 1));
        var result = testObj.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        Assert.False(result);
    }

    [Fact]
    public void AddUserShouldReturnFalseWhenCreditLimitBelowThreshold()
    {
        var (testObj, _, _) = CreateUserServiceWithMocks("NormalClient", 200);
        var (firstName, lastName, email, dateOfBirth, clientId) = GetMockUserData();
        var result = testObj.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        Assert.False(result);
    }

    [Fact]
    public void AddUserShouldReturnTrueForNormalClientWithValidInputData()
    {
        var (testObj, _, _) = CreateUserServiceWithMocks("NormalClient");
        var (firstName, lastName, email, dateOfBirth, clientId) = GetMockUserData();
        var result = testObj.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        Assert.True(result);
    }

    [Fact]
    public void AddUserShouldReturnFalseWhenUserJustUnderMinimumAge()
    {
        var today = DateTime.Today;
        var mockDateOfBirth = today.AddYears(-MinimumAge).AddDays(1);

        var (testObj, _, _) = CreateUserServiceWithMocks("NormalClient");
        var (firstName, lastName, email, dateOfBirth, clientId) = GetMockUserData(dateOfBirth: mockDateOfBirth);
        var result = testObj.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.False(result);
    }

    [Fact]
    public void AddUserShouldReturnTrueWhenUserExactlyAtMinimumAge()
    {
        var today = DateTime.Today;
        var mockDateOfBirth = today.AddYears(-MinimumAge);

        var (testObj, _, _) = CreateUserServiceWithMocks("NormalClient");
        var (firstName, lastName, email, dateOfBirth, clientId) = GetMockUserData(dateOfBirth: mockDateOfBirth);
        var result = testObj.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }
}