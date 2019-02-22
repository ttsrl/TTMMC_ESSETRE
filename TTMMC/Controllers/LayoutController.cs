using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TTMMC_ESSETRE.Models;
using TTMMC_ESSETRE.Models.DBModels;
using TTMMC_ESSETRE.Models.ViewModels;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Controllers
{
    public class LayoutController : Controller
    {
        private readonly MachinesService _machines;
        private readonly owlDBContext _owlDb;
        private readonly TTMMCContext _dB;
        private readonly LayoutListener _lListener;

        public LayoutController(MachinesService machines, owlDBContext owlDb, TTMMCContext dB, LayoutListener lListener)
        {
            _machines = machines ?? throw new ArgumentNullException(nameof(machines));
            _owlDb = owlDb ?? throw new ArgumentNullException(nameof(owlDb));
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _lListener = lListener ?? throw new ArgumentNullException(nameof(lListener));
        }

        public async Task<IActionResult> Index()
        {
            var disposizioni = await _owlDb.Decofast35Datiesterni.ToListAsync();
            var layouts = await _dB.Layouts.ToListAsync();
            foreach (var d in disposizioni)
            {
                bool exist = false;
                foreach (var l in layouts)
                {
                    if (l.Color == d.Colore && l.ItemCode == d.CodiceArticolo && l.ItemDescription == d.DescrizioneArticolo && 
                        l.LayoutNumber == d.NumDisposizione && l.LayoutPhase == d.FaseDisposizione && l.LayoutType == d.TipoDisposizione &&
                        l.MachineName == d.NomeMacchina && l.MachineNumber == d.NumeroMacchina && l.Meters == d.MetriDisposti && 
                        l.Quantity == d.PezzeDisposte && l.StartTimestamp == d.DataDisposizione)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    int machine = (_machines.GetMachineByName(d.NomeMacchina) is IMachine) ? _machines.GetMachineByName(d.NomeMacchina).Id : 0;
                    var newl = new Layout
                    {
                        Color = d.Colore,
                        ItemCode = d.CodiceArticolo,
                        ItemDescription = d.DescrizioneArticolo,
                        LayoutNumber = d.NumDisposizione,
                        LayoutPhase = d.FaseDisposizione,
                        LayoutType = d.TipoDisposizione,
                        MachineNumber = d.NumeroMacchina,
                        MachineName = d.NomeMacchina,
                        Machine = machine,
                        Meters = d.MetriDisposti,
                        Quantity = d.PezzeDisposte,
                        StartTimestamp = d.DataDisposizione ?? DateTime.Now,
                        Status = Status.Waiting
                    };
                    _dB.Layouts.Add(newl);
                }
            }
            await _dB.SaveChangesAsync();

            //reload layouts
            layouts = await _dB.Layouts.Include(l => l.LayoutActRecords).ThenInclude(lr => lr.Fields).OrderByDescending(l => l.StartTimestamp).ToListAsync();
            var machines = _machines.GetMachines().ToList();
            var m = new IndexLayoutModel
            {
                Layouts = layouts,
                Machines = machines
            };
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Start(int id)
        {
            if(id != 0)
            {
                var layout = await _dB.Layouts.Where(l => l.Status == Status.Waiting).FirstOrDefaultAsync(l => l.Id == id);
                if(layout is Layout)
                {
                    if(!_lListener.Contains(layout))
                        _lListener.Add(layout);
                    var ll = _lListener.GetLayoutListenItem(layout);
                    ll.TimerTick = 2;
                    ll.Rounded = true;
                    ll.RoundedPrecision = 2;
                    await ll.Start();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Stop(int id)
        {
            if (id != 0)
            {
                var layout = await _dB.Layouts.Where(l => l.Status == Status.Recording).FirstOrDefaultAsync(l => l.Id == id);
                if (layout is Layout)
                {
                    await _lListener.Remove(layout);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Report(int id)
        {
            if(id != 0)
            {
                var layout = await _dB.Layouts.Where(l => l.Status == Status.Finished || l.Status == Status.Stopped).FirstOrDefaultAsync(l => l.Id == id);
                if(layout is Layout)
                {
                    return RedirectToAction("Report", "Pdf", new { id });
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            try
            {
                //aggiorno le disposizioni
                var disposizioni = await _owlDb.Decofast35Datiesterni.ToListAsync();
                var layouts = await _dB.Layouts.Include(l => l.LayoutActRecords).ThenInclude(r => r.Fields).ToListAsync();
                foreach (var d in disposizioni)
                {
                    Layout tmpLayout = null;

                    //sincronizzo disposizioni
                    foreach (var l in layouts)
                    {
                        if (l.Color == d.Colore && l.ItemCode == d.CodiceArticolo && l.ItemDescription == d.DescrizioneArticolo &&
                            l.LayoutNumber == d.NumDisposizione && l.LayoutPhase == d.FaseDisposizione && l.LayoutType == d.TipoDisposizione &&
                            l.MachineName == d.NomeMacchina && l.MachineNumber == d.NumeroMacchina && l.Meters == d.MetriDisposti &&
                            l.Quantity == d.PezzeDisposte && l.StartTimestamp == d.DataDisposizione)
                        {
                            tmpLayout = l;
                            break;
                        }
                    }
                    if (tmpLayout == null)
                    {
                        int machine = (_machines.GetMachineByName(d.NomeMacchina) is IMachine) ? _machines.GetMachineByName(d.NomeMacchina).Id : 0;
                        var newl = new Layout
                        {
                            Color = d.Colore,
                            ItemCode = d.CodiceArticolo,
                            ItemDescription = d.DescrizioneArticolo,
                            LayoutNumber = d.NumDisposizione,
                            LayoutPhase = d.FaseDisposizione,
                            LayoutType = d.TipoDisposizione,
                            MachineNumber = d.NumeroMacchina,
                            MachineName = d.NomeMacchina,
                            Machine = machine,
                            Meters = d.MetriDisposti,
                            Quantity = d.PezzeDisposte,
                            StartTimestamp = d.DataDisposizione ?? DateTime.Now,
                            Status = Status.Waiting
                        };
                        _dB.Layouts.Add(newl);
                        tmpLayout = newl;
                    }

                    //sincronizzo records
                    var values = await _owlDb.Decofast35Getvalue.Where(v => v.NumDisposizione == d).ToListAsync();
                    if ((values.Count > 0 && tmpLayout.LayoutActRecords == null) || (values.Count > tmpLayout.LayoutActRecords.Count))
                    {
                        tmpLayout.LayoutActRecords = (tmpLayout.LayoutActRecords == null) ? new List<LayoutRecord>() : tmpLayout.LayoutActRecords;
                        foreach (var v in values)
                        {
                            var exist = tmpLayout.LayoutActRecords.Select(r => r.Id).ToList().Contains(v.Id);
                            if (!exist)
                            {
                                var fields = createFields(v);
                                var record = new LayoutRecord { Fields = fields, Timestamp = v.CreatedAt ?? DateTime.Now };
                                tmpLayout.LayoutActRecords.Add(record);
                            }
                        }
                    }
                    if (values.Count != tmpLayout.LayoutActRecords.Count)
                    {
                        return RedirectToAction("Index", "Error", new { id = 5 });
                    }
                    if (tmpLayout.LayoutActRecords.Count > 0)
                    {
                        tmpLayout.Status = Status.Finished;
                    }
                }
                await _dB.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch { }
            return RedirectToAction("Index", "Error", new { id = 5 });
        }


        private List<LayoutRecordField> createFields(Decofast35Getvalue v)
        {
            var elm = new Dictionary<string, string>();
            var fields = new List<LayoutRecordField>();

            elm = new Dictionary<string, string>();
            elm.Add("0", v.VelMacchina.ToString());
            fields.Add(new LayoutRecordField { Key = "Velocita", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.NominaleTensioneElettrFeltro.ToString());
            fields.Add(new LayoutRecordField { Key = "TensioneFeltro", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.VelCilindroGommato01.ToString());
            fields.Add(new LayoutRecordField { Key = "VelocitaCilindro01", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.CoppiaCilindroGommato01.ToString());
            fields.Add(new LayoutRecordField { Key = "CoppiaCilindro01", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.VelocitaCilindroDecatit.ToString());
            fields.Add(new LayoutRecordField { Key = "VelocitaDecatitore", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.CoppiaCilindroDecatit.ToString());
            fields.Add(new LayoutRecordField { Key = "CoppiaDecatitore", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.VelCilindroGommato02.ToString());
            fields.Add(new LayoutRecordField { Key = "VelocitaCilindro02", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.CoppiaCilindroGommato02.ToString());
            fields.Add(new LayoutRecordField { Key = "CoppiaCilindro02", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.TiroCentratore.ToString());
            fields.Add(new LayoutRecordField { Key = "Centratore", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.ContametriLunghezAttuale.ToString());
            fields.Add(new LayoutRecordField { Key = "ContametriAttuale", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.ContamentriLunghezzAttualePrecedente.ToString());
            fields.Add(new LayoutRecordField { Key = "ContametriPrecedente", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.RiscaldamentoCilindroDecatitoreBar.ToString());
            elm.Add("1", v.RiscaldamentoCilindroDecatitoreGradi.ToString());
            elm.Add("2", v.RiscaldamentoCilindroDecatitorePercent.ToString());
            fields.Add(new LayoutRecordField { Key = "RiscaldatoreDecatitore", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.VaporeInVascaBar.ToString());
            elm.Add("1", v.VaporeInVascaGradi.ToString());
            elm.Add("2", v.VaporeInVascaPercent.ToString());
            fields.Add(new LayoutRecordField { Key = "Vapore", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.PressGuarnizioneVasca.ToString());
            fields.Add(new LayoutRecordField { Key = "PressioneVasca", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.RiscaldamentoCilindroDecatitoreMin.ToString());
            elm.Add("1", v.RiscaldamentoCilindroDecatitoreSec.ToString());
            fields.Add(new LayoutRecordField { Key = "RiscaldamentoDecatitore", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.RiscaldamentoVascaMin.ToString());
            elm.Add("1", v.RiscaldamentoVascaSec.ToString());
            fields.Add(new LayoutRecordField { Key = "RiscaldamentoVasca", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.CicloRaffreddMin.ToString());
            elm.Add("1", v.CicloRaffreddSec.ToString());
            fields.Add(new LayoutRecordField { Key = "CicloRaffreddamento", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.AspirazioneBocchetteDepress.ToString());
            fields.Add(new LayoutRecordField { Key = "DepressioneAspirazione", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.AlimentAriaPressione.ToString());
            fields.Add(new LayoutRecordField { Key = "PressioneAria", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.GuarnizVascaPressione.ToString());
            fields.Add(new LayoutRecordField { Key = "PressioneGuarnizione", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.TempoOperativoH.ToString());
            elm.Add("1", v.TempoOperativoMin.ToString());
            fields.Add(new LayoutRecordField { Key = "TempoOperativo", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.TempoNonOperativoH.ToString());
            elm.Add("1", v.TempoNonOperativoMin.ToString());
            fields.Add(new LayoutRecordField { Key = "TempoNonOperativo", Value = JsonConvert.SerializeObject(elm) });
            elm = new Dictionary<string, string>();
            elm.Add("0", v.TempoLavorazioneH.ToString());
            elm.Add("1", v.TempoLavorazioneMin.ToString());
            fields.Add(new LayoutRecordField { Key = "TempoLavorazione", Value = JsonConvert.SerializeObject(elm) });

            return fields;
        }

    }
}
