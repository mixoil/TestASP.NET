using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestASP.NET.Models
{
    public class Footballer
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Не указан пол")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Не указана команда")]
        public string Team { get; set; }
        [Required(ErrorMessage = "Не указана страна")]
        public Country Country { get; set; }
    }
}
