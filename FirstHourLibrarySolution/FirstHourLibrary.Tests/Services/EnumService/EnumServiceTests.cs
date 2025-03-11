using Xunit;
using Moq;
using FirstHourLibrary.Scripts.Services;
using System.Diagnostics;

using System.ComponentModel;

namespace FirstHourLibrary.Tests.Services;

public class EnumServiceTests
{

    private readonly IEnumService _EnumService;

    public EnumServiceTests()
    {
        _EnumService = new EnumService();
    }

    [Fact]
    public void Enum_Description_Is_Obtained()
    {

        EnumSkeleton enumSkeletonValue = EnumSkeleton.Example1;
        string enumDescription = _EnumService.GetEnumDescription(enumSkeletonValue);
        Assert.Equal("Example1", enumDescription);

        enumSkeletonValue = EnumSkeleton.Example2;
        enumDescription = _EnumService.GetEnumDescription(enumSkeletonValue);
        Assert.Equal("Example 2", enumDescription);

        enumSkeletonValue = EnumSkeleton.Example3;
        enumDescription = _EnumService.GetEnumDescription(enumSkeletonValue);
        Assert.Equal("Example 3 You can add whatever string you want to associate.", enumDescription);

    }

}

public enum EnumSkeleton
{
    [Description("Example1")]
    Example1,

    [Description("Example 2")]
    Example2,

    [Description("Example 3 You can add whatever string you want to associate.")]
    Example3
}




