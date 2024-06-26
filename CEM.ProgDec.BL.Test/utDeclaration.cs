using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEM.ProgDec.BL;
using CEM.ProgDec.BL.Models;

namespace CEM.ProgDec.BL.Test
{
    [TestClass]
    public class utDeclaration
    {
        [TestMethod]
        public void InsertTest()
        {
            Declaration student = new Declaration
            {
                StudentId= 4,
                ProgramId= 3,

            };

            Assert.AreEqual(1, DeclarationManager.Insert(student, true));
        }

        [TestMethod]
        public void UpdateTest()
        {
            Declaration student = DeclarationManager.LoadById(3);
            student.ProgramId = 11;
            Assert.IsTrue(DeclarationManager.Update(student, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, DeclarationManager.Delete(2, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(5, DeclarationManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, DeclarationManager.LoadById(1).Id);
        }
    }
}