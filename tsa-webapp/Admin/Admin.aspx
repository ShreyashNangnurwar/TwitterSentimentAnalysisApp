<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="tsa_webapp.Admin" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Dashboard</title>
        <link rel="stylesheet" href="../css/semantic.css" type="text/css" />
        <link rel="stylesheet" href="../css/kitchensink.css" type="text/css" />
        <script src="../jquery/jquery-2.1.3.js"></script>
        <script src="../jquery/1.js"></script>
        <script src="../jquery/semantic.js"></script>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="ui attached inverted segment">


                <div class="ui large inverted menu">
                    <a class="item">
                        <i class="home icon"></i>Home
                    </a>



                    <div class="right menu">


                        <a class="item" href="Contact Us.html">
                            <i class="mail icon"></i>Contact Us
                        </a>
                        <a class="item" href="../Login/Login.aspx">
                            <i class="sign out icon"></i>Logout
                        </a>

                    </div>
                </div>
            </div>
            </div>
            <div class="ui stackable two column centered grid">

                <div class="ten wide column">
                    <div class="ui basic segment">
                        <div class="ui four item pointing menu">
                            <a class="active item" data-tab="first">Add User</a>
                            <a class="item" data-tab="second">Update User</a>
                            <a class="item" data-tab="third">Delete User</a>
                            <a class="item" data-tab="fourth">Search User</a>
                        </div>
                        <div class="ui active tab segment" data-tab="first">





                            <div class="ui basic form" runat="server">
                                <div class="ui blue inverted center aligned segment">Register New User</div>
                                <%--<div class="ui error message"></div>--%>
                                <%--<div class="ui success message">
                                    <i class="close icon"></i>
                                    <div class="center aligned ui header">
                                        Your user registration was successful.
                                    </div>
                                </div>--%>
                                
                                <div class="field">
                                    <label>User Name</label>
                                    <div class="ui left icon input">
                                        <i class="user icon"></i>
                                        <asp:TextBox ID="TextBoxAddUserName" placeholder="Enter user name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Email</label>
                                    <div class="ui left icon input">
                                        <i class="mail icon"></i>
                                        <asp:TextBox ID="TextBoxAddEmail" placeholder="Enter email id" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="two fields">
                                    <div class="field">
                                        <label>Role</label>
                                        <asp:DropDownList ID="DropDownListRole" class="ui selection dropdown" runat="server">
                                            <asp:ListItem Enabled="true" Text="Select Role" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="User" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br/>





                                <div class="two fields">
                                    <div class="field">
                                        <label>Password</label>
                                        <div class="ui left icon input">
                                            <i class="lock icon"></i>
                                            
                                            <asp:TextBox ID="TextBoxAddPassword" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="field">
                                        <label>Confirm Password</label>
                                        <div class="ui left icon input">
                                            <i class="lock icon"></i>
                                            <asp:TextBox ID="TextBoxAddRePassword" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                                 

                            </div>
                               <asp:Button ID="ButtonAddSubmit" class="ui blue submit button" runat="server" Text="Add User" OnClick="ButtonAddSubmit_Click" />
                               <asp:Button ID="ButtonClear" class="ui reset basic button" runat="server" Text="Clear" OnClick="ButtonClear_Click" />
                               <div class="ui success message" runat="server" visible="false" id="AdminUserRegistrationDivSuccess">
                                  <i class="close icon"></i>
                                  <div class="header">
                                    <asp:Label ID="LabelHeader" runat="server" Text="New User/Admin added was successful."></asp:Label>
                                  </div>
                                  <p>
                                   <asp:Label ID="LabelPara" runat="server" Text="You may now log-in with the username you have created."></asp:Label></p>
                                </div>
                            <div class="ui negative message" runat="server" visible="false" id="AdminUserRegistrationDivError">
                              <i class="close icon"></i>
                              <div class="header">
                                  <asp:Label ID="LabelHeaderError" runat="server" Text="Error while creating new user."></asp:Label>
                              </div>
                              <p>
                                  <asp:BulletedList ID="BulletedListError" runat="server"></asp:BulletedList>
                                  <asp:Label ID="LabelParaError" runat="server" Text=""></asp:Label>
                            </p></div>
                        </div>
                        <div class="ui tab segment" data-tab="second">

                            <div class="ui form">
                                <%--<div class="field">
                                    <label>User Name</label>
                                    <div class="ui left icon input">
                                        <i class="user icon"></i>
                                        <input type="text" placeholder="Enter user name" name="name">
                                    </div>
                                </div>

                                <div class="ui basic button">Search User</div>
                                <br>
                                <br>--%>
                                <div class="field">
                                    <label>User Name</label>
                                    <div class="ui left icon input">
                                        <i class="user icon"></i>
                                        <asp:TextBox ID="TextBoxUpdateUserName" placeholder="Enter user name" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="field">
                                    <label>Reset Password</label>
                                    <div class="ui left icon input">
                                        <i class="lock icon"></i>
                                        <asp:TextBox ID="TextBoxUpdatePassword" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                                <div >
                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update User" class="ui primary button" OnClick="ButtonUpdate_Click" /></div>
                                
                                <div class="ui negative message" runat="server" visible="false" id="UpdateUserErrorDiv">
                                  <i class="close icon"></i>
                                  <div class="header">
                                    We're sorry we can't apply that update
                                  </div>
                                  <p>
                                      <asp:Label ID="LabelUpdateUserError" runat="server" Text=""></asp:Label>
                                </p></div>
                                <div class="ui positive message" runat="server" id="UpdateUserSuccessDiv" visible="false">
                                  <i class="close icon"></i>
                                  <div class="header">
                                    Your user update was successful.
                                  </div>
                                  <p>You may now log-in with the new password you have chosen</p>
                                </div>
                            </div>

                        </div>
                        <div class="ui tab segment" data-tab="third">
                            <div class="ui form">
                                <div class="field">
                                    <label>User Name</label>
                                    <div class="ui left icon input">
                                        <i class="user icon"></i>
                                        <asp:TextBox ID="TextBoxDeleteUser" runat="server" placeholder="Enter user name"></asp:TextBox>
                                    </div>
                                </div>
                                <div >
                                    
                            </div>
                                </div>
                                <asp:Button ID="ButtonDelete" runat="server" Text="Delete User" class="ui red submit button" OnClick="ButtonDelete_Click"/>
                                <div class="ui positive message" runat="server" visible="false" id="DeleteUserSuccessDiv">
                                  <i class="close icon"></i>
                                  <div class="header">
                                    Delete User Successful.
                                  </div>
                                  <p> User deleted from database. </p>
                                </div>
                                <div class="ui negative message" runat="server" visible="false" id="DeleteUserErrorDiv">
                                  <i class="close icon"></i>
                                  <div class="header">
                                    We're sorry we can't apply delete on perticular user.
                                  </div>
                                  <p>
                                      <asp:Label ID="LabelDeleteUserError" runat="server" Text=""></asp:Label>
                                </p></div>
                        </div>
                        <div class="ui tab segment" data-tab="fourth">

                            <div class="ui form">
                                <div class="field">
                                    <label>User Name</label>
                                    <div class="ui left icon input">
                                        <i class="user icon"></i>
                                        <asp:TextBox ID="TextBoxSearchUserName" placeholder="Enter user name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div >
                                <asp:Button ID="ButtonSearch" runat="server" class="ui primary button" Text="Search User" OnClick="ButtonSearch_Click" /></div>
                                <div class="ui negative message" runat="server" visible="false" id="SearchUserErrorDiv">
                                  <i class="close icon"></i>
                                  <div class="header">
                                    Search Failed.
                                  </div>
                                  <p>
                                      <asp:Label ID="LabelSearchUserError" runat="server" Text=""></asp:Label>
                                </p></div>
                                <div class="ui positive message" runat="server" visible="false" id="SearchUserSuccessDiv"> 
                                  <i class="close icon"></i>
                                  <div class="header">
                                    Search successful.
                                  </div>
                                  <p>User searched and populated in table.</p>
                                </div>
                                <table class="ui single line table" runat="server" id="SearchTable">
                                      <thead>
                                        <tr>
                                          <th>User Id</th>
                                          <th>User Name</th>
                                          <th>E-mail address</th>
                                          <th>Role</th>
                                        </tr>
                                      </thead>
                                      <tbody>
                                      </tbody>
                                 </table>
                            </div>

                        </div>


                    </div>

                </div>
        </form>
    </body>

    </html>