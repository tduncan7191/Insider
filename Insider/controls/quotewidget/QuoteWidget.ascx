<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuoteWidget.ascx.cs" Inherits="QuoteWidget" %>
<table border="0" cellpadding="6" cellspacing="0" style="background-color: lightyellow;" width="400" runat="server" id = "tblQuote">

<tr>
    <td colspan="2">
        <asp:Label  Font-Size="Medium" Font-Names="Times New Roman" ID="lblQuote" ForeColor="Blue" Font-Italic="true" runat="server"></asp:Label>
    </td>
</tr>
<tr>
    <td width="15">
    </td>
    <td>
        <asp:Label  Font-Size="Medium" Font-Names="Times New Roman" ID="lblAuthor" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
    </td>
</tr>
</table> 