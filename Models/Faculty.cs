using System.ComponentModel.DataAnnotations;

namespace DemoMVC2.Models
{
    public class Faculty
    {
        [Key]
        public string FacultyID {get; set; }
        public string FacultyName {get; set; }
    }
}