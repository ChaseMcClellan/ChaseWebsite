using CEM.ProgDec.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using CEM.ProgDec.UI;
using CEM.ProgDec.UI.Controllers;

namespace CEM.ProgDec.UI.Test
{
    [TestClass]
    public class utDegreeType
    {
        [TestMethod]
        public void LoadTest()
        {
            DegreeTypeController controller = new DegreeTypeController();
            var results = controller.Index() as ViewResult;
            var list = results.Model as List<DegreeType>;
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            DegreeTypeController controller = new DegreeTypeController();
            DegreeType degreeType = new DegreeType();
            degreeType.Name = "a";
            var results = controller.Create(degreeType, true) as RedirectToActionResult;
            Assert.AreEqual("Index", results.ActionName);
        }

        [TestMethod]
        public void InsertFailTest()
        {
            try
            {
                DegreeTypeController controller = new DegreeTypeController();
                DegreeType degreeType = new DegreeType();
                degreeType.Name = null;
                var results = controller.Create(degreeType, true) as RedirectToActionResult;
                Assert.AreEqual("Index", results.ActionName);
            }
            catch (Exception)
            {

                Assert.IsTrue(true);
            }
        }
    }
}
