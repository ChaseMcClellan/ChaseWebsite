using CEM.ProgDec.BL;
using CEM.ProgDec.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEM.ProgDec.UI.Controllers
{
    public class DegreeTypeController : Controller
    {
        // GET: DegreeTypeController
        public ActionResult Index()
        {
            return View(DegreeTypeManager.Load());
        }

        // GET: DegreeTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View(DegreeTypeManager.LoadById(id));
        }

        // GET: DegreeTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DegreeTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                DegreeTypeManager.Insert(degreeType, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DegreeTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DegreeTypeManager.LoadById(id));
        }

        // POST: DegreeTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DegreeType degreeType)
        {
            try
            {
                DegreeTypeManager.Update(degreeType);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DegreeTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(DegreeTypeManager.LoadById(id));
        }

        // POST: DegreeTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DegreeType degreeType)
        {
            try
            {
                DegreeTypeManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
