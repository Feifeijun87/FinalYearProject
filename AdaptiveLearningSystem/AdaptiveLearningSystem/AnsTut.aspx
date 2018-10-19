<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnsTut.aspx.cs" Inherits="AdaptiveLearningSystem.AnsTut" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" runat="server" media="screen" href="studMain.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="TutorialCreate.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <style>
            .background {
                background-color: black;
                opacity: 0.8;
                filter: alpha(opacity=90);
            }

            .editProf td {
                height: 40px;
                border-bottom: none;
            }

            .pnlChgPass, .pnlChgOffice, .pnlChgContact {
                width: 40%;
                background-color: white;
            }

            .panelDtl {
                float: left;
                width: 100%;
            }

            .editProf {
                float: left;
                margin-left: 20px;
                margin-top: 20px;
            }

            .stayRight {
                text-align: right;
            }

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
                text-align: left;
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
            .detailPosition{
                float:left;margin-left:20px;display:block; width:100%;
                margin-bottom:20px;
            }
            .right {
                margin-left: 70%;
            }
            .qwer{
                margin-left:73%;
            }
            .hide{
                display:none;
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
            height: 34px;
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
                    <asp:LinkButton CssClass="SecondNavList" ID="HomeLinkButton" runat="server" OnClick="HomeLinkButton_Click">Home</asp:LinkButton></li>
                
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server" OnClick="ResultLinkButton_Click1" >Tutorial Result</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>
        <%-- start your coding inside div below --%>

        <div class="wrap-content">
            <div class="Header">
                <h1><i>Answer Tutorial Question</i>
                </h1>
            </div>
            <div class="profilePanel">
                <div class="auto-style2">
                    
                    <table style="margin-left: 20px; border-bottom-style: none;" class="auto-style1">
                        <tr>
                            <td colspan="2" class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:&nbsp; </label>&nbsp;
                                <asp:Label ID="lblCourse" runat="server"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td style="width: 258px">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial:&nbsp; 
                                <asp:Label ID="lblTutorial" runat="server"></asp:Label>
                                <br />
                                    <asp:Label ID="lblTutNumEnter" runat="server" ForeColor="Red"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 646px">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial Title: 
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                <br />
                                    <asp:Label ID="lblTutTitleEnter" runat="server" ForeColor="Red"></asp:Label>
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
                                <td>

                                    <asp:Label ID="lblQuest" runat="server"></asp:Label>

                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Answers:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAns" runat="server" Height="50px" Width="460px" TextMode="MultiLine"></asp:TextBox><asp:Label ID="Label8" Visible="false" runat="server" Text="correct"></asp:Label><br />
                                    <asp:Label ID="lblAnsEnter" runat="server" ForeColor="Red"></asp:Label>
                                    <br />
</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="always">
        <ContentTemplate>
                                        <asp:Label ID="lblTimeCount" runat="server"></asp:Label>
                                    <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                                    </asp:Timer>
                       </ContentTemplate>
    </asp:UpdatePanel>
                    </td>
                            </tr>
                        </table>
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                        <br />
                    </div>
                   

                    <div class="BottomButtonGroup">
                        
                        <asp:Button CssClass="BottomButton" ID="btnReset" runat="server" OnClick="Button3_Click" Text="Reset" />
                        <asp:Button CssClass="BottomButton" ID="btnScore" runat="server" OnClick="btnScore_Click" Text="Result" />
                        <asp:Button CssClass="BottomButton" ID="btnNext" runat="server" OnClick="Button2_Click" Text="Next" />
                        <asp:Button CssClass="BottomButton" ID="btnDone" runat="server" OnClick="Button4_Click" Text="Done" />
                    </div>
                    
                        
                </div>
                 </div>
            </div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="hide" PopupControlID="pnlScore" BackgroundCssClass="background" runat="server"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlScore" CssClass="pnlChgContact" runat="server">
             <div class="detailPosition">
                 <asp:Label ID="Label6" runat="server" Text="Question"></asp:Label>
                 <asp:Label CssClass="right" ID="Label7" runat="server" Text="Points"></asp:Label>
             </div>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>              
            <div class="detailPosition"">
                <asp:Label ID="Label4" runat="server" Text='<%# String.Concat("Question ",  Container.ItemIndex + 1) %>'></asp:Label>
                <asp:Label  CssClass="qwer" ID="Label5" runat="server" Text='<%#Eval("Points")%>'></asp:Label>
            </div>
                    </ItemTemplate>
            </asp:Repeater>
                        <div class="BottomButtonGroup">
                <asp:Button ID="btnContactCancel" CssClass="BottomButton" runat="server" Text="Close" />
 </div>
            <asp:Button ID="hide" runat="server" Text="Button" CssClass="hide" />
        </asp:Panel>
                
    </form>
</body>
</html>
