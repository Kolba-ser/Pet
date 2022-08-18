using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Randomizer
{
  public interface IRandomService : IService
  {
    public int Next(int minValue, int maxValue);
  }
}