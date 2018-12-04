<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseList.aspx.cs" Inherits="AdaptiveLearningSystem.CourseList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />

    <link rel="stylesheet" runat="server" media="screen" href="grid.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="main.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="CourseList.css"/>
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
    <style type="text/css">

    </style>
</head>
<body >
    <form id="form1" runat="server">
        <%-- First Line Navigation Bar Below --%>
        <div id="upperNavBar">

            <ul id="UpperNavList">
                <li>
                    <img id="tarcIcon" src="images/tarcIcon.png" width="50" /><p>Adaptive Learning System</p>
                </li>
                <li class="dropdown">
                <a>
                    <asp:Image ID="profilePic" ImageUrl="~/images/profIcon.png" runat="server" Style="width: 35px" />
                    <span id="welcome"><small>Welcome</small><br />
                        <small><asp:Label ID="lblUserName" runat="server" Text="Label">Loo Zhe Xin</asp:Label></small>
                    </span>
                    <div id="downIcon">&#9660;</div>
                </a>
                    <div class="dropdown-content">
                        
                        <asp:LinkButton CssClass="dropdown-item" ID="LogOutLinkButton" runat="server" OnClick="LogOutLinkButton_Click">Log Out</asp:LinkButton>
                    </div>

                </li>
                
            </ul>
        </div>
        <div>
            <%-- Second Line Navigation Bar Below --%>
            <ul class="SecondNavBar">
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="HomeLinkButton" runat="server" OnClick="HomeLinkButton_Click">Home</asp:LinkButton></li>
                <li class="coursebtn">
                   <div class="SecondNavList"> Course</div>
                    <div class="course-content">
                        <asp:LinkButton CssClass="course-item" ID="MyCourseLinkButton" runat="server" OnClick="MyCourseLinkButton_Click">My Course</asp:LinkButton>
                        <asp:LinkButton CssClass="course-item" ID="EnrollCourseLinkButton" runat="server" OnClick="TutorialLinkButton_Click">Enroll Course</asp:LinkButton>
                    </div>
                </li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" OnClick="ResultLinkButton_Click" runat="server">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>
       <%-- start your coding inside div below --%>
    <div class="wrap-content" >
        <div class="Header">
            <h1><i>
            Enroll Course</i>
            </h1>
       </div>
        <div style="margin-left:7%; margin-bottom:20px;">
        <asp:TextBox CssClass="txtSearchName" ID="txtSearchName" runat="server" OnTextChanged="txtSearchName_TextChanged" AutoPostBack="true" placeholder="Search Course Name"></asp:TextBox>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
            <div style="margin-top:20px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" Width="95%" runat="server" CssClass="mydatagrid" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PagerStyle-CssClass="page" HeaderStyle-CssClass="header" RowStyle-CssClass="rows">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="Enroll" />
                    </Columns>
                    <HeaderStyle CssClass="header" />
                    <PagerStyle CssClass="page" />
                    <RowStyle CssClass="rows" />
 </asp:GridView>
        
                </ContentTemplate>
    </asp:UpdatePanel>
</div>
        </div>
        <div class="NoResult" runat="server" id="NoResultPanel">
                <asp:Image CssClass="NoResultImg" ID="imgNoTutorial" ImageUrl="~/images/no-results.png" Width="30%" Height="20%" runat="server" />
                 <div class="caption">
                    <h1><asp:Label CssClass="NoResultTitle" ID="lblNoTutorialTitle" runat="server" Text="Whooops!"></asp:Label></h1>
                    <asp:Label CssClass="NoResultDesc" ID="lblNoTutorialDesc" runat="server" Text="Currently there has no available course left."></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

