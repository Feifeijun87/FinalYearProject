<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivateCourse.aspx.cs" Inherits="AdaptiveLearningSystem.ActivateCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="DropCourse.css" />
    <link rel="stylesheet" runat="server" media="screen" href="adminMain.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
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

    </style>
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
                   <div class="SecondNavList"> Users</div>
                    <div class="course-content">
                        <asp:LinkButton CssClass="course-item" ID="btnAddStudent" OnClick="btnAddStudent_Click" runat="server">Add Student</asp:LinkButton>
                        <asp:LinkButton CssClass="course-item" ID="btnAddLecturer" runat="server" OnClick="btnAddLecturer_Click">Add Lecturer</asp:LinkButton>
                    </div>
                </li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="btnAddCourse" runat="server" OnClick="btnAddCourse_Click">Add Course</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ActivateCourseLinkButton" runat="server" OnClick="btnActivateCourse_Click">Activate Course</asp:LinkButton></li>
            </ul>
        </div>

         <div class="wrap-content">
            <div class="Header">
                <h1><i>Add Course</i>
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
                    <asp:Label ID="lblCourse" runat="server" Text="Course: "></asp:Label>
                    <asp:DropDownList CssClass="dropdownFormat" ID="ddlCourse" runat="server" ></asp:DropDownList>
                    <asp:Label ID="lblErrorCourse" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="displayFormat">
                    <asp:Label ID="lblIntake" runat="server" Text="Intake: "></asp:Label>
                    <asp:DropDownList ID="ddlIntake" runat="server"></asp:DropDownList>
                    <asp:Label ID="lblErrorIntake" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                </div>

            </div>
            <div>
                <div class="ButtonGroup">
                    <asp:Button CssClass="DropButton" ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                </div>
            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>

    </form>
</body>
</html>
