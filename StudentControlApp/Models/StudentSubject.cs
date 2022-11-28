using StudentControlApp.enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentControlApp.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Display(Name = "Статус")]
        public SubjectStatus? Status { get; set; }
        [Display(Name = "Студент")]
        public int StudentdataId { get; set; }
        public virtual Studentdata? Studentdata { get; set; }

    }
}
