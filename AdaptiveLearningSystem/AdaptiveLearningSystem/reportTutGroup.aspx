<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportTutGroup.aspx.cs" Inherits="AdaptiveLearningSystem.reportTutGroup" %>

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

           .auto-style1 {
            height: 58px;
        }

        .auto-style5 {
            width: 646px;
            height: 50px;
        }

        .auto-style6 {
            height: 58px;
            width: 800px;
        }

        .auto-style7 {
            margin-left: 0px;
        }

        .auto-style8 {
            width: 100%;
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
                    <td colspan="2">
                        <div class="Header">
                <h1><i>Tutorial Group Performance</i></h1>
            </div>
                    </td>
                </tr>
                 <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td colspan="2" class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:&nbsp; </label>
                                <asp:Label ID="lblCourse" runat="server"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td colspan="2" class="auto-style5">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial:&nbsp; 
                                <asp:Label ID="lblTutorial" runat="server"></asp:Label>
                                </label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="auto-style5">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Intake : 
                                <asp:Label ID="lblIntake" runat="server"></asp:Label>
                              
                                </label>

                                </td>
                            <td class="auto-style5">
                                <label id="tutgrp" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial Group : 
                                <asp:Label ID="lblTutGroup" runat="server"></asp:Label>
                               
                                </label>

                                </td>
                        </tr>
                    
                    <tr><td colspan="2">
                        <div class="container" runat="server" id="container">
                     
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Width ="110px" Text="StudentID" Font-Bold="True"></asp:Label>  
                            <asp:Label ID="Label2" runat="server" Width="170px" Text="Student Name" Font-Bold="True"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="Total Score" Font-Bold="True"></asp:Label>
                            
                            <asp:Repeater ID="Repeater1"  runat="server" OnItemCommand="Repeater1_ItemCommand" >
                             <ItemTemplate>
                               <div class="MyCourseList">
                <div class="CourseDesc">
                       
                                <asp:Label ID="lblNumber" runat="server" Width ="5px" Text='<%# Container.ItemIndex + 1 %>'> </asp:Label> . 
               
                <asp:Label ID="lblStudentID" runat="server" Width =" 110px" Text='<%# String.Format("{0,-20}",Eval("StudentID"))%>' ></asp:Label>
                          
                     <asp:Label ID="lblStudName" runat="server" Width="180px" Text ='<%# String.Format("{0,-60}",Eval("StudentName"))%>' ></asp:Label>  
                        
                    <asp:Label ID="Label4" runat="server" Width =" 10px" Text=" | "></asp:Label>         
                
                         <asp:Label ID="lblTotalScore" runat="server" Text='<%# String.Format("{0,4}",Eval("Total Score"))%>'></asp:Label>
                    
                    </div>
                   <div class="CourseButtonGroup">
                    <asp:Button CssClass="SelectButton CourseButton" ID="btnStudIndi" runat="server" Text="View student individual result" CommandName="select" />
                </div>
                       </div>
            </ItemTemplate>
        </asp:Repeater>

                        </div></td></tr>
                </table></asp:Panel>


           




        
            <div class="BottomButtonGroup">
        <asp:Button ID="lblBack" runat="server" Text="Back" CssClass="BottomButton" OnClick="lblBack_Click" />
        <asp:Button ID="lblSavePDF" runat="server" OnClick="Button2_Click" Text="Save to PDF" CssClass="BottomButton" />
            </div></div>
     
         <%-- control put inside itemtemplate --%>            
                    
                    <%-- if not need combine string "<%# Eval("CourseID") %>" will do,
                         if want call c# function and bind the return value "<%# calculatePercentage(int.Parse(Eval("doneNumber").ToString())) %>"  something like that--%>                    <%-- The value inside eval is the column name of the data in sql  --%>                    <%-- next, c# code there --%>
                 
         <asp:Panel ID="Panel2" runat="server" Visible="false" >
            <table style="margin-left: 20px; border-bottom-style: none; width:600px" class="auto-style6">
                <tr>
                    <td colspan="2">
                        
                <h1><i>Tutorial Group Performance</i></h1>
                    </td>
                </tr>
                 <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td colspan="2" class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:&nbsp; </label>
                                <asp:Label ID="lblCourse2" runat="server"></asp:Label>
                                
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td colspan="2" class="auto-style5">
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

                                </td>
                            <td class="auto-style5">
                                <label id="tutgrp" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial Group : 
                                <asp:Label ID="lblTutGroup2" runat="server"></asp:Label>
                                </label>

                                </td>
                        </tr>
                <tr>
                    <td colspan="2"><hr /></td>
                </tr>
                    
                    <tr><td colspan="2">
                        <table class="auto-style8">
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Width ="110px" Text="StudentID" Font-Bold="True"></asp:Label>  
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Width="163px" Text="Student Name" Font-Bold="True" CssClass="auto-style7"></asp:Label>
                            
                                </td>
                                <td>
                                    
                                </td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="Total Score" Font-Bold="True"></asp:Label>
                            
                                </td>
                            </tr>
                            <asp:Repeater ID="Repeater2"  runat="server" >
                             <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNumber" runat="server" Width ="5px" Text='<%# Container.ItemIndex + 1 %>'> </asp:Label> . 
               
                <asp:Label ID="lblStudentID" runat="server" Width =" 120px" Text='<%# String.Format("{0,-20}",Eval("StudentID"))%>' ></asp:Label>
                          
                                </td>
                                <td>
                                    <asp:Label ID="lblStudName" runat="server" Width="200px" Text ='<%# String.Format("{0,-60}",Eval("StudentName"))%>' ></asp:Label>  
                       
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Width =" 10px" Text=" | "></asp:Label>         
                
                                </td>
                                <td>
                                     <asp:Label ID="lblTotalScore" runat="server" Text='<%# String.Format("{0,4}",Eval("Total Score"))%>'></asp:Label>
                    
                                </td>
                            </tr>
                        
                               
            </ItemTemplate>
        </asp:Repeater></table>
</td></tr>
                </table></asp:Panel>
                    
                
    </form>
</body>
</html>

