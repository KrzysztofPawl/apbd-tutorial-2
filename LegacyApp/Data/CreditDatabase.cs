using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp;

public class CreditDatabase : ICreditDatabase
{
    /// <summary>
    /// Simulating database
    /// </summary>
    private readonly Dictionary<string, int> _database = new Dictionary<string, int>
    {
        { "Kowalski", 200 },
        { "Malewski", 20000 },
        { "Smith", 10000 },
        { "Doe", 3000 },
        { "Kwiatkowski", 1000 }
    };

    public int GetCustomerCreditLimit(string lastName, DateTime dateOfBirth)
    {
        var randomWaitingTime = new Random().Next(3000); //Simulate DB access delay
        Thread.Sleep(randomWaitingTime);

        if (_database.TryGetValue(lastName, out var creditLimit))
        {
            return creditLimit;
        }

        throw new ArgumentException($"Client {lastName} does not exist");
    }
}