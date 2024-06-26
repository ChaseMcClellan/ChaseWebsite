using CEM.ProgDec.BL;
using CEM.ProgDec.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEM.ProgDec.UI.Controllers
{
    public class DeclarationController : Controller
    {
        // GET: DeclarationController
        public ActionResult Index()
        {
            return View(DeclarationManager.Load());
        }

        public ActionResult Browse(int id)
        {
            return View(nameof(Index), DeclarationManager.Load(id));
        }

        // GET: DeclarationController/Details/5
        public ActionResult Details(int id)
        {
            return View(DeclarationManager.LoadById(id));
        }

        // GET: DeclarationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeclarationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Declaration declaration, bool rollback = false)
        {
            try
            {
                DeclarationManager.Insert(declaration, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeclarationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DeclarationManager.LoadById(id));
        }

        // POST: DeclarationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Declaration declaration)
        {
            try
            {
                DeclarationManager.Update(declaration);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeclarationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(DeclarationManager.LoadById(id));
        }

        // POST: DeclarationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Declaration declaration)
        {
            try
            {
                DeclarationManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
