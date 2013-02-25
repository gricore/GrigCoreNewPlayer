using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCorePlayer.Services
{
    public interface IAsyncService
    {
        void RunAsync(AsyncService.SimpleAsyncMethod doBefore, AsyncService.SimpleAsyncMethod doMethod,
                      AsyncService.SimpleAsyncMethod completeMethod);

        void RunAsyncListAdding(int count, AsyncService.OnAddingToListAsync addingToListMethod, int threadSleepTime);

        void RunAsyncForever(AsyncService.SimpleAsyncMethod completeMethod, int threadSleepTime);
    }
}
