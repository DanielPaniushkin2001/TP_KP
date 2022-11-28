using Microsoft.AspNetCore.Identity;
using StudentControlApp.enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentControlApp.Models
{
    public class TeacherData
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string? Name { get; set; }
        [Display(Name = "Возраст")]
        public int? Age { get; set; }
        [Display(Name = "Научная степень")]
        public DegreeType? DegreeType { get; set; }
        [Display(Name = "Пользователь")]
        public string? Userid { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}
