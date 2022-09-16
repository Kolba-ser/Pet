using Random = UnityEngine.Random;

namespace Pet.Service.Randomizer
{
    public class RandomService : IRandomService
    {
        public int Range(int min, int max) =>
          Random.Range(min, max);
    }
}