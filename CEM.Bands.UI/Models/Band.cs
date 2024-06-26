using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CEM.Bands.UI.Models
{
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }

        [DisplayName("Date Founded")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateFounded { get; set; }
        public string Image { get; set; }

        


    }
}
