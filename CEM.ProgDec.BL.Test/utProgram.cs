using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEM.ProgDec.BL;
using CEM.ProgDec.BL.Models;

namespace CEM.ProgDec.BL.Test
{
    [TestClass]
    public class utProgram
    {
        [TestMethod]
        public void InsertTest()
        {
            Program program = new Program
            {
                Name = "Sally",
                DegreeTypeId = 4
            };

            Assert.AreEqual(1, ProgramManager.Insert(program, true));
        }

        [TestMethod]
        public void UpdateTest()
        {
            Program program = ProgramManager.LoadById(3);
            program.Name = "Bob";
            Assert.IsTrue(ProgramManager.Update(program, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, ProgramManager.Delete(2, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(16, ProgramManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, ProgramManager.LoadById(1).Id);
        }
    }
}
