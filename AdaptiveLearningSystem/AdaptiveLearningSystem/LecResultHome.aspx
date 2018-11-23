<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LecResultHome.aspx.cs" Inherits="AdaptiveLearningSystem.LecResultHome" %>

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
            .auto-style3 {
                width: 157px;
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
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%-- start your coding inside div below --%>
    <div class="wrap-content" >
        <div class="Header">
            <h1><i>
            Student Result</i>
            </h1>
       </div>
        <div style="margin-left:7%; margin-bottom:20px;">
        
            <div style="margin-top:20px;">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td colspan="2">
                            Please select the type of report : 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                             <asp:RadioButtonList ID="radReportSelect" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="tutGroup" Selected="True">View student performance by tutorial group</asp:ListItem>
                    <asp:ListItem Value="tutProg">View student performance by programme</asp:ListItem>
                                 <asp:ListItem Value="questGroup">View tutotial question performance by tutorial group</asp:ListItem>
                                 <asp:ListItem Value="quest">View tutotial question performance by programme</asp:ListItem>
                </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                                <br />           
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:Label ID="lblCourse" runat="server" Text="Course : "></asp:Label> 
                             </td>
                        <td><asp:DropDownList ID="ddlCourse" runat="server" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"  AutoPostBack="True">
                </asp:DropDownList>
                             
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNoIntake" runat="server" Text="You haven't enrolled any courses." Visible="False"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style3">
                          <asp:Label ID="lblIntake" runat="server" Text="Intake : " Visible ="false"></asp:Label>
                                      
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIntake" runat="server" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged"  AutoPostBack="True" style="height: 29px" Visible="False">
                </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                         <td colspan="2">
                             <asp:Label ID="lblNoTutorial" runat="server" Text="No tutorial found" Visible="False"></asp:Label>
                         </td>
                     </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:Label ID="lblTutorial" runat="server" Text="Tutorial : " Visible="False"></asp:Label>
                        </td>
                        <td>
                             <asp:DropDownList ID="ddlTutorial" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNoTutGroup" runat="server" Text="No tutorial group found" Visible="False"></asp:Label>
                
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:Label ID="lblTutGroup" runat="server" Text="Tutorial Group : " Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTutGroup" runat="server" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlTutGroup_SelectedIndexChanged">
                </asp:DropDownList>
                        </td>
                    </tr>
                </table>
              
            <div class="BottomButtonGroup">
                        <asp:Button CssClass="BottomButton" ID="btnDone" runat="server" Text="Done" OnClick="btnDone_Click" Visible="False" />
                        </div>
</div>
            </ContentTemplate>
    </asp:UpdatePanel>
                  
        </div>
        </div>
    </form>
</body>
</html>
