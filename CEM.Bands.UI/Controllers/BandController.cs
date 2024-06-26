﻿using CEM.Bands.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CEM.Bands.UI.Controllers
{



    public class BandController : Controller
    {
        Band[] bands;

        public void LoadBands()
        {
            bands = new Band[]
            {
                new Band {Id = 2, Name = "Sublime", Genre="Ragge", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQZNub40r5Vbe4vdB-FGiY5KxWRQ9KxxFlFfg&usqp=CAU", DateFounded = new DateTime(1988, 6, 14)},
                new Band {Id = 3, Name = "Red Hot Chili Peppers",Image = "data:image/jpeg;base64,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", Genre="Punk Rock", DateFounded = new DateTime(1982, 4, 01)},
                new Band {Id = 4, Name= "Nirvana" , Genre="Rock",Image = "data:image/jpeg;base64,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", DateFounded=new DateTime(1989, 6, 15)}
            };
        }
        [HttpGet]
        public IActionResult Index()
        {
            //load the data
            GetBands();
            //show the view
            return View("IndexCard", bands);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }
        [HttpPost]
        public IActionResult Delete(int id, Band band)
        {
            try
            {
                GetBands();
                var newbands = bands.Where(b => b.Id != id);
                bands = newbands.ToArray();
                SetBands();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        private void GetBands()
        {
            if (HttpContext.Session.GetObject<Band[]>("bands") != null)
            {
                bands = HttpContext.Session.GetObject<Band[]>("bands");
            }
            else
            {
                LoadBands();
            }
            

        }
        private void SetBands()
        {
            HttpContext.Session.SetObject("bands", bands);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Band band = new Band();
            return View(band);
        }
        [HttpPost]
        public IActionResult Create(Band band)
        {
            GetBands();
            //resize the array
            Array.Resize(ref bands, bands.Length + 1);
            //calc the new id
            band.Id = bands.Length;
            bands[bands.Length - 1] = band;
            SetBands();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            GetBands();
            Band band = bands.FirstOrDefault(b=>b.Id == id);
            return View(band);
        }
        [HttpPost]
        public IActionResult Edit(int id, Band band)
        {
            GetBands();

            bands[id - 1] = band;
            SetBands();
            return RedirectToAction(nameof(Index));
        }
    }
}