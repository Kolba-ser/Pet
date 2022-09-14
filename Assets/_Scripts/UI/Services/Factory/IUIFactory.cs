using Pet.Services;
using System.Threading.Tasks;

namespace Pet.UI.Services
{
    public interface IUIFactory : IService
    {
        public void CreateShop();

        Task CreateUIRootAsync();
    }
}