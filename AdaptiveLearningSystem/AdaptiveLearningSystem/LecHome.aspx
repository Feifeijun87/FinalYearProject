<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LecHome.aspx.cs" Inherits="AdaptiveLearningSystem.LecHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaptive Learning System</title>
    <link rel="icon" href="images/tarIco.ico" />

    <link rel="stylesheet"  media="screen" href="main.css" />
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
        .BottomButton {
            position:absolute;
            margin-top:50px;
            display:none;
    background-color: #1A7FEC;
    border: none;
    color: white;
    padding: 15px 32px;
    text-align: center;
    text-decoration: none;
    font-size: 16px;
    cursor: pointer;
}
        body{
            overflow:hidden;
        }

    .BottomButton:active {
        background-color: #1421CC;
    }
    .tutorialCompletion:hover .BottomButton{
        display:inline-block;
    }
        .pnlButton {
            display: inline-block;
            background-color: #1A7FEC;
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            font-size: 16px;
            cursor: pointer;
            float: right;
            margin-top: 50px;
            margin-right: 10px;
        }
        .background {
    background-color: black;
    opacity: 0.8;
    filter: alpha(opacity=90);
}
        .EnrollButton {
    background-color: #1A7FEC;
    border: none;
    color: white;
    padding: 10px 27px;
    text-align: center;
    text-decoration: none;
    display: block;
    font-size: 12px;
    cursor: pointer;
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
        <div class="wrap-content">
            <div class="Header">
                <h1><i>TutorialTrack</i>
                </h1>
            </div>
            <div style="width:100%;">
            <asp:Repeater ID="rptrTutorialTrack" runat="server" OnItemCommand="rptrTutorialTrack_ItemCommand1">
                <ItemTemplate>
                    <div class="tutorialCompletion" style="float: left; width: 100%; height: 140px;">
                        <div class="container" style="float: left; margin-left: 20px; margin-top: 20px;">

                            <div class="progress-bar1" data-percent='<%# calculatePercentage(int.Parse(Eval("doneNumber").ToString()), int.Parse(Eval("totalStud").ToString())) %>' data-duration="1000" data-color="#ccc,#E74C3C" runat="server"></div>
                        </div>

                        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
                        <script src="jQuery-plugin-progressbar.js"></script>
                        <script src="js.js"></script>

                        <div class="description" style="margin-top: 20px; margin-left: 120px; width:55%; float: left;">
                            <div class="courseName" style="font-size: 20pt">

                                <asp:Label ID="Label1" runat="server"><%# Eval("CourseID") %></asp:Label>

                                &nbsp;<asp:Label ID="Label6" runat="server"><%# Eval("CourseName") %></asp:Label>

                            </div>
                            <div class="tutorialName">

                                <asp:Label ID="lblTutorial" runat="server" Text='<%# String.Concat("Tutorial ", Eval("TutorialNumber")," : " )%> '></asp:Label>
                                <asp:Label ID="lblChapterName" runat="server" Text='<%# Eval("ChapterName") %>'></asp:Label>

                            </div>

                            <div class="tutorialDetail" style="font-size: 8pt; margin-top: 20px; position: absolute">

                                <asp:Label ID="Label5" runat="server"><%# Eval("doneNumber") %> / <%# Eval("totalStud") %> done</asp:Label>
                            </div>

                            <div class="tutorialDetail" style="font-size: 8pt; margin-top: 32px; position: absolute">

                                <asp:Label ID="Label4" runat="server">Date:<%# Eval("StartDate") %> to <%# Eval("ExpiryDate") %></asp:Label>
                            </div>
                            

                        </div>
                        <asp:Button ID="btnSelect" CommandName="select" CssClass="BottomButton" runat="server" Text="View Who Not Done" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
            <div class="NoResult" runat="server" id="NoResultPanel">
                <asp:Image CssClass="NoResultImg" ID="imgNoTutorial" ImageUrl="~/images/no-results.png" Width="30%" Height="20%" runat="server" />
                 <div class="caption">
                    <h1><asp:Label CssClass="NoResultTitle" ID="lblNoTutorialTitle" runat="server" Text="Whooops!"></asp:Label></h1>
                    <asp:Label CssClass="NoResultDesc" ID="lblNoTutorialDesc" runat="server" Text="Currently there has no tutorial been activated."></asp:Label>
                </div>
                 <div style="float: left; margin-left: 43%; margin-top: 10px; margin-bottom: 10px">
                    <asp:Button CssClass="EnrollButton" ID="btnEnroll" runat="server" Text="Go Activate" OnClick="btnEnroll_Click" />
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Panel ID="pnlStudentNotDone" runat="server" style="width:45%; background-color:white; height:300px; overflow-y:auto;">
            <div style="background-color:white;">
                <div style="background-color:aqua;">
                <asp:Label ID="Label16" runat="server" Text="Student List" style="font-family:'Times New Roman', Times, serif; font-size:14pt;color:blue; font-weight:bold;margin-left:35%;"></asp:Label>
                    </div>
                <div class="pnlHeader" style="margin-bottom:10px; margin-top:10px;">
                    <asp:Label ID="Label7" runat="server" Text="No." style="margin-left:10px;"></asp:Label>
                    <asp:Label ID="Label8" runat="server" Text="StudentID" style="margin-left:10px;"></asp:Label>
                    <asp:Label ID="Label9" runat="server" Text="Student Name" style="margin-left:40px;"></asp:Label>
                    <asp:Label ID="Label10" runat="server" Text="Programme" style="margin-left:90px;"></asp:Label>
                    <asp:Label ID="Label11" runat="server" Text="Tutorial Group" style="margin-left:30px;"></asp:Label>
                </div>

                    <asp:Repeater ID="rptStudentNotDome" runat="server">

                        <ItemTemplate>    
                            <div class="pnlContent" style="margin-bottom:10px; width:100%; display:inline-block;">                    
                    <asp:Label ID="lblPnlContent" runat="server" Text='<%# Container.ItemIndex + 1 %>' style="position:absolute;margin-left:8px;"></asp:Label>
                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("StudentID")%>' style="position:absolute;margin-left:45px;"></asp:Label>
                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("StudentName")%>' style="position:absolute;margin-left:160px; width:150px;"></asp:Label>
                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("ProgramID")%>' style="position:absolute;margin-left:360px;"></asp:Label>
                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("TutorialGroupID")%>' style="position:absolute;margin-left:490px;"></asp:Label>
                                 </div>
                            </ItemTemplate>
                        <AlternatingItemTemplate>
                                                        <div class="pnlContent" style="margin-bottom:10px; width:100%; display:inline-block;">                    
                    <asp:Label ID="lblPnlContent" runat="server" Text='<%# Container.ItemIndex + 1 %>' style="position:absolute;margin-left:8px;"></asp:Label>
                    <asp:Label ID="Label12" runat="server" Text='<%#Eval("StudentID")%>' style="position:absolute;margin-left:45px;"></asp:Label>
                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("StudentName")%>' style="position:absolute;margin-left:160px; width:150px;"></asp:Label>
                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("ProgramID")%>' style="position:absolute;margin-left:360px;"></asp:Label>
                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("TutorialGroupID")%>' style="position:absolute;margin-left:490px;"></asp:Label>
                                 </div>
                        </AlternatingItemTemplate>

                    </asp:Repeater>
                    </div>

                <asp:Button CssClass="pnlButton" ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
           
        </asp:Panel>
        <asp:Button ID="hide" runat="server" Text="Button" style="display:none;" />
        <ajaxToolkit:ModalPopupExtender ID="mdlStudentNotDone" TargetControlID="hide" BackgroundCssClass="background" PopupControlID="pnlStudentNotDone" runat="server"></ajaxToolkit:ModalPopupExtender>
    </form>
</body>
</html>

