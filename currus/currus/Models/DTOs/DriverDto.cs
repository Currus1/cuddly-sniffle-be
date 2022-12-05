using System.ComponentModel.DataAnnotations;

namespace currus.Models.DTOs
{
    public class DriverDto
    {
        [Required]
        public string? DriversLicense { get; set; }
        [Required]
        public string? VehicleType { get; set; }
        [Required]
        public string? LicenseNumber { get; set; }
    }
}
