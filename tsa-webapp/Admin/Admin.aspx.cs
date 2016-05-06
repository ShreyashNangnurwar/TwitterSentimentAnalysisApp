using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using tsa_BL;
using tsa_Model;

namespace tsa_webapp
{
    public partial class Admin : Login.ChangePassword
    {
        AdminBL AdminBL = new AdminBL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            // clear all the textfields
            TextBoxAddEmail.Text = "";
            TextBoxAddPassword.Text = "";
            TextBoxAddRePassword.Text = "";
            TextBoxAddUserName.Text = "";
            AdminUserRegistrationDivError.Visible = false;
            AdminUserRegistrationDivSuccess.Visible = false;
        }

        protected void ButtonAddSubmit_Click(object sender, EventArgs e)
        {
            UserDetails NewUser = new UserDetails();
            bool isNewUserAdded = false;

            if (TextBoxAddUserName.Text == "" || DropDownListRole.SelectedValue == "" || TextBoxAddEmail.Text == "" || TextBoxAddPassword.Text == "" || TextBoxAddRePassword.Text == "")
            {
                BulletedListError.Items.Clear();
                if (TextBoxAddUserName.Text == "")
                {
                    BulletedListError.Items.Add("Username is blank");
                }

                if (TextBoxAddEmail.Text == "")
                {
                    BulletedListError.Items.Add("Email address is blank");
                }

                if (TextBoxAddPassword.Text == "")
                {
                    BulletedListError.Items.Add("Password is blank");
                }

                if (TextBoxAddRePassword.Text == "")
                {
                    BulletedListError.Items.Add("Re-password is blank");
                }

                if (DropDownListRole.SelectedIndex == 0)
                {
                    BulletedListError.Items.Add("select a Role");
                }
                AdminUserRegistrationDivError.Visible = true;
            }
            else
            {
                // check both password and re-password are same
                if (TextBoxAddPassword.Text != TextBoxAddRePassword.Text)
                {
                    BulletedListError.Items.Clear();
                    LabelParaError.Text = "Password and re-password are not matching.";
                    AdminUserRegistrationDivError.Visible = true;
                    AdminUserRegistrationDivSuccess.Visible = false;
                }
                else
                {
                    // read from the fields
                    NewUser.role_id = Convert.ToInt32(DropDownListRole.SelectedValue);
                    NewUser.user_name = TextBoxAddUserName.Text;
                    NewUser.user_password = TextBoxAddPassword.Text;
                    NewUser.user_email = TextBoxAddEmail.Text;

                    // call BL function
                    isNewUserAdded = AdminBL.AddNewUserBL(NewUser);
                    if (isNewUserAdded)
                    {
                        AdminUserRegistrationDivSuccess.Visible = true;
                        AdminUserRegistrationDivError.Visible = false;
                    }
                    else
                    {
                        LabelParaError.Text = "Error while registering new user/admin";
                        AdminUserRegistrationDivError.Visible = true;
                    }
                }
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UserDetails updateUser = new UserDetails();
            bool isupdated = false;

            if (TextBoxUpdateUserName.Text == "" || TextBoxUpdatePassword.Text == "")
            {
                UpdateUserSuccessDiv.Visible = false;
                LabelUpdateUserError.Text = "Username/password is blank.";
                UpdateUserErrorDiv.Visible = true;

            }
            else
            {
                updateUser.user_name = TextBoxUpdateUserName.Text;
                updateUser.user_password = TextBoxUpdatePassword.Text;
                isupdated = AdminBL.UpdateUserBL(updateUser);
                if (isupdated)
                {
                    UpdateUserSuccessDiv.Visible = true;
                    UpdateUserErrorDiv.Visible = false;
                }
                else
                {
                    UpdateUserSuccessDiv.Visible = false;
                    LabelUpdateUserError.Text = "Error while updating user.";
                    UpdateUserErrorDiv.Visible = true;
                }
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            string uname = "";
            uname = TextBoxDeleteUser.Text;

            if (uname == "")
            {
                DeleteUserErrorDiv.Visible = true;
                DeleteUserSuccessDiv.Visible = false;
                LabelDeleteUserError.Text = "Username is blank.";
            }
            else
            {
                bool isDelete = false;
                isDelete = AdminBL.DeleteUserBL(uname);
                if (isDelete)
                {
                    DeleteUserSuccessDiv.Visible = true;
                    DeleteUserErrorDiv.Visible = false;
                }
                else
                {
                    DeleteUserErrorDiv.Visible = true;
                    DeleteUserSuccessDiv.Visible = false;
                    LabelDeleteUserError.Text = "Delete user operation failed over database.";
                }
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            string uname = "";
            uname = TextBoxSearchUserName.Text;
            UserDetails user = new UserDetails();
            bool isFound = false;

            if (uname == "")
            {
                SearchUserErrorDiv.Visible = true;
                SearchUserSuccessDiv.Visible = false;
                LabelSearchUserError.Text = "username is blank.";
            }
            else
            {
                isFound = AdminBL.SearchUserBL(uname, user);
                string role = "";

                if (isFound == true)
                {
                    SearchUserSuccessDiv.Visible = true;
                    SearchUserErrorDiv.Visible = false;
                    // show in table
                    HtmlTableRow row = new HtmlTableRow();

                    HtmlTableCell user_id = new HtmlTableCell();
                    user_id.Controls.Add(new LiteralControl(user.user_id.ToString()));
                    row.Cells.Add(user_id);

                    HtmlTableCell user_name = new HtmlTableCell();
                    user_name.Controls.Add(new LiteralControl(user.user_name));
                    row.Cells.Add(user_name);

                    HtmlTableCell user_email = new HtmlTableCell();
                    user_email.Controls.Add(new LiteralControl(user.user_email));
                    row.Cells.Add(user_email);

                    if (user.role_id == 1)
                    {
                        role = "Admin";
                    }
                    else if (user.role_id == 2)
                    {
                        role = "User";
                    }

                    HtmlTableCell user_role = new HtmlTableCell();
                    user_role.Controls.Add(new LiteralControl(role));
                    row.Cells.Add(user_role);

                    SearchTable.Rows.Add(row);
                }
                else
                {
                    SearchUserErrorDiv.Visible = true;
                    LabelSearchUserError.Text = "Username not found.";
                    SearchUserSuccessDiv.Visible = false;
                }
            }
        }
    }
}