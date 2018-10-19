<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="ADM.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtAns" runat="server"></asp:TextBox>
                
        <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Button" />
                
    &nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Width="200px" style="text-align:right;"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                
        <br />
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
                
    </form>
</body>
</html>

