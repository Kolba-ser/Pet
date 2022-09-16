using Pet.Data;

namespace Pet.Service.Progress
{
    public interface ISavedProgressReader
    {
        public void Load(PlayerProgress progress);
    }
}