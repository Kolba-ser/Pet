using Pet.Data;

namespace Pet.Services.Progress
{
    public interface ISavedProgressReader
    {
        public void Load(PlayerProgress progress);
    }
}