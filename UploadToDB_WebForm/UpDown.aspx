<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpDown.aspx.cs" Inherits="UploadTest.UpDown" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="File" runat="server" />
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
            <asp:GridView ID="gv1" runat="server"></asp:GridView>

            <%--<asp:GridView ID="gv1" runat="server" CssClass="table table-hover table-bordered" OnRowDataBound="gdvData_RowDataBound"
                AutoGenerateColumns="true" OnSelectedIndexChanged="gv1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="FILE_NAME" HeaderText="文件名稱" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="下載">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                CommandArgument='<%# Eval("GUID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>

        </div>
    </form>
</body>
</html>
