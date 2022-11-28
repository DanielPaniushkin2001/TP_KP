using Microsoft.AspNetCore.Identity;
using StudentControlApp.enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentControlApp.Models
{
    public class Studentdata
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string? Name { get; set; }
        [Display(Name = "Возраст")]
        public int? Age { get; set; }
        [Display(Name = "Тип обучения")]
        public StudentType? Type { get; set; }

        [Display(Name = "Группа")]
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }
        [Display(Name = "Пользователь")]
        public string? Userid { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}
