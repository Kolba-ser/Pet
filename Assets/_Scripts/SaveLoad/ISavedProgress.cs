using Pet.Data;

namespace Pet.Services.Progress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void Save(PlayerProgress progress);
    }
}