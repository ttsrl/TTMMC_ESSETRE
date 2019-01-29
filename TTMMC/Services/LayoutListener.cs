using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TTMMC.Models;
using TTMMC.Models.DBModels;

namespace TTMMC.Services
{
    public class LayoutListener
    {
        private List<LayoutListenItem> listenItems = new List<LayoutListenItem>();
        private readonly DBContext _dB;
        private readonly MachinesService _machinesService;

        public LayoutListener([FromServices] MachinesService machinesService)
        {
            _dB = DBContext.Instance;
            _machinesService = machinesService;
        }

        public void Add(Layout layout)
        {
            if (layout is Layout)
            {
                var machine = _machinesService.GetMachineById(layout.Machine);
                if (machine is IMachine)
                {
                    add(layout, machine, LayoutListenItem.DefaultTimerTick);
                }
            }
        }

        public void Add(Layout layout, int timerTick)
        {
            if (layout is Layout)
            {
                var machine = _machinesService.GetMachineById(layout.Machine);
                if (machine is IMachine)
                {
                    add(layout, machine, timerTick);
                }
            }
        }

        private void add(Layout layout, IMachine machine, int timerTick)
        {
            var it = new LayoutListenItem(layout, machine);
            it.TimerTick = timerTick;
            listenItems.Add(it);
        }

        public bool Contains(Layout layout)
        {
            var it = GetLayoutListenItemById(layout.Id);
            if (it is LayoutListenItem)
            {
                return true;
            }
            return false;
        }

        public async Task Remove(Layout layout)
        {
            var it = GetLayoutListenItemById(layout.Id);
            if (it is LayoutListenItem)
            {
                if (it.IsBusy)
                   await it.Stop();

                it.Dispose();
                listenItems.Remove(it);
            }
        }

        public LayoutListenItem GetLayoutListenItem(Layout layout)
        {
            foreach (var it in listenItems)
            {
                if (it.Layout.Id == layout.Id)
                {
                    return it;
                }
            }
            return null;
        }

        public LayoutListenItem GetLayoutListenItemById(int id)
        {
            foreach (var it in listenItems)
            {
                if (it.Layout.Id == id)
                {
                    return it;
                }
            }
            return null;
        }

    }
}