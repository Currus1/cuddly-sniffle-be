using currus.Enums;
using System.ComponentModel.DataAnnotations;

namespace currus.Models.DTOs
{
    public class DriverPropsDto
    {
        [Required]
        [RegularExpression(@"^\d{8}$")]
        public string DriversLicense { get; set; }
        [Required]
        public string VehicleType { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{3}\d{3}$")]
        public string LicenseNumber { get; set; }

        public DriverPropsDto(string driversLicense, string vehicleType, string licenseNumber)
        {
            DriversLicense = driversLicense;
            LicenseNumber = licenseNumber;

            if(vehicleType != null && Enum.IsDefined(typeof(VehicleTypes), vehicleType))
            {
                VehicleType = vehicleType;
            }
        }
    }
}
