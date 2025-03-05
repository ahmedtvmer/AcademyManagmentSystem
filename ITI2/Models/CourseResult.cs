using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.Models
{
    public class CourseResult
    {
        public int Id { get; set; }
        public decimal Degree { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }
    }
}
