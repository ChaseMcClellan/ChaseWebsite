using Microsoft.EntityFrameworkCore.Storage;

namespace CEM.ProgDec.PL.Test
{
    [TestClass]
    public class utDegreeType
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
            int expected = 3;

            int actual = 0;

           //get the rows 
           //SELECT * FROM tblDegreeType

            //var dc = new ProgDecEntities();

            var rows = dc.tblDegreeTypes;

            actual = rows.Count();

            Assert.AreEqual(expected, actual);
            
            
        }

        [TestMethod]
        public void InsertTest() 
        {
            //create a new row 
            tblDegreeType row = new tblDegreeType();

            // set the prop of the new row
            row.Id = 4;
            row.Name = "chase";

            //insert the row to the table
            dc.tblDegreeTypes.Add(row);
            int results = dc.SaveChanges();

            //assert
            Assert.IsTrue(results == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //get the row to update
            //select * from tbldegreetype where id =2;
            tblDegreeType row = dc.tblDegreeTypes.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                //change values for the columns
                row.Name = "new name";

                int results = dc.SaveChanges();

                Assert.AreNotEqual(0, results);

            }

        }

        [TestMethod]

        public void DeleteTest()
        {
            tblDegreeType row = dc.tblDegreeTypes.Where(dt => dt.Id == 2).FirstOrDefault();
            if (row != null)
            {
                dc.tblDegreeTypes.Remove(row);


                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }

        }

    }
}