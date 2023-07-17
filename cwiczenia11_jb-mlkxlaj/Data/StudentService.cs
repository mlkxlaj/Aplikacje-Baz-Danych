namespace Zadanie11.Data
{
    public class StudentService
    {
        private List<Student> students;

        public StudentService()
        {
            students = new List<Student>
            {
                new Student()
                {
                    IdStudent = 1,
                    FirstName = "Anna",
                    LastName = "Smith",
                    BirthDate = "2000-05-15",
                    Studies = "Business Administration",
                    Icon = "https://ui-avatars.com/api/?name=Anna+Smith&background=random&size=32"
                },
                new Student()
                {
                    IdStudent = 5,
                    FirstName = "Michael",
                    LastName = "Johnson",
                    BirthDate = "2003-09-20",
                    Studies = "Physics",
                    Icon = "https://ui-avatars.com/api/?name=Michael+Johnson&background=random&size=32"
                },
                new Student()
                {
                    IdStudent = 4,
                    FirstName = "Emily",
                    LastName = "Brown",
                    BirthDate = "2001-07-10",
                    Studies = "English Literature",
                    Icon = "https://ui-avatars.com/api/?name=Emily+Brown&background=random&size=32"
                },
                new Student()
                {
                    IdStudent = 3,
                    FirstName = "David",
                    LastName = "Lee",
                    BirthDate = "2002-03-08",
                    Studies = "Chemistry",
                    Icon = "https://ui-avatars.com/api/?name=David+Lee&background=random&size=32"
                },
                new Student()
                {
                    IdStudent = 2,
                    FirstName = "Sophia",
                    LastName = "Garcia",
                    BirthDate = "2000-12-05",
                    Studies = "Psychology",
                    Icon = "https://ui-avatars.com/api/?name=Sophia+Garcia&background=random&size=32"
                }
            };
        }

        public List<Student> GetStudents()
        {
            return students.ToList();
        }

        public void DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.IdStudent == id);
            if (student != null)
            {
                students.Remove(student);
            }
        }

        public Task<Student?> GetStudentById(int id)
        {
            var student = students.FirstOrDefault(s => s.IdStudent == id);
            return Task.FromResult(student);
        }
    }
}
