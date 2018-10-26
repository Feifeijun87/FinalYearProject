<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportQuest.aspx.cs" Inherits="AdaptiveLearningSystem.reportQuest" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="LecCourse.css" />
        <link rel="stylesheet" runat="server" media="screen" href="TutorialCreate.css" />
    <link rel="stylesheet" runat="server" media="screen" href="main.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script> <%-- To allow jquery implementation --%>

    <script>
        <%-- The dropdown function after onclick (Right Upper Button) --%>
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
         .MyCourseList {
            float: left;
            width: 100%;
            clear: left;
            border-bottom: 1px solid black;
        }
           .auto-style1 {
            height: 58px;
        }
           .container {
    border-top: 1px solid black;
    margin-bottom: 10%;
}
        .auto-style5 {
            width: 646px;
            height: 50px;
        }

        .auto-style9 {
            height: 31px;
        }

        </style>
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
                    <asp:LinkButton CssClass="SecondNavList" ID="TutorialLinkButton" runat="server" OnClick="TutorialLinkButton_Click">Course</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server" OnClick="ResultLinkButton_Click">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>

<%-- start your coding inside div below --%>

        <div class="wrap-content">
            <asp:Panel ID="Panel1" runat="server" >
            <table style="margin-left: 20px; border-bottom-style: none;" class="auto-style1">
                <tr>
                    <td>
                        <div class="Header">
                <h1><i>Tutorial Question Performance</i></h1>
            </div>
                    </td>
                </tr>
                 <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:&nbsp; </label>
                                <asp:Label ID="lblCourse" runat="server"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td class="auto-style5">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial:&nbsp; 
                                <asp:Label ID="lblTutorial" runat="server"></asp:Label>
                                </label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="auto-style5">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Intake : 
                                <asp:Label ID="lblIntake" runat="server"></asp:Label></label>
                       
                                </td>
                        </tr>
                          <tr><td>
                              <div class="container" runat="server" id="container">
                              <asp:Repeater ID="Repeater1"  runat="server">
                             <ItemTemplate>
                 <div class="MyCourseList"> 
                <table style="margin-left: 20px; border-bottom-style: none;" class="auto-style1">                       
                       <tr><td>
                                <asp:Label ID="lblNumber" runat="server" Width ="5px" Text='<%# Container.ItemIndex + 1 %>'> </asp:Label> . 
               
                <asp:Label ID="lblStudentID" runat="server" Width =" 110px" Text='<%#Eval("Question")%>' ></asp:Label>
                          </td></tr>
                    <tr><td>
                        Level : <asp:Label ID="lblStudName" runat="server" Width="180px" Text ='<%# Eval("Level")%>' ></asp:Label>  
                           
                        </td></tr>
                        <tr><td>
                    Total Answers : <asp:Label ID="lblTotalAns" runat="server" Text='<%# Eval("Total Answer")%>'></asp:Label>
                    
                            </td></tr>
                <tr><td>
                     Average Points : <asp:Label ID="lblAvgPoint" runat="server" Text='<%# Eval("Average Points")%>'></asp:Label>
                    
                    </td></tr>
                </table>
                  </div>
            </ItemTemplate>
        </asp:Repeater>
                            </div>  
                              </td></tr>
   </table>
            <div class="BottomButtonGroup">
        <asp:Button ID="lblBack" runat="server" Text="Back" CssClass="BottomButton" OnClick="lblBack_Click" />
        <asp:Button ID="lblSavePDF" runat="server" OnClick="Button2_Click" Text="Save to PDF" CssClass="BottomButton" />
            </div>

                 <div class="NoResult" runat="server" id="NoResultPanel">
                <asp:Image CssClass="NoResultImg" ID="imgNoTutorial" ImageUrl="~/images/no-results.png" Width="30%" Height="20%" runat="server" />
                 <div class="caption">
                    <h1><asp:Label CssClass="NoResultTitle" ID="lblNoTutorialTitle" runat="server" Text="Whooops!"></asp:Label></h1>
                    <asp:Label CssClass="NoResultDesc" ID="lblNoResultDesc" runat="server" Text="Report is not available."></asp:Label>
                </div>
                 <div style="float: left; margin-left: 43%; margin-top: 10px; margin-bottom: 10px">
                    <asp:Button CssClass="EnrollButton" ID="btnBack" runat="server" Text="Back to report selection" OnClick="btnEnroll_Click" />
                </div>
            </div>

            </asp:Panel>


        </div> 
     
            
         <asp:Panel ID="Panel2" runat="server" Visible="False" >
            <table style="margin-left: 20px; border-bottom-style: none; width:600px" class="auto-style1">
                <tr>
                    <td>
                        
                <h1><i>Tutorial Question Performance</i></h1>
            
                    </td>
                </tr>
                 <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:&nbsp; </label>
                                <asp:Label ID="lblCourse2" runat="server"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td class="auto-style5">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial:&nbsp; 
                                <asp:Label ID="lblTutorial2" runat="server"></asp:Label>
                                </label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="auto-style5">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Intake : 
                                <asp:Label ID="lblIntake2" runat="server"></asp:Label>
                              
                                </label>

                                </td></tr>
                           <tr><td><br /><hr /><br /></td></tr>
                        
                     <asp:Repeater ID="Repeater2"  runat="server" >
                             <ItemTemplate>
                               
                       <tr><td>
                                <asp:Label ID="lblNumber" runat="server" Width ="5px" Text='<%# Container.ItemIndex + 1 %>'> </asp:Label> . 
               
                <asp:Label ID="lblStudentID" runat="server" Width =" 110px" Text='<%#Eval("Question")%>' ></asp:Label>
                          </td></tr>
                    <tr><td>
                        Level : <asp:Label ID="lblStudName" runat="server" Width="180px" Text ='<%# Eval("Level")%>' ></asp:Label>  
                           
                        </td></tr>
                        <tr><td>
                    Total Answers : <asp:Label ID="lblTotalAns" runat="server" Text='<%# Eval("Total Answer")%>'></asp:Label>
                    
                            </td></tr>
                <tr><td>
                     Average Points : <asp:Label ID="lblAvgPoint" runat="server" Text='<%# Eval("Average Points")%>'></asp:Label>
                    
                    </td></tr>
                                 <tr><td><br /><hr /></td></tr>

            </ItemTemplate>
        </asp:Repeater>

                 
                </table></asp:Panel>
                    
                
    </form>
</body>
</html>
