using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace currus.Models.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public UserRegisterDto(string name, string surname, string email,
            DateTime birthdate, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthdate = birthdate;
            PhoneNumber = phoneNumber;
        }
    }
}
