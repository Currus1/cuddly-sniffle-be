using System.ComponentModel.DataAnnotations;

namespace currus.Models.DTOs
{
    public class DriverPropsDto
    {
        [Required]
        public string DriversLicense { get; set; } = "";
        [Required]
        public string VehicleType { get; set; } = "";
        [Required]
        public string LicenseNumber { get; set; } = "";

        public DriverPropsDto(string driversLicense, string vehicleType, string licenseNumber)
        {
            DriversLicense = driversLicense;
            VehicleType = vehicleType;
            LicenseNumber = licenseNumber;
        }
    }
}
