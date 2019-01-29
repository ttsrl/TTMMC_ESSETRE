using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMMC.Models.DBModels;
using TTMMC.Models.ViewModels;
using TTMMC.Services;

namespace TTMMC.Controllers
{
    public class MouldController : Controller
    {

        private readonly DBContext _dB;
        private readonly MachinesService _machines;
        private readonly IHostingEnvironment _environment;

        public MouldController(DBContext dB, MachinesService machines, IHostingEnvironment iHostingEnvironment)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _machines = machines ?? throw new ArgumentNullException(nameof(machines));
            _environment = iHostingEnvironment ?? throw new ArgumentNullException(nameof(iHostingEnvironment));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var moulds = await _dB.Moulds
                .Include(m => m.DefaultClient)
                .Include(m => m.DefaultMixture)
                .ToListAsync();
            var model = new IndexMouldModel { Moulds = moulds };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var clients = await _dB.Clients.ToListAsync();
            var mixtures = await _dB.Mixtures
                .Include(mm => mm.Items)
                    .ThenInclude(it => it.Material)
                .ToListAsync();
            var m = new NewMouldViewModel
            {
                Mixtures = mixtures,
                Clients = clients
            };
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewMouldModel model)
        {
            if (ModelState.IsValid) //se il modello è valido
            {
                var existMould = await _dB.Moulds.FirstOrDefaultAsync(m => m.Code == model.Code);
                var client = await _dB.Clients.FindAsync(model.Client);
                var mixture = await _dB.Mixtures.FirstOrDefaultAsync(m => m.Id == model.Mixture);
                if (mixture is Mixture && client is Client && existMould == null)
                {
                    var newFileName = "";
                    if (model.Image != null && model.Image.Length > 0) //se è presente un'immagine
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(model.Image.ContentDisposition).FileName.Trim('"');
                        newFileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(fileName); //set unique id filename + extension
                        fileName = Path.Combine(_environment.WebRootPath, "mouldImages") + $@"\{newFileName}";
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            model.Image.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    var mould = new Mould
                    {
                        Location = model.Location,
                        DefaultClient = client,
                        DefaultMixture = mixture,
                        Image = (model.Image != null && model.Image.Length > 0) ? "mouldImages/" + newFileName : "",
                        Code = model.Code,
                        Description = model.Description,
                        Notes = model.Notes
                    };
                    _dB.Moulds.Add(mould);
                    await _dB.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Error", new { id = 3 });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var mould = await _dB.Moulds
                .Include(mm => mm.DefaultClient)
                .Include(mm => mm.DefaultMixture)
                .FirstOrDefaultAsync(mm => mm.Id == id);
            var clients = await _dB.Clients.ToListAsync();
            var mixtures = await _dB.Mixtures
                .Include(mm => mm.Items)
                    .ThenInclude(it => it.Material)
                .ToListAsync();
            if (mould is Mould)
            {
                var m = new EditMouldViewModel
                {
                    Mixtures = mixtures,
                    Clients = clients,
                    Mould = mould
                };
                return View(m);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string code, int client, int mixture, string description, string location, IFormFile image, string notes)
        {
            if (ModelState.IsValid)
            {
                var mould = await _dB.Moulds.FirstOrDefaultAsync(m => m.Id == id);
                var existClient = await _dB.Clients.FirstOrDefaultAsync(c => c.Id == client);
                var mix = await _dB.Mixtures.FirstOrDefaultAsync(m => m.Id == mixture);
                if (mix is Mixture && mould is Mould && existClient is Client)
                {
                    var urlImg = (mould.Image == "" || mould.Image == null) ? "" : Path.Combine(_environment.WebRootPath, "mouldImages") + $@"\{mould.Image.Replace("mouldImages/", "")}";
                    var newFileName = "";
                    if (image != null && image.Length > 0) //se è presente un'immagine
                    {
                        //cancello l'immagine precedente se c 'era
                        if (urlImg != "" && System.IO.File.Exists(urlImg))
                            System.IO.File.Delete(urlImg);

                        var fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
                        newFileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(fileName); //set unique id filename + extension
                        fileName = Path.Combine(_environment.WebRootPath, "mouldImages") + $@"\{newFileName}";
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            image.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    mould.DefaultMixture = mix;
                    mould.DefaultClient = existClient;
                    mould.Code = code;
                    mould.Description = description;
                    mould.Image = (newFileName != "") ? "mouldImages/" + newFileName : mould.Image;
                    mould.Location = mould.Location;
                    mould.Notes = mould.Notes;
                    await _dB.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Error", new { id = 4 });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var mould = await _dB.Moulds.FirstOrDefaultAsync(m => m.Id == id);
            if (mould is Mould)
            {
                var urlImg = (mould.Image == "" || mould.Image == null) ? "" : Path.Combine(_environment.WebRootPath, "mouldImages") + $@"\{mould.Image.Replace("mouldImages/", "")}";
                if (urlImg != "" && System.IO.File.Exists(urlImg))
                    System.IO.File.Delete(urlImg);
                mould.Image = "";
                await _dB.SaveChangesAsync();
                return RedirectToAction("Edit", "Mould", new { id });
            }
            return RedirectToAction("Index", "Error", new { id = 5 });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var mould = await _dB.Moulds.FirstOrDefaultAsync(m => m.Id == id);
                if (mould is Mould)
                {
                    _dB.Moulds.Remove(mould);
                    await _dB.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> NewLayout(int id)
        {
            if (id != 0)
            {
                var mould = await _dB.Moulds.FirstOrDefaultAsync(m => m.Id == id);
                if (mould is Mould)
                {
                    return RedirectToAction("New", "Layout", new { mould = id });
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewModule(int id)
        {
            if(id != 0)
            {
                var mould = await _dB.Moulds.FirstOrDefaultAsync(m => m.Id == id);
                if (mould is Mould)
                {
                    return RedirectToAction("MouldModule", "Pdf", new { id });
                }
            }
            return RedirectToAction("Index");
        }
    }
}