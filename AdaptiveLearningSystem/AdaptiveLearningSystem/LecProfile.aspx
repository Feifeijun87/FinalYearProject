<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LecProfile.aspx.cs" Inherits="AdaptiveLearningSystem.LecProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />

    <link rel="stylesheet" runat="server" media="screen" href="main.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="ProfileLec.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        .lblError{
            margin-left:4%;
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
       <%-- Content Code Below --%>
    <div class="wrap-content" >
        <div class="Header">
            <h1><i>
            Profile</i>
            </h1>
       </div>
        
        <div class="profilePanel">
            <div style="float:left" >
            <asp:Image ID="LecProfileImg"  runat="server" Height="229px" Width="244px" ImageUrl="~/images/defaultProfileImg.jpg" />
            </div>

            <div style="float:left; ">
           <table class="profileTable" style="width:600px; margin-left:20px;"  >
                        <tr >
                            <td class="stayRight" style="text-align:right;padding:10pt;width:25% " >&nbsp;
                                <asp:Label ID="tlblTutorName" runat="server" Text="Name :"></asp:Label>
                            </td>
                            <td class="auto-style3">
                                <asp:Label ID="lblTutorName" runat="server" Text="Ms. Loo Zhe Xin" ></asp:Label>
                            </td>
                           
                        </tr>

                         <tr>
                            <td class="stayRight" style="text-align:right; padding:10pt;">&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="Position :" ></asp:Label>
                            </td>
                            <td class="auto-style23">
                                <asp:Label ID="lblPosition" runat="server" Text="Senior Lecturer" ></asp:Label>
                            </td>
                           
                        </tr>

                        <tr>
                            <td class="stayRight" style="text-align:right; padding:10pt;">&nbsp;
                                <asp:Label ID="Label3" runat="server" Text="Contact No :" ></asp:Label>
                            </td>
                            <td class="auto-style23">
                                <asp:Label ID="lblContact" runat="server" Text="012-8832901" ></asp:Label>
                            </td>
                           
                        </tr>

                         <tr>
                            <td class="stayRight" style="text-align:right; padding:10pt;">&nbsp;
                                <asp:Label ID="Label5" runat="server" Text="Faculty :" ></asp:Label>
                            </td>
                            <td class="auto-style23">
                                <asp:Label ID="lblFaculty" runat="server" Text="Faculty of Computing And Information Technology" ></asp:Label>
                            </td>
                           
                        </tr>
        
                        <tr>
                            <td class="stayRight" style="text-align:right; padding:10pt; ">&nbsp;
                                <asp:Label ID="Label7" runat="server" Text="Email :" ></asp:Label>
                            </td>
                            <td class="auto-style23">
                                <asp:Label ID="lblEmail" runat="server" Text="loozx@tarc.edu.my" ></asp:Label>
                            </td>
                           
                        </tr>

                        <tr>
                            <td class="stayRight" style="text-align:right; padding:10pt;">&nbsp;
                                <asp:Label ID="Label9" runat="server" Text="Office Location :" ></asp:Label>
                            </td>
                            <td class="auto-style23" >
                                <asp:Label ID="lblOfficeLoc" runat="server" Text="Block A 202" ></asp:Label>
                            </td>
                           
                        </tr>

                    </table>
                </div>
        </div>
        <div class="BottomButtonGroup">

            <asp:Button CssClass="BottomButton" ID="ChgPhoneBtn" runat="server" Text="Change Phone No." OnClick="ChgPhoneBtn_Click" />
            <asp:Button CssClass="BottomButton" ID="ChgOfficeBtn" runat="server" Text="Change Office Location" OnClick="ChgOfficeBtn_Click" />
            <asp:Button CssClass="BottomButton" ID="ChgPwdBtn" runat="server" Text="Change Password" OnClick="ChgPwdBtn_Click" />
        </div>
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Panel ID="pnlChgPass" CssClass="pnlChgPass" runat="server">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
            <div class="panelDtl">
                
                <table class="editProf">
                    <tr>
                        <td class="stayRight">
                            <asp:Label ID="Label2" runat="server"> Please enter old password: </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="stayRight">
                            <asp:Label ID="Label4" runat="server"> Please enter new password: </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="stayRight">
                            <asp:Label ID="Label6" runat="server"> Please  reenter  password: </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblCheckPass" CssClass="lblCheckPass" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                       
            </div>
           <asp:Label CssClass="lblError" ID="lblPassErrorMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <div class="BottomButtonGroup">
                <asp:Button ID="btnChgNewPass" CssClass="BottomButton" runat="server" Text="Change"  OnClick="btnChgNewPass_Click"/>
                <asp:Button ID="btnPassCancel" CssClass="BottomButton" runat="server" Text="Close" OnClick="btnPassCancel_Click" />
 </div> 
                </ContentTemplate>
                    </asp:UpdatePanel>        
                  
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="ChgPwdBtn"  PopupControlID="PnlChgPass" BackgroundCssClass="background" runat="server"></ajaxToolkit:ModalPopupExtender>
        
          <asp:Panel ID="pnlChgOffice" CssClass="pnlChgOffice" runat="server">
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
            <div class="panelDtl">
                
                <table class="editProf">
                    <tr>
                        <td class="stayRight">
                            <asp:Label ID="Label8" runat="server"> Please enter new office location : </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewOffice" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                   
                </table>
                       
            </div>
           <asp:Label CssClass="lblError" ID="lblOfficeErrorMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <div class="BottomButtonGroup">
                <asp:Button ID="btnChgOffice" CssClass="BottomButton" runat="server" Text="Change" OnClick="btnChgOffice_Click"  />
                <asp:Button ID="btnOfficeCancel" CssClass="BottomButton" runat="server" Text="Close" OnClick="btnOfficeCancel_Click" />
 </div> 
                </ContentTemplate>
                    </asp:UpdatePanel>
                
        </asp:Panel>
        
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" TargetControlID="ChgOfficeBtn" PopupControlID="PnlChgoffice" BackgroundCssClass="background" runat="server"></ajaxToolkit:ModalPopupExtender>
        
         <asp:Panel ID="pnlChgContact" CssClass="pnlChgContact" runat="server">
             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
            <div class="panelDtl">
                
                <table class="editProf">
                    <tr>
                        <td class="stayRight">
                            <asp:Label ID="Label10" runat="server"> Please enter new contact : </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewContact" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                   
                </table>
                       
            </div>
           <asp:Label CssClass="lblError" ID="lblContactErrorMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <div class="BottomButtonGroup">
                <asp:Button ID="btnChgContact" CssClass="BottomButton" runat="server" Text="Change" OnClick="btnChgContact_Click"   />
                <asp:Button ID="btnContactCancel" CssClass="BottomButton" runat="server" Text="Close" OnClick="btnContactCancel_Click" />
 </div> 
                </ContentTemplate>
                    </asp:UpdatePanel>

        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" TargetControlID="ChgPhoneBtn" PopupControlID="pnlChgContact" BackgroundCssClass="background" runat="server"></ajaxToolkit:ModalPopupExtender>
        
         </form>
</body>
</html>

