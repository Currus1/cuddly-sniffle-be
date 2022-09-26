using System.Collections.Generic;

namespace currus.Models
{
    public class DriverModel
    {
        private string name { get; set; }
        private string surname { get; set; }
        private DateTime birthday { get; set; }
        private string email { get; set; }
        private string phoneNumber { get; set; }
        private string vechileType { get; set; }
        private string licenseNumber { get; set; }

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
