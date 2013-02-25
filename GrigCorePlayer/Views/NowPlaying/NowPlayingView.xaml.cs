using GrigCorePlayer.Controllers;
using GrigCorePlayer.EventAggregators;
using GrigCorePlayer.Model;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrigCorePlayer.Views.NowPlaying
{
    /// <summary>
    /// Interaction logic for NowPlayingView.xaml
    /// </summary>
    public partial class NowPlayingView : Page
    {
        private readonly IEventAggregator _eventAggregator;
        private double duration;

        public NowPlayingView(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();

            VolumeSlider.Value = 12.5;
            DataContext = container.Resolve<NowPlayingController>();

            _eventAggregator.GetEvent<UpdateMetaInfoEvent>().Subscribe(OnMediaOpened);
        }

        private void OnMediaOpened(MediaMetaDataModel obj)
        {
            if (obj.MediaEventType == EventType.MediaOpened)
            {
                // NOTE: Here i get media duration
                duration = obj.Duration;
            }

            if (obj.MediaEventType == EventType.MediaPlaying)
            {
                // NOTE: Here i update border layout

                // Get border with  
                if (duration > 0)
                    DBorder.Width = (obj.Position / duration) * DGrid.ActualWidth;
                else
                {
                    DBorder.Width = 0;
                }

            }
        }

        private void ChangePosition_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var x = e.GetPosition(this.DGrid).X;
            // x = currenttime
            // lenght = duration
            // cuttenttime = (x*duration)/length
            var currentTime = (x*duration)/this.DGrid.ActualWidth;

            _eventAggregator.GetEvent<PlayerCommandEvent>().Publish(new MediaModel
                {
                    PlayerCommand = PlayerCommand.PositionChange,
                    Position = currentTime
                });
        }
       
    }
}
