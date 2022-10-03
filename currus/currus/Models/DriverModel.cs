using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace currus.Models
{
    public class DriverModel : IComparable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
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

        public int CompareTo(object? obj)   // Sorting drivers by their name alhapebtically
        {
            DriverModel other = (DriverModel)obj;   // narrowing type conversion
            if (string.Compare(Name, other.Name, StringComparison.Ordinal) < 0) 
            {
                return -1;
            }
            else if (string.Compare(Name, other.Name, StringComparison.Ordinal) > 0)
            {
                return 1;
            }
            else
                return 0;
        }
    }
}
