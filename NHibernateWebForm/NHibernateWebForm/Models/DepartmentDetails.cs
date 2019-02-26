using System.Collections.Generic;

namespace NHibernateWebForm.Models
{
    public class DepartmentDetails
    {
        public virtual int Dept_Id { get; set; }
        public virtual string Dept_Name { get; set; }
        public virtual IList<StudentDetails> StudentDetails { get; set; }
    }
}