using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GrigCorePlayer.Annotations;
using GrigCorePlayer.Controls.CustomItems;

namespace GrigCorePlayer.Model
{
    public class ArtistModel : INotifyPropertyChanged, ICloneable
    {
        private string _name = string.Empty;
        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }


        private string _pictureUrl = string.Empty;
        /// <summary>
        /// Gets or sets artist picture url
        /// </summary>
        public string PictureUrl
        {
            get { return _pictureUrl; }
            set
            {
                if (_pictureUrl != value)
                {
                    _pictureUrl = value;
                    OnPropertyChanged("PictureUrl");
                }
            }
        }


        private string _artistBio = string.Empty;
        /// <summary>
        /// Gets or sets artist bio
        /// </summary>
        public string ArtistBio
        {
            get { return _artistBio; }
            set
            {
                if (_artistBio != value)
                {
                    _artistBio = value;
                    OnPropertyChanged("ArtistBio");
                }
            }
        }

        private TilesListBoxItemSources _albums = new TilesListBoxItemSources();
        /// <summary>
        /// Bind Albums
        /// </summary>
        public TilesListBoxItemSources Albums
        {
            get { return _albums; }
            set
            {
                if (!Equals(_albums, value))
                {
                    _albums = value;
                    OnPropertyChanged("Albums");
                }
            }
        }

        private TilesListBoxItemSources _similar = new TilesListBoxItemSources();
        /// <summary>
        /// Gets or set artist similar
        /// </summary>
        public TilesListBoxItemSources Similar
        {
            get { return _similar; }
            set
            {
                if (!Equals(_similar, value))
                {
                    _similar = value;
                    OnPropertyChanged("Similar");
                }
            }
        }

        private TrackListBoxItemCollection _tracks = new TrackListBoxItemCollection();
        /// <summary>
        /// Gets or sets current tracks.
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

        private TagsListBoxItemsCollection _tags = new TagsListBoxItemsCollection();
        /// <summary>
        /// Bind tags
        /// </summary>
        public TagsListBoxItemsCollection Tags
        {
            get { return _tags; }
            set
            {
                if (!Equals(_tags, value))
                {
                    _tags = value;
                    OnPropertyChanged("Tags");
                }
            }
        }
       

        private int _selectedAlbumIndex;
        /// <summary>
        /// Gets or sets selected album index
        /// </summary>
        public int SelectedAlbumIndex
        {
            get { return _selectedAlbumIndex; }
            set
            {
                if (_selectedAlbumIndex != value)
                {
                    _selectedAlbumIndex = value;
                    OnPropertyChanged("SelectedAlbumIndex");
                }
            }
        }
          

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public object Clone()
        {
            ArtistModel model = new ArtistModel();
            model = this;
            return model;
        }
    }
}
