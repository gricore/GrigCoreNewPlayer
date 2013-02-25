using System.Windows;
using System.Windows.Input;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomItems;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Interfaces;
using GrigCorePlayer.Model;
using GrigCorePlayer.Services;
using GrigCorePlayer.Views.Extentions;
using GrigCorePlayer.Views.Station;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Commands.Utilities;

namespace GrigCorePlayer.Controllers
{
    [UsedImplicitly]
    public class StationController : IControllerBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILastFmService _lastFmService;
        private readonly IAsyncService _asyncService;
        private readonly IUnityContainer _container;
        private readonly IFrameNavigationService _navigationService;

        #region Ctor

        public StationController(IEventAggregator eventAggregator, ILastFmService lastFmService,
            IAsyncService asyncService, IUnityContainer container, IFrameNavigationService navigationService)
        {
            _eventAggregator = eventAggregator;
            _lastFmService = lastFmService;
            _asyncService = asyncService;
            _container = container;
            _navigationService = navigationService;
            OnStationLoaded();
        }

        #endregion

        #region Model

        private StationModel _model = new StationModel();
        /// <summary>
        /// Bind Station Model
        /// </summary>
        public StationModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        private StationFrameworkModel _frameworkModel = new StationFrameworkModel();
        /// <summary>
        /// Bind framework model
        /// </summary>
        public StationFrameworkModel FrameworkModel
        {
            get { return _frameworkModel; }
            set { _frameworkModel = value; }
        }


        #endregion

        #region EventHandler

        private void OnStationLoaded()
        {
            // Subscribe to station changed event.
            _eventAggregator.GetEvent<StationUpdateEvent>().Subscribe(OnStationUpdated);
        }
        #endregion

        #region Methods

        /// <summary>
        /// On Station Updated
        /// </summary>
        /// <param name="obj"></param>
        private void OnStationUpdated(Model.StationModel obj)
        {
            TagTracksAction(obj);
        }


        private void TagTracksAction(StationModel model)
        {
            var tracks = new TrackListBoxItemCollection();

            _asyncService.RunAsync(() => { FrameworkModel.TrackContent = new LoadingView(); },
                () =>
                {
                    try
                    {
                        tracks = _lastFmService.GetTagTopTrackByTagName(model.TagName).Clone() as TrackListBoxItemCollection;
                    }
                    catch { }
                }, () =>
                {
                    var tagTracks = _container.Resolve<_StationTracksView>();
                    tagTracks.DataContext = this;
                    _container.Resolve<InfoUpdateAction>().PlayTrackSupport(tagTracks.TopTracksListBox);
                    FrameworkModel.TrackContent = tagTracks;

                    if (tracks.Count == 0)
                    {
                        FrameworkModel.TrackContent = new FrameworkElement();
                        return;
                    }

                    Model.Tracks = tracks;
                    TagArtistsAction(model);
                    //if (tracks.Count > 0)
                    //    _asyncService.RunAsyncListAdding(tracks.Count,
                    //        index => Model.Tracks.Add(tracks[index]), 15);
                });
        }

        private void TagArtistsAction(StationModel model)
        {
            var artists = new TilesListBoxItemSources();

            _asyncService.RunAsync(() => { FrameworkModel.ArtistsContent = new LoadingView(); },
                () =>
                {
                    try
                    {
                        artists = _lastFmService.GetTagTopArtistsByTagName(model.TagName);
                    }
                    catch { }
                }, () =>
                {
                    var tagArtists = _container.Resolve<_StationTopArtists>();
                    tagArtists.DataContext = this;
                    _container.Resolve<InfoUpdateAction>().UpdateArtistSupport(tagArtists.TopArtistsListBox);
                    FrameworkModel.ArtistsContent = tagArtists;

                    //if (artists.Count == 0)
                    //{
                    //    FrameworkModel.ArtistsContent = new FrameworkElement();
                    //    return;
                    //}

                    Model.Artists = artists;
                    //if (artists.Count > 0)
                    //    _asyncService.RunAsyncListAdding(tracks.Count,
                    //        index => Model.Tracks.Add(tracks[index]), 15);
                });
        }

        #endregion
    }
}
