using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Controls.CustomItems
{
    #region Target Types

    public class TilesListBoxItemSources : ObservableCollection<TilesListBoxItem>, ICloneable
    {
        public object Clone()
        {
            var sources = new TilesListBoxItemSources();
            foreach (var vvvv in this)
            {
                sources.Add(vvvv);
            }
            return sources;
        }
    }

    public class TilesListBoxItem : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }


        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _picture = string.Empty;
        public string Picture
        {
            get { return _picture; }
            set
            {
                if (_picture != value)
                {
                    _picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }



        #region Notify Property Changed



        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    #endregion
}
