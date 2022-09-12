using Pet.Data;

namespace Pet.Infrastructure.Services.Progress
{
    public class ProgressHolderService : IProgressHolderService
    {
        public PlayerProgress Progress
        {
            get; set;
        }
    }
}