namespace PinballStride.Scripts.Services;

public class BooleanService : IBooleanService
{
   public async Task<int> ConvertInversionBoolToInt(bool isInverted)
   {
      int inverterValue = 1;
      if (isInverted)
      {
         returnValue = -1;
      }
      return inverterValue;
   }

}

