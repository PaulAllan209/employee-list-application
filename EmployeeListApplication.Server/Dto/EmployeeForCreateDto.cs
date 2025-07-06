using System.ComponentModel.DataAnnotations;

namespace EmployeeListApplication.Server.Dto
{
    public record EmployeeForCreateDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; init; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; init; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; init; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; init; }
    }
}
