using System;
using System.ComponentModel;
using System.Reflection;

namespace FirstHourLibrary.Scripts.Services;

public class EnumService : IEnumService
{
    public string GetEnumDescription(Enum enumValue)
    {
        if (enumValue == null)
        {
            return "Enum Value was Null";
        }

        string enumAsString = enumValue.ToString();

        FieldInfo? enumFieldInfo = enumValue.GetType().GetField(enumAsString);

        DescriptionAttribute? enumDescriptionAttribute = enumFieldInfo?.GetCustomAttribute<DescriptionAttribute>();

        string? enumDescriptionAttributeAsString = enumDescriptionAttribute?.Description;

        if (enumDescriptionAttributeAsString == null)
        {
            return "Enum Description Not Found";
        }

        return enumDescriptionAttributeAsString;
    }

}