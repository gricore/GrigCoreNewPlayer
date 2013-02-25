using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GrigCorePlayer.Controls.CustomItems;

namespace GrigCorePlayer.Controls.CustomListBox
{
    /// <summary>
    /// Interaction logic for TilesListBox.xaml
    /// </summary>
    public partial class TilesListBox
    {
        #region Events

        public delegate void SelectedChangedEventHandler(object sender, SelectionChangedEventArgs e);
        public event SelectedChangedEventHandler SelectedChanged;

        public delegate void ItemDoubleClickedEventHandler(object sender, MouseButtonEventArgs e);
        public event ItemDoubleClickedEventHandler ItemDoubleClicked;

        #endregion

        #region SelectedIndex
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int),
            typeof(TilesListBox), new PropertyMetadata(default(int)));


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        #endregion

        #region Selected Item

        public static readonly DependencyProperty SelectedItemProperty =
           DependencyProperty.Register("SelectedItem", typeof(object),
           typeof(TilesListBox), new PropertyMetadata(default(object)));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        #region Sources
        public static readonly DependencyProperty ListBoxItemSourcesProperty =
           DependencyProperty.Register("ListBoxItemSources", typeof(TilesListBoxItemSources),
           typeof(TilesListBox), new PropertyMetadata(default(TilesListBoxItemSources)));

        public TilesListBoxItemSources ListBoxItemSources
        {
            get { return (TilesListBoxItemSources)GetValue(ListBoxItemSourcesProperty); }
            set { SetValue(ListBoxItemSourcesProperty, value); }
        }

        #endregion

        #region Ctor
        public TilesListBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers

        private void TheListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedChanged != null)
                SelectedChanged(sender, e);
        }

        private void TilesListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2) return;

            if (ItemDoubleClicked != null)
                ItemDoubleClicked(sender, e);
        }

        #endregion
    }    
}
