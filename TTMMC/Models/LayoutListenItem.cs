using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTMMC.Models.DBModels;
using TTMMC.Services;

namespace TTMMC.Models
{
    public class LayoutListenItem
    {
        private readonly Layout _layout;
        private long workCount = 0;
        private bool isBusy = false;
        private int timerTick = 5;
        private Timer _timer;
        private readonly DBContext _dB;
        private readonly IMachine _machine;

        public int TimerTick { get => timerTick; set => timerTick = value; }
        public bool IsBusy { get => isBusy; }
        public long WorkCount { get => workCount; }
        public Layout Layout { get => _layout; }
        public IMachine Machine { get => _machine; }
        public static int DefaultTimerTick = 5;

        public LayoutListenItem(Layout layout, IMachine machine)
        {
            _machine = machine;
            _layout = layout;
            _dB = DBContext.Instance;
        }

        public void Start()
        {
            _timer = new Timer(Do, null, TimeSpan.Zero, TimeSpan.FromSeconds(timerTick));
            isBusy = true;
            _machine.Recording = true;
        }

        private async void Do(object state)
        {
            //sql writer
            workCount += 1;
        }

        public void Stop()
        {
            isBusy = false;
            _machine.Recording = false;
            _timer?.Change(Timeout.Infinite, 0);
        }

        public void Dispose()
        {
            workCount = 0;
            _timer?.Dispose();
        }
    }
}
