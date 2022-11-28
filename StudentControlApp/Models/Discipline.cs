using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentControlApp.Models
{
    public class Discipline
    {
        public int Id { get; set; }
        [Display(Name = "Название предмета")]
        public string? Name { get; set; }
        [Display(Name = "Группа")]
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }

        public int? TeacherDataId { get; set; }
        public virtual TeacherData? TeacherData { get; set; }
    }
}
