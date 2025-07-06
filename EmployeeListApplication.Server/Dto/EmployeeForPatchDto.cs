using System.ComponentModel.DataAnnotations;

namespace EmployeeListApplication.Server.Dto
{
    public record EmployeeForPatchDto
    {
        [Required(AllowEmptyStrings = false)]
        public string? FirstName { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string? LastName { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string? Email { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string? PhoneNumber { get; init; }

        [Required(AllowEmptyStrings = false)]
        public string? Position { get; init; }
    }
}
