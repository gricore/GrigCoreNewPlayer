using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Views.Home;
using GrigCorePlayer.Views.NowPlaying;
using GrigCorePlayer.Views.Player;
using GrigCorePlayer.Views.Shell;
using GrigCorePlayer.Views.Station;
using GrigCorePlayer.Views.User;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace GrigCorePlayer.Modules
{
    public class RootModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public RootModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _regionManager.Regions["NavigationContent"].RequestNavigate(typeof(StationView).FullName);
            _regionManager.Regions["NavigationContent"].RequestNavigate(typeof(NowPlayingView).FullName);
            _regionManager.Regions["NavigationContent"].RequestNavigate(typeof(UserView).FullName);
            _regionManager.Regions["NavigationContent"].RequestNavigate(typeof(ArtistView).FullName);
            _regionManager.Regions["NavigationContent"].RequestNavigate(typeof(HomeView).FullName);

            _container.Resolve<_BasicInfoView>();            

            _regionManager.RegisterViewWithRegion("LeftPanelContent", typeof(_LeftPanelView));
            _regionManager.RegisterViewWithRegion("MenuContent", typeof(_Menu));
            _regionManager.RegisterViewWithRegion("PlayerContent", typeof(PlayerView));
        }
    }
}
