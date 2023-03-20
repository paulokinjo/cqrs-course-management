namespace Domain.Courses;

using Domain.Core;

public class Course : Entity
{
    public virtual string? Name { get; protected set; }
    public virtual int Credits { get; protected set; }

    protected Course() { }
    public Course(string? name, int credits) : this()
    {
        Name = name;
        Credits = credits;
    }
}
