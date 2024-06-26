using CEM.ProgDec.BL.Models;
using CEM.ProgDec.PL;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CEM.ProgDec.BL
{
    public static class DeclarationManager
    {
        private const string NOTFOUND_MESSAGE = "Row does not exist";

        public static int Insert(Declaration declaration, bool rollback = false)
        {
			try
			{
				int results = 0;
				using(ProgDecEntities dc = new ProgDecEntities()) 
				{
					IDbContextTransaction dbContextTransaction = null;
					if(rollback) dbContextTransaction = dc.Database.BeginTransaction();
					tblDeclaration row = new tblDeclaration();
					row.Id = dc.tblDeclarations.Any() ? dc.tblDeclarations.Max(d=>d.Id) +1 : 1;

					row.ProgramId = declaration.ProgramId;
					row.StudentId= declaration.StudentId;
					row.ChangeDate = DateTime.Now;
					declaration.Id = row.Id;
					dc.tblDeclarations.Add(row);
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
        
        public static List<Declaration> Load(int? programId =null) 
        {
            try
            {
                List<Declaration> rows = new List<Declaration>();
                using(ProgDecEntities dc= new ProgDecEntities())
                {
                    var declarations = (from pd in dc.tblDeclarations
                                        join s in dc.tblStudents on pd.StudentId equals s.Id
                                        join p in dc.tblPrograms on pd.ProgramId equals p.Id
                                        join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                                        where pd.ProgramId == programId || programId== null
                                        orderby pd.ChangeDate
                                        select new
                                        {
                                            Id = pd.Id,
                                            ProgramId = pd.ProgramId,
                                            StudentId = pd.StudentId,
                                            ChangeDate = pd.ChangeDate,
                                            ProgramName = p.Name,
                                            s.FirstName, 
                                            s.LastName,
                                            DegreeTypeName = dt.Name

                                        }).ToList();
                    declarations.ForEach(pd => rows.Add(new Declaration
                    {
                        Id = pd.Id,
                        ProgramId = pd.ProgramId,
                        StudentId = pd.StudentId,
                        ChangeDate = pd.ChangeDate,
                        ProgramName = pd.ProgramName,
                        StudentName = pd.LastName + ", " + pd.FirstName,
                        DegreeTypeName = pd.DegreeTypeName
                    }));
                }
                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public static int Update(Declaration declaration, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblDeclaration row = dc.tblDeclarations.FirstOrDefault(dc=>dc.Id == declaration.Id);

                    row.ProgramId = declaration.ProgramId;
                    row.StudentId = declaration.StudentId;
                    row.ChangeDate = DateTime.Now;

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

        public static Declaration LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    var row = (from pd in dc.tblDeclarations
                               join s in dc.tblStudents on pd.StudentId equals s.Id
                               join p in dc.tblPrograms on pd.ProgramId equals p.Id
                               join dt in dc.tblDegreeTypes on p.DegreeTypeId equals dt.Id
                               where pd.Id == id
                               select new
                               {
                                   Id = pd.Id,
                                   ProgramId = pd.ProgramId,
                                   StudentId = pd.StudentId,
                                   ChangeDate = pd.ChangeDate,
                                   ProgramName = p.Name,
                                   s.FirstName, 
                                   s.LastName,
                                   DegreeTypeName = dt.Name
                               }).FirstOrDefault();
                    if(row  != null)
                    {
                        return new Declaration
                        {
                            Id = row.Id,
                            ProgramId = row.ProgramId,
                            StudentId = row.StudentId,
                            ChangeDate = row.ChangeDate,
                            StudentName = row.LastName + ", " + row.FirstName,
                            DegreeTypeName = row.DegreeTypeName,
                            ProgramName= row.ProgramName
                        };
                    }
                    else
                    {
                        throw new Exception(NOTFOUND_MESSAGE);

                    }
                }
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

                    tblDeclaration row = dc.tblDeclarations.FirstOrDefault(dc => dc.Id == id);

                    dc.tblDeclarations.Remove(row);
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
    }
}
