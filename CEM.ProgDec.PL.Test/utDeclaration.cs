using Microsoft.EntityFrameworkCore.Storage;

namespace CEM.ProgDec.PL.Test
{
    [TestClass]
    public class utDeclaration
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
            int expected = 5;

            int actual = 0;

            //get the rows 
            //SELECT * FROM tblDeclaration

            //var dc = new ProgDecEntities();

            var rows = dc.tblDeclarations;

            actual = rows.Count();

            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void InsertTest()
        {
            //create a new row 
            tblDeclaration row = new tblDeclaration();

            // set the prop of the new row
            row.StudentId = 3;
            row.ProgramId = 4;
            row.ChangeDate = DateTime.Now;

            //insert the row to the table
            dc.tblDeclarations.Add(row);
            int results = dc.SaveChanges();

            //assert
            Assert.IsTrue(results == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //get the row to update
            //select * from tbldegreetype where id =2;
            tblDeclaration row = dc.tblDeclarations.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                //change values for the columns
                row.StudentId = 3;
                row.ProgramId = 4;
                row.ChangeDate = new DateTime(2002, 4, 12);

                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);

            }

        }

        [TestMethod]

        public void DeleteTest()
        {
            tblDeclaration row = dc.tblDeclarations.Where(dt => dt.Id == 2).FirstOrDefault();
            if (row != null)
            {
                dc.tblDeclarations.Remove(row);


                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }

        }

    }
}