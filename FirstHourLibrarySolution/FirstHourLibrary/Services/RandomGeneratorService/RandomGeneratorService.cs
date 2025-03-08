using System.Security.Cryptography;
using System.Text;

namespace FirstHourLibrary.Scripts.Services;

public class RandomGeneratorService : IRandomGeneratorService
{

    private readonly RandomNumberGenerator _randomNumberGenerator;

    public RandomGeneratorService()
    {
        _randomNumberGenerator = RandomNumberGenerator.Create();
    }

    
    public char GenerateRandomCharacter()
    {
        const string availableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        int randomIndex = GenerateRandomNumberInRange(0, availableCharacters.Length - 1);
        return availableCharacters[randomIndex];
    }
    

    
    
    public int GenerateRandomNumber()
    {
        byte[] randomBytes = new byte[4];
        _randomNumberGenerator.GetBytes(randomBytes);
        int randomNumber = BitConverter.ToInt32(randomBytes, 0);
        return randomNumber;
    }

    public int GenerateRandomPositiveNumber()
    {   // Includes 0
        int randomPositiveNumber = (GenerateRandomNumber() & int.MaxValue);
        return randomPositiveNumber;
    }

    public int GenerateRandomNegativeNumber()
    {   // Excludes 0
        int randomNegativeNumber = (GenerateRandomNumber() | int.MinValue);
        return randomNegativeNumber;
    }

    


    public int GenerateRandomNumberInRange(int minimumNumber, int maximumNumber)
    {
        if (minimumNumber == maximumNumber)
        {
            return minimumNumber;
        }
        else if (minimumNumber > maximumNumber)
        {
            int temporaryMinimumNumber = minimumNumber;
            minimumNumber = maximumNumber;
            maximumNumber = temporaryMinimumNumber;
        }

        int numberRange = maximumNumber - minimumNumber + 1;

        int randomNumber = GenerateRandomPositiveNumber();

        int randomNumberInRange = minimumNumber + (randomNumber % numberRange);

        return randomNumberInRange;
    }



    //     Environment.Ticks

    //     DateTime.Now.Ticks

    //     System/Security/Cryptography
    // /RandomNumberGenerator.cs







    public bool GenerateRandomBoolean()
    {
        byte[] randomByte = new byte[1];
        _randomNumberGenerator.GetBytes(randomByte);
        bool randomBool = false;
        if (randomByte[0] % 2 == 0)
        {
            randomBool = true;
        }
        return randomBool;
    }






    public string GenerateRandomString(int length)
    {
        char[] randomString = new char[length];
        for (int i = 0; i < length; i++)
        {
            randomString[i] = GenerateRandomCharacter();
        }
        return new string(randomString);
    }















}


