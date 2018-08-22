<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoteMaint.aspx.cs" Inherits="QuoteMaint"
    EnableEventValidation="true" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Quote Maintence" />
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Quote Maintence" />
        <div id="main">
            <div>
                <table border="1" width="500" cellpadding="8" style="font-family: Arial; font-size: 11px;">
                    <tr>
                        <td>
                            Quote
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtQuote" TextMode="MultiLine" runat="server" Width="350" Style="font-family: Arial;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuote" runat="server" ErrorMessage="Quote Text"
                                ForeColor="Red" ControlToValidate="txtQuote" ValidationGroup="AddQuote">*
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Author
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuthor" runat="server" Width="150"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Add Quote" ValidationGroup="AddQuote" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                            <asp:ValidationSummary ID="vsQuote" runat="server" DisplayMode="BulletList" HeaderText="Please enter the following values:"
                                ValidationGroup="AddQuote" />
                        </td>
                    </tr>
                </table>
            </div>
            <p>
            </p>
            <div>
                <asp:GridView ID="dgQuotes" runat="server" AutoGenerateColumns="false" Font-Names="Arial"
                    BorderStyle="Solid" BorderWidth="2">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="50" ReadOnly="true">
                            <HeaderStyle BackColor="Silver" Font-Bold="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Text" HeaderText="Quote" ItemStyle-Width="350">
                            <HeaderStyle BackColor="Silver" Font-Bold="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Author" HeaderText="Author" ItemStyle-Width="150">
                            <HeaderStyle BackColor="Silver" Font-Bold="true" />
                        </asp:BoundField>
                        <asp:CommandField ButtonType="Button" ShowEditButton="true" ItemStyle-Width="75"
                            HeaderText="Edit">
                            <HeaderStyle BackColor="Silver" Font-Bold="true" />
                        </asp:CommandField>
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" ItemStyle-Width="75"
                            HeaderText="Delete">
                            <HeaderStyle BackColor="Silver" Font-Bold="true" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
</body>
</html>
