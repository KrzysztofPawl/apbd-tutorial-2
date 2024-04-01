using System;

namespace LegacyApp;

public interface IUserCreditService
{
    /// <summary>
    /// Evaluates the credit limit for a customer based on their last name and date of birth.
    /// This method may involve additional business logic beyond retrieving the credit limit.
    /// </summary>
    /// <param name="lastName">The last name of the customer.</param>
    /// <param name="dateOfBirth">The date of birth of the customer.</param>
    /// <returns>The evaluated credit limit for the customer.</returns>
    int EvaluateCustomerCreditLimit(string lastName, DateTime dateOfBirth);
}