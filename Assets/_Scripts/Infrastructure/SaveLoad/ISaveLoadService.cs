using Pet.Data;
using Pet.Infrastructure.Services;

namespace Pet.Infrastructure.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public void SaveProgress();

        public PlayerProgress LoadProgress();
    }
}