namespace FirstHourLibrary.Scripts.Services;

public class BooleanService : IBooleanService
{
   public int ConvertInversionBoolToInt(bool isInverted)
   {
      int inverterValue = 1;
      if (isInverted)
      {
         inverterValue = -1;
      }
      return inverterValue;
   }

}

