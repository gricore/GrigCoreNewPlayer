using GrigCorePlayer.Annotations;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrigCorePlayer.Model;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using GrigCorePlayer.Services;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using GrigCorePlayer.Views.NowPlaying;

namespace GrigCorePlayer.Controllers
{
    [UsedImplicitly]
    public class PlayerController : IControllerBase
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IVkService _vkService;
        private readonly IUnityContainer _container;
        private readonly IAsyncService _asyncService;
        private readonly ILastFmService _lastFmService;

        private TrackModel _trackModel;
        #endregion

        #region Commands
        public ICommand MediaEndedCommand { get; set; }
        #endregion

        #region Models

        private PlayerModel _model = new PlayerModel();

        public PlayerModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        #endregion

        #region Ctor

        public PlayerController(IEventAggregator eventAggregator, IVkService vkService,
            IUnityContainer container, IAsyncService asyncService, ILastFmService lastFmService)
        {
            _eventAggregator = eventAggregator;
            _vkService = vkService;
            _container = container;
            _asyncService = asyncService;
            _lastFmService = lastFmService;
            OnPlayerCtor();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Do on start-up.
        /// </summary>
        private void OnPlayerCtor()
        {
            _eventAggregator.GetEvent<PlayTrackEvent>().Subscribe(OnTrackPlay);
            _eventAggregator.GetEvent<PlayerCommandEvent>().Subscribe(OnPlayerCommand);

            MediaEndedCommand = new DelegateCommand(() =>
                OnPlayerCommand(new MediaModel { PlayerCommand = PlayerCommand.Forward }));

        }

        /// <summary>
        /// Do Player command
        /// </summary>
        /// <param name="obj"></param>
        private void OnPlayerCommand(MediaModel obj)
        {
            if (obj.PlayerCommand == PlayerCommand.Forward)
            {
                if (_trackModel.TrackIndex < _trackModel.TrackList.Count - 1)
                {
                    _trackModel.TrackIndex++;
                    _eventAggregator.GetEvent<PlayTrackEvent>().Publish(_trackModel);
                }
            }
        }

        /// <summary>
        /// Play selected track
        /// </summary>
        /// <param name="obj"></param>
        private void OnTrackPlay(TrackModel obj)
        {
            _trackModel = obj;
            string trackurl = string.Empty;
            var trackArtist = obj.TrackList[obj.TrackIndex].Artist;
            var trackName = obj.TrackList[obj.TrackIndex].Track;
            var track = trackArtist + " - " + trackName;


            _asyncService.RunAsync(null, () =>
            {
                trackurl = _vkService.GetUrlByTrackAndArtist(track);
                _lastFmService.ScrobbleTrack(trackArtist, trackName);
            }, () =>
                { Model.Mp3Url = trackurl; });


            _eventAggregator.GetEvent<NowPlayingUpdateEvent>().Publish(new NowPlayingModel
            {
                Track = track
            });

            //Model.Mp3Url = _vkService.GetUrlByTrackAndArtist(track);
            _eventAggregator.GetEvent<PlayerCommandEvent>().Publish(new MediaModel { PlayerCommand = PlayerCommand.Play });
        }

        #endregion
    }
}
