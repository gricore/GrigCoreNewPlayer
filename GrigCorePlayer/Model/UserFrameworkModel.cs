using System.ComponentModel;
using GrigCorePlayer.Annotations;
using System.Windows;

namespace GrigCorePlayer.Model
{
    public class UserFrameworkModel : INotifyPropertyChanged
    {
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


        private FrameworkElement _userInfoContent = new FrameworkElement();

        public FrameworkElement UserInfoContent
        {
            get { return _userInfoContent; }
            set
            {
                if (!Equals(_userInfoContent, value))
                {
                    _userInfoContent = value;
                    OnPropertyChanged("UserInfoContent");
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
