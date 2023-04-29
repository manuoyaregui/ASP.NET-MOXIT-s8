using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Range(1, 20)]
        public short Stock { get; set; }

        public GenreDto Genre { get; set; }

        [Required]
        public short GenreID { get; set; }
    }
}
