using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tsa_BL;
using tsa_Model;

namespace tsa_webapp.Login
{
    public partial class Login : System.Web.UI.Page
    {
        LoginBL LoginBL = new LoginBL();
        UserDetails user = new UserDetails();
        bool isDetailsValid = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !isDetailsValid)
            {
                LoginErrorDiv.Visible = true;
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            user.user_name = TextBoxUserUserName.Text;
            user.user_password = TextBoxPassword.Text;

            if (TextBoxUserUserName.Text == "" || TextBoxPassword.Text == "")
            {
                LabelLoginError.Text = "Username or password is blank. Please enter the same.";
                LoginErrorDiv.Visible = true;
            }
            else
            {
                isDetailsValid = LoginBL.ValidateLoginDetailsBL(user);

                if (isDetailsValid)
                {
                    switch (user.role_id)
                    {
                        case 1:
                            Response.Redirect("../Admin/Admin.aspx");
                            break;
                        case 2:
                            Response.Redirect("../User/UserDashboard.aspx");
                            break;
                    }
                }
                else
                {
                    LabelLoginError.Text = "Username or password is wrong. Try again!";
                    LoginErrorDiv.Visible = true;
                    //Response.Redirect("../Login/Login.aspx");
                }
            }
        }
    }
}