using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEM.ProgDec.BL;
using CEM.ProgDec.BL.Models;

namespace CEM.ProgDec.BL.Test
{
    [TestClass]
    public class utStudent
    {
        [TestMethod]
        public void InsertTest()
        {
            Student student = new Student
            {
                FirstName = "Chase",
                LastName = "McClellan",
                StudentId = "500196790"
            };

            Assert.AreEqual(1, StudentManager.Insert(student, true));
        }

        [TestMethod]
        public void InsertNotRollbackTest()
        {
            Student student = new Student
            {
                FirstName = "Chase",
                LastName = "McClellan",
                StudentId = "500196790"
            };

            StudentManager.Insert(student);

            Student insertedStudent = StudentManager.LoadById(student.Id);
            StudentManager.Delete(student.Id);
            Assert.IsNotNull(insertedStudent);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Student student = StudentManager.LoadById(3);
            student.FirstName = "Bob";
            Assert.IsTrue(StudentManager.Update(student, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Assert.AreEqual(1, StudentManager.Delete(2, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(5, StudentManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, StudentManager.LoadById(1).Id);
        }
    }
}