namespace DbConn
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
    }
}