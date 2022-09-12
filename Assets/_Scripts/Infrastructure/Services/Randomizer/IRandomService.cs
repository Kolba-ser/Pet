using Pet.Infrastructure.Services;

namespace Pet.Services.Randomizer
{
    public interface IRandomService : IService
    {
        public int Range(int minValue, int maxValue);
    }
}