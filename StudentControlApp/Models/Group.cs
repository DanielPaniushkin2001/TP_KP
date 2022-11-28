using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentControlApp.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string? Name { get; set; }
    }
}
