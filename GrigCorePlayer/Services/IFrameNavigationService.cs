using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCorePlayer.Services
{
    public interface IFrameNavigationService
    {
        void NavigateToView<T>();
    }
}
