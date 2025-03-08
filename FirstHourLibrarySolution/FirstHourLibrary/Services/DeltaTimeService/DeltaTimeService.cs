// This class will initialize DeltaTime and make it easy to get DeltaTime.
// It will be a global variable.

// put the DT line of code within your script, then when accessing it will get the Delta Time

namespace FirstHourLibrary.Scripts.Services;

public class DeltaTimeService : IDeltaTimeService
{
    // private float DT => (float)Game.UpdateTime.Elapsed.TotalSeconds;
    private float DT => await GetDeltaTime();
    
    public async Task<float> GetDeltaTime()
    {
        float deltaTime = (float)Game.UpdateTime.Elapsed.TotalSeconds;
        return deltaTime;
    }

}


