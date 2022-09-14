using Pet.Data;

namespace Pet.Services.Progress
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