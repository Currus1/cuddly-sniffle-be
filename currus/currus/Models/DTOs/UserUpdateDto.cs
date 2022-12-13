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
        public DateTime BirthDate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string DriversLicense { get; set; }
        public string VehicleType { get; set; }
        public string LicenseNumber { get; set; }

        public DriverDto(string name, string surname, string email,
            DateTime birthDate, string phoneNumber, string driversLicense, string vehicleType, string licenseNumber)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            DriversLicense = driversLicense;
            VehicleType = vehicleType;
            LicenseNumber = licenseNumber;
        }
    }
}
