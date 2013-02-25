using System.Windows;
using GrigCorePlayer.Controls.CustomItems;
using GrigCorePlayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrigCorePlayer.Model;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Views.Extentions;
using Microsoft.Practices.Prism.Events;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Services;
using Microsoft.Practices.Unity;
using GrigCorePlayer.Views.User;
using GrigCorePlayer.Commands.Utilities;

namespace GrigCorePlayer.Controllers
{
    public class UserController : IControllerBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILastFmService _lastFmService;
        private readonly IAsyncService _asyncService;
        private readonly IUnityContainer _container;

        #region Models

        private UserModel _model = new UserModel();
        /// <summary>
        /// Bind user model
        /// </summary>
        public UserModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        private UserFrameworkModel _frameworkModel = new UserFrameworkModel();
        /// <summary>
        /// Bind framework model
        /// </summary>
        public UserFrameworkModel FrameworkModel
        {
            get { return _frameworkModel; }
            set { _frameworkModel = value; }
        }


        #endregion

        #region Ctor

        public UserController(IEventAggregator eventAggregator, ILastFmService lastFmService,
            IAsyncService asyncService, IUnityContainer container)
        {
            _eventAggregator = eventAggregator;
            _lastFmService = lastFmService;
            _asyncService = asyncService;
            _container = container;
            OnUserLoaded();
        }

        #endregion

        #region Methods

        private void OnUserLoaded()
        {
            _eventAggregator.GetEvent<UserUpdateEvent>().Subscribe(OnUserUpdateAction);
        }

        private void OnUserUpdateAction(UserModel obj)
        {
            UserTopArtistsAction();
        }

        private void UserTopArtistsAction()
        {
            //Model.Similar.Clear();
            var topArtist = new TilesListBoxItemSources();

            _asyncService.RunAsync(() => { FrameworkModel.ArtistsContent = new LoadingView(); },
                () =>
                {
                    try
                    {
                        topArtist = _lastFmService.GetUserTopArtistsByUsername();
                    }
                    catch { }
                }, () =>
                {
                    var userArtists = _container.Resolve<_UserTopArtistsView>();
                    userArtists.DataContext = this;
                    // Add support. On Artist Selected go to artist.
                    _container.Resolve<InfoUpdateAction>().UpdateArtistSupport(userArtists.TopArtistsListBox);

                    FrameworkModel.ArtistsContent = userArtists;

                    Model.TopArtists = topArtist;
                    UserTopTracksAction();
                });
        }


        private void UserTopTracksAction()
        {
            //Model.Similar.Clear();
            var topTracks = new TrackListBoxItemCollection();

            _asyncService.RunAsync(() => { FrameworkModel.TrackContent = new LoadingView(); },
                () =>
                {
                    try
                    {
                        topTracks = _lastFmService.GetUserTopTracks();
                    }
                    catch { }
                }, () =>
                {
                    var userTracks = _container.Resolve<_UserTopTracksView>();
                    userTracks.DataContext = this;
                    // Add support. On Artist Selected go to artist.
                    _container.Resolve<InfoUpdateAction>().PlayTrackSupport(userTracks.TopTracksListBox);

                    FrameworkModel.TrackContent = userTracks;

                    Model.TopTracks = topTracks;
                });
        }

        #endregion
    }
}
