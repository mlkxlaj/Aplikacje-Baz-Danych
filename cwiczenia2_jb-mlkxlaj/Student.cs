namespace Zadanie2
{
    internal class Student
    {
        public int index { get; set; }
        public string name { get; set; }
        public string surename { get; set; }
        public string email { get; set; }
        public DateTime birthDate  { get; set; }
        public Studies Studies { get; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }



        public Student(int index, string name, string surename, string email, DateTime birthDate, Studies studies, string mothersName, string fathersName)
        {
            this.index = index;
            this.name = name;
            this.surename = surename;
            this.email = email;               
            this.birthDate = birthDate;
            Studies = studies;
            this.mothersName = mothersName;
        }
    
    }
}