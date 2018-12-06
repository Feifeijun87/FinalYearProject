<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTut.aspx.cs" Inherits="AdaptiveLearningSystem.EditTut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
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

     <script>
        function confirm_user() {
            if (confirm("Are you sure you want to leave? The changes made might not be saved.") == true)
                return true;
            else
                return false;
        }
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
        .rowEven{
    background-color:#D6EAF8;
}

.rowOdd{
    background-color:lightblue;
}
     
    </style>
    <style type="text/css">
        .auto-style13 {
            float: left;
            width: 60%;
            height: 600px;
        }
        .auto-style14 {
            float: left;
            width: 90%;
            margin-left: 20px;
            margin-top: 20px;
        }

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
        .auto-style7 {
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
    width:80%;
    background-color:white;
}
.panelDtl{
    float:left;
    width:100%;
}

    .design{
    width:350px;
    padding:40px;
    text-align:center;
    border:1px solid black;
    background-color :#D6EAF8;
    border-radius: 25px;
    float:initial;
    margin-left:27px;
    margin-top:27%;
     
}
.editProf{
      float:initial;
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

        .auto-style15 {
            height: 51px;
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
                        <asp:LinkButton CssClass="dropdown-item" ID="LogOutLinkButton" runat="server"  OnClientClick="return confirm_user()" OnClick="LogOutLinkButton_Click">Log Out</asp:LinkButton>
                    </div>

                </li>
                
            </ul>
        </div>
        <div>
            <%-- Second Line Navigation Bar Below --%>
            <ul class="SecondNavBar">
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="HomeLinkButton" runat="server"  OnClientClick="return confirm_user()" OnClick="HomeLinkButton_Click">Home</asp:LinkButton></li>
                <li class="coursebtn">
                   <div class="SecondNavList"> Course</div>
                    <div class="course-content">
                        <asp:LinkButton CssClass="course-item" ID="MyCourseLinkButton" runat="server"  OnClientClick="return confirm_user()" OnClick="MyCourseLinkButton_Click">My Course</asp:LinkButton>
                        <asp:LinkButton CssClass="course-item" ID="EnrollCourseLinkButton" runat="server"  OnClientClick="return confirm_user()" OnClick="TutorialLinkButton_Click">Enroll Course</asp:LinkButton>
                    </div>
                </li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton"  OnClientClick="return confirm_user()" OnClick="ResultLinkButton_Click" runat="server">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server"  OnClientClick="return confirm_user()" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="wrap-content">
            <div class="Header">
                
                <h1>&nbsp;<i>Edit Tutorial Question</i>
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
                                <asp:Label ID="lblCourse" style="font-family: Arial, Helvetica, sans-serif; font-size: large" runat="server"></asp:Label>
                                <br />
                                
                                <br />
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td class="auto-style9">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial:&nbsp; <asp:TextBox ID="txtTutNum" runat="server" Width="101px" OnTextChanged="txtTutNum_TextChanged"></asp:TextBox>
                                <br />
                                    <asp:Label ID="lblTutNumEnter" runat="server" ForeColor="Red"></asp:Label>
                                </label>
                            </td>
                            <td class="auto-style10">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial Title: <asp:TextBox ID="txtTutName" runat="server" Width="288px"></asp:TextBox>
                                <br />
                                    <asp:Label ID="lblTutTitleEnter" runat="server" ForeColor="Red"></asp:Label>
                                </label>

                                </td>
                        </tr>
                        

                    </table>
                    <hr />
                    <div class="auto-style2">

                        <table style="margin-left: 20px; margin-right: 20px; border-bottom-style: none;" class="auto-style1">
                           
                            <tr>
                                <td  colspan="2">
                                    <asp:Label ID="Label1" runat="server" Text="Please note that all tutorial question will not be save until you complete edit the whole tutorial and update the compulsory number of question for this tutorial."></asp:Label>
                                    
                                </td>
                            </tr>
                             <tr>
                                <td class="auto-style4" style="width:200px">Question
                                    <asp:Label ID="lblQNum" runat="server" Text="1"></asp:Label>
                                    :
                                </td>
                                <td>

                                    <asp:TextBox ID="txtQues" runat="server" Height="50px" Width="460px" Font-Names="Arial" TextMode="MultiLine"></asp:TextBox>

                                    <br />
                                    <asp:Label ID="lblQuestEnter" runat="server" ForeColor="Red" ></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4" style="width:200px">Sample answers:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAns" runat="server" Height="50px" Width="460px" Font-Names="Arial" TextMode="MultiLine"></asp:TextBox><br />
                                    <asp:Label ID="lblAnsEnter" runat="server" ForeColor="Red"></asp:Label>
                                    <br />
</td>
                            </tr>
                            <tr>
                                <td class="auto-style4" style="width:200px">Keywords: 
                                </td>
                                <td><asp:TextBox ID="txtKeyword" runat="server" Height="50px" Width="460px" Font-Names="Arial" TextMode="MultiLine"></asp:TextBox>
                                    <br />Please seperate each keyword by ','<br />
                                    <asp:Label ID="lblKeyEnter" runat="server" ForeColor="Red"></asp:Label>
                                    <br />
</td>
                            </tr>

                            <tr>
                                <td class="auto-style4" style="width:200px">Time allowed to get points:<br /> &nbsp;<br />
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
                                 <td class="auto-style4" style="width:200px">Difficulty Level:<br />
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
                        
                        <br />
                    </div>
                   

                    <div class="BottomButtonGroup">
                        <asp:Button CssClass="BottomButton" ID="btnRemove" runat="server" Text="Remove this question" OnClick="btnRemove_Click" Visible="True" />
                        <asp:Button CssClass="BottomButton" ID="btnBack" runat="server" OnClick="Button1_Click" Text="Previous" Visible="False"  />
                        <asp:Button CssClass="BottomButton" ID="btnNext" runat="server" OnClick="Button2_Click" Text="Next" />
                        <asp:Button CssClass="BottomButton" ID="btnReset" runat="server" OnClick="Button3_Click" Text="Reset" Visible="True"  />
                        <asp:Button CssClass="BottomButton" ID="btnFin" runat="server" OnClick="Button4_Click" Text="Finish"  />
                    </div>
                </div>

                 </div>
            </div>
        </ContentTemplate>
                    </asp:UpdatePanel>
                        
                    <asp:Panel ID="compQuestNum" CssClass="pnlChgContact" runat="server" >
             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
            <div class="auto-style13" style="overflow-y:scroll; background-color:white" >
                
               <table class="auto-style14" border="0" style="border-style: none; border-width: 0px; border-collapse: collapse; border-spacing: 0px;" >
                    <tr>
                        <td class="auto-style12" style="font-size: x-large; text-decoration: underline; background-color:#2980B9  " >
                              <b>Tutorial Question Preview</b>
                        </td>
                    </tr>
                   
             <asp:Repeater ID="categoryRepeater" runat="server" >
                 <ItemTemplate><asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" >
    <tr class="<%# Container.ItemIndex % 2 == 0 ? "rowOdd" : "rowEven" %>">
        <td>
            <table class="auto-style14" border="0" style="border-style: none; border-width: 0px; border-collapse: collapse; border-spacing: 0px;" >

            <tr><td><asp:Label ID="lblNumber" runat="server" Width ="5px" Text='<%# Eval("Key") %>'></asp:Label> .
            <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Value") %>' ><ItemTemplate>
           <asp:Label ID="lblquest" 
                        runat="server" Font-Bold="True"
                        Text='<%# Eval("Key") %>' /></td></tr>
   
      
                     <asp:Repeater ID="nestedRepeater" runat="server" DataSource='<%# Eval("Value") %>'>
                         <ItemTemplate>
                             <tr>
                                 <td><b>Sample answer :</b>
                                     <asp:Label ID="lblAns" runat="server" Text='<%# Eval("Key") %>' />
                                 </td>
                             </tr>
                             <asp:Repeater ID="nestedRepeater1" runat="server" DataSource='<%# Eval("Value") %>'>
                                 <ItemTemplate>
                                     <tr>
                                         <td><b>Keyword :</b>
                                             <asp:Label ID="lblKey" runat="server" Text='<%# Eval("Key") %>' />
                                         </td>
                                     </tr>
                                     <asp:Repeater ID="nestedRepeater1" runat="server" DataSource='<%# Eval("Value") %>'>
                                         <ItemTemplate>
                                             <tr>
                                                 <td><b>Time allowed to get points: </b>
                                                     <asp:Label ID="lblTime" runat="server" Text='<%# Eval("Key") %>' /> minutes
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td><b>Difficulty level :</b>
                                                     <asp:Label ID="lblLevel" runat="server" Text='<%# Eval("Value") %>' />
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <br />
                                                 </td>
                                             </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                 </ItemTemplate>
                             </asp:Repeater>
                         </ItemTemplate>
                     </asp:Repeater></ItemTemplate>
                     </asp:Repeater></td>
    </tr></table></asp:Panel></ItemTemplate></asp:Repeater>
   
      
        </table>
                    </div>
                        <div id="num" style="width:40%; float:right">
                <div class="design">
                     <table class="editProf">
                            <tr>
                        <td style="text-align:left;font-size:large" >
                           <b>Please enter the number of compulsory question for student to answer for each difficulty level</b>
                        </td></tr>
                </table>

                                <table class="editProf" style="border: 1px solid #000000; padding: 1px;">
                    <tr>
                        <td class="auto-style7" style="text-align:left">
                            Compulsory Easy Question :</td>
                        <td >
                            <asp:TextBox ID="txtEasy" Style="text-align:center" runat="server" Width="40px"></asp:TextBox> / <asp:Label ID="lblCompEasyNum" Width="20px" runat="server">0</asp:Label><asp:Label ID="lblErrorEasy" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="stayRight" style="text-align:left">
                            Compulsory Medium Question :</td>
                        <td >
                            <asp:TextBox ID="txtMed" Style="text-align:center" runat="server" Width="40px"></asp:TextBox>
                            /
                            <asp:Label ID="lblCompMedNum" Width="20px" runat="server">0</asp:Label><asp:Label ID="lblErrorMed" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td  class="stayRight" style="text-align:left">Compulsory Difficult Question:</td>
                        <td >
                            <asp:TextBox ID="txtDifficult" Style="text-align:center" runat="server" Width="40px"></asp:TextBox>
                            /
                            <asp:Label ID="lblCompDiffNum" Width="20px" runat="server">0</asp:Label><asp:Label ID="lblErrorDiff" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    </table>
                        <table class="editProf">
                            <tr>
                        <td>
                            <asp:Label ID="lblCompErrorMsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        </td></tr>
                </table>
                       
            
           <div class="PanelBottomButtonGroup">
                           
                 
                            <asp:Button ID="btnCancel" runat="server" CssClass="BottomButton" OnClick="btnContactCancel_Click" Text="Close" />
                            <asp:Button ID="btnDone" CssClass="BottomButton" runat="server" Text="Done" OnClick="btnChgContact_Click" />
                 
 </div></div></div>
                </ContentTemplate>
                    </asp:UpdatePanel>
                        
                
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" TargetControlID="hide" PopupControlID="compQuestNum" BackgroundCssClass="background" runat="server"></ajaxToolkit:ModalPopupExtender>
        
                <asp:Button ID="hide"  runat="server" OnClick="btnChgContact_Click" />
                    </div>
    </form>
</body>
</html>
