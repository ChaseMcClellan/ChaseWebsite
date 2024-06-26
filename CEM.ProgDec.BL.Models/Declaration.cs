using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEM.ProgDec.BL.Models
{
    public class Declaration
    {
        //field

        //prop
        
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(DataFormatString ="{0:mm/dd/yyyy}")]
        public DateTime ChangeDate { get; set; }
        [DisplayName("Program Name")]
        public string ProgramName { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [DisplayName("Degree Type Name")]
        public string DegreeTypeName { get; set; }
        //const


        //methods
    }
}
