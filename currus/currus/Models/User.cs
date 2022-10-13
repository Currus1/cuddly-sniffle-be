using System.ComponentModel.DataAnnotations;

namespace currus.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }

    [RegularExpression(
        @"^([a-zA-Z0-9_\-\.]+)@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})$")]
    public string Email { get; set; }

    [RegularExpression(@"^((86|\+3706)\d{7})$")]
    public string PhoneNumber { get; set; }

    public User(int id, string name, string surname, DateTime birthdate, string email, string phoneNumber)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Birthdate = birthdate;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}