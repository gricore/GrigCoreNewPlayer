using System.Windows;
using Microsoft.Practices.Unity;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using GrigCorePlayer.Views.Artist;
using GrigCorePlayer.Controllers;

namespace GrigCorePlayer.Views.Shell
{
    /// <summary>
    /// Interaction logic for _LeftPanelView.xaml
    /// </summary>
    public partial class _LeftPanelView : UserControl
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public _LeftPanelView(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;

            InitializeComponent();
            DataContext = _container.Resolve<ShellController>();
        }

        private void ArtistButtonClick(object sender, RoutedEventArgs e)
        {
            _regionManager.Regions["NavigationContent"].RequestNavigate(typeof(ArtistView).FullName);
        }
    }
 
}
