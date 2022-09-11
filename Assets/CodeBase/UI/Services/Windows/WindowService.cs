namespace CodeBase.UI.Services
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {

            switch (windowId)
            {
                case WindowId.None:
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
                default:
                    break;
            }
        }
    }
}
