namespace FirstHourLibrary.Scripts.Services;

public interface IRandomGeneratorService
{

    public int GenerateRandomNumber();

    public int GenerateRandomNumberInRange(int minimumNumber, int maximumNumber);

    public bool GenerateRandomBoolean();

    public char GenerateRandomCharacter();

    public string GenerateRandomString(int length);

}










