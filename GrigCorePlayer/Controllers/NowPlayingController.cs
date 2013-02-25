using System.Windows.Input;
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

namespace GrigCorePlayer.Controllers
{
    [UsedImplicitly]
    public class NowPlayingController : IControllerBase
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Commands

        public ICommand ValueChangedCommand { get; set; }
        public ICommand PlayForwardCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand PlayCommand { get; set; }

        #endregion

        #region Model

        private NowPlayingModel _model = new NowPlayingModel();

        public NowPlayingModel Model
        {
            get { return _model; }
            set { _model = value; }
        }


        #endregion

        #region Ctor

        public NowPlayingController(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Volume change
            ValueChangedCommand = new ActionCommand(OnVolumeValueChanged);

            // Stop
            StopCommand = new DelegateCommand(() => _eventAggregator.GetEvent<PlayerCommandEvent>().
                Publish(new MediaModel { PlayerCommand = PlayerCommand.Stop }));

            // Pause
            PauseCommand = new DelegateCommand(() => _eventAggregator.GetEvent<PlayerCommandEvent>().
                Publish(new MediaModel { PlayerCommand = PlayerCommand.Pause }));

            // Play
            PlayCommand = new DelegateCommand(() => _eventAggregator.GetEvent<PlayerCommandEvent>().
                Publish(new MediaModel { PlayerCommand = PlayerCommand.Play }));

            // Play with forward
            PlayForwardCommand = new DelegateCommand(() => _eventAggregator.GetEvent<PlayerCommandEvent>().
                Publish(new MediaModel { PlayerCommand = PlayerCommand.Forward }));

            _eventAggregator.GetEvent<NowPlayingUpdateEvent>().Subscribe(OnNowPlayingChanged);

        }



        #endregion

        #region Methods


        private void OnVolumeValueChanged(object obj)
        {
            if (obj == null)
                return;

            var valueDouble = (double)obj;
            _eventAggregator.GetEvent<PlayerCommandEvent>().Publish(new MediaModel
                {
                    Volume = valueDouble / 25,
                    PlayerCommand = PlayerCommand.VolumeChange
                });
        }

        private void OnNowPlayingChanged(NowPlayingModel obj)
        {
            Model.ClipArtUrl = obj.ClipArtUrl;
            Model.Track = obj.Track;
        }

        #endregion
    }
}
