using Pet.Service;

namespace Pet.Service.Randomizer
{
    public interface IRandomService : IService
    {
        public int Range(int minValue, int maxValue);
    }
}