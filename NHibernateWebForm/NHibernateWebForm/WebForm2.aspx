<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="NHibernateWebForm.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 412px; width: 694px; margin-left: 277px;">

            <h3 style="margin-left: 250px; width: 160px;">Department Details</h3>
            <p style="margin-left: 120px">
            <span id="space" runat="server"></span>
               
            <asp:Label ID="Label2" runat="server" Text="Department Name" />
            <asp:TextBox ID="TextBox2" runat="server" Width="189px" Height="22px"/>
                <asp:RequiredFieldValidator ID="RequiredDept_Name" runat="server" ControlToValidate="TextBox2" ErrorMessage="Department Name" ForeColor="Red"></asp:RequiredFieldValidator>
            <br /><br /><br /><br />
           
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            <asp:Button ID="insert" runat="server" type="submit" Text="Insert" OnClick="insert_Click" Width="115px" Height="40px" />&nbsp&nbsp
            <asp:Button ID="Read" runat="server" type="button" Text="Read" OnClick="Read_Click" Width="113px" Height="39px" CausesValidation="False" />&nbsp&nbsp
            </p>
            <span runat="server" ID="two"></span>
            <asp:GridView ID="departmentBook" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" Width="690px">
                <Columns>
                    <asp:TemplateField HeaderText="Department Id">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Dept_Id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department Name">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Dept_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
    </form>
</body>
</html>
