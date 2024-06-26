using CEM.ProgDec.BL.Models;
using CEM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata.Ecma335;

namespace CEM.ProgDec.BL

{
    public static class StudentManager
    {
        //methods
        public static int Insert(string firstName, string lastName, string studentId, ref int id)
        {
            try
            {
                Student student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    StudentId = studentId
                };
                int results = Insert(student);
                id= student.Id;
                return results;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<Student> Load()
        {
            try
            {
                List<Student> rows = new List<Student>(); 

                using(ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblStudents
                     select new
                     {
                         s.Id,
                         s.FirstName,
                         s.LastName,
                         s.StudentId
                     })
                     .ToList()
                     .ForEach(student => rows.Add(new Student
                     {
                         Id = student.Id,
                         FirstName = student.FirstName,
                         LastName = student.LastName,
                         StudentId = student.StudentId
                     }));
                }
                return rows;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Student LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblStudent row = dc.tblStudents.Where(s=> s.Id == id).FirstOrDefault();

                    if (row != null)
                    {
                        return new Student
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            StudentId = row.StudentId
                        };
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(Student student, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if(rollback) dbContextTransaction = dc.Database.BeginTransaction();




                    tblStudent row = new tblStudent();

                    row.Id = dc.tblStudents.Any() ? dc.tblStudents.Max(s=> s.Id)+1 : 1;
                    row.FirstName = student.FirstName;
                    row.LastName = student.LastName;
                    row.StudentId =  student.StudentId;

                    //important backfill the id
                    student.Id = row.Id;

                    dc.tblStudents.Add(row);
                    results = dc.SaveChanges();
                    if (rollback) dbContextTransaction.Rollback();

                }

                return results;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int Update(Student student, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();




                    tblStudent row = dc.tblStudents.FirstOrDefault(s => s.Id == student.Id);
                    if (row != null)
                    {
                        row.FirstName = student.FirstName;
                        row.LastName = student.LastName;
                        row.StudentId = student.StudentId;


                        results = dc.SaveChanges();
                        if (rollback) dbContextTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("row does not exist");
                    }

                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblStudent row = dc.tblStudents.FirstOrDefault(s => s.Id == id);
                    if (row != null)
                    {
                        dc.tblStudents.Remove(row);
                        results = dc.SaveChanges();

                        if (rollback) dbContextTransaction.Rollback();

                    }
                    else
                    {
                        throw new Exception("row does not exist");
                    }

                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}