
namespace NHibernateWebForm.Models
{
    public class StudentDetails
    {
        public virtual int Id { get; set; }
        public virtual string StudentName { get; set; }
        public virtual string FatherName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
        public virtual string Mobile { get; set; }      

        public virtual DepartmentDetails DepartmentDetails { get; set; }
    }
}