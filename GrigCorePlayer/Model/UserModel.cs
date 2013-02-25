using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomItems;

namespace GrigCorePlayer.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        private TilesListBoxItemSources _topArtists = new TilesListBoxItemSources();

        public TilesListBoxItemSources TopArtists
        {
            get { return _topArtists; }
            set
            {
                if (!Equals(_topArtists, value))
                {
                    _topArtists = value;
                    OnPropertyChanged("TopArtists");
                }
            }
        }


        private TrackListBoxItemCollection _topTracks = new TrackListBoxItemCollection();

        public TrackListBoxItemCollection TopTracks
        {
            get { return _topTracks; }
            set
            {
                if (!Equals(_topTracks, value))
                {
                    _topTracks = value;
                    OnPropertyChanged("TopTracks");
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
