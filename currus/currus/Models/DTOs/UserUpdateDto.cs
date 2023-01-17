using currus.Enums;
using System.ComponentModel.DataAnnotations;

namespace currus.Models.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [MaxLength(256)]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]
        public string Email { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [RegularExpression(@"^((86|\+3706)\d{7})$")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^\d{8}$")]
        public string? DriversLicense { get; set; }
        [Required]
        public string? VehicleType { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{3}\d{3}$")]
        public string? LicenseNumber { get; set; }

        public UserUpdateDto(string name, string surname, string email,
            DateTime birthDate, string phoneNumber, string driversLicense, string vehicleType, string licenseNumber)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthdate = birthDate;
            PhoneNumber = phoneNumber;
            DriversLicense = driversLicense;
            LicenseNumber = licenseNumber;

            if (vehicleType != null && Enum.IsDefined(typeof(VehicleTypes), vehicleType))
            {
                VehicleType = vehicleType;
            }
        }
    }
}
