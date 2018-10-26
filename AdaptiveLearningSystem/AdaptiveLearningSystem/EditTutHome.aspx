<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTutHome.aspx.cs" Inherits="AdaptiveLearningSystem.EditTutHome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="LecCourse.css" />
        <link rel="stylesheet" runat="server" media="screen" href="TutorialCreate.css" />
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

           .auto-style1 {
            height: 58px;
        }

        .auto-style2 {
            height: 82px;
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

        <div class="wrap-content">
           
            <table style="margin-left: 20px; border-bottom-style: none; width:100%" class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        <div class="Header">
                <h1><i>Tutorial List </i></h1>
            </div>
                    </td>
                </tr> 
                    <tr><td>
                        <div class="container" runat="server" id="container">
                     <asp:Repeater ID="Repeater1"  runat="server" OnItemCommand="Repeater1_ItemCommand" >
                             <ItemTemplate>
                           
                               <div class="MyCourseList">
                <div class="CourseDesc">
                         
                <asp:Label ID="lblCourse" runat="server"  Text='<%#Eval("Course")%>' ></asp:Label>
                          <br />
                     <asp:Label ID="lblTutorial" runat="server"  Text ='<%#  String.Concat( "Tutorial ", Eval("TutorialNumber")," - ",  Eval("ChapterName"))%>' ></asp:Label>  
                    <br />                        
                         Number of compulsory question: <asp:Label ID="lblTotalScore" runat="server" Text='<%#Eval("No Of Question")%>'></asp:Label>
                    <br />
                    </div>
                   <div class="CourseButtonGroup">
                       <asp:Button CssClass="SelectButton CourseButton" ID="btnEditTut" runat="server" Text="Edit Tutorial" CommandName="select" />
                
                    <asp:Button CssClass="SelectButton CourseButton" ID="btnDeleteTut" runat="server" Text="Delete Tutorial" CommandName="delete" />
                
                   </div>
                       </div>
            
                            </ItemTemplate>
        </asp:Repeater>
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </div>
                        </td></tr>
                </table>

          </div>
     
    </form>
</body>
</html>

