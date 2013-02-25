using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Model
{
    public class ArtistFrameworkModel : INotifyPropertyChanged
    {
        private FrameworkElement _artistBasicInfoContent = new FrameworkElement();

        public FrameworkElement ArtistBasicInfoContent
        {
            get { return _artistBasicInfoContent; }
            set
            {
                if (!Equals(value, _artistBasicInfoContent))
                {
                    _artistBasicInfoContent = value;
                    OnPropertyChanged("ArtistBasicInfoContent");
                }
            }
        }

        private FrameworkElement _artistAlbumsContent = new FrameworkElement();

        public FrameworkElement ArtistAlbumsContent
        {
            get { return _artistAlbumsContent; }
            set
            {
                if (!Equals(_artistAlbumsContent, value))
                {
                    _artistAlbumsContent = value;
                    OnPropertyChanged("ArtistAlbumsContent");
                }
            }
        }

        private FrameworkElement _tagsContent = new FrameworkElement();

        public FrameworkElement TagsContent
        {
            get { return _tagsContent; }
            set
            {
                if (!Equals(_tagsContent, value))
                {
                    _tagsContent = value;
                    OnPropertyChanged("TagsContent");
                }
            }
        }


        private FrameworkElement _similarContent = new FrameworkElement();

        public FrameworkElement SimilarContent
        {
            get { return _similarContent; }
            set
            {
                if (!Equals(_similarContent, value))
                {
                    _similarContent = value;
                    OnPropertyChanged("SimilarContent");
                }
            }
        }

        private FrameworkElement _tracksContent = new FrameworkElement();

        public FrameworkElement TracksContent
        {
            get { return _tracksContent; }
            set
            {
                if (!Equals(_tracksContent, value))
                {
                    _tracksContent = value;
                    OnPropertyChanged("TracksContent");
                }
            }
        }


        #region NorityPropertyChanged
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
