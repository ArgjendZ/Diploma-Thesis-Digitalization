using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaThesisDigitalization.Models.Entities
{
    public class DiplomaThesis
    {
        [ForeignKey("Student")]
        public int Id { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime SubmissionDate { get; set; }
        public byte? Assessment { get; set; }
        public string Level { get; set; }

        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }
        public virtual Title Title { get; set; }

        public ICollection<ConsultationSchedule> ConsultationSchedules { get; set; }
    }
}
