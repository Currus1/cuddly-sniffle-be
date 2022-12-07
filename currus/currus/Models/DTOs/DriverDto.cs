using System.ComponentModel.DataAnnotations;

namespace currus.Models.DTOs
{
    public class DriverDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string? Number { get; set; }
        [Required]
        public string? DriversLicense { get; set; }
        [Required]
        public string? VehicleType { get; set; }
        [Required]
        public string? LicenseNumber { get; set; }

        public DriverDto(string? name, string? surname, string? email, 
            DateTime birthDate, string? number, string? driversLicense, string? vehicleType, string? licenseNumber)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            Number = number;
            DriversLicense = driversLicense;
            VehicleType = vehicleType;
            LicenseNumber = licenseNumber;
        }
    }
}
