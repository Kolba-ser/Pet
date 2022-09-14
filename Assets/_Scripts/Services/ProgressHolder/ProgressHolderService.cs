using Pet.Data;

namespace Pet.Services.Progress
{
    public class ProgressHolderService : IProgressHolderService
    {
        public PlayerProgress Progress
        {
            get; set;
        }
    }
}