namespace Pet.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory) =>
            _uiFactory = uiFactory;

        public void Open(WindowType windowId)
        {
            switch (windowId)
            {
                case WindowType.None:
                    break;

                case WindowType.Shop:
                    _uiFactory.CreateShop();
                    break;

                default:
                    break;
            }
        }
    }
}