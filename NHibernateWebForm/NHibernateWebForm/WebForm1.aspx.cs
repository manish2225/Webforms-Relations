using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Transform;
using NHibernateWebForm.Models;
using System;
using System.Web.UI.WebControls;

namespace NHibernateWebForm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filldrop();
            } 
        }

        private void filldrop()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    var names = session.CreateSQLQuery("select * from DepartmentDetails").SetResultTransformer(Transformers.AliasToBean<DepartmentDetails>()).List<DepartmentDetails>();
                    if (names.Count > 0)
                    {
                        DropDownList1.DataSource = names;
                        DropDownList1.DataTextField = "Dept_Name";
                        DropDownList1.DataValueField = "Dept_Id";
                        DropDownList1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Data Not Found in the Database ')</script>");
                    }
                }
            }
        }

        protected void insert_Click(object sender, EventArgs e)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var student = new StudentDetails
                        {
                            StudentName = sname.Text,
                            FatherName = fname.Text,
                            Email = Email.Text,
                            Address = address.Text,
                            Mobile = mobile.Text,

                            DepartmentDetails = new DepartmentDetails
                            {
                                Dept_Id = Convert.ToInt32(DropDownList1.SelectedValue)
                            }
                    };
                        
                        var q = session.CreateSQLQuery("insert into StudentDetails (StudentName,FatherName,Email,Address,Mobile,Dept_Id) values('" + student.StudentName + "','" + student.FatherName + "','" + student.Email + "','" + student.Address + "','" + student.Mobile + "'," + student.DepartmentDetails.Dept_Id + ")");
                        q.List<StudentDetails>();
                        transaction.Commit();
                        Response.Write("<Script>alert('Data Saved Sucessfully in Database')</Script>");
                    }
                    catch (GenericADOException)
                    {
                        Response.Write("<script>alert('Please Enter Valid Department Id')</script>");
                    }
                    catch (Exception)
                    {
                        Response.Write("<script>alert('Please Check your filled Information')</script>");
                    }

                }
            }
        }

        protected void Read_Click(object sender, EventArgs e)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var students = session.CreateSQLQuery("select s.Id, s.StudentName,s.FatherName,s.Email,s.Address,s.Mobile,s.Dept_Id ,d.Dept_Name from StudentDetails as s, DepartmentDetails as d  where s.Dept_Id = d.Dept_Id").SetResultTransformer(Transformers.AliasToBean<DepartmentDTO>()).List<DepartmentDTO>();
                    
                    if (students.Count > 0)
                    {
                        studentBook.DataSource = students;
                        studentBook.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Data Not Found in the Database ')</script>");
                    }
                }
            }
        }

        protected void studentBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            studentBook.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        private void PopulateGridview()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    
                    var students = session.CreateSQLQuery("select s.Id, s.StudentName,s.FatherName,s.Email,s.Address,s.Mobile,s.Dept_Id,d.Dept_Name  from StudentDetails as s ,DepartmentDetails as d  where s.Dept_Id=d.Dept_Id").SetResultTransformer(Transformers.AliasToBean<DepartmentDTO>()).List<DepartmentDTO>();

                    if (students.Count > 0)
                    {
                        studentBook.DataSource = students;                       
                        studentBook.DataBind();
                    }else
                    {
                        Response.Write("<script>alert('Data Not Found in the Database ')</script>");
                    }
                }
            }
        }
        protected void studentBook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && studentBook.EditIndex==e.Row.RowIndex)
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var dropdown = (DropDownList)e.Row.FindControl("DropDownList2");
                       
                        var DList = session.CreateSQLQuery("Select * from DepartmentDetails").SetResultTransformer(Transformers.AliasToBean<DepartmentDetails>()).List<DepartmentDetails>();
                        dropdown.DataSource = DList;
                        dropdown.DataTextField = "Dept_Name";
                        dropdown.DataValueField = "Dept_Id";
                        dropdown.DataBind(); 
                    }
                }
            }
        }

        protected void studentBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            studentBook.EditIndex = -1;
            PopulateGridview();
        }

        protected void studentBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        Label Id = studentBook.Rows[e.RowIndex].FindControl("Label6") as Label;
                        TextBox StudentName = studentBook.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
                        TextBox FatherName = studentBook.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;
                        TextBox Email = studentBook.Rows[e.RowIndex].FindControl("TextEmail") as TextBox;
                        TextBox Address = studentBook.Rows[e.RowIndex].FindControl("TextBox3") as TextBox;
                        TextBox Mobile = studentBook.Rows[e.RowIndex].FindControl("TextBox4") as TextBox;                        
                        DropDownList Dept_Id =studentBook.Rows[e.RowIndex].FindControl("DropDownList2") as DropDownList;
                        
                        var q = session.CreateSQLQuery("Update StudentDetails set StudentName='" + StudentName.Text + "',FatherName='" + FatherName.Text + "',Email='" + Email.Text + "',Address='" + Address.Text + "',Mobile='" + Mobile.Text + "',Dept_Id=" + Dept_Id.Text + " where Id= " + Id.Text + "");
                        var ans = q.ExecuteUpdate();
                        transaction.Commit();
                        Response.Write("<Script>alert('Data Updated Sucessfully')</Script>");
                        studentBook.EditIndex = -1;
                        PopulateGridview();
                    }
                    catch (GenericADOException)
                    {
                        Response.Write("<script>alert('Please Enter Valid Department Id')</script>");
                    }
                    catch (Exception)
                    {
                        Response.Write("<Script>alert('please check Information you want to Update')</Script>");
                    }
                }
            }
        }

        protected void studentBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Label Id = studentBook.Rows[e.RowIndex].FindControl("Label1") as Label;

                    var q = session.CreateSQLQuery("Delete from StudentDetails where Id=" + Id.Text + "");
                    var ans = q.ExecuteUpdate();
                    transaction.Commit();
                    Response.Write("<Script>alert('Your Information Deleted Sucessfully ')</Script>");
                    studentBook.EditIndex = -1;
                    PopulateGridview();
                }
            }
        }
    }
}