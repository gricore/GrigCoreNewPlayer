using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Model
{
    public class NowPlayingModel : INotifyPropertyChanged
    {
        private string _clipartUrl;
        /// <summary>
        /// Bind Clip Art
        /// </summary>
        public string ClipArtUrl
        {
            get { return _clipartUrl; }
            set
            {
                if (value != _clipartUrl)
                {
                    _clipartUrl = value;
                    OnPropertyChanged("ClipArtUrl");
                }
            }
        }


        private double _position;

        public double Position
        {
            get { return _position; }
            set
            {
                if (!Equals(value, _position))
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        private double _currentWidth;

        public double CurrentWidth
        {
            get { return _currentWidth; }
            set
            {
                if (!Equals(_currentWidth, value))
                {
                    _currentWidth = value;
                    OnPropertyChanged("CurrentWidth");
                }
            }
        }

        private string _track = string.Empty;

        public string Track
        {
            get { return _track; }
            set
            {
                if (_track != value)
                {
                    _track = value;
                    OnPropertyChanged("Track");
                }
            }
        }


        public int Duration { get; set; }

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
