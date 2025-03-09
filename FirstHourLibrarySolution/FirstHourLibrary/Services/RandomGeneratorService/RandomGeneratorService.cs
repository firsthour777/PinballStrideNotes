using System.Security.Cryptography;
using System.Text;

namespace FirstHourLibrary.Scripts.Services;

public class RandomGeneratorService : IRandomGeneratorService
{

    private readonly RandomNumberGenerator _randomNumberGenerator;
    private readonly ICharacterService _characterService;

    public RandomGeneratorService(ICharacterService? characterService)
    {
        if (characterService == null)
        {
            _characterService = new CharacterService();
        }
        else
        {
            _characterService = characterService;
        }
        
        _randomNumberGenerator = RandomNumberGenerator.Create();
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


    public char GenerateRandomCharacter()
    {
        string availableCharactersString = _characterService.GetAvailableCharactersString();
        int randomIndex = GenerateRandomNumberInRange(0, availableCharactersString.Length - 1);
        char randomCharacter = availableCharactersString[randomIndex];
        return randomCharacter;
    }


    public string GenerateRandomString(int lengthOfRandomString)
    {
        char[] randomCharacters = new char[lengthOfRandomString];

        for (int i = 0; i < lengthOfRandomString; i++)
        {
            randomCharacters[i] = GenerateRandomCharacter();
        }

        string randomString = new string(randomCharacters);
        return randomString;
    }


}


