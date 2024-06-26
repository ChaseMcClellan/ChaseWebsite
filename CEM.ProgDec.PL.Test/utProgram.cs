using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace CEM.ProgDec.PL.Test
{
    [TestClass]

    public class utProgram
    {
        protected ProgDecEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new ProgDecEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]

        public void TestCleanup() 
        {
            transaction.Rollback();
            transaction.Dispose();
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(16, dc.tblPrograms.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblProgram row = new tblProgram();

            row.Id= -99;
            row.DegreeTypeId = 2;
            row.Name = "Test";

            dc.tblPrograms.Add(row);
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblProgram row = dc.tblPrograms.FirstOrDefault();
            row.DegreeTypeId = 3;
            row.Name = "NewName";
            int results = dc.SaveChanges();
            Assert.AreNotEqual(0, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblProgram row = dc.tblPrograms.FirstOrDefault();
            dc.tblPrograms.Remove(row);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(0, result);
        }


    }
}
