using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private Layout _layout;
        private long workCount = 0;
        private bool _isBusy = false;
        private int timerTick = 5;
        private Timer _timer;
        private readonly DBContext _dB;
        private readonly IMachine _machine;

        public int TimerTick { get => timerTick; set => timerTick = value; }
        public bool IsBusy { get => _isBusy; }
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

        public async Task Start()
        {
            var ll = await _dB.Layouts.Include(l => l.LayoutRecords).FirstOrDefaultAsync(l => l.Id == _layout.Id);
            if (ll is Layout)
            {
                if (ll.Status == Status.Waiting)
                {
                    ll.Status = Status.Recording;
                    await _dB.SaveChangesAsync();
                }
                _layout = ll;
                _timer = new Timer(Do, null, TimeSpan.Zero, TimeSpan.FromSeconds(timerTick));
                _isBusy = true;
                _machine.Recording = true;
            }
        }

        private async void Do(object state)
        {
            //sql writer
            var r = new LayoutRecord
            {
                Value = "dsa"
            };
            _layout.LayoutRecords.Add(r);
            await _dB.SaveChangesAsync();
            workCount += 1;
        }

        public async Task Stop()
        {
            if (_layout.Status == Status.Recording)
            {
                _layout.Status = Status.Stopped;
                await _dB.SaveChangesAsync();
            }
            _isBusy = false;
            _machine.Recording = false;
            _timer?.Change(Timeout.Infinite, 0);
        }

        public void Dispose()
        {
            _layout = null;
            workCount = 0;
            _timer?.Dispose();
        }
    }
}
