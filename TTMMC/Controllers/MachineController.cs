﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TTMMC.Models;
using TTMMC.Models.ViewModels;
using TTMMC.Services;

namespace TTMMC.Controllers
{
    public class MachineController : Controller
    {
        private readonly MachinesService _machines;

        public MachineController(MachinesService machines)
        {
            _machines = machines ?? throw new ArgumentNullException(nameof(machines));
        }

        public IActionResult Index()
        {
            var machines = _machines.GetMachines().ToList();
            var m = new HomeModel
            {
                Machines = machines
            };
            return View(m);
        }

        public IActionResult Details(int id)
        {
            if(id != 0)
            {
                var mdb = _machines.GetMachineById(id);
                if (mdb is IMachine)
                {
                    var m = new MachineDetailsModel
                    {
                        Machine = mdb
                    };
                    return View(m);
                }

            }
            return RedirectToAction("Index");
        }

    }
}