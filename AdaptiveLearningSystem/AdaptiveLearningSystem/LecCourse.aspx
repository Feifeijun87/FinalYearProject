<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LecCourse.aspx.cs" Inherits="AdaptiveLearningSystem.LecCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="LecCourse.css" />
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
                <h1><i>My Course</i>
                </h1>
            </div>
    
        <div class="container" runat="server" id="container">
            <%-- control put inside itemtemplate --%>
            <asp:Repeater ID="Repeater1"  runat="server" OnItemCommand="Repeater1_ItemCommand">
        <ItemTemplate>
            
            <div class="MyCourseList">
                <div class="CourseDesc">
                <div class="CourseName">
                    <%-- if not need combine string "<%# Eval("CourseID") %>" will do,
                         if want call c# function and bind the return value "<%# calculatePercentage(int.Parse(Eval("doneNumber").ToString())) %>"  something like that--%>
                    <%-- The value inside eval is the column name of the data in sql  --%>
                    <%-- next, c# code there --%>
                <asp:Label ID="lblCourseName" runat="server" Text='<%# String.Concat( Eval("CourseID").ToString().Trim(),"-",  Eval("CourseName").ToString().Trim())%>' ></asp:Label>
                </div>
                <div class="CourseDetails">
                    <asp:Label ID="lblTutorial" runat="server" Text='<%# String.Concat( Eval("NoOfTutorial") ," Tutorials")%>'></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text=" |"></asp:Label>
                    <asp:Label ID="lblGroup" runat="server" Text='<%# String.Concat( Eval("NoOfGroup")," Tutorial Groups" )%> '></asp:Label>
                </div>
                    </div>
                <div class="CourseButtonGroup">
                    <asp:Button CssClass="SelectButton CourseButton" ID="btnGoTutorial" runat="server" Text="Select" CommandName="select" /><asp:Button CssClass="DropButton CourseButton" ID="btnDrop" runat="server" Text="Drop" CommandName="drop" />
                </div>
            </div>
            
            </ItemTemplate>
        </asp:Repeater>
        </div>
           <div style="margin-bottom:20px; height:30px;" runat="server" id="filler">

           </div>

            <div class="NoResult" runat="server" id="NoResultPanel">
                <asp:Image CssClass="NoResultImg" ID="imgNoTutorial" ImageUrl="~/images/no-results.png" Width="30%" Height="20%" runat="server" />
                <div class="caption">
                    <h1>
                        <asp:Label CssClass="NoResultTitle" ID="lblNoTutorialTitle" runat="server" Text="Whooops!"></asp:Label></h1>
                    <asp:Label CssClass="NoResultDesc" ID="lblNoTutorialDesc" runat="server" Text="You have not enroll any courses yet."></asp:Label>

                </div>
                <div style="float: left; margin-left: 43%; margin-top: 10px; margin-bottom: 10px">
                    <asp:Button CssClass="EnrollButton" ID="btnEnroll" runat="server" Text="Go Enroll One" OnClick="btnEnroll_Click" />
                </div>
            </div>
            </div>
   
    </form>
</body>
</html>