<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_2_1_galleriet.Default"
    ViewStateMode="Disabled" Trace="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Galleriet</title>
    <link href="Content/Style.css" rel="stylesheet" />
    <script src="Content/Script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
            <h1>Galleriet</h1>
            <div id="main">
                <asp:Image ID="BigImage" runat="server" />
                <div id="showThumbNails">
                    <%--Repeater ca foreach-loop. Mellan start och slut skrivs vad som ska repeteras.--%>
                    <%--ItemType=talar om för kontrollen att det objekt som ska visas är av en viss typ. Får då hjälp
                      med vilka medlemmar som finns i klassen, här FileInfo (ex Extention, Name, metoder mm)
                    SelectMethod-talar om vilken metod i codebehind man vill använda för att hämta ut data--%>
                    <asp:Repeater ID="ThumbnailsRepeater" runat="server">
                        <%-- OnItemDataBound="ThumbnailsRepeater_ItemDataBound">ItemType="System.IO.FileInfo" SelectMethod="ThumbnailsRepeater_GetData" --%>
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <%--databindningsuttyck oldschool <%# %> files som är knutet har egenskapen Name
                            Eval("Name") ger namnet på filen. I href (NavigateUrl) måste sökvägen till.
                            ("Name", "~/Content/Images/{0}") är motsvarande string.Format men värdet som
                            skjuts in står först--%>
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="HyperLink1" runat="server"
                                    NavigateUrl='<%# "Default.aspx?bild="+ Eval("Name","~/Content/Images/{0}")%>'>

                                    <asp:Image ID="Image1" runat="server"
                                        ImageUrl='<%# Eval("Name","~/Content/Images/ThumbNails/{0}")%>' />
                                </asp:HyperLink>

                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
                <div id="ValidationDiv">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </div>

                <asp:FileUpload ID="FileUpload" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="En fil måste väljas" ControlToValidate="FileUpload"
                    Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ApprovedFileTypes" runat="server"
                    ErrorMessage="Bara bilder av typ gif, jpeg eller png är tillåtna."
                    Display="Dynamic" ValidationExpression='^.*\.(gif|jpg|png)$' Text="*"
                    ControlToValidate="FileUpload"> 
                </asp:RegularExpressionValidator>

                <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
                <p>
                    <asp:Label ID="rightUploadMessage" runat="server" CssClass="green" ></asp:Label>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
