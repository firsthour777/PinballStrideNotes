namespace FirstHourLibrary.Scripts.Services;

public interface IRandomGeneratorService
{

    public int GenerateRandomNumber();

    public int GenerateRandomPositiveNumber();

    public int GenerateRandomNegativeNumber();

    public int GenerateRandomNumberInRange(int minimumNumber, int maximumNumber);

    public bool GenerateRandomBoolean();

    public char GenerateRandomCharacter();

    public string GenerateRandomString(int lengthOfRandomString);

}










