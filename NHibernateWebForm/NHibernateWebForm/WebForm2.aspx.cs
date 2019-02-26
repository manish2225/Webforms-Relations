using NHibernate;
using NHibernate.Transform;
using NHibernateWebForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHibernateWebForm
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void insert_Click(object sender, EventArgs e)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                try
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var department = new DepartmentDetails
                        {
                            Dept_Name = TextBox2.Text
                        };
                        var q = session.CreateSQLQuery("insert into DepartmentDetails (Dept_Name) values('" + department.Dept_Name + "')");
                        q.List<StudentDetails>();
                        transaction.Commit();
                        Response.Write("<Script>alert('Data Saved Sucessfully in Database')</Script>");
                    }
                }
                catch (Exception)
                {
                    Response.Write("<Script>alert('please Check your Data')</Script>");
                }
            }
        }

        protected void Read_Click(object sender, EventArgs e)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    var department = session.CreateSQLQuery("select * from DepartmentDetails").SetResultTransformer(Transformers.AliasToBean<DepartmentDetails>()).List<DepartmentDetails>();
                    if (department.Count > 0)
                    {
                        departmentBook.DataSource = department;
                        departmentBook.DataBind();
                    }
                    else
                    {
                        Response.Write("<Script>alert('Data Not Found in DB')</Script>");
                    }
                }
            }
        }
    }
}