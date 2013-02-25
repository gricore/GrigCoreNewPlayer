using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GrigCorePlayer.Controls.CustomItems;
using GrigCorePlayer.Controls.Telegrik;

namespace GrigCorePlayer.Controls.CustomListBox
{
    /// <summary>
    /// Interaction logic for TrackListBox.xaml
    /// </summary>
    public partial class TrackListBox : UserControl
    {
        public delegate void TrackSelectedEventHandler(object sender, MouseButtonEventArgs e);
        public event TrackSelectedEventHandler TrackSelected;

        public static readonly DependencyProperty SourceCollectionProperty = DependencyProperty.Register("SourceCollection", typeof(TrackListBoxItemCollection), typeof(TrackListBox), new PropertyMetadata(default(TrackListBoxItemCollection)));
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(TrackListBox), new PropertyMetadata(default(int)));
        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(object), typeof(TrackListBox), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(TrackListBox), new PropertyMetadata(default(object)));

        public TrackListBox()
        {
            InitializeComponent();

            CustomFilterBehavior customFilterBehavior = new CustomFilterBehavior(radGridView, textBoxFilterValue);
        }


        public TrackListBoxItemCollection SourceCollection
        {
            get { return (TrackListBoxItemCollection)GetValue(SourceCollectionProperty); }
            set { SetValue(SourceCollectionProperty, value); }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private void ListBoxItem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // if(e.ClickCount != 2) return;
            var sItem = SelectedItem as TrackListBoxItem;
            if (sItem != null)
                SelectedIndex = int.Parse(sItem.Index) - 1;

            if (TrackSelected != null)
                TrackSelected(sender, e);
        }

    }

}
