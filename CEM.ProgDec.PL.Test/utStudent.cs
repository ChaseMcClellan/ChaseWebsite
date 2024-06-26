using Microsoft.EntityFrameworkCore.Storage;

namespace CEM.ProgDec.PL.Test
{
    [TestClass]
    public class utStudent
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
            //SELECT * FROM tblStudent

            //var dc = new ProgDecEntities();

            var rows = dc.tblStudents;

            actual = rows.Count();

            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void InsertTest()
        {
            //create a new row 
            tblStudent row = new tblStudent();

            // set the prop of the new row
            row.Id = -99;
            row.FirstName = "Me";
            row.LastName = "You";
            row.StudentId = "111111111";

            //insert the row to the table
            dc.tblStudents.Add(row);
            int results = dc.SaveChanges();

            //assert
            Assert.IsTrue(results == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //get the row to update
            //select * from tbldegreetype where id =2;
            tblStudent row = dc.tblStudents.Where(dt => dt.Id == 2).FirstOrDefault();

            if (row != null)
            {
                //change values for the columns
                row.FirstName = "Me";
                row.LastName = "You";
                row.StudentId = "111111111";

                int results = dc.SaveChanges();

                Assert.AreNotEqual(0, results);

            }

        }

        [TestMethod]

        public void DeleteTest()
        {
            tblStudent row = dc.tblStudents.Where(dt => dt.Id == 2).FirstOrDefault();
            if (row != null)
            {
                dc.tblStudents.Remove(row);


                int results = dc.SaveChanges();
                Assert.AreNotEqual(0, results);
            }

        }

    }
}