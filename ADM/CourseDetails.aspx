<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="AdaptiveLearningSystem.CourseDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />

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
        .auto-style1 {
            width: 126px;
        }
        .auto-style2 {
            width: 427px;
        }
        .course-content {
            display: none;
            overflow: hidden;
            position: absolute;
            top: 15%;
            right: 53%;
            background-color: #f9f9f9;
            min-width: 171px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
            text-align:left;
        }

        .course-item {
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
        }

        .coursebtn:hover .course-content {
            display: block;
        }

        .course-content .course-item:hover {
            background-color: #f1f1f1;
        }
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
                <li><a><img src="images/notificationIcon.png" width="35px" /></a></li>
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
                        <asp:LinkButton CssClass="course-item" ID="MyCourseLinkButton" runat="server">My Course</asp:LinkButton>
                        <asp:LinkButton CssClass="course-item" ID="EnrollCourseLinkButton" runat="server" OnClick="TutorialLinkButton_Click">Enroll Course</asp:LinkButton>
                    </div>
                </li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>
       <%-- start your coding inside div below --%>
    <div class="wrap-content" >
        <div class="Header">
            <h1><i>
            Course Details</i>
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

            
        <div class="tutorialGrpList">
            <asp:Label ID="lblErrorMessage" runat="server" Text="[lblErrorMessage]" ForeColor="Red"></asp:Label>
            <asp:CheckBoxList ID="chkboxTutorialGroup" runat="server"></asp:CheckBoxList>
        </div>
                <div class="BottomButtonGroup">
                    <asp:Button ID="btnBack" CssClass="BottomButton" runat="server" Text="Back" OnClick="btnBack_Click" />
                    <asp:Button ID="btnConfrim" CssClass="BottomButton" runat="server" Text="Confirm" OnClick="btnConfrim_Click" />
                </div>

        
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

