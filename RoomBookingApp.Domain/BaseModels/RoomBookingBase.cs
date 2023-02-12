namespace RoomBookingApp.Domain.BaseModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class RoomBookingBase : IValidatableObject
    {
        [Required]
        [StringLength(80)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date < DateTime.Now.Date)
            {
                //nameof(Date) or "Date", bas hayd eadvantage enno if we changed Date to BookingDate it will change automatically
                yield return new ValidationResult("Date Must be in the future", new[] { nameof(Date) });
            }
        }
    }
}
