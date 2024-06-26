using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEM.ProgDec.BL.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}

