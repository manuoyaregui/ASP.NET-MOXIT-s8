using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if ( customer.MembershipTypeId == MembershipType.Unknown || 
                 customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthday == null || customer.MembershipTypeId == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            var age = DateTime.Today.Year - customer.Birthday.Value.Year;

             return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer Should be at least 18 years old.");
        }
    }
}
