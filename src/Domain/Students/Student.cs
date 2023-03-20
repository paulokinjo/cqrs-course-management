﻿namespace Domain.Students;

using Domain.Core;
using Domain.Courses;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Student : Entity
{
    public virtual string? Name { get; set; }
    public virtual string? Email { get; set; }

    private readonly IList<Enrollment> _enrollments = new List<Enrollment>();
    public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

    [ForeignKey("EnrollmentId")]
    public virtual long FirstEnrollmentId { get; set; }
    public virtual Enrollment FirstEnrollment => GetEnrollment(0);
    [ForeignKey("EnrollmentId")]
    public virtual long SecondEnrollmentId { get; set; }
    public virtual Enrollment SecondEnrollment => GetEnrollment(1);

    private readonly IList<Disenrollment> _disenrollments = new List<Disenrollment>();
    public virtual IReadOnlyList<Disenrollment> Disenrollments => _disenrollments.ToList();

    protected Student()
    {
    }

    public Student(string name, string email)
        : this()
    {
        Name = name;
        Email = email;
    }

    public virtual Enrollment GetEnrollment(int index)
    {
        if (_enrollments.Count > index)
            return _enrollments[index];

        return null;
    }

    public virtual void RemoveEnrollment(Enrollment enrollment, string comment)
    {
        _enrollments.Remove(enrollment);

        var disenrollment = new Disenrollment(enrollment.Student, enrollment.Course, comment);
        _disenrollments.Add(disenrollment);
    }

    public virtual void Enroll(Course? course, Grade grade)
    {
        if (_enrollments.Count >= 2)
            throw new Exception("Cannot have more than 2 enrollments");

        var enrollment = new Enrollment(this, course, grade);
        _enrollments.Add(enrollment);
    }
}
