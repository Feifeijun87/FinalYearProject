<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentForgetPass.aspx.cs" Inherits="AdaptiveLearningSystem.StudentForgetPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forget Password - Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" runat="server" media="screen" href="LecForgetPass.css"/>
    <script type="text/javascript">
        function check() {
            if (!document.getElementById("txtEmail").value) {
               
                document.getElementById('lblError').style.display = 'inherit';
                document.getElementById("lblError").innerText = "Email Address is empty";
                return false;
            }
            
            return true;
        }

        function checkDetails() {
            if (!document.getElementById("txtCode").value) {

                document.getElementById('lblError').style.display = 'inherit';
                document.getElementById("lblError").innerText = "Code is empty";
                return false;
            }
            else if (!document.getElementById("txtNewPass").value) {

                document.getElementById('lblError').style.display = 'inherit';
                document.getElementById("lblError").innerText = "New password cannot be empty";
                return false;
            }
            else if (!document.getElementById("txtConfirmPass").value) {

                document.getElementById('lblError').style.display = 'inherit';
                document.getElementById("lblError").innerText = "Confirm password cannot be empty";
                return false;
            }

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="SystemTitle">
                <h1>Adaptive Learning System</h1>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="ForgetPanel" id="MainPanel" runat="server">

                        <div class="ForgetTitle">
                            <h1>
                                <asp:Label ID="Label1" runat="server" Text="Label"> Forget Password</asp:Label></h1>
                        </div>
                        <div class="description">
                            <asp:Label ID="lblDesc" runat="server" ForeColor="white">Enter your student ID to retrieve your password</asp:Label>
                        </div>
                        <div class="errorMessage">

                            <asp:Label ID="lblError" runat="server" Text="Label" ForeColor="Red" Style="display: none;"></asp:Label>
                        </div>
                        <div runat="server" id="EmailPanel">
                            <div class="EmailDetails">

                                <asp:TextBox ID="txtStudentID" CssClass="ForgetTextbox" runat="server" placeholder="Student ID"></asp:TextBox>

                            </div>

                            <div class="ForgetButton">
                                <asp:Button CssClass="btnSubmit" ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return check()" OnClick="btnSubmit_Click" />

                            </div>
                        </div>
                    <div runat="server" id="ResetPanel">
                        <div class="ResetDetails">

                            <asp:TextBox ID="txtCode" CssClass="ForgetTextbox" runat="server" placeholder="Code"></asp:TextBox>
                            <asp:TextBox ID="txtNewPass" CssClass="ForgetTextbox" TextMode="Password"  runat="server" placeholder="Enter New Password"></asp:TextBox>
                            <asp:TextBox ID="txtConfirmPass" CssClass="ForgetTextbox" TextMode="Password" runat="server" placeholder="Confirm New Password"></asp:TextBox>

                        </div>

                        <div class="loginButton">
                            <asp:Button CssClass="btnSubmit" ID="btnChange" runat="server" Text="Confirm" OnClientClick="return checkDetails()"  OnClick="btnChange_Click" />
                            <asp:Button CssClass="btnSubmit" ID="btnBackLogin" runat="server" Text="Back To Login" OnClick="btnBackLogin_Click" />
                        </div>
                    </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>




    </form>
</body>
</html>

