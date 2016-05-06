<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="tsa_webapp.User.UserDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
                        <a class="active item" data-tab="first">Search</a>
                        <a class="item" data-tab="second">Processed Tweets</a>
                        <a class="item" data-tab="third">Graph</a>
                        <a class="item" data-tab="fourth">Previous Result</a>
                    </div>
                    <div class="ui active tab segment" data-tab="first">
                        <div class="ui basic form">
                            <div class="field">
                                <label>Enter text to search</label>
                                <div class="ui input">
                                    <asp:TextBox ID="TextBoxSearchQuery" runat="server" placeholder="Enter input to search"></asp:TextBox>
                                </div>
                            </div>
                            <div >
                                <asp:Button ID="ButtonSearchQuery" runat="server" class="ui basic button" Text="Online Search" OnClick="ButtonSearchQuery_Click" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="ButtonOfflineSearchQuery" runat="server" class="ui basic button" Text="Offline Search" OnClick="ButtonOfflineSearchQuery_Click" />
                            </div>
                            <div class="ui negative message" runat="server" visible="false" id="SearchTweetsErrorDiv">
                              <i class="close icon"></i>
                              <div class="header">
                                We're sorry we can't fetch tweets
                              </div>
                              <p>
                                  <asp:Label ID="LabelSearchTweetsError" runat="server" Text=""></asp:Label>
                            </p></div>
                            <div class="ui positive message" runat="server" visible="false" id="SearchTweetsSuccessDiv">
                              <i class="close icon"></i>
                              <div class="header">
                                Tweets Fetched successfully.
                              </div>
                              <p>
                                  <asp:Label ID="LabelSearchSuccess" runat="server" Text=""></asp:Label></p>
                            </div>
                            <table class="ui teal single line table" runat="server" id="TweetsTable">
                              <thead>
                                <tr>
                                  <th>UserId</th>
                                  <th>Text</th>
                                  <th>Time</th>
                                </tr>
                              </thead>
                            </table>
                        </div>
                    </div>
                    
                    <div class="ui tab segment" data-tab="second">
                        
                        <asp:Button ID="ButtonShowPreprocessedTweets" runat="server" class="ui primary button" Text="Show Preprocessed Tweets" OnClick="ButtonShowPreprocessedTweets_Click" />

                        <table class="ui green single line table" runat="server" id="processedTable">
                            <thead>
                                <tr>
                                    <th>Text</th>
                                </tr>
                            </thead>
                            <tbody>
                                
                            </tbody>
                        </table>
                    </div>

                    <div class="ui tab segment" data-tab="third">
                        
                                    <div class="ui basic form" runat="server">
                                    <asp:Button ID="ButtonShowGraph" runat="server" class="ui primary button" Text="Show Graph" OnClick="ButtonShowGraph_Click" />                
                                    <%--<asp:DropDownList ID="DropDownListGraphType" class="ui selection dropdown" runat="server"> </asp:DropDownList>--%>
                                    </div>
                        
                        
                                <asp:Chart ID="tsaChart" runat="server" Height="500" Width="500">
                                    <Series>
                                        <asp:Series Name="tsaSeries"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                        <table class="ui green single line table" runat="server" id="TableResults">
                            <thead>
                                <tr>
                                    <th>Category</th>
                                    <th>Count</th>
                                </tr>
                            </thead>
                            <tbody>
                                
                            </tbody>
                        </table>

                        <table class="ui red single line table" runat="server" id="TableFinalResult">
                            <thead>
                                <tr>
                                    <th>No</th>
                                    <th>Text</th>
                                    <th>Category</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>

                    <div class="ui tab segment" data-tab="fourth">                        
                        <div class="ui form" runat="server">
                                <%--<div class="field">
                                    <label>Search Query</label>
                                    <div class="ui left icon input">
                                        <i class="edit icon"></i>
                                        <asp:TextBox ID="TextBoxSearchResultName" placeholder="Enter Result Query" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                        <asp:Button ID="ButtonSearch" runat="server" class="ui primary button" Text="Search Results" OnClick="ButtonSearch_Click"/>
                        <%--<asp:Button ID="ButtonLastTen" runat="server" class="ui primary button" Text="Show last 10 Search Results" />--%>
                        </div>
                        <table class="ui red single line table" runat="server" id="searchTable">
                            <thead>
                                <tr>
                                    <th>Text</th>
                                    <th>Datetime</th>
                                    <th>Positive Score</th>
                                    <th>Negative Score</th>
                                    <th>Neutral Score</th>
                                </tr>
                            </thead>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </form>
    </body>
</html>