using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GrigCorePlayer.Annotations;
using System.Windows;

namespace GrigCorePlayer.Model
{
    public class StationFrameworkModel : INotifyPropertyChanged
    {
        private FrameworkElement _trackContent = new FrameworkElement();

        public FrameworkElement TrackContent
        {
            get { return _trackContent; }
            set
            {
                if (!Equals(_trackContent, value))
                {
                    _trackContent = value;
                    OnPropertyChanged("TrackContent");
                }
            }
        }


        private FrameworkElement _artistsContent = new FrameworkElement();

        public FrameworkElement ArtistsContent
        {
            get { return _artistsContent; }
            set
            {
                if (!Equals(_artistsContent, value))
                {
                    _artistsContent = value;
                    OnPropertyChanged("ArtistsContent");
                }
            }
        }


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
