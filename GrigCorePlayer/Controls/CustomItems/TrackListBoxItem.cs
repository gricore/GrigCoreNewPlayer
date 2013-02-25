using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Controls.CustomItems
{
    public class TrackListBoxItem : INotifyPropertyChanged
    {
        private string _index = string.Empty;

        public string Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged("Index");
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


        private string _artist = string.Empty;

        public string Artist
        {
            get { return _artist; }
            set
            {
                if (_artist != value)
                {
                    _artist = value;
                    OnPropertyChanged("Artist");
                }
            }
        }


        private string _duration = string.Empty;

        public string Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }



        #region Notify Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class TrackListBoxItemCollection : ObservableCollection<TrackListBoxItem>, ICloneable
    {
        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var collection = new TrackListBoxItemCollection();
            foreach (var item in this) { collection.Add(item); }
            return collection;
        }
    }
}
