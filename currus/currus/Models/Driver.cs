namespace currus.Models;

public class Driver : User, IComparable
{
    public string VehicleType { get; set; }
    public string LicenseNumber { get; set; }
    public Driver(int id, string name, string surname, DateTime birthdate, string email, string phoneNumber,
        string vehicleType, string licenseNumber) : base(id, name, surname, birthdate, email, phoneNumber)
    {
        VehicleType = vehicleType;
        LicenseNumber = licenseNumber;
    }
    public int CompareTo(object? obj) // Sorting users by their name alhapebtically
    {
        var other = (Driver)obj; // narrowing type conversion
        if (string.Compare(Name, other.Name, StringComparison.Ordinal) < 0)
            return -1;
        if (string.Compare(Name, other.Name, StringComparison.Ordinal) > 0)
            return 1;
        return 0;
    }
}