using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Vidly.Models
{
    public class Customer
    {

        public int Id { get; set; }


        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        

        public bool IsSubscribedToNewsletter { get; set; }
        
        public MembershipType? MembershipType { get; set; }

        [Required]
        public byte MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
    }
}
