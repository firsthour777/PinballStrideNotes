namespace FirstHourLibrary.Scripts.Services;

public class CharacterService : ICharacterService
{

    private const string availableCharactersString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public string GetAvailableCharactersString()
    {
        return availableCharactersString;
    }

}










