using System;
using System.Collections.Generic;

namespace CEM.ProgDec.PL;

public partial class tblProgram
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DegreeTypeId { get; set; }
}
