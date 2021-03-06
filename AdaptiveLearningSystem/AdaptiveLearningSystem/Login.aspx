﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AdaptiveLearningSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page - Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />

    <link rel="stylesheet" runat="server" media="screen" href="Login.css"/>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300" type="text/css" />
    <script type="text/javascript">
        function check() {
            if (!document.getElementById("txtLecturerID").value) {
               
                document.getElementById('lblError').style.display = 'inherit';
                document.getElementById("lblError").innerText = "Username is empty";
                return false;
            }
            else if (!document.getElementById("txtLecturerPassword").value) {
                
                document.getElementById('lblError').style.display = 'inherit';
                document.getElementById("lblError").innerText = "Password is empty";
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="SystemTitle" style="width:700px;">
                <h1>Adaptive Learning System</h1>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="loginPanel" id="MainPanel" runat="server">

                        <div class="loginTitle">
                            <h1>
                                <asp:Label ID="Label1" runat="server" Text="Label"> LOGIN</asp:Label></h1>
                        </div>
                        <div class="errorMessage">
                            
                                <asp:Label ID="lblError" runat="server" Text="Label" ForeColor="Red" style="display: none;"></asp:Label>
                        </div>
                        <div class="categoryButton" runat="server" id="CategoryPanel">
                            <asp:Button CssClass="btnSignIn" ID="btnStudent" runat="server" Text="I Am A Student" OnClick="btnStudent_Click" />
                            <asp:Button CssClass="btnSignIn" ID="btnLecturer" runat="server" Text="I Am A Lecturer" OnClick="btnLecturer_Click" />
                        </div>


                        <div runat="server" id="StudentPanel">
                            <div class="loginDetails">

                                <asp:TextBox ID="txtStudentID" CssClass="LoginTextbox" runat="server" placeholder="Student ID"></asp:TextBox>
                                <asp:TextBox ID="txtStudentPassword" CssClass="LoginTextbox" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                                <asp:LinkButton CssClass="ForgetPass" ID="lbtnStudForgetPass" runat="server" OnClick="lbtnStudForgetPass_Click" style="height: 20px">Forget Password?</asp:LinkButton>
                            </div>

                            <div class="loginButton">
                                <asp:Button CssClass="btnSignIn" style="margin-bottom:10px" ID="btnStudentSignIn" runat="server" Text="Sign In" OnClick="btnStudentSignIn_Click" />
                            </div>
                        </div>

                        <div runat="server" id="LecturerPanel">
                            <div class="loginDetails">

                                <asp:TextBox ID="txtLecturerID" CssClass="LoginTextbox" runat="server" placeholder="Username"></asp:TextBox>
                                <asp:TextBox ID="txtLecturerPassword" CssClass="LoginTextbox" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                <asp:LinkButton CssClass="ForgetPass" ID="lbtnLecForgetPass" runat="server" OnClick="lbtnLecForgetPass_Click">Forget Password?</asp:LinkButton>
                            </div>

                            <div class="loginButton">
                                <asp:Button CssClass="btnSignIn" style="margin-bottom:10px" ID="btnLecturerSignIn" runat="server" Text="Sign In" OnClientClick="return check()"  OnClick="btnLecturerSignIn_Click"/>
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
              

        
   
    </form>
</body>
</html>
