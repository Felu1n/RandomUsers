using RandomUsers.Models;
using Bogus;
using System;
using System.Collections.Generic;

namespace RandomUsers.Utility
{
    public interface IUserDataService
    {
        List<UserData> GenerateUserData(int count, double errorRate, int seed, string region);
    }

    public class UserDataService : IUserDataService
    {
        public List<UserData> GenerateUserData(int count, double errorRate, int seed, string region)
        {
            var faker = new Bogus.Faker(region);
            faker.Random = new Bogus.Randomizer(seed);
            var random = new Random(seed);

            var userDataList = new List<UserData>();

            for (int i = 0; i < count; i++)
            {
                var userData = new UserData
                {
                    Id = GenerateUUIDFromSeed(random), // Генерация UUID
                    FullName = faker.Name.FullName(),
                    Address = faker.Address.StreetAddress(),
                    PhoneNumber = faker.Phone.PhoneNumberFormat()
                };


                if (errorRate > 0)
                {
     
                    int errors = (int)Math.Floor(errorRate);
                    double errorChance = errorRate - errors;
                    if (random.NextDouble() < errorChance)
                        errors++; 

                    for (int j = 0; j < errors; j++)
                    {
                        int fieldIndex = random.Next(0, 3);
                        int errorType = random.Next(0, 3);

                        switch (fieldIndex)
                        {
                            case 0: 
                                userData.FullName = ApplyError(userData.FullName, errorType, random);
                                break;
                            case 1: 
                                userData.Address = ApplyError(userData.Address, errorType, random);
                                break;
                            case 2: 
                                userData.PhoneNumber = ApplyError(userData.PhoneNumber, errorType, random);
                                break;
                        }
                    }
                }
                userDataList.Add(userData);

            }
            

            return userDataList;
            
        }

        private string ApplyError(string value, int errorType, Random random)
        {
            int index = random.Next(value.Length);

            switch (errorType)
            {
                case 0: 
                    return RemoveRandomCharacter(value, random);
                case 1: 
                    return AddRandomCharacter(value, random);
                case 2: 
                    return SwapAdjacentCharacters(value, random);
                default:
                    return value;
            }
        }

        private string RemoveRandomCharacter(string value, Random random)
        {
            if (value.Length == 0)
                return value;

            int index = random.Next(0, value.Length);
            return value.Remove(index, 1);
        }

        private string AddRandomCharacter(string value, Random random)
        {
            char randomChar = (char)random.Next(33, 127);
            int index = random.Next(0, value.Length + 1);
            return value.Insert(index, randomChar.ToString());
        }

        private string SwapAdjacentCharacters(string value, Random random)
        {
            if (value.Length < 2)
                return value;

            int index = random.Next(0, value.Length - 1);
            return value.Substring(0, index) + value[index + 1] + value[index] + value.Substring(index + 2);
        }

        private string GenerateUUIDFromSeed(Random random)
        {
            byte[] bytes = new byte[16];
            random.NextBytes(bytes);
            
            Guid guid = new Guid(bytes);
            return guid.ToString();
        }
        
        
    }
}
