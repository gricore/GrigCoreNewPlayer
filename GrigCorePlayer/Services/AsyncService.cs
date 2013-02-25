using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using GrigCorePlayer.Annotations;

namespace GrigCorePlayer.Services
{
    [UsedImplicitly]
    public class AsyncService : IAsyncService
    {
        #region Fields
        public delegate void SimpleAsyncMethod();
        public delegate void OnAddingToListAsync(int index);
        #endregion

        #region Methods

        /// <summary>
        /// Run method async with complete method
        /// </summary>
        /// <param name="doBefore"></param>
        /// <param name="doMethod"></param>
        /// <param name="completeMethod"></param>
        public void RunAsync(SimpleAsyncMethod doBefore, SimpleAsyncMethod doMethod, SimpleAsyncMethod completeMethod)
        {
            // Do First
            if (doBefore != null)
                doBefore.Invoke();
            // Declare background worker.
            var backgroundWorker = new BackgroundWorker();
            // Declare do method.
            backgroundWorker.DoWork += (sender, args) => doMethod.Invoke();

            // Delclare complete method.
            backgroundWorker.RunWorkerCompleted += (sender, args) => completeMethod.Invoke();

            // Run worker.
            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Run async adding to list.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="addingToListMethod"></param>
        /// <param name="threadSleepTime"></param>
        public void RunAsyncListAdding(int count, OnAddingToListAsync addingToListMethod, int threadSleepTime)
        {
            int i = 0;
            var worker = new BackgroundWorker { WorkerSupportsCancellation = true };

            // Do Work Method
            worker.DoWork += (sender, args) => Thread.Sleep(threadSleepTime);

            // Complete work method
            worker.RunWorkerCompleted += (sender, args) =>
            {
                if (args.Cancelled) return;

                addingToListMethod(i);
                i++;

                if (i < count && !worker.IsBusy)
                    worker.RunWorkerAsync();

            };

            // Run worker.
            if (!worker.IsBusy)
                worker.RunWorkerAsync();

        }

        /// <summary>
        /// Run async forever
        /// </summary>
        /// <param name="completeMethod"></param>
        /// <param name="threadSleepTime"></param>
        public void RunAsyncForever(SimpleAsyncMethod completeMethod, int threadSleepTime)
        {          
            var worker = new BackgroundWorker { WorkerSupportsCancellation = true };

            // Do Work Method
            worker.DoWork += (sender, args) => Thread.Sleep(threadSleepTime);

            // Complete work method
            worker.RunWorkerCompleted += (sender, args) =>
            {
                if (args.Cancelled) return;

                completeMethod();
                if(!worker.IsBusy)
                    worker.RunWorkerAsync();
            };

            // Run worker.
            if (!worker.IsBusy)
                worker.RunWorkerAsync();

        }

        #endregion
    }
}
