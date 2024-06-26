using CEM.ProgDec.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using CEM.ProgDec.UI;
using CEM.ProgDec.UI.Controllers;
using CEM.ProgDec.UI.ViewModels;
using CEM.ProgDec.BL;

namespace CEM.ProgDec.UI.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void LoadTest()
        {
            ProgramController controller = new ProgramController();
            var results = controller.Index() as ViewResult;
            var list = results.Model as List<Program>;
            Assert.AreEqual(16, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            ProgramController controller = new ProgramController();
            ProgramVM programVM = new ProgramVM();
            programVM.Program = new Program();
            programVM.DegreeTypes = DegreeTypeManager.Load();

            programVM.Program.Name = "a";
            programVM.Program.DegreeTypeId = 3;
            var results = controller.Create(programVM, true) as RedirectToActionResult;
            Assert.AreEqual("Index", results.ActionName);
        }


    }
}
