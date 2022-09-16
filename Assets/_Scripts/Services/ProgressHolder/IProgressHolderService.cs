using Pet.Data;

namespace Pet.Service.Progress
{
    public interface IProgressHolderService : IService
    {
        PlayerProgress Progress
        {
            get;
            set;
        }

        public void NewProgress();
    }
}