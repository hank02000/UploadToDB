<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="UploadTest.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <asp:FileUpload ID="File" runat="server" />
        <br />
        <br />
        <br />
        <asp:Button ID="Upload" runat="server" Text="上傳" OnClick="Upload_Click" />
        <br />
        <asp:Label ID="lblErrMsg" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <span>ID:</span>
        <asp:TextBox ID="txtFileId" runat="server"></asp:TextBox>
        <asp:Button ID="DownloadFile" runat="server" Text="下載" OnClick="DownloadFile_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    
    </div>
    </form>
</body>
</html>
