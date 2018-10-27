<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudHome.aspx.cs" Inherits="AdaptiveLearningSystem.StudHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" runat="server" media="screen" href="studMain.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="CourseList.css"/>
     <link rel="stylesheet" href="LecCourse.css" />
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
    <div id="upperNavBar">
        
    <ul id="UpperNavList">
        <li><img id="tarcIcon" src="images/tarcIcon.png" width="50"/><p>Adaptive Learning System</p></li>
        <li class="dropdown"><a><asp:Image ID="profilePic"  ImageUrl="~/images/profIcon.png" runat="server" style="width: 35px" />
            <span id="welcome"><small>Welcome</small><br />
                <small><asp:Label ID="lblUserName" runat="server" Text="Label">Loo Zhe Xin</asp:Label></small>
            </span>
            <div id="downIcon"> &#9660;</div></a>
            <div class="dropdown-content">
      <asp:LinkButton  CssClass="dropdown-item" ID="LogOutLinkButton" OnClick="LogOutLinkButton_Click" runat="server">Log Out</asp:LinkButton>
    </div>
                  
          </li>          
          
        
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          
          
        
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
    

        <div class="wrap-content" >
        <div class="Header">
            <h1><i>
               Student Home Page</i>
            </h1>
       </div>
        <div style="margin-left:7%; margin-bottom:20px;">
        
            <div style="margin-top:20px;">

                <asp:Label ID="lblNoData" runat="server" Text=""></asp:Label>
                <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td>
                            <div class="container" runat="server" id="container">
                     <asp:Repeater ID="Repeater1"  runat="server" OnItemCommand="Repeater1_ItemCommand" >
                             <ItemTemplate>
                           
                               <div class="MyCourseList">
                <div class="CourseDesc">
                         
                <asp:Label ID="lblCourseID" runat="server"  Text='<%#Eval("CourseID")%>' ></asp:Label> <asp:Label ID="lblCoursename" runat="server" Text='<%#Eval("CourseName")%>'></asp:Label>
                          <br />Tutorial 
                     <asp:Label ID="lblTutorial" runat="server"  Text ='<%#Eval("TutorialNumber")%>' ></asp:Label>  - <asp:Label ID="lblTutName" runat="server" Text='<%#Eval("ChapterName")%>'></asp:Label>
                    <br />                        
                         
                    Number of question done : <asp:Label ID="lblDone" runat="server" Text='<%#Eval("Done Question")%>'></asp:Label> / <asp:Label ID="lblTotalQuest" runat="server" Text='<%#Eval("NoOfQuestion")%>'></asp:Label>
                    <br />
                    Expiry date: <asp:Label ID="lblExpDate" runat="server" Text='<%#Eval("ExpiryDate")%>'></asp:Label>
                    <br />

                </div>
                   <div class="CourseButtonGroup">
                       <asp:Button CssClass="SelectButton CourseButton" ID="btnSelect" runat="server" Text="Answer Tutorial" CommandName="select" />
                
                
                   </div>
                       </div>
            
                            </ItemTemplate>
        </asp:Repeater>
                          
                        </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
    </asp:UpdatePanel>
</div>
        </div>
        </div>
    </form>
</body>
</html>


