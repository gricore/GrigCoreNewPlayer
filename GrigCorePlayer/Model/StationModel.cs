using System.ComponentModel;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomItems;

namespace GrigCorePlayer.Model
{
    public class StationModel : INotifyPropertyChanged
    {
        private string _tagName = string.Empty;
        /// <summary>
        /// Bind tag name
        /// </summary>
        public string TagName
        {
            get { return _tagName; }
            set
            {
                if (_tagName != value)
                {
                    _tagName = value;
                    OnPropertyChanged("TagName");
                }
            }
        }


        private TrackListBoxItemCollection _tracks = new TrackListBoxItemCollection();
        /// <summary>
        /// Bind tracks
        /// </summary>
        public TrackListBoxItemCollection Tracks
        {
            get { return _tracks; }
            set
            {
                if (!Equals(_tracks, value))
                {
                    _tracks = value;
                    OnPropertyChanged("Tracks");
                }
            }
        }

        private TilesListBoxItemSources _artists = new TilesListBoxItemSources();
        /// <summary>
        /// Bind artists
        /// </summary>
        public TilesListBoxItemSources Artists
        {
            get { return _artists; }
            set
            {
                if (_artists != value)
                {
                    _artists = value;
                    OnPropertyChanged("Artists");
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
