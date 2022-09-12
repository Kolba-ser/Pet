using Pet.Data;

namespace Pet.Infrastructure.Services.Progress
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}