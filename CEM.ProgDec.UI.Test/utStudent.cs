using CEM.ProgDec.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using CEM.ProgDec.UI;
using CEM.ProgDec.UI.Controllers;

namespace CEM.ProgDec.UI.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void LoadTest()
        {
            StudentController controller = new StudentController();
            var results = controller.Index() as ViewResult;
            var list = results.Model as List<Student>;
            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void DetailsTest()
        {
            StudentController controller = new StudentController();
            var results = controller.Details(3) as ViewResult;
            var student = results.Model as Student;
            Assert.AreEqual(3, student.Id);
        }

        [TestMethod]
        public void DeleteTest()
        {
            StudentController controller = new StudentController();
            var results = controller.Delete(3, null, true) as RedirectToActionResult;
            Assert.AreEqual("Index", results.ActionName);
        }

        [TestMethod]
        public void InsertTest()
        {
            StudentController controller = new StudentController();
            Student student = new Student();
            student.FirstName = "a";
            student.LastName = "b";
            student.StudentId = "500586693";

            var results = controller.Create(student, true) as RedirectToActionResult;
            Assert.AreEqual("Index", results.ActionName);
        }

        [TestMethod]
        public void UpdateTest() 
        {
            StudentController controller = new StudentController();
            var results = controller.Details(3) as ViewResult;
            Student student = results.Model as Student;
            student.FirstName = "blah";
            student.LastName = "halb";
            student.StudentId = "500346693";

            var results2 = controller.Edit(student.Id, student, true) as RedirectToActionResult;
            Assert.AreEqual("Index", results2.ActionName);

        }

    }
}