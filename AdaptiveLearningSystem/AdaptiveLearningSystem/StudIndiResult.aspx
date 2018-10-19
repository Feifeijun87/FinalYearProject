<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudIndiResult.aspx.cs" Inherits="AdaptiveLearningSystem.StudIndiResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="LecCourse.css" />
        <link rel="stylesheet" runat="server" media="screen" href="TutorialCreate.css" />
    <link rel="stylesheet" runat="server" media="screen" href="studMain.css"/>
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
       
        .MyCourseList {
            float: left;
            width: 100%;
            clear: left;
            border-bottom: 1px solid black;
        }
    .CourseName {
    font-weight: bold;
    font-size: 14pt;
    font-family: "Times New Roman", Times, serif;
}
    .container {
    border-top: 1px solid black;
    margin-bottom: 10%;
}


        .auto-style8 {
            width: 369px;
        }
        .auto-style13 {
            width: 599px;
        }
        .auto-style14 {
            width: 100%;
            margin-right: 0px;
        }
        


        .auto-style16 {
            width: 203px;
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
             <asp:Panel ID="Panel1" runat="server"><div>
            <table style="margin-left: 20px; border-bottom-style: none;" class="auto-style1">
                     <tr><td colspan="2">
            <div class="Header">
                <h1><i>Student Individual Tutorial Result</i></h1></div></td></tr>
                
                       <tr>
                            <td colspan="2" class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:&nbsp; </label>&nbsp;
                                <asp:Label ID="lblCourse" runat="server"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td style="width: 258px">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial:&nbsp; </label>
                                <asp:Label ID="lblTutorial" runat="server"></asp:Label>
                                <br />
                                
                            </td>
                            <td style="width: 646px">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial Title: 
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                <br />
                                </label>

                                </td>
                        </tr>
                    <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                        <td class="auto-style1">Student ID :
                        <asp:Label ID="lblStudID" runat="server"></asp:Label>
                        </td>

                        <td class="auto-style1">Student Name :
                            <asp:Label ID="lblStudName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                        <td>Tutorial Group :
                     
                        <asp:Label ID="lblTutGroup" runat="server"></asp:Label>
                     </td>
                        <td>
                            Intake : <asp:Label ID="lblIntake" runat="server" Text=""></asp:Label>
                        </td>
                        </tr>
                      
                    
<tr><td colspan ="2">
   
     <div class="container" runat="server" id="container">
            <%-- control put inside itemtemplate --%>

         <div style="width: 100%">
            <asp:Repeater ID="Repeater1"  runat="server" OnItemCommand="Repeater1_ItemCommand" >
        <ItemTemplate>
             
                
            <div class="MyCourseList">                   
     <table style="margin-left: 20px; border-bottom-style: none;" class="auto-style1">
                    <%-- if not need combine string "<%# Eval("CourseID") %>" will do,
                         if want call c# function and bind the return value "<%# calculatePercentage(int.Parse(Eval("doneNumber").ToString())) %>"  something like that--%>
                    <%-- The value inside eval is the column name of the data in sql  --%>
                    <%-- next, c# code there --%>
                    <tr><td><asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'> </asp:Label>. 
               
                      <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question")%>' ></asp:Label>
                </td></tr>
                    <tr><td>Student Answer : <asp:Label ID="lblStudAns" runat="server" Text='<%# Eval("Answer")%>' ></asp:Label>             
                </td></tr>
                
                    <tr><td>Time Spent : <asp:Label ID="lblTimeSpent" runat="server" Text='<%#  Eval("TimeSpent")%>'></asp:Label>second(s)
                    <asp:Label ID="Label4" runat="server" Text=" | "></asp:Label>
                    Matching Percentage: <asp:Label ID="lblMatchPercent" runat="server" Text='<%#  Eval("MatchPercent")%> '></asp:Label>%
                    </td></tr>
                    <tr><td>Point(s) earned : <asp:Label ID="Label1" runat="server" Text='<%# Eval("Points")%>' ></asp:Label>             
                </td></tr>
                    <tr><td>Sample Answer : <asp:Label ID="lblSampleAns" runat="server" Text='<%# Eval("SampleAns")%>' ></asp:Label>             
               </td></tr>
                    </table> 
                    
               
            </div>
            </ItemTemplate>
        </asp:Repeater></div></div></td></tr>
               <tr><td colspan="2"><asp:Label ID="Label3" runat="server" Text="Comments : "></asp:Label>
                 <asp:Label ID="Label2" runat="server" Text="6- Excellent ; 5 - Good ; 4 - Moderate ; 3 - Pass ; 2 - Poor ; 1 - Very Poor"></asp:Label>
             </td></tr> 
         </table></div>
                 </asp:Panel>

        <div class="BottomButtonGroup">
        <asp:Button ID="lblBack" runat="server" Text="Back" CssClass="BottomButton" OnClick="lblBack_Click" />
        <asp:Button ID="lblSavePDF" runat="server" OnClick="Button2_Click" Text="Save to PDF" CssClass="BottomButton" />
            </div>

             

        </div>
       
        <asp:Panel ID="Panel2" runat="server" Visible="False" >
            <table style="margin-left: 20px; border-bottom-style: none; width:600px; margin-right: 20px" >
                    <tr><td colspan="2">
            
                <h1><i>Student Individual Tutorial Result</i></h1>

                   </td></tr>
                
                       <tr>
                            <td colspan="2" class="auto-style5">
                                <label id="subjname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Course:</label>
                                <asp:Label ID="lblCourse2" runat="server"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                            <td class="auto-style8">
                                <label id="tutnum" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial: </label>
                                <asp:Label ID="lblTutorial2" runat="server"></asp:Label>
                                
                            </td>
                            <td class="auto-style13">
                                <label id="tutname" style="font-family: Arial, Helvetica, sans-serif; font-size: large">Tutorial Title: </label>
                                <asp:Label ID="lblTitle2" runat="server"></asp:Label>
                                
                                </td>
                        </tr>
                    <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                        <td class="auto-style8">Student ID :
                        <asp:Label ID="lblStudID2" runat="server"></asp:Label>
                        </td>

                        <td class="auto-style13">Student Name :
                            <asp:Label ID="lblStudName2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="font-family: Arial, Helvetica, sans-serif; font-size: medium">
                        <td class="auto-style8">Tutorial Group :
                     
                        <asp:Label ID="lblTutGroup2" runat="server"></asp:Label>
                     </td>
                        <td class="auto-style13">
                            Intake : <asp:Label ID="lblIntake2" runat="server"></asp:Label>
                        </td>
                        </tr>
                <tr><td colspan="2"><hr /></td></tr>
                    
                <tr><td colspan="2">

                    <table class="auto-style14">
                        
                   <asp:Repeater ID="Repeater2"  runat="server" >
                   <ItemTemplate>
                    <tr>
                        <td colspan="4">
                      <asp:Label ID="lblNumber" runat="server" Text='<%# Container.ItemIndex + 1 %>'> </asp:Label>. 
                      <asp:Label ID="lblQuestion" runat="server"  Text='<%# Eval("Question")%>' ></asp:Label>
                </td>
                </tr>
                <tr>
                    <td>
                        Student Answer : 
                        </td>
                    <td colspan="3">
                        <asp:Label ID="lblStudAns" runat="server" Text='<%# Eval("Answer")%>' ></asp:Label>             
                </td></tr>
                
                    <tr>
                        <td>Time Spent : </td>
                        <td>
                            <asp:Label ID="lblTimeSpent" runat="server" Text='<%#  Eval("TimeSpent")%>'></asp:Label>second(s) </td>
                            
                            <td>
                                Matching Percentage: 
                            </td>
                            <td>
                                <asp:Label ID="lblMatchPercent" runat="server" Text='<%#  Eval("MatchPercent")%> '></asp:Label>%
                    
                            </td>
                   
                    </tr>
                    <tr><td>Point(s) earned : </td>
                        <td colspan="3"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Points")%>' ></asp:Label>             
                </td></tr>
                    <tr><td>Sample Answer : </td>
                        <td colspan="3"><asp:Label ID="lblSampleAns" runat="server" Text='<%# Eval("SampleAns")%>' ></asp:Label>             
               </td></tr>
                  <tr><td colspan="4"><hr /></td></tr>

        </ItemTemplate>
        </asp:Repeater>
                        
                    </table> 
                    
               
            </td></tr> <tr><td colspan="2"><asp:Label ID="Label5" runat="server" Text="Comments : "></asp:Label>
                 </td></tr> 
                <tr><td colspan="2"><asp:Label ID="Label7" runat="server" Text="6- Excellent ; 5 - Good ; 4 - Moderate ; 3 - Pass ; 2 - Poor ; 1 - Very Poor"></asp:Label>
            
             </td></tr> 
                 </table></asp:Panel>

    </form>
</body>
</html>

