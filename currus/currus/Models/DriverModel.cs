using System.Collections.Generic;

namespace currus.Models
{
    public class DriverModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthday { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string vechileType { get; set; }
        public string licenseNumber { get; set; }

        public DriverModel(string name, string surname, DateTime birthday, string email, string phoneNumber, string vechileType, string licenseNumber)
        {
            this.name = name;
            this.surname = surname;
            this.birthday = birthday;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.vechileType = vechileType;
            this.licenseNumber = licenseNumber;
        }
    }
}
