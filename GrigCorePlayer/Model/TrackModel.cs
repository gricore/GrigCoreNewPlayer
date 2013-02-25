using System.ComponentModel;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomItems;

namespace GrigCorePlayer.Model
{
    public class TrackModel : INotifyPropertyChanged
    {
        private int _trackIndex;

        public int TrackIndex
        {
            get { return _trackIndex; }
            set
            {
                if (_trackIndex != value)
                {
                    _trackIndex = value;
                    OnPropertyChanged("TrackIndex");
                }
            }
        }

        private TrackListBoxItemCollection _trackList = new TrackListBoxItemCollection();

        public TrackListBoxItemCollection TrackList
        {
            get { return _trackList; }
            set
            {
                if (_trackList != value)
                {
                    _trackList = value;
                    OnPropertyChanged("TrackList");
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
