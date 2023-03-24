# Course Management System
This is a task-driven application built with .NET Core that implements CQRS architecture. The application allows for the management of student enrollment and personal information.

# Installation
Clone this repository and run the application using Visual Studio or the .NET Core CLI.

# Usage
The StudentController is responsible for handling HTTP requests related to student management. The following API endpoints are available:

## GET /students
Retrieves a list of students. You can optionally filter by whether the student is enrolled or not, and by student number.

## POST /students
Registers a new student with the provided information.

## DELETE /students/{id}
Removes a student with the provided ID.

## POST /students/{id}/enrollment
Enrolls a student in a course.

## PUT /students/{id}/enrollment/{enrollmentNumber}
Transfers a student to a different course.

## POST /students/{id}/enrollment/{enrollmentNumber}/deletion
Disenrolls a student from a course.

## PUT /students/{id}
Edits a student's personal information.

# Contributing
Contributions are welcome! Please open a pull request or issue if you have any ideas or suggestions for improvement.

# License
This project is licensed under the MIT License.