using LegacyApp;

namespace LegacyAppTest
{
    public class UserCreditServiceTest
    {
        //Mocks
        private class MockCreditDatabase : ICreditDatabase
        {
            private readonly Dictionary<string, int> _mockData;
            private readonly bool _shouldThrow;

            public MockCreditDatabase(Dictionary<string, int> mockData, bool shouldThrow = false)
            {
                _mockData = mockData;
                _shouldThrow = shouldThrow;
            }

            public int GetCustomerCreditLimit(string lastName, DateTime dateOfBirth)
            {
                if (_shouldThrow)
                {
                    throw new ArgumentException($"Client {lastName} does not exist");
                }

                if (_mockData.TryGetValue(lastName, out var creditLimit))
                {
                    return creditLimit;
                }

                throw new ArgumentException($"Client {lastName} does not exist");
            }
        }

        //Tests
        [Fact]
        public void EvaluateCustomerCreditLimitReturnsCorrectLimitWhenCustomerExists()
        {
            var mockData = new Dictionary<string, int> { { "Kowalski", 500 } };
            var mockCreditDatabase = new MockCreditDatabase(mockData);
            var service = new UserCreditService(mockCreditDatabase);
            var lastName = "Kowalski";
            var dateOfBirth = new DateTime(1980, 1, 1);

            var result = service.EvaluateCustomerCreditLimit(lastName, dateOfBirth);

            Assert.Equal(500, result);
        }

        [Fact]
        public void EvaluateCustomerCreditLimitThrowsExceptionWhenNoCustomerInDatabase()
        {
            var mockData = new Dictionary<string, int>();
            var mockCreditDatabase = new MockCreditDatabase(mockData, shouldThrow: true);
            var service = new UserCreditService(mockCreditDatabase);
            var lastName = "NonExistent";
            var dateOfBirth = new DateTime(1980, 1, 1);

            Assert.Throws<ArgumentException>(() => service.EvaluateCustomerCreditLimit(lastName, dateOfBirth));
        }
    }
}