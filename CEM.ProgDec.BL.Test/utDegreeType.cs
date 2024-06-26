using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEM.ProgDec.BL;
using CEM.ProgDec.BL.Models;

namespace CEM.ProgDec.BL.Test
{
    [TestClass]
    public class utDegreeType
    {
        [TestMethod]
        public void InsertTest()
        {
            DegreeType degreetype = new DegreeType
            {
                Name = "Sally",
            };

            Assert.AreEqual(1, DegreeTypeManager.Insert(degreetype, true));
        }

        [TestMethod]
        public void UpdateTest()
        {
            DegreeType degreetype = DegreeTypeManager.LoadById(3);
            degreetype.Name = "Bob";
            Assert.IsTrue(DegreeTypeManager.Update(degreetype, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, DegreeTypeManager.Delete(2, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, DegreeTypeManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, DegreeTypeManager.LoadById(1).Id);
        }
    }
}

