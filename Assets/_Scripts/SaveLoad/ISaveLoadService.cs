using Pet.Data;
using Pet.Service;

namespace Pet.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public void SaveProgress();

        public PlayerProgress LoadProgress();
    }
}