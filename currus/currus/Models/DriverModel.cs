using System.Collections.Generic;

namespace currus.Models
{
    public class DriverModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleType { get; set; }
        public string LicenseNumber { get; set; }

        public DriverModel(string name, string surname, DateTime birthday, string email, string phoneNumber, string vehicleType, string licenseNumber)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Email = email;
            PhoneNumber = phoneNumber;
            VehicleType = vehicleType;
            LicenseNumber = licenseNumber;
        }
    }
}
