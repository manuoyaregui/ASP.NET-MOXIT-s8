using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required] 
        public DateTime DateAdded { get; set; }

        [Range(1,20)]
        public short Stock { get; set; }

        [Display(Name = "Genre")]
        
        public Genre? Genre { get; set; }
        
        [Required]
        public short GenreID { get; set; }
    }
}
