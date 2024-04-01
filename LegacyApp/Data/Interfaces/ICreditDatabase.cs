using System;

namespace LegacyApp;

public interface ICreditDatabase
{
    /// <summary>
    /// Retrieves the credit limit for a specified customer based on their last name.
    /// </summary>
    /// <param name="lastName">The last name of the customer.</param>
    /// <param name="dateOfBirth">The date of birth of the customer.</param>
    /// <returns>The credit limit for the customer.</returns>
    int GetCustomerCreditLimit(string lastName, DateTime dateOfBirth);
}