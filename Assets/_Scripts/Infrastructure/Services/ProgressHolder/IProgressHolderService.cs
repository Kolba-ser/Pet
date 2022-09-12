using Pet.Data;

namespace Pet.Infrastructure.Services.Progress
{
    public interface IProgressHolderService : IService
    {
        PlayerProgress Progress
        {
            get;
            set;
        }
    }
}