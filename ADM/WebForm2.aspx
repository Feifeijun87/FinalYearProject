<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="AdaptiveLearningSystem.WebForm2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" runat="server" media="screen" href="main.css"/>
    <link rel="stylesheet" runat="server" media="screen" href="css.css"/>
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
        .course-content{
                display:none;
    overflow: hidden;
    position:absolute;
    top:15%;
    right:53%;
    background-color: #f9f9f9;
    min-width: 171px;
    box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
    z-index: 1;
        }
        .course-item{
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    display: block;
}
  .coursebtn:hover .course-content{
            display:block;
        }
.course-content .course-item:hover {background-color: #f1f1f1;}
    </style>
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
      <asp:LinkButton  CssClass="dropdown-item" ID="ProfileLinkButton" runat="server">Profile</asp:LinkButton>
      <asp:LinkButton  CssClass="dropdown-item" ID="LogOutLinkButton" runat="server">Log Out</asp:LinkButton>
    </div>
                  
          </li>
        <li><a><img src="images/notificationIcon.png" width="35px"/></a></li>
          
          
        
    </ul>
    </div>   
       <div>
        <ul class="SecondNavBar">
          <li><asp:LinkButton CssClass="SecondNavList" ID="HomeLinkButton" runat="server" >Home</asp:LinkButton></li>
            
          <li class="coursebtn"><asp:LinkButton CssClass="SecondNavList" ID="TutorialLinkButton" runat="server" OnClick="TutorialLinkButton_Click">Course</asp:LinkButton>
              <div class="course-content">
                  <asp:LinkButton  CssClass="course-item" ID="MyCourseLinkButton" runat="server">My Course</asp:LinkButton>
                     <asp:LinkButton  CssClass="course-item" ID="EnrollCourseLinkButton" runat="server">Enroll Course</asp:LinkButton>
              </div>
          </li>
          <li><asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server" >Report</asp:LinkButton></li>
          <li><asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server">My Profile</asp:LinkButton></li>
        </ul>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

        <asp:FileUpload ID="FileUpload1" runat="server" />

     &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Button" />
&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        <asp:Image ID="Image1" runat="server" />
        <div class="container" style="float: left; margin-left: 20px; margin-top: 20px;">

                            <div class="progress-bar1" data-percent=0 data-duration="1000" data-color="#ccc,#E74C3C" runat="server"></div>
                        </div>

                        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
                        <script src="jQuery-plugin-progressbar.js"></script>
                        <script src="js.js"></script>
     </form>
</body>
</html>
