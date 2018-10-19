<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTut.aspx.cs" Inherits="AdaptiveLearningSystem.CreateTut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" runat="server" media="screen" href="main.css" />
    <link rel="stylesheet" runat="server" media="screen" href="TutorialCreate.css" />
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
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            float: left;
        }

        .auto-style4 {
            width: 200px;
        }
        .auto-style5 {
            height: 33px;
        }
        .auto-style6 {
            width: 115px;
        }
        .auto-style7 {
            height: 40px;
        }
        .auto-style8 {
            width: 115px;
            height: 40px;
        }
        .background {
    background-color: black;
    opacity: 0.8;
    filter: alpha(opacity=90);
}

        
    .editProf td {
    height:40px;
    border-bottom:none;
}
.pnlChgContact{
    width:40%;
    background-color:white;
}
.panelDtl{
    float:left;
    width:100%;
}
.editProf{
    float:left;
    margin-left:20px;
    margin-top:20px;
}

.hide{
    display:none;
}

        .auto-style9 {
            width: 258px;
            height: 95px;
        }
        .auto-style10 {
            width: 646px;
            height: 95px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div id="upperNavBar">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <ul id="UpperNavList">
                <li>
                    <img id="tarcIcon" src="images/tarcIcon.png" width="50" /><p>Adaptive Learning System</p>
                </li>
                <li class="dropdown"><a>
                    <asp:Image ID="profilePic" ImageUrl="~/images/profIcon.png" runat="server" Style="width: 35px" />
                    <span id="welcome"><small>Welcome</small><br />
                        <small>
                            <asp:Label ID="lblUserName" runat="server" Text="Label">Loo Zhe Xin</asp:Label></small>
                    </span>
                    <div id="downIcon">&#9660;</div>
                </a>
                    <div class="dropdown-content">
                        <asp:LinkButton CssClass="dropdown-item" ID="ProfileLinkButton" runat="server">Profile</asp:LinkButton>
                        <asp:LinkButton CssClass="dropdown-item" ID="LogOutLinkButton" runat="server">Log Out</asp:LinkButton>
                    </div>

                </li>

            </ul>
        </div>
        <div>
            <ul class="SecondNavBar">
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="HomeLinkButton" runat="server">Home</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="TutorialLinkButton" runat="server">Course</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server" OnClick="ResultLinkButton_Click">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server">My Profile</asp:LinkButton></li>
            </ul>
        </div>
        <%-- start your coding inside div below --%>
        
        <div class="wrap-content">
            <div class="Header">
                
                <h1><i>Create Tutorial Question</i>
                </h1>
            </div>
            <div class="profilePanel">
                <div class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                    <table style="margin-left: 20px; border-bottom-style: none;" class="auto-style1">
                        <tr>
                            <td colspan="2" class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:&nbsp; </label>&nbsp;
                                <asp:DropDownList ID="ddlCourse" runat="server" Width="241px" Height="33px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td class="auto-style9">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial:&nbsp; <asp:TextBox ID="txtTutNum" runat="server" Width="101px" OnTextChanged="txtTutNum_TextChanged"></asp:TextBox>
                                <br />
                                    <asp:Label ID="lblTutNumEnter" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                </label>
                            </td>
                            <td class="auto-style10">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial Title: <asp:TextBox ID="txtTutName" runat="server" Width="288px"></asp:TextBox>
                                <br />
                                    <asp:Label ID="lblTutTitleEnter" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                </label>

                                </td>
                        </tr>
                        

                    </table>
                    <hr />
                    <div class="auto-style2">

                        <table style="margin-left: 20px; border-bottom-style: none;" class="auto-style1">
                            <tr>
                                <td class="auto-style4">Question
                                    <asp:Label ID="lblQNum" runat="server" Text="1"></asp:Label>
                                    :
                                </td>
                                <td colspan="2">

                                    <asp:TextBox ID="txtQues" runat="server" Height="50px" Width="460px"></asp:TextBox>

                                    <br />
                                    <asp:Label ID="lblQuestEnter" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Sample answers:
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtAns" runat="server" Height="50px" Width="460px"></asp:TextBox><br />
                                    <asp:Label ID="lblAnsEnter" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                    <br />
</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Keywords: 
                                </td>
                                <td colspan="2"><asp:TextBox ID="txtKeyword" runat="server" Height="50px" Width="460px"></asp:TextBox>
                                    <br />Please seperate each keyword by ','<br />
                                    <asp:Label ID="lblKeyEnter" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                    <br />
</td>
                            </tr>

                            <tr>
                                <td class="auto-style4">Maximum Completion Time:<br />
                                    &nbsp;</td>
                               <td class="auto-style4">
                                    <asp:DropDownList ID="ddlCompleteTime" runat="server" Width="104px" Height="30px">
                                        <asp:ListItem>1 min</asp:ListItem>
                                        <asp:ListItem Value="2 mins">2 mins</asp:ListItem>
                                        <asp:ListItem>3 mins</asp:ListItem>
                                        <asp:ListItem>4 mins</asp:ListItem>
                                        <asp:ListItem>5 mins</asp:ListItem>
                                        <asp:ListItem>6 mins</asp:ListItem>
                                        <asp:ListItem>7 mins</asp:ListItem>
                                        <asp:ListItem>8 mins</asp:ListItem>
                                        <asp:ListItem>9 mins</asp:ListItem>
                                        <asp:ListItem>10 mins</asp:ListItem>
                                        <asp:ListItem>11 mins</asp:ListItem>
                                        <asp:ListItem>12 mins</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                               
                            </tr>
                            <tr>
                                 <td>Difficulty Level:<br />
                                </td>
                                
                                <td>
                                    <asp:DropDownList ID="ddlLevel" runat="server" Width="109px" Height="30px">
                                        <asp:ListItem>Easy</asp:ListItem>
                                        <asp:ListItem>Medium</asp:ListItem>
                                        <asp:ListItem>Difficult</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                        </table>
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                        <br />
                    </div>
                   

                    <div class="BottomButtonGroup">
                        <asp:Button CssClass="BottomButton" ID="btnRemove" runat="server" Text="Remove this question" OnClick="btnRemove_Click" />
                        <asp:Button CssClass="BottomButton" ID="btnBack" runat="server" OnClick="Button1_Click" Text="Back" />
                        <asp:Button CssClass="BottomButton" ID="btnNext" runat="server" OnClick="Button2_Click" Text="Next" />
                        <asp:Button CssClass="BottomButton" ID="btnReset" runat="server" OnClick="Button3_Click" Text="Reset" />
                        <asp:Button CssClass="BottomButton" ID="btnFin" runat="server" OnClick="Button4_Click" Text="Finish" />
                    </div>
                </div>

                 </div>
            </div>
        </ContentTemplate>
                    </asp:UpdatePanel>
                        
                    <asp:Panel ID="compQuestNum" CssClass="pnlChgContact" runat="server">
             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
            <div class="panelDtl">
                
                <table class="editProf">
                    <tr>
                        <td class="auto-style7">
                            Compulsory Easy:</td>
                        <td class="auto-style8">
                            <asp:TextBox ID="txtEasy" runat="server" Width="62px" Height="29px"></asp:TextBox>/ <asp:Label ID="lblCompEasyNum" runat="server">0</asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="stayRight">
                            Compulsory Medium:</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtMed" runat="server" Width="62px"></asp:TextBox>
                            /
                            <asp:Label ID="lblCompMedNum" runat="server">0</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td  class="stayRight">Compulsory Difficult:</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtDifficult" runat="server" Width="62px"></asp:TextBox>
                            /
                            <asp:Label ID="lblCompDiffNum" runat="server">0</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblCompErrorMsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                       
            </div>
           
            <div class="BottomButtonGroup">
                
 </div> <div class="BottomButtonGroup">
                           
                 
                            <asp:Button ID="btnCancel" runat="server" CssClass="BottomButton" OnClick="btnContactCancel_Click" Text="Close" />
                            <asp:Button ID="btnDone" CssClass="BottomButton" runat="server" Text="Done" OnClick="btnChgContact_Click" />
                 
 </div>
                </ContentTemplate>
                    </asp:UpdatePanel>
                        
                
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" TargetControlID="hide" PopupControlID="compQuestNum" BackgroundCssClass="background" runat="server"></ajaxToolkit:ModalPopupExtender>
        
                <asp:Button ID="hide"  runat="server" OnClick="btnChgContact_Click" />
    </form>
</body>
</html>
