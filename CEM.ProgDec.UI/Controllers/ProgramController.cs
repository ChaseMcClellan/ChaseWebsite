using CEM.ProgDec.BL;
using CEM.ProgDec.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEM.ProgDec.UI.Controllers
{
    public class ProgramController : Controller
    {
        // GET: ProgramController
        public ActionResult Index()
        {
            ViewBag.Title = "Programs";
            return View(ProgramManager.Load());
        }

        // GET: ProgramController/Details/5
        public ActionResult Details(int id)
        {
            return View(ProgramManager.LoadById(id));
        }

        // GET: ProgramController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create Program";

            ProgramVM programVM = new ProgramVM();

            programVM.Program = new BL.Models.Program();
            programVM.DegreeTypes = DegreeTypeManager.Load();


            return View(programVM);
        }

        // POST: ProgramController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProgramVM programVM, bool rollback = false)
        {
            try
            {
                ProgramManager.Insert(programVM.Program, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProgramController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Program";

            ProgramVM programVM = new ProgramVM();

            programVM.Program = ProgramManager.LoadById(id);
            programVM.DegreeTypes = DegreeTypeManager.Load();


            return View(programVM);
        }

        // POST: ProgramController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProgramVM programVM)
        {
            try
            {
                //the following code will break
                //program.Name = null;
                ProgramManager.Update(programVM.Program);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: ProgramController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Program";
            return View(ProgramManager.LoadById(id));
        }

        // POST: ProgramController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BL.Models.Program program)
        {
            try
            {
                ProgramManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
