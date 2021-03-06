﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LecturerRegister.aspx.cs" Inherits="AdaptiveLearningSystem.LecturerRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="StudentRegister.css" />
    <link rel="stylesheet" runat="server" media="screen" href="adminMain.css"/>
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

                </li>            </ul>
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

          <%-- Content Code Below --%>
    <div class="wrap-content" >
        <div class="Header">
            <h1><i>
            Lecturer Registration</i>
            </h1>
       </div>

        <div class="panelAddStudent" style="display:block">
            <asp:Label CssClass="personalTitle" ID="Label3" runat="server" Text="Personal Infromation"></asp:Label>
            <table style="width: 100%;">
                <tr>
                    <td>
            <asp:Label CssClass="label" ID="Label1" runat="server" Text="Username: "></asp:Label><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorUsername" runat="server" Text="Label"></asp:Label>
            </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label2" runat="server" Text="Lecturer Name: "></asp:Label><asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorName" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label4" runat="server" Text="IC Number: "></asp:Label><asp:TextBox ID="txtIC" runat="server"> </asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorIC" runat="server" Text="Label"></asp:Label>
                        <asp:Label CssClass="lblReminder" ID="Label5" runat="server" style="font-size:12pt;" Text="*The pasword by be same with ICNo by default"></asp:Label></td>
 
                </tr>
                <tr>
                    <td> <asp:Label CssClass="label" ID="Label7" runat="server" Text="Contact No: "></asp:Label><asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorContact" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td> <asp:Label CssClass="label" ID="Label6" runat="server" Text="Address: "></asp:Label><asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorAddress" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
                <div class="panelAddStudent" style="display:block; margin-top:10px;margin-bottom:10px">
            <asp:Label CssClass="personalTitle" ID="Label8" runat="server" Text="Education Infromation"></asp:Label>
            <table style="width: 100%;">
                <tr>
                    <td>
            <asp:Label CssClass="label" ID="Label9" runat="server" Text="Lecturer Email: "></asp:Label><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorEmail" runat="server" Text="Label"></asp:Label>
            </td>
                </tr>
                <tr>
                    <td>
            <asp:Label CssClass="label" ID="Label12" runat="server" Text="Office Location: "></asp:Label><asp:TextBox ID="txtOffice" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorOffice" runat="server" Text="Label"></asp:Label>
            </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label10" runat="server" Text="Lecturer Title : "></asp:Label><asp:DropDownList ID="ddlTitle" runat="server" >
                        <asp:ListItem Value="Mr">Mr</asp:ListItem><asp:ListItem Value="Ms">Miss</asp:ListItem><asp:ListItem Value="Mdm">Madam</asp:ListItem>
                        <asp:ListItem Value="Doctor">Doctor</asp:ListItem></asp:DropDownList>
                        <asp:Label CssClass="lblError" ID="lblErrorTitle" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label11" runat="server" Text="Position :"></asp:Label><asp:DropDownList ID="ddlPosition" runat="server">
                        <asp:ListItem Value="Lecturer">Lecturer</asp:ListItem> <asp:ListItem Value="Senior Lecturer">Senior Lecturer</asp:ListItem> <asp:ListItem Value="Principal Leader">Principal Leader</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label CssClass="lblError" ID="lblErrorPosition" runat="server" Text="Label"></asp:Label>
                    </td>
 
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label14" runat="server" Text="Faculty : "></asp:Label><asp:DropDownList ID="ddlFaculty" runat="server"></asp:DropDownList>
                        <asp:Label CssClass="lblError" ID="lblErrorFaculty" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                         <asp:Label CssClass="label" ID="Label13" runat="server" Text="Profile Image: "></asp:Label><asp:FileUpload CssClass="imgupload" ID="imgupload" runat="server" onchange="this.form.submit();" />
                        <br />
                        <asp:Label CssClass="lblImgUpload" ID="lblImgUpload" runat="server" Text="No file Selected"></asp:Label>
                        <asp:Label CssClass="lblError" ID="lblErrorImage" runat="server" Text="Label"></asp:Label>
                        </ContentTemplate></asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div class="BottomButtonGroup">
            <asp:Button CssClass="BottomButton" ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
            
        </div>
    </div>

    </form>
</body>
</html>
