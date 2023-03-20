namespace Domain.Courses;

using Domain.Core;
using Domain.Students;
using System.ComponentModel.DataAnnotations.Schema;

public class Enrollment : Entity
{
    public virtual Student Student { get; protected set; }
    [ForeignKey("CourseId")]
    public virtual long CourseId { get; set; }
    public virtual Course? Course { get; set; }
    public virtual Grade Grade { get; protected set; }

    protected Enrollment()
    {
    }

    public Enrollment(Student student, Course? course, Grade grade)
        : this()
    {
        Student = student;
        Course = course;
        Grade = grade;
    }

    public virtual void Update(Course? course, Grade grade)
    {
        Course = course;
        Grade = grade;
    }
}