using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Degree { get; set; }
        public decimal MinDegree { get; set; }
        [ForeignKey("Dept")]
        public int DeptId { get; set; }
        public Department Dept { get; set; }

        public List<CourseResult> CourseResults { get; set; }
        public List<Instructor> Instructors { get; set; }
    }
}
