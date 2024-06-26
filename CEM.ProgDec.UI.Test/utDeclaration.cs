using CEM.ProgDec.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using CEM.ProgDec.UI;
using CEM.ProgDec.UI.Controllers;

namespace CEM.ProgDec.UI.Test
{
    [TestClass]
    public class utDeclaration
    {
        [TestMethod]
        public void BrowseTest()
        {
            DeclarationController controller = new DeclarationController();
            var results = controller.Browse(10) as ViewResult;
            var list = results.Model as List<Declaration>;
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void LoadTest()
        {
            DeclarationController controller = new DeclarationController();
            var results = controller.Index() as ViewResult;
            var list = results.Model as List<Declaration>;
            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            DeclarationController controller = new DeclarationController();
            Declaration declaration = new Declaration();
            declaration.ProgramName = "Test";
            declaration.StudentName = "Test";
            declaration.DegreeTypeName = "Test";
            declaration.ChangeDate = DateTime.Now;
            var results = controller.Create(declaration, true) as RedirectToActionResult;
            Assert.AreEqual("Index", results.ActionName);
        }


    }
}