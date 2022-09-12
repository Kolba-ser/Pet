using Pet.Data;

namespace Pet.Infrastructure.Services.Progress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }
}