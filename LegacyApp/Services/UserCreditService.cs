using System;
using LegacyApp.Data.Interfaces;

namespace LegacyApp.Services
{
    public class UserCreditService : IUserCreditService, IDisposable
    {
        private readonly ICreditDatabase _creditDatabase;

        public UserCreditService(ICreditDatabase creditDatabase)
        {
            _creditDatabase = creditDatabase ?? throw new ArgumentNullException(nameof(creditDatabase));
        }

        public void Dispose()
        {
            //Simulating disposing of resources
        }

        /// <summary>
        /// This method is simulating contact with remote service which is used to get info about someone's credit limit
        /// </summary>
        /// <returns>Client's credit limit</returns>
        public int EvaluateCustomerCreditLimit(string lastName, DateTime dateOfBirth)
        {
            return _creditDatabase.GetCustomerCreditLimit(lastName, dateOfBirth);
        }
    }
}