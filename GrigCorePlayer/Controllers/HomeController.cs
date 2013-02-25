using System;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Commands;
using GrigCorePlayer.Interfaces;
using System.Windows.Controls;
using System.Windows.Input;
using GrigCorePlayer.Model;
using GrigCorePlayer.Views.Extentions;
using GrigCorePlayer.Views.Home;
using Microsoft.Practices.Prism.Commands;
using System.Threading;
using System.ComponentModel;
using GrigCorePlayer.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Views.Station;
using GrigCorePlayer.Views.User;

namespace GrigCorePlayer.Controllers
{
    [UsedImplicitly]
    public partial class HomeController : IControllerBase, INotifyPropertyChanged
    {
        #region Fields

        private readonly IAsyncService _asyncService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _container;
        private readonly IFrameNavigationService _navigationService;
        private readonly ILastFmService _lastFmService;
        private readonly IDataService _dataService;

        #endregion

        #region Commands

        public ICommand SearchBoxKeyDownCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Models

        private HomeModel _model = new HomeModel();
        /// <summary>
        /// Gets or sets home model
        /// </summary>
        public HomeModel Model
        {
            get { return _model; }
            set { _model = value; }
        }


        private HomeFrameworkModel _frameworkModel = new HomeFrameworkModel();
        /// <summary>
        /// Gets or sets FrameworkModel
        /// </summary>
        public HomeFrameworkModel FrameworkModel
        {
            get { return _frameworkModel; }
            set { _frameworkModel = value; }
        }

        #endregion

        #region Ctor

        public HomeController(IAsyncService asyncService, IEventAggregator eventAggregator,
            IUnityContainer container, IFrameNavigationService navigationService, ILastFmService lastFmService,
            IDataService dataService)
        {
            _asyncService = asyncService;
            _eventAggregator = eventAggregator;
            _container = container;
            _navigationService = navigationService;
            _lastFmService = lastFmService;
            _dataService = dataService;

            OnCtorLoading();
        }

        #endregion

        #region Methods

        private void OnCtorLoading()
        {
            PrepareLoginPanel();
            RealizeLoginCommand();

            _eventAggregator.GetEvent<LoginChangeEvent>().Subscribe(OnLoginChangeAction);

            Model.ArtistChecked = true;
            // SearchBoxKeyDownCommand = _container.Resolve<SearchBoxKeyDownCommandT>();
            SearchBoxKeyDownCommand = new DelegateCommand(() =>
            {
                if (Keyboard.PrimaryDevice.IsKeyDown(Key.Enter))
                {
                    ArtistModel artistModel = new ArtistModel();
                    artistModel.Name = Model.SearchBoxText;

                    if (Model.ArtistChecked)
                    {
                        _eventAggregator.GetEvent<ArtistSelectedEvent>().Publish(artistModel.Clone() as ArtistModel);
                        _navigationService.NavigateToView<ArtistView>();
                    }

                    if (Model.TagChecked)
                    {
                        _eventAggregator.GetEvent<StationUpdateEvent>().Publish(new StationModel { TagName = Model.SearchBoxText });
                        _navigationService.NavigateToView<StationView>();
                    }
                }
            });
        }

        private void OnLoginChangeAction(LoginModel obj)
        {
            switch (obj.LoginCommand)
            {
                case LoginCommandType.Logout:
                    Logout();
                    break;
            }
        }

        /// <summary>
        /// Prepare Login Panel
        /// </summary>
        private void PrepareLoginPanel()
        {
            var loginContent = new _LoginView();
            loginContent.DataContext = this;
            FrameworkModel.LoginContent = loginContent;
            OnStartUpLogin();
        }

        private void RealizeLoginCommand()
        {
            // TODO: Realize Login Command
            LoginCommand = new DelegateCommand(LoginCommandPart);

        }

        private void LoginCommandPart()
        {
            try
            {
                if (!_lastFmService.HasLfmSession())
                {
                    var loginModel = _lastFmService.LfmLoginRequest();
                    Model.LoginText = loginModel.LoginText;
                    Model.LoginPicture = loginModel.LoginPicture;
                    if (string.IsNullOrWhiteSpace(Model.LoginText))
                        _dataService.ClearLfmSessionDataFromSettings();
                }
                else
                {
                    // On login pressed go to user page
                    _eventAggregator.GetEvent<UserUpdateEvent>().Publish(new UserModel());
                    _navigationService.NavigateToView<UserView>();
                }
            }
            catch (Exception)
            {
                Model.LoginText = "Login Error. Try again";
                _dataService.ClearLfmSessionDataFromSettings();
            }
        }

        private void Logout()
        {
            _dataService.ClearLfmSessionDataFromSettings();
            OnStartUpLogin();
        }

        private void OnStartUpLogin()
        {
            try
            {
                if (_lastFmService.HasLfmSession())
                {
                    var loginModel = _lastFmService.GetLfmUserData();
                    Model.LoginPicture = loginModel.LoginPicture;
                    Model.LoginText = loginModel.LoginText;

                }
                else
                {
                    Model.LoginText = "Login";
                    Model.LoginPicture = string.Empty;
                }
            }
            catch (Exception)
            {
                Model.LoginText = "Login Error. Try again";
                _dataService.ClearLfmSessionDataFromSettings();
            }
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
