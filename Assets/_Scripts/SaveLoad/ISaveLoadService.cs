using Pet.Data;
using Pet.Services;

namespace Pet.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public void SaveProgress();

        public PlayerProgress LoadProgress();
    }
}