namespace currus.Models;

public class Driver : User
{
    public string VehicleType { get; set; }
    public string LicenseNumber { get; set; }

    public Driver(int id, string name, string surname, DateTime birthdate, string email, string phoneNumber,
        string vehicleType, string licenseNumber) : base(id, name, surname, birthdate, email, phoneNumber)
    {
        VehicleType = vehicleType;
        LicenseNumber = licenseNumber;
    }
}