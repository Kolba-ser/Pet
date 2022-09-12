using Pet.Infrastructure.Services;

namespace Pet.UI.Services
{
    public interface IWindowService : IService
    {
        void Open(WindowType windowId);
    }
}