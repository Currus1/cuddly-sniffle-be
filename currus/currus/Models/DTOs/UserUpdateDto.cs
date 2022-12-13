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
        public string Email { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string? DriversLicense { get; set; }
        [Required]
        public string? VehicleType { get; set; }
        [Required]
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
            VehicleType = vehicleType;
            LicenseNumber = licenseNumber;
        }
    }
}
