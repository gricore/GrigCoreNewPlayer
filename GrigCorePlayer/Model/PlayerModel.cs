using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Model
{

    public sealed class PlayerModel : INotifyPropertyChanged
    {
        private string _mp3url = string.Empty;
        /// <summary>
        /// Gets or sets mp3 url.
        /// </summary>
        public string Mp3Url
        {
            get { return _mp3url; }
            set
            {
                if (_mp3url != value)
                {
                    _mp3url = value;
                    OnPropertyChanged("Mp3Url");
                }
            }
        }

        private int _currentIndex;

        public int CurrnetIndex
        {
            get { return _currentIndex; }
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    OnPropertyChanged("CurrnetIndex");
                }
            }
        }

        private TimeSpan _position;

        public TimeSpan Position
        {
            get { return _position; }
            set
            {
                if (Equals(_position, value))
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }
        }
        



        #region Notify Property Changed
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
