using System.Globalization;

namespace currus.Models;
public class UserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public UserModel(string name, string surname, DateTime birthdate, string email, string phone)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        Birthdate = birthdate;
    }


}
