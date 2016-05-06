<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tsa_webapp.Login.Login" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Login</title>
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


                    </div>
                </div>
            </div>
            </div>

            <div class="ui stackable two column centered grid">

                <div class="six wide column">
                    <div class="ui basic segment">

                        <div class="column">
                            <div class="ui form segment">
                                <h2 class="ui header">
  <i class="user icon"></i>
  <div class="content">
    Twitter Sentiment Analysis App
  </div>
</h2>
                                <div class="ui divider"></div>
                                <div class="ui error message">
                                    <i class="close icon"></i>
                                    <div class="center aligned ui header">
                                        Password Incorrect
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Username</label>
                                    <div class="ui left icon input">
                                        <%--<input type="text" placeholder="Enter email address" name="email">--%>
                                        <asp:TextBox ID="TextBoxUserUserName" runat="server" placeholder="Enter User Name"></asp:TextBox>
                                        <i class="user icon"></i>
                                    </div>
                                </div>
                                <div class="field">
                                    <label>Password</label>
                                    <div class="ui left icon input">
                                        <%--<input type="password" placeholder="Enter Password" name="password">--%>
                                        <asp:TextBox ID="TextBoxPassword" runat="server" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                        <i class="lock icon"></i>
                                    </div>
                                </div>
                                <div class="ui divider"></div>
                                <div class="ui central">
                                    
                                        <asp:Button ID="ButtonLogin" runat="server" Text="Login" class="ui primary button" OnClick="ButtonLogin_Click"/>
                                    
                                    <%--<div class="hidden content">
                                        <i class="arrow right icon"></i>
                                        
                                    </div>--%>
                                </div>
                                <br>
                                <div class="ui negative message" runat="server" id="LoginErrorDiv" visible="false">
                                <i class="close icon"></i>
                                <div class="header">
                                Login Error
                                </div>
                                <p><asp:Label ID="LabelLoginError" runat="server" Text=""></asp:Label>
                                </p></div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </form>
    </body>

    </html>