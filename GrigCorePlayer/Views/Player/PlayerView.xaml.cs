using System;
using GrigCorePlayer.Controllers;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Model;
using GrigCorePlayer.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System.Windows;

namespace GrigCorePlayer.Views.Player
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView
    {
        #region Fields

        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IAsyncService _asyncService;

        #endregion

        #region Ctor

        public PlayerView(IUnityContainer container, IEventAggregator eventAggregator, IAsyncService asyncService)
        {
            // Services
            _container = container;
            _eventAggregator = eventAggregator;
            _asyncService = asyncService;

            InitializeComponent();

            // Set Default Volume
            MediaPlayer.Volume = 0.5;
            // Set DataContext
            DataContext = _container.Resolve<PlayerController>();
            // Set OnPlayCommanded subscribe
            _eventAggregator.GetEvent<PlayerCommandEvent>().Subscribe(OnPlayCommanded);
            // Set On View/MediaElement loaded event handler
            Loaded += PlayViewLoaded;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// On current view loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayViewLoaded(object sender, RoutedEventArgs e)
        {
            _asyncService.RunAsyncForever(() =>
            {
                // Check media player access.
                if (!MediaPlayer.CheckAccess())
                    return;
                // Get current position in seconds (double).
                var position = MediaPlayer.Position.TotalSeconds;
                _eventAggregator.GetEvent<UpdateMetaInfoEvent>().Publish(new MediaMetaDataModel
                {
                    MediaEventType = EventType.MediaPlaying,
                    Position = position
                });
            }
               , 500);
        }

        /// <summary>
        /// On Play Command. Sended play command to play, stop, pause track
        /// </summary>
        /// <param name="obj"></param>
        private void OnPlayCommanded(MediaModel obj)
        {
            if (obj.PlayerCommand == PlayerCommand.Play)
                MediaPlayer.Play();
            if (obj.PlayerCommand == PlayerCommand.VolumeChange)
                MediaPlayer.Volume = obj.Volume;
            if (obj.PlayerCommand == PlayerCommand.Pause)
                MediaPlayer.Pause();
            if (obj.PlayerCommand == PlayerCommand.Stop)
                MediaPlayer.Stop();
            if (obj.PlayerCommand == PlayerCommand.PositionChange)
            {
                this.MediaPlayer.Position = new TimeSpan(0, 0, (int)obj.Position);
            }
        }

        /// <summary>
        /// Media Opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMediaOpened(object sender, RoutedEventArgs e)
        {
            var duration = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            _eventAggregator.GetEvent<UpdateMetaInfoEvent>().Publish(new MediaMetaDataModel
                {
                    Duration = duration,
                    MediaEventType = EventType.MediaOpened
                });
        }

        #endregion
    }
}
