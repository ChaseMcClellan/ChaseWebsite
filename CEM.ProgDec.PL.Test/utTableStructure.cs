using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CEM.ProgDec.PL.Test
{
    public enum DataTypes
    {
        String = 0,
        Double,
        Int32,
        Guid,
        DateTime
    }

    [TestClass]
    public class utTableStructure
    {
        const string connstrlocal = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEM.ProgDec.DB;Integrated Security=True";

        private bool CheckColumnDefinition(Type tableType,
                                          string columnName,
                                          DataTypes dataType,
                                          ref string message,
                                          ref string errmessage)
        {
            try
            {
                if (tableType.GetProperty(columnName) != null)
                {
                    var property = tableType.GetProperty(columnName);
                    if (property.PropertyType.Name.Equals(dataType.ToString()))
                    {
                        message += "Passed: " + tableType.Name + "." + columnName + " (" + dataType.ToString() + ")\r\n";
                        return true;
                    }
                    else
                    {
                        errmessage += "Failed: Data Type: "
                                    + tableType.Name
                                    + "." + columnName
                                    + " (" + property.PropertyType.Name
                                    + " is not "
                                    + dataType.ToString()
                                    + ")\r\n";

                        message += "Failed: Data Type: "
                                    + tableType.Name
                                    + "." + columnName
                                    + " (" + property.PropertyType.Name
                                    + " is not "
                                    + dataType.ToString()
                                    + ")\r\n";

                        return false;
                    }
                }
                else
                {
                    errmessage += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                    message += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                errmessage += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                message += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                return false;
            }

        }

        public class ColumnInfo
        {
            public string Name { get; set; }
            public DataTypes DataType { get; set; }
            public string DataTypeDesc { get; set; }
            public ColumnInfo(string name, DataTypes dataType)
            {
                Name = name;
                DataType = dataType;
                DataTypeDesc = dataType.ToString();


            }
            public void setDataType(DataTypes dataType)
            {
                DataType = dataType;
            }

        }
        private class Structure
        {
            public string TableName { get; set; }
            public Type Type { get; set; }
            public List<ColumnInfo> ColumnInfos { get; set; }
        }

        [TestMethod]
        public void CheckTableStructureLocal()
        {
            string message = "Results:\r\n";
            string errMessage = "Errors:\r\n";
            bool results = true;
            List<Structure> structures = new List<Structure>();

            structures = ReadStructures();
            if (structures.Count != 4)
                structures = CreateStructures();


            foreach (Structure structure1 in structures)
            {
                foreach (ColumnInfo column in structure1.ColumnInfos)
                {
                    results = CheckColumnDefinition(structure1.Type,
                                                    column.Name,
                                                    column.DataType,
                                                    ref message,
                                                    ref errMessage) ? results : false;
                }
                CheckCounts(structure1.Type, ref message, ref errMessage);
            }

            
            Debug.WriteLine(message);
            Assert.IsTrue(results, errMessage);

        }

        public int GetRowCount<T>(DbContext context)
        {
            // Get the generic type definition
            var method = typeof(DbContext).GetMethod(
                nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(typeof(T));

            var iEnumerable = method.Invoke(context, null) as IQueryable<T>;

            return (iEnumerable ?? throw new InvalidOperationException()).Count();
        }
        
        private bool CheckCounts(Type tableType, ref string message, 
                                 ref string errmessage)
        {
            try
            {
                
                SqlConnection connection;
                SqlCommand command;

                connection = new SqlConnection();
                connection.ConnectionString = connstrlocal;
                connection.Open();

                string ssql = "SELECT COUNT(*) FROM " + tableType.Name;
                command = new SqlCommand(ssql, connection);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int rows = reader.GetInt32(0);
                reader.Close();

                if (rows > 0)
                {
                    message += "Passed: " + tableType.Name + ". Rows: " + rows + "\r\n";
                    return true;
                }
                else
                {
                    errmessage += "Failed: Missing Rows: "
                                   + tableType.Name
                                   + ")\r\n";
                    message += "Failed: Missing Rows: "
                                   + tableType.Name
                                   + ")\r\n";
                    return false;
                }

                
            }
            catch (System.Exception ex)
            {
                errmessage += "Failed: Get Count for " + tableType.Name + "\r\n";
                message += "Failed: Get Count for " + tableType.Name + "\r\n";
                return false;
            }
        }


        private static List<Structure> ReadStructures()
        {
            List<Structure> structures = new List<Structure>();
            try
            {
                StreamReader sr = new StreamReader("structure.json");
                String data = sr.ReadToEnd();
                structures = JsonConvert.DeserializeObject<List<Structure>>(data);
                Debug.WriteLine(structures.Count + " structures found.");
                sr.Close();
            }
            catch (Exception)
            {
                Debug.WriteLine("structure.json not found.");
            }

            return structures;
        }

        private static List<Structure> CreateStructures()
        {
            List<Structure> structures = new List<Structure>();
            Structure structure = new Structure();

            structure.Type = typeof(tblProgram);
            structure.TableName = "tblProgram";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("Id", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Name", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("DegreeTypeId", DataTypes.Int32));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblDegreeType);
            structure.TableName = "tblDegreeType";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("Id", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Name", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblStudent);
            structure.TableName = "tblStudent";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("Id", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("StudentId", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("FirstName", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("LastName", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblDeclaration);
            structure.TableName = "tblDeclaration";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("Id", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("ChangeDate", DataTypes.DateTime));
            structure.ColumnInfos.Add(new ColumnInfo("Id", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Id", DataTypes.Int32));
            structures.Add(structure);

            string serializedObject = JsonConvert.SerializeObject(structures);
            StreamWriter sw = File.CreateText("structure.json");
            sw.WriteLine(serializedObject);
            sw.Close();
            Debug.WriteLine("Rewrote structure.json.");
            return structures;
        }
    }
}
