namespace PinballStride.Scripts.Services;

public interface IBooleanService
{
    Task<int> ConvertInversionBoolToInt(bool isInverted);
}