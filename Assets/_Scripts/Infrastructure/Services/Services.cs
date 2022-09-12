namespace Pet.Infrastructure.Services
{
    public class Services
    {
        private static Services _instance;
        public static Services Container => _instance ?? (_instance = new Services());

        public void RegisterSingle<TService>(TService implemantation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implemantation;

        public TService Single<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}