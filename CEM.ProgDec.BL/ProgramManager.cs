using CEM.ProgDec.BL.Models;
using CEM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace CEM.ProgDec.BL

{
    public static class ProgramManager
    {
        //methods
        public static int Insert(string name, int degreeTypeId, ref int id)
        {
            try
            {
                Program program = new Program
                {
                    Name = name,
                    DegreeTypeId = degreeTypeId
                };
                int results = Insert(program);
                id = program.Id;
                return results;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Program> Load()
        {
            try
            {
                List<Program> rows = new List<Program>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from p in dc.tblPrograms
                     join dt in dc.tblDegreeTypes
                     on p.DegreeTypeId equals dt.Id
                     orderby p.Name ascending
                     select new
                     {
                         p.Id,
                         p.Name,
                         p.DegreeTypeId,
                         DegreeTypeName = dt.Name
                    
                     })
                     .ToList()
                     .ForEach(program => rows.Add(new Program
                     {
                         Id = program.Id,
                         Name = program.Name,
                         DegreeTypeId = program.DegreeTypeId, DegreeTypeName= program.DegreeTypeName
                     }));
                }
                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Program LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    var row = (from p in dc.tblPrograms
                               join dt in dc.tblDegreeTypes
                               on p.DegreeTypeId equals dt.Id
                               where p.Id == id
                               select new
                               {
                                   p.Id,
                                   p.Name,
                                   p.DegreeTypeId,
                                   DegreeTypeName = dt.Name
                               }).FirstOrDefault();

                    if (row != null)
                    {
                        return new Program
                        {
                            Id = row.Id,
                            Name = row.Name,
                            DegreeTypeId = row.DegreeTypeId,
                            DegreeTypeName = row.DegreeTypeName
                        };
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(Program program, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();




                    tblProgram row = new tblProgram();

                    row.Id = dc.tblPrograms.Any() ? dc.tblPrograms.Max(s => s.Id) + 1 : 1;
                    
                    row.Name = program.Name;
                    row.DegreeTypeId = program.DegreeTypeId;

                    //important backfill the id
                    program.Id = row.Id;

                    dc.tblPrograms.Add(row);
                    results = dc.SaveChanges();
                    if (rollback) dbContextTransaction.Rollback();

                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Update(Program program, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();




                    tblProgram row = dc.tblPrograms.FirstOrDefault(s => s.Id == program.Id);
                    if (row != null)
                    {
                        row.Name = program.Name;
                        row.DegreeTypeId = program.DegreeTypeId;


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
            catch (Exception)
            {
                throw;
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

                    tblProgram row = dc.tblPrograms.FirstOrDefault(s => s.Id == id);
                    if (row != null)
                    {
                        dc.tblPrograms.Remove(row);
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}