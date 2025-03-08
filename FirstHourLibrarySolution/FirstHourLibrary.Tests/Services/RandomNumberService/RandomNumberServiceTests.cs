using Xunit;
using Prime.Services;

namespace FirstHourLibrary.Tests.Services.RandomNumberService
{
    public class RandomNumberServiceTests
    {
        [Fact]
        public void Random_Number_Is_Generated()
        {
            var primeService = new PrimeService();
            bool result = primeService.IsPrime(1);

            Assert.False(result, "1 should not be prime");
        }
    }
}






