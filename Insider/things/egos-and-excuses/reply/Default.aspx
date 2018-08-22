<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="EgosAndExcuses_reply" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Egos and Excuses Recycle" />
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader2" runat="server" heading="Egos and Excuses Recycle" />
        <div id="main">
            <div class="contentBlock">
                <img src="/images/recycle64.png" class="pageTitleIcon" alt="recycle icon" />
                <asp:Label ID="commenttypeHeader" runat="server" Text="Label" CssClass="pageTitle" />
                <h2 class="commentRetrieved">
                    <asp:Label ID="CommentToReplyTo" runat="server" Text="Label" /></h2>
                <div class="blank">
                </div><asp:PlaceHolder ID="recycledGridholder" runat="server"></asp:PlaceHolder>
                <asp:Label ID="alert1" runat="server" Text="Label" Visible="false" style="color:Red;font-weight:bold;"></asp:Label>
                <asp:Repeater ID="commentRepliesRepeater" runat="server">
                    <HeaderTemplate>
                        <table width="650px" style="font: 8pt verdana">
                            <tr>
                                <th align="left">
                                    Recycled
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #FFECD8">
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "comment2", "")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "lastName")%><span>, </span>
                                <%# DataBinder.Eval(Container.DataItem, "firstName")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div style="padding: 15px 0 0 0px;">
                    <asp:DropDownList ID="DropDownList1" runat="server" DataValueField="contactID" DataTextField="name"
                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div style="padding: 5px 0 0 0px;">
                    <asp:TextBox ID="ReplyComment" runat="server" Height="90px" Width="650px" 
                        TextMode="MultiLine"></asp:TextBox></div>
                <div style="padding: 15px 0 0 0px;">
                    <asp:Button ID="Button1" runat="server" Text="recycle" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="recycle more" PostBackUrl="~//things/egos-and-excuses/" />
                </div>
            </div>
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
    </div>
</body>
</html>
