<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LecHome.aspx.cs" Inherits="AdaptiveLearningSystem.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />

    <link rel="stylesheet" runat="server" media="screen" href="main.css" />
    <link rel="stylesheet" href="css.css" /> <%-- CSS for the circular progress bar --%>
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

        .NoResult{
            display:block;
            text-align:center;
            margin-bottom:10px;
            color:gray
        }
        .caption{
            display:block;
        }
    </style>
    
</head>
<body>
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
                        <asp:LinkButton CssClass="course-item" ID="MyCourseLinkButton" runat="server" OnClick="MyCourseLinkButton_Click">My Course</asp:LinkButton>
                        <asp:LinkButton CssClass="course-item" ID="EnrollCourseLinkButton" runat="server" OnClick="TutorialLinkButton_Click">Enroll Course</asp:LinkButton>
                    </div>
                </li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>
        <%-- Content Code Below --%>
        <div class="wrap-content">
            <div class="Header">
                <h1><i>TutorialTrack</i>
                </h1>
            </div>
            <asp:Repeater ID="rptrTutorialTrack" runat="server">
                <ItemTemplate>
                    <div class="tutorialCompletion" style="float: left; width: 100%; height: 140px;">
                        <div class="container" style="float: left; margin-left: 20px; margin-top: 20px;">

                            <div class="progress-bar1" data-percent='<%# calculatePercentage(int.Parse(Eval("doneNumber").ToString()), int.Parse(Eval("totalStud").ToString())) %>' data-duration="1000" data-color="#ccc,#E74C3C" runat="server"></div>
                        </div>

                        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
                        <script src="jQuery-plugin-progressbar.js"></script>
                        <script src="js.js"></script>

                        <div class="description" style="margin-top: 20px; margin-left: 120px; float: left;">
                            <div class="courseName" style="font-size: 20pt">

                                <asp:Label ID="Label1" runat="server"><%# Eval("CourseID") %></asp:Label>

                                &nbsp;<asp:Label ID="Label6" runat="server"><%# Eval("CourseName") %></asp:Label>

                            </div>
                            <div class="tutorialName">

                                <asp:Label ID="Label2" runat="server">Tutorial <%# Eval("TutorialNumber") %> :</asp:Label>
                                <asp:Label ID="Label3" runat="server"><%# Eval("ChapterName") %></asp:Label>

                            </div>

                            <div class="tutorialDetail" style="font-size: 8pt; margin-top: 20px; position: absolute">

                                <asp:Label ID="Label5" runat="server"><%# Eval("doneNumber") %> / <%# Eval("totalStud") %> done</asp:Label>
                            </div>

                            <div class="tutorialDetail" style="font-size: 8pt; margin-top: 32px; position: absolute">

                                <asp:Label ID="Label4" runat="server">Date:<%# Eval("StartDate") %> to <%# Eval("ExpiryDate") %></asp:Label>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="NoResult" runat="server" id="NoResultPanel">
                <asp:Image CssClass="NoResultImg" ID="imgNoTutorial" ImageUrl="~/images/no-results.png" Width="30%" Height="20%" runat="server" />
                 <div class="caption">
                    <h1><asp:Label CssClass="NoResultTitle" ID="lblNoTutorialTitle" runat="server" Text="Whooops!"></asp:Label></h1>
                    <asp:Label CssClass="NoResultDesc" ID="lblNoTutorialDesc" runat="server" Text="Currently there has no tutorial been activated."></asp:Label>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
