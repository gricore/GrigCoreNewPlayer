using System.Windows;
using GrigCorePlayer.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using GrigCorePlayer.Views.Shell;
using Microsoft.Practices.Unity;
using GrigCorePlayer.Views.Home;
using GrigCorePlayer.Modules;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Views.User;
using GrigCorePlayer.Interfaces;
using GrigCorePlayer.Controllers;
using GrigCorePlayer.Services;
using System.Windows.Input;
using GrigCorePlayer.Views.NowPlaying;
using GrigCorePlayer.Views.Station;
using GrigCorePlayer.Commands.Utilities;

namespace GrigCorePlayer
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var shellWindow = new ShellWindow();
            shellWindow.Show();
            return shellWindow;
        }

        protected override IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IAsyncService, AsyncService>();
            container.RegisterType<IFrameNavigationService, FrameNavigationService>();
            container.RegisterType<ILastFmService, LastFmService>();
            container.RegisterType<IDataService, DataService>();
            container.RegisterType<ITextParser, TextParser>();
            container.RegisterType<IVkService, VkService>();

            container.RegisterType<ICommand, SearchBoxKeyDownCommandT>();
            container.RegisterType<IActionBase, InfoUpdateAction>();

            container.RegisterType<IFrameworkInputElement, _BasicInfoView>();
            container.RegisterType<IFrameworkInputElement, _AlbumsView>();
            container.RegisterType<IFrameworkInputElement, _SimilarView>();
            container.RegisterType<IFrameworkInputElement, _TracksView>();
            container.RegisterType<IFrameworkInputElement, _TagsView>();

            container.RegisterType<IControllerBase, ShellController>();
            container.RegisterType<IControllerBase, UserController>();
            container.RegisterType<IControllerBase, ArtistController>();
            container.RegisterType<IControllerBase, HomeController>();
            container.RegisterType<IControllerBase, PlayerController>();
            container.RegisterType<IControllerBase, NowPlayingController>();
            container.RegisterType<IControllerBase, StationController>();

            container.RegisterType(typeof(Object), typeof(HomeView), typeof(HomeView).FullName);
            container.RegisterType(typeof(Object), typeof(ArtistView), typeof(ArtistView).FullName);
            container.RegisterType(typeof(Object), typeof(UserView), typeof(UserView).FullName);
            container.RegisterType(typeof(Object), typeof(NowPlayingView), typeof(NowPlayingView).FullName);
            container.RegisterType(typeof(Object), typeof(StationView), typeof(StationView).FullName);

            return container;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();

            catalog.AddModule(typeof(RootModule));

            return catalog;
        }

    }
}
