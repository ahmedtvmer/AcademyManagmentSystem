using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        [ForeignKey("Dept")]
        public int? DeptId { get; set; }
        public Department Dept { get; set; }
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public Course Course { get; set; }
    }
}
