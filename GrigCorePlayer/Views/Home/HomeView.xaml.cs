using GrigCorePlayer.Controllers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrigCorePlayer.Views.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        private readonly IUnityContainer _container;
        private const string SearchBoxStardartText = "Enter an artist name";

        public HomeView(IUnityContainer container)
        {
            _container = container;
            InitializeComponent();

            DataContext = _container.Resolve<HomeController>();
            this.Loaded += HomeViewLoaded;
        }

        private void HomeViewLoaded(object sender, RoutedEventArgs e)
        {
            SearchBoxTextBox.Text = SearchBoxStardartText;
        }

        private void SearchBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBoxTextBox.Text == SearchBoxStardartText)
                SearchBoxTextBox.Text = string.Empty;
        }

        private void SearchBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBoxTextBox.Text))
                SearchBoxTextBox.Text = SearchBoxStardartText;
        }
    }


}
