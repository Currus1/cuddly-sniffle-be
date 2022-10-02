using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace currus.Models;
public class UserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
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
