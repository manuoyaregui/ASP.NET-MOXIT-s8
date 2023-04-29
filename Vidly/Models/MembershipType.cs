using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public short SignUpFree { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        public static readonly byte 
            Unknown = 0,
            PayAsYouGo = 1;
/*        public static readonly byte PayAsYouGo = 2;
        public static readonly byte PayAsYouGo = 3;*/
    }
}
