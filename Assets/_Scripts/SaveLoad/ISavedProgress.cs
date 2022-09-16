using Pet.Data;

namespace Pet.Service.Progress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void Save(PlayerProgress progress);
    }
}