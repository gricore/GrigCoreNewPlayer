using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCorePlayer.Services
{
    public class FrameNavigationService : IFrameNavigationService
    {
        private readonly IRegionManager _regionManager;

        public FrameNavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void NavigateToView<T>()
        {
            _regionManager.Regions["NavigationContent"].RequestNavigate(typeof(T).FullName);
        }
    }
}
