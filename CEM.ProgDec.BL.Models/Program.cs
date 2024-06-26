using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEM.ProgDec.BL.Models
{
    public class Program
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int DegreeTypeId { get; set; }
        [DisplayName("Degree Type Name")]
        public string DegreeTypeName { get; set; }




    }
}
