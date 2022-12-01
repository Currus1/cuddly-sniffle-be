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
        public DateTime BirthDate { get; set; }
        [Required]
        public string Number { get; set; }

        public UserRegisterDto(string name, string surname, string email,
            DateTime birthDate, string number)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            Number = number;
        }
    }
}
