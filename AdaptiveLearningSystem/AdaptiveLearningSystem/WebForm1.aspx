<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AdaptiveLearningSystem.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css.css" /> <%-- CSS for the circular progress bar --%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="container" style="float: left; margin-left: 20px; margin-top: 20px;">

                            <div class="progress-bar1" data-percent="50" data-duration="1000" data-color="#ccc,#E74C3C" runat="server"></div>
                        </div>

                        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
                        <script src="jQuery-plugin-progressbar.js"></script>
                        <script src="js.js"></script>
    </div>
    </form>
</body>
</html>
