using System.ComponentModel.DataAnnotations;

namespace ITI.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public List<Instructor> Instructors { get; set; } = new();
        public List<Trainee> Trainees { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
    }
}
