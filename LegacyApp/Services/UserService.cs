using System;
using LegacyApp.Data;
using LegacyApp.Data.Interfaces;
using LegacyApp.Factories;
using LegacyApp.Models;

namespace LegacyApp.Services
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;
        private const int MinimumAge = 21;
        private const int MinimumCreditLimit = 500;

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService) =>
            (_clientRepository, _userCreditService) = (clientRepository, userCreditService);

        public UserService()
            : this(ClientRepositoryFactory.CreateClientRepository(), UserCreditServiceFactory.Create())
        {
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //Validate Input
            if (!IsValidNames(firstName, lastName))
            {
                return false;
            }

            if (!IsValidEmail(email))
            {
                return false;
            }

            if (!IsValidAge(dateOfBirth))
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };


            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    int creditLimit = _userCreditService.EvaluateCustomerCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit * 2;
                    break;
                default:
                    user.HasCreditLimit = true;
                    user.CreditLimit = _userCreditService.EvaluateCustomerCreditLimit(user.LastName, user.DateOfBirth);
                    break;
            }

            if (user.HasCreditLimit && user.CreditLimit < MinimumCreditLimit)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static bool IsValidNames(string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
        }

        private static bool IsValidEmail(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }

        private static bool IsValidAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age >= MinimumAge;
        }
    }
}