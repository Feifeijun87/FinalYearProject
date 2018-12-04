<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRegister.aspx.cs" Inherits="AdaptiveLearningSystem.StudentRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="StudentRegister.css" />
    <link rel="stylesheet" runat="server" media="screen" href="main.css"/>
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
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>

          <%-- Content Code Below --%>
    <div class="wrap-content" >
        <div class="Header">
            <h1><i>
            Student Registration</i>
            </h1>
       </div>

        <div class="panelAddStudent" style="display:block">
            <asp:Label CssClass="personalTitle" ID="Label3" runat="server" Text="Personal Infromation"></asp:Label>
            <table style="width: 100%;">
                <tr>
                    <td>
            <asp:Label CssClass="label" ID="Label1" runat="server" Text="Student ID: "></asp:Label><asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorID" runat="server" Text="Label"></asp:Label>
            </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label2" runat="server" Text="Student Name: "></asp:Label><asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorName" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label4" runat="server" Text="IC Number: "></asp:Label><asp:TextBox ID="txtIC" runat="server"> </asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorIC" runat="server" Text="Label"></asp:Label>
                        <asp:Label CssClass="lblReminder" ID="Label5" style="font-size:12pt;" runat="server" Text="*The pasword by be same with ICNo by default"></asp:Label></td>
 
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
            <asp:Label CssClass="label" ID="Label9" runat="server" Text="Student Email: "></asp:Label><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <asp:Label CssClass="lblError" ID="lblErrorEmail" runat="server" Text="Label"></asp:Label>
            </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label10" runat="server" Text="Intake : "></asp:Label><asp:DropDownList ID="ddlIntake" runat="server"></asp:DropDownList>
                        <asp:Label CssClass="lblError" ID="lblErrorIntake" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label CssClass="label" ID="Label11" runat="server" Text="Tutorial Group:"></asp:Label><asp:DropDownList ID="ddlGroup" runat="server">
                        <asp:ListItem Value="">Please Select</asp:ListItem><asp:ListItem Value="G1">Group 1</asp:ListItem> <asp:ListItem Value="G2">Group 2</asp:ListItem> <asp:ListItem Value="G3">Group 3</asp:ListItem> <asp:ListItem Value="G4">Group 4</asp:ListItem> <asp:ListItem Value="G5">Group 5</asp:ListItem> <asp:ListItem Value="G6">Group 6</asp:ListItem>
                        <asp:ListItem Value="G7">Group 7</asp:ListItem><asp:ListItem Value="G8">Group 8</asp:ListItem><asp:ListItem Value="G9">Group 9</asp:ListItem><asp:ListItem Value="G10">Group 10</asp:ListItem><asp:ListItem Value="G11">Group 11</asp:ListItem><asp:ListItem Value="G12">Group 12</asp:ListItem>
                        <asp:ListItem Value="G13">Group 13</asp:ListItem><asp:ListItem Value="G14">Group 14</asp:ListItem><asp:ListItem Value="G15">Group 15</asp:ListItem></asp:DropDownList>
                        <asp:Label CssClass="lblError" ID="lblErrorGroup" runat="server" Text="Label"></asp:Label>
                    </td>
 
                </tr>
                <tr>
                    <td>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                         <asp:Label CssClass="label" ID="Label13" runat="server" Text="Profile Image: "></asp:Label><asp:FileUpload CssClass="imgupload" ID="imgupload" runat="server" ClientIDMode="Static" onchange="this.form.submit();" />
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
