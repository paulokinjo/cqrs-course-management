
namespace Domain.Courses;

using Domain.Core;
using Domain.Students;
using System;

public class Disenrollment : Entity
{
    public virtual Student? Student { get; protected set; }
    public virtual Course? Course { get; protected set; }
    public virtual DateTimeOffset DateTime { get; protected set; }
    public virtual string? Comment { get; protected set; }
    
    protected Disenrollment() { }   

    public Disenrollment(Student student, Course course, string comment) :this()
    {
        Student = student;
        Course = course;
        Comment = comment;

        DateTime = DateTimeOffset.UtcNow;
    }
}
