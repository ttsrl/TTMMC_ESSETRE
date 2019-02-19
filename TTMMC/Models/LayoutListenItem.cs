using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TTMMC_ESSETRE.ConfigurationModels;
using TTMMC_ESSETRE.Models.DBModels;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Models
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

        private string _referenceKeyLogOld = "";

        public LayoutListenItem(Layout layout, IMachine machine)
        {
            _machine = machine;
            _layout = layout;
            _dB = DBContext.Instance;
        }

        public async Task Start()
        {
            var ll = await _dB.Layouts.Include(l => l.LayoutActRecords).FirstOrDefaultAsync(l => l.Id == _layout.Id);
            if (ll is Layout)
            {
                _layout = ll;
                if (ll.Status == Status.Waiting)
                {
                    ll.Status = Status.Recording;
                }
                
                var refRead = _machine.GetReferenceKeyRead();
                _referenceKeyLogOld = await _machine.ReadAsync(refRead.Value[0].Address, _machine.GetDataItemType(refRead.Value[0]));

                //log sets
                var writes = _machine.GetParametersWrite();
                var fields = new List<LayoutRecordField>();
                foreach (var par in writes)
                {
                    var newIt = new Dictionary<string, string>();
                    foreach (var dataIt in par.Value)
                    {
                        var type = _machine.GetDataItemType(dataIt);
                        var val = _machine.Read(dataIt.Address, type) ?? "";
                        newIt.Add(par.Value.IndexOf(dataIt).ToString(), val);
                    }
                    var json = JsonConvert.SerializeObject(newIt);
                    fields.Add(new LayoutRecordField { Key = par.Key, Value = json });
                }

                var record = new LayoutRecord
                {
                    Fields = fields
                };

                ll.LayoutSetRecord = record;
                ll.StartTimestamp = DateTime.Now;
                await _dB.SaveChangesAsync();

                _timer = new Timer(Do, null, TimeSpan.Zero, TimeSpan.FromSeconds(timerTick));
                _isBusy = true;
                _machine.Recording = true;
            }
        }

        private async void Do(object state)
        {
            if (await isChangedReferenceKey())
            {
                var fields = new List<LayoutRecordField>();
                var acts = _machine.GetParametersRead();

                var refRead = _machine.GetReferenceKeyRead();
                var refWrite = _machine.GetReferenceKeyWrite();
                foreach (var act in acts)
                {
                    if (act.Key.Substring(0, 1) != "[" && act.Key.Substring(act.Key.Length - 1, 1) != "]") // se non è una proprietà nascosta
                    {
                        var newIt = new Dictionary<string, string>();
                        foreach (var dataIt in act.Value)
                        {
                            var type = _machine.GetDataItemType(dataIt);
                            var val = _machine.Read(dataIt.Address, type) ?? "";
                            newIt.Add(act.Value.IndexOf(dataIt).ToString(), val);
                        }
                        var json = JsonConvert.SerializeObject(newIt) ?? "";
                        fields.Add(new LayoutRecordField { Key = act.Key, Value = json });
                    }
                }
                var record = new LayoutRecord
                {
                    Fields = fields
                };

                var referenceKeyRead = await _machine.ReadAsync(refRead.Value[0].Address, _machine.GetDataItemType(refRead.Value[0]));
                var referenceKeyFinisched = await _machine.ReadAsync(refWrite.Value[0].Address, _machine.GetDataItemType(refWrite.Value[0]));

                //if is finished
                if (referenceKeyRead == referenceKeyFinisched)
                {
                    _layout.Status = Status.Finished;
                    _isBusy = false;
                    _machine.Recording = false;
                    _timer?.Change(Timeout.Infinite, 0);
                }
                _layout.LayoutActRecords.Add(record);
                await _dB.SaveChangesAsync();
                workCount += 1;
            }
        }

        private async Task<bool> isChangedReferenceKey()
        {
            var di = _machine.GetReferenceKeyRead().Value[0];
            var actV = await _machine.ReadAsync(di.Address, _machine.GetDataItemType(di));
            if(actV != _referenceKeyLogOld)
            {
                _referenceKeyLogOld = actV;
                return true;
            }
            return false;
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
