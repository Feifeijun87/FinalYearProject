﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudResult.aspx.cs" Inherits="AdaptiveLearningSystem.StudResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" runat="server" media="screen" href="studMain.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="CourseList.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="grid.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script> 

        $(document).ready(function () {
            $(".dropdown").click(function (e) {
                e.stopPropagation();
                if ($('.dropdown').hasClass('active')) {
                    
                    $(".dropdown").removeClass("active");
                    $(".dropdown").removeClass("activeBackground");
                }
                else {
                    $(".dropdown").addClass("active");
                    $(".dropdown").addClass("activeBackground");
                }
            });
        });

        $(document).click(function () {
            
            $(".dropdown").removeClass("active");
            $(".dropdown").removeClass("activeBackground");
           
        });


        </script>
    </head>
<body >
    <form id="form1" runat="server">
    <div id="upperNavBar">
        
    <ul id="UpperNavList">
        <li><img id="tarcIcon" src="images/tarcIcon.png" width="50"/><p>Adaptive Learning System</p></li>
        <li class="dropdown"><a><asp:Image ID="profilePic"  ImageUrl="~/images/profIcon.png" runat="server" style="width: 35px" />
            <span id="welcome"><small>Welcome</small><br />
                <small><asp:Label ID="lblUserName" runat="server" Text="Label">Loo Zhe Xin</asp:Label></small>
            </span>
            <div id="downIcon"> &#9660;</div></a>
            <div class="dropdown-content">
      <asp:LinkButton  CssClass="dropdown-item" ID="LogOutLinkButton" OnClick="LogOutLinkButton_Click" runat="server">Log Out</asp:LinkButton>
    </div>
                  
          </li>       
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          
          
        
    </ul>
    </div>   
       <div>
        <ul class="SecondNavBar">
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="HomeLinkButton" runat="server" OnClick="HomeLinkButton_Click">Home</asp:LinkButton></li>
                
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server" OnClick="ResultLinkButton_Click1" >Tutorial Result</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div> 
       <%-- start your coding inside div below --%>
    <div class="wrap-content" >
        <div class="Header">
            <h1><i>
                Result List</i>
            </h1>
       </div>
        <div style="margin-left:7%; margin-bottom:20px;">
        
            <div style="margin-top:20px;">

                <asp:Label ID="lblNoData" runat="server" Text=""></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:GridView ID="GridView1" Width="95%" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="page" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" PagerSettings-PageButtonCount="4">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="View Result" />
                    </Columns>
                    <HeaderStyle CssClass="header" />
                    <PagerStyle CssClass="page" />
                    <RowStyle CssClass="rows" />
 </asp:GridView>

                
            </ContentTemplate>
    </asp:UpdatePanel>
</div>
        </div>
        </div>
    </form>
</body>
</html>
