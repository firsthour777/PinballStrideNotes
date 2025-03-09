using Xunit;
using Moq;
using FirstHourLibrary.Scripts.Services;
using System.Diagnostics;

namespace FirstHourLibrary.Tests.Services.RandomNumberService;
public class RandomGeneratorServiceTests
{

    private readonly RandomGeneratorService _randomGeneratorService;

    public RandomGeneratorServiceTests()
    {
        _randomGeneratorService = new RandomGeneratorService(null);
    }


    [Fact]
    public void Random_Number_Is_Generated()
    {
        List<int> randomNumberList = new List<int>();

        for (int i = 0; i < 100; i++)
        {
            int randomNumber = _randomGeneratorService.GenerateRandomNumber();
            randomNumberList.Add(randomNumber);
        }

        randomNumberList = randomNumberList.Distinct().ToList();

        Assert.True(randomNumberList.Count > 50);

    }

    [Fact]
    public void Random_Positive_Number_Is_Generated()
    {
        List<int> randomPositiveNumberList = new List<int>();

        for (int i = 0; i < 100; i++)
        {
            int randomPositiveNumber = _randomGeneratorService.GenerateRandomPositiveNumber();
            randomPositiveNumberList.Add(randomPositiveNumber);
        }

        randomPositiveNumberList = randomPositiveNumberList.Distinct().ToList();

        Assert.True(randomPositiveNumberList.Count > 50);

        for (int i = 0; i < randomPositiveNumberList.Count; i++)
        {
            Assert.True(randomPositiveNumberList[i] >= 0);
        }

        

    }


    [Fact]
    public void Random_Negative_Number_Is_Generated()
    {
        List<int> randomNegativeNumberList = new List<int>();

        for (int i = 0; i < 100; i++)
        {
            int randomNegativeNumber = _randomGeneratorService.GenerateRandomNegativeNumber();
            randomNegativeNumberList.Add(randomNegativeNumber);
        }

        randomNegativeNumberList = randomNegativeNumberList.Distinct().ToList();

        Assert.True(randomNegativeNumberList.Count > 50);

        for (int i = 0; i < randomNegativeNumberList.Count; i++)
        {
            Assert.True(randomNegativeNumberList[i] < 0);
        }

    }



    [Theory]
    [InlineData(-50, 50)]
    [InlineData(0, 100)] 
    [InlineData(-100, -1)]
    public void Random_Number_In_Range_Is_Generated(int minimumNumber, int maximumNumber)
    {
        List<int> randomNumberList = new List<int>();

        for (int i = 0; i < 100; i++)
        {
            int randomNumber = _randomGeneratorService.GenerateRandomNumberInRange(minimumNumber, maximumNumber);
            randomNumberList.Add(randomNumber);
        }

        randomNumberList = randomNumberList.Distinct().ToList();

        Assert.True(randomNumberList.Count > 50);

        for (int i = 0; i < randomNumberList.Count; i++)
        {
            Assert.True(randomNumberList[i] >= minimumNumber);
            Assert.True(randomNumberList[i] <= maximumNumber);
        }

    }


    [Fact]
    public void Random_Boolean_Is_Generated()
    {
        bool isHasTrue = false;
        bool isHasFalse = false;
        bool isHasTrueAndFalse = false;

        Stopwatch stopwatch = Stopwatch.StartNew();
        int timeoutSeconds = 30;

        while (!isHasTrueAndFalse)
        {
            if (stopwatch.Elapsed.TotalSeconds > timeoutSeconds)
            {
                Assert.Fail("The test timed out after 30 seconds.");
                return;
            }

            bool randomBool = _randomGeneratorService.GenerateRandomBoolean();

            if (randomBool)
            {
                isHasTrue = true;
            }
            else
            {
                isHasFalse = true;
            }

            if (isHasTrue && isHasFalse)
            {
                isHasTrueAndFalse = true;
            }
        }

        Assert.True(isHasTrueAndFalse, "Generated at least one True and False value in less than 30 seconds.");
    }


    [Fact]
    public void Random_Character_Is_Generated()
    {
        ICharacterService characterService = new CharacterService();
        string availableCharactersString = characterService.GetAvailableCharactersString();

        List<char> randomCharacterList = new List<char>();

        for (int i = 0; i < 100; i++)
        {
            char randomCharacter = _randomGeneratorService.GenerateRandomCharacter();
            randomCharacterList.Add(randomCharacter);
        }

        randomCharacterList = randomCharacterList.Distinct().ToList();

        Assert.True(randomCharacterList.Count > 5);

        for (int i = 0; i < randomCharacterList.Count; i++)
        {
            Assert.True(availableCharactersString.Contains(randomCharacterList[i]));
        }
    }


    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    public void Random_String_Is_Generated(int lengthOfRandomString)
    {
        List<string> randomStringList = new List<string>();

        for (int i = 0; i < 100; i++)
        {
            string randomString = _randomGeneratorService.GenerateRandomString(lengthOfRandomString);
            randomStringList.Add(randomString);
        }

        randomStringList = randomStringList.Distinct().ToList();

        Assert.True(randomStringList.Count > 5);

        for (int i = 0; i < randomStringList.Count; i++)
        {
            Assert.Equal(randomStringList[i].Length, lengthOfRandomString);
        }

        
    }
    


}






