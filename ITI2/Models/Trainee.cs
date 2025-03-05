using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Address { get; set; }
        public decimal Grade { get; set; }
        [ForeignKey("Dept")]
        public int DeptId { get; set; }
        public Department Dept { get; set; }
        public List<CourseResult> CourseResults { get; set; }
    }
}
