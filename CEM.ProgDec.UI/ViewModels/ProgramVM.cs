
using CEM.ProgDec.BL.Models;

namespace CEM.ProgDec.UI.ViewModels
{
    public class ProgramVM
    {
        public BL.Models.Program Program{get; set;}
        public List<DegreeType> DegreeTypes { get; set;}
    }
}
