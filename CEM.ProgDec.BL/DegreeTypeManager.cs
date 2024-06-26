using CEM.ProgDec.BL.Models;
using CEM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata.Ecma335;

namespace CEM.ProgDec.BL

{
    public static class DegreeTypeManager
    {
        //methods
        public static int Insert(string name, ref int id)
        {
            try
            {
                DegreeType degreetype = new DegreeType
                {
                    Name = name,
                };
                int results = Insert(degreetype);
                id = degreetype.Id;
                return results;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<DegreeType> Load()
        {
            try
            {
                List<DegreeType> rows = new List<DegreeType>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblDegreeTypes
                     select new
                     {
                         s.Id,
                         s.Name,
                         s.DegreeTypeId

                     })
                     .ToList()
                     .ForEach(degreetype => rows.Add(new DegreeType
                     {
                         Id = degreetype.Id,
                         Name = degreetype.Name,
                     }));
                }
                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DegreeType LoadById(int id)
        {
            try
            {

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblDegreeType row = dc.tblDegreeTypes.Where(s => s.Id == id).FirstOrDefault();

                    if (row != null)
                    {
                        return new DegreeType
                        {
                            Id = row.Id,
                            Name = row.Name
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

        public static int Insert(DegreeType degreetype, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();




                    tblDegreeType row = new tblDegreeType();

                    row.Id = dc.tblDegreeTypes.Any() ? dc.tblDegreeTypes.Max(s => s.Id) + 1 : 1;

                    row.Name = degreetype.Name;

                    //important backfill the id
                    degreetype.Id = row.Id;

                    dc.tblDegreeTypes.Add(row);
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

        public static int Update(DegreeType degreetype, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();




                    tblDegreeType row = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == degreetype.Id);
                    if (row != null)
                    {
                        row.Name = degreetype.Name;


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

                    tblDegreeType row = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == id);
                    if (row != null)
                    {
                        dc.tblDegreeTypes.Remove(row);
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
