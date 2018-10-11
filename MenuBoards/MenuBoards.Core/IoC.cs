using System.Configuration;
using MenuBoards.DataAccess;
using MenuBoards.Interfaces.DataAccess;
using MenuBoards.Interfaces.Web;
using MenuBoards.Services;
using Microsoft.Practices.Unity.Configuration;
using Unity;
using Unity.Lifetime;

namespace MenuBoards.Core
{
    public sealed class IoC
    {
        private static IoC instance = null;
        private static readonly object padlock = new object();

        private readonly IUnityContainer container;

        private IoC()
        {
            container = new UnityContainer();

            container.RegisterType<ILoginService, LoginService>(new SingletonLifetimeManager());
            container.RegisterType<IAccountService, AccountService>(new SingletonLifetimeManager());
            container.RegisterType<ISessionService, SessionService>(new SingletonLifetimeManager());
            container.RegisterType<IUserStateService, UserStateService>(new SingletonLifetimeManager());


            container.RegisterType<ISlideService, SlideService>(new SingletonLifetimeManager());
            container.RegisterType<IMenuService, MenuService>(new SingletonLifetimeManager());
            container.RegisterType<IMenuItemService, MenuItemService>(new SingletonLifetimeManager());

            container.RegisterType<IDisplayService, DisplayService>(new SingletonLifetimeManager());
            container.RegisterType<IDesignSettingsService, DesignSettingsService>(new SingletonLifetimeManager());
            container.RegisterType<IDisplaySettingsService, DisplaySettingsService>(new SingletonLifetimeManager());
            container.RegisterType<IGlobalSettingsService, GlobalSettingsService>(new SingletonLifetimeManager());

            container.RegisterType<ISettingsRepository, SettingsRepository>(new SingletonLifetimeManager());
            container.RegisterType<ISlideRepository, SlideRepository>(new SingletonLifetimeManager());
            container.RegisterType<IMenuRepository, MenuRepository>(new SingletonLifetimeManager());
            container.RegisterType<IMenuItemRepository, MenuItemRepository>(new SingletonLifetimeManager());
            container.RegisterType<IGlobalSettingsRepository, GlobalSettingsRepository>(new SingletonLifetimeManager());

            container.RegisterType<IAccountRepository, AccountRepository>(new SingletonLifetimeManager());


            container.RegisterType<ITimeStampRepository, TimeStampRepository>(new SingletonLifetimeManager());
        }

        public static IoC Container
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new IoC();
                    }
                }
                return instance;
            }
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}