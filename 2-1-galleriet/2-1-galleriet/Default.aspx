<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_2_1_galleriet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Galleriet</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:FileUpload ID="FileUpload" runat="server"  /><br />
        <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
        <div id="showUploadInfo">
        
            Filnamn: <asp:Label ID="FileName" runat="server" Text="Label"></asp:Label><br />
            Filinnehåll: <asp:Label ID="FileContent" runat="server" Text="Label"></asp:Label><br />
            Filstorlek: <asp:Label ID="FileSize" runat="server" Text="Label"></asp:Label><br />
            Sökväg: <asp:Label ID="Path" runat="server" Text="Label"></asp:Label>       
        </div>
    </div>
    </form>
</body>
</html>
