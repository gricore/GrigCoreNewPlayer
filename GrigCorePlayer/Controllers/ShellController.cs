using System;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Interfaces;
using System.Windows.Input;
using GrigCorePlayer.Model;
using Microsoft.Practices.Prism.Commands;
using GrigCorePlayer.Views.Home;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Views.User;
using GrigCorePlayer.Services;
using GrigCorePlayer.Views.NowPlaying;
using GrigCorePlayer.Views.Station;
using Microsoft.Practices.Prism.Events;

namespace GrigCorePlayer.Controllers
{
    [UsedImplicitly]
    public class ShellController : IControllerBase
    {
        #region Fields

        private readonly IFrameNavigationService _navigationService;
        private readonly ILastFmService _lastFmService;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Commands
        public ICommand NavigateToArtistCommand { get; set; }
        public ICommand NavigateToHomeCommand { get; set; }
        public ICommand NavigateToUserCommand { get; set; }
        public ICommand NavigateToNowPlayingCommand { get; set; }
        public ICommand NavigateToStationCommand { get; set; }

        public ICommand LogoutCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion

        #region Ctor

        public ShellController(IFrameNavigationService navigationService, ILastFmService lastFmService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _lastFmService = lastFmService;
            _eventAggregator = eventAggregator;

            NavigateToHomeCommand = new DelegateCommand(() => _navigationService.NavigateToView<HomeView>());
            NavigateToArtistCommand = new DelegateCommand(() => _navigationService.NavigateToView<ArtistView>());
            NavigateToUserCommand = new DelegateCommand(() => _navigationService.NavigateToView<UserView>());
            NavigateToNowPlayingCommand = new DelegateCommand(() => _navigationService.NavigateToView<NowPlayingView>());
            NavigateToStationCommand = new DelegateCommand(() => _navigationService.NavigateToView<StationView>());

            MenuCommands();
        }

        #endregion

        #region Handlers

        private void MenuCommands()
        {
            LogoutCommand = new DelegateCommand(OnLogout);
            ExitCommand = new DelegateCommand(OnExit);
        }

        private void OnExit()
        {
            Environment.Exit(0);
        }

        private void OnLogout()
        {
            _eventAggregator.GetEvent<LoginChangeEvent>().Publish(new LoginModel { LoginCommand = LoginCommandType.Logout });
        }

        #endregion
    }
}
