<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DropCourse.aspx.cs" Inherits="AdaptiveLearningSystem.DropCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="DropCourse.css" />
    <link rel="stylesheet" runat="server" media="screen" href="main.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script> <%-- To allow jquery implementation --%>

    <script>
        <%-- The dropdown function after onclick (Right Upper Button) --%>
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
    <style>

    </style>
</head>
<body>
    <form id="form2" runat="server">
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
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server" OnClick="ResultLinkButton_Click">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>

            <%-- Content Code Below --%>
        <div class="wrap-content">
            <div class="Header">
                <h1><i>Drop Course</i>
                </h1>
            </div>
       <div class="Header">
            <h2><i>
            Please choose tutorial group(s).</i>
            </h2>           
       </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                
            <div class="dropDetail">
                <div class="displayFormat">
                    <asp:Label ID="lblProgramme" runat="server" Text="Programme: "></asp:Label>
                    <asp:DropDownList CssClass="dropdownFormat" ID="ddlProgramme" runat="server" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged" AutoPostBack="True" Enabled="False"></asp:DropDownList>
                    <asp:Label ID="lblerrorProgramme" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="displayFormat">
                    <asp:Label ID="lblTutorialGroup" runat="server" Text="Tutorial Group: "></asp:Label>
                    <asp:DropDownList ID="ddlTutorialGroup" runat="server" Enabled="False"></asp:DropDownList>
                    <asp:Label ID="lblErrorGroup" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                </div>

            </div>
            <div>
                <div class="ButtonGroup">
                    <asp:Button CssClass="DropButton" ID="btnDrop" runat="server" Text="Drop" OnClick="btnDrop_Click" />
                </div>
            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>