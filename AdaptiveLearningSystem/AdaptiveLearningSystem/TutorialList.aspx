<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TutorialList.aspx.cs" Inherits="AdaptiveLearningSystem.TutorialList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />
    <link rel="stylesheet" href="TutorialList.css" />
    <link rel="stylesheet" runat="server" media="screen" href="main.css" />
    <link rel="stylesheet" href="css.css" /> <%-- CSS for the circular progress bar --%>
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

</head>
<body>
    <form id="form2" runat="server">
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
                    <asp:LinkButton CssClass="SecondNavList" ID="ResultLinkButton" runat="server" OnClick="ResultLinkButton_Click">Report</asp:LinkButton></li>
                <li>
                    <asp:LinkButton CssClass="SecondNavList" ID="ProfilesLinkButton" runat="server" OnClick="ProfilesLinkButton_Click">My Profile</asp:LinkButton></li>
            </ul>
        </div>
        <%-- Content Code Below --%>
        <div class="wrap-content">
            <div class="Header">
                <h1><i>Tutoral List - </i>
                    <asp:Label ID="lblCourseName" runat="server" Text="Label"></asp:Label>
                </h1>
            </div>
    
        <div class="container" runat="server" id="container">
            <asp:Repeater ID="Repeater1"  runat="server" OnItemCommand="Repeater1_ItemCommand">
        <ItemTemplate>
            
            <div class="MyCourseList">
                <div class="CourseDesc">

                <div class="CourseName">
                <asp:Label ID="lblChapterName" runat="server" Text='<%# String.Concat( "Tutorial ",Eval("TutorialNumber"),": ",  Eval("ChapterName"))%>' ></asp:Label>
                </div>
                <div class="CourseDetails">
                    <asp:Label ID="lblQuestion" runat="server" Text='<%# String.Concat( Eval("NoOfQuestion") ," Questions")%>'></asp:Label>
                </div>
                    </div>
                <div class="CourseButtonGroup">
                    <asp:Button CssClass="SelectButton CourseButton" ID="btnEditTutorial" runat="server" Text="Edit" CommandName="edit" /><asp:Button CssClass="DropButton CourseButton" ID="btnActivate" runat="server" Text="Activate" CommandName="activate"  />
                </div>
            </div>
            
            </ItemTemplate>
        </asp:Repeater>
        <div class="BottomButtonGroup">
            <asp:Button CssClass="BottomButton" ID="btnAddTut" runat="server" Text="Add Tutorial" OnClick="btnAddTut_Click" />
        </div>
        </div>
        
                <div class="NoResult" runat="server" id="NoResultPanel">
                <asp:Image CssClass="NoResultImg" ID="imgNoTutorial" ImageUrl="~/images/no-results.png" Width="30%" Height="20%" runat="server" />
                <div class="caption">
                    <h1>
                        <asp:Label CssClass="NoResultTitle" ID="lblNoTutorialTitle" runat="server" Text="Whooops!"></asp:Label></h1>
                    <asp:Label CssClass="NoResultDesc" ID="lblNoTutorialDesc" runat="server" Text="There has no tutorial"></asp:Label>

                </div>
                <div style="float: left; margin-left: 43%; margin-top: 10px; margin-bottom: 10px">
                    <asp:Button CssClass="CreateButton" ID="btnCreateTutorial" runat="server" Text="Go Create One" OnClick="btnCreateTutorial_Click" />
                </div>
            </div>
            </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Panel ID="pnlActivateTutorial" CssClass="pnlActivateTutorial" runat="server">
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
            <div class="panelDtl">
                <div style="float:left;clear:left">
                <table class="activateDtl" >
                    <tr>
                        <td class="stayRight">
                            <asp:Label ID="Label8" runat="server"> Start Date : </asp:Label>
                        </td>
                        <td style="position:relative;height:50px;" >
                            <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" ></asp:TextBox>
                            <asp:ImageButton ID="imgStartDate" style="position:absolute;bottom:10px;left:170px;" ImageUrl="~/images/calendar.png" Width="35px" Height="30px" OnClick="imgStartDate_Click" runat="server" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td></td>
                        <td> <asp:Calendar ID="Calendar2" runat="server" OnDayRender="Calendar2_DayRender" OnSelectionChanged="Calendar2_SelectionChanged" Height="220px" Width="400px" DayStyle-HorizontalAlign="Center" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" NextPrevFormat="FullMonth" TitleFormat="Month">
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                        <DayStyle HorizontalAlign="Center" Width="14%" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle HorizontalAlign="Center" ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                        <TodayDayStyle BackColor="#CCCC99" />
                             </asp:Calendar></td>
                    </tr>
                   <tr>
                        <td class="stayRight">
                            <asp:Label ID="Label1" runat="server"> End Date : </asp:Label>
                        </td>
                        <td style="position:relative;height:50px;">
                            <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:ImageButton ID="imgEndDate" style="position:absolute;bottom:10px;left:170px;" ImageUrl="~/images/calendar.png" Width="35px" Height="30px" OnClick="imgEndDate_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                        </td>
                        <td> <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" Height="220px" Width="400px" DayStyle-HorizontalAlign="Center" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" NextPrevFormat="FullMonth" TitleFormat="Month">
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                        <DayStyle HorizontalAlign="Center" Width="14%" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle HorizontalAlign="Center" ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                        <TodayDayStyle BackColor="#CCCC99" />
                             </asp:Calendar></td>
                    </tr>

                </table>
                    <asp:Label ID="lblActvateError" style="float:left;margin-left:8%" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                    </div>
                       
            </div>
           
            <div class="BottomButtonGroup">
                <asp:Button ID="btnActivateTutorial" CssClass="BottomButton" runat="server" Text="Activate" OnClick="btnActivateTutorial_Click"  />
                <asp:Button ID="btnActivateCancel" CssClass="BottomButton" runat="server" Text="Close" OnClick="btnActivateCancel_Click" />

 </div> 
                </ContentTemplate>
                    </asp:UpdatePanel>
            <asp:Button ID="hide" runat="server" CssClass="hide" />
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popoutActivate" TargetControlID="hide" PopupControlID="pnlActivateTutorial" BackgroundCssClass="background" runat="server"></ajaxToolkit:ModalPopupExtender>
    </form>
</body>
</html>

