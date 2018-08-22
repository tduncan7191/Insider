<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="EgosAndExcuses" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Egos and Excuses" />
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Egos and Excuses" />
        <div id="main">
            <div class="contentBlock">
                <img src="/images/recycle64.png" class="pageTitleIcon" alt="recycle icon" />
                <asp:Label ID="commenttypeHeader" runat="server" Text="Egos and Excuses" CssClass="pageTitle" />
                <div style="padding:10px 0 0px 0">
                <asp:Label ID="Label1" runat="server" Text="Click on ego or excuse to Recycle"></asp:Label>
                </div>                
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                    <HeaderTemplate>
                        <table width="650px" style="font: 8pt verdana; padding: 10px 0 10px 0;">
                            <tr style="background-color: #DFA894">
                                <th align="left">
                                    <b>Excuses</b>
                                </th>
                                <th align="left">
                                    <b>Recycled</b>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #FFECD8">
                            <td>
                                <asp:HyperLink runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "comment")%>'
                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "id","/things/egos-and-excuses/reply/?id={0}")%>' />
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "count")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CN_INSIDER %>"
                    ProviderName="" SelectCommand="SELECT * FROM [comments] Left join countSubComments commentcount on comments.id = commentcount.parent_comment_id where comments.topicId = 1">
                </asp:SqlDataSource>
                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
                    <HeaderTemplate>
                        <table width="650px" style="font: 8pt verdana">
                            <tr style="background-color: DFA894">
                                <th align="left">
                                    <b>Egos</b>
                                </th>
                                <th align="left">
                                    <b>Recycled</b>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #FFECD8">
                            <td>
                                <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "comment")%>'
                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "id","/things/egos-and-excuses/reply/?id={0}")%>' />
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "count")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CN_INSIDER %>"
                    ProviderName="" SelectCommand="SELECT * FROM [comments] Left join countSubComments commentcount on comments.id = commentcount.parent_comment_id where comments.topicId =2">
                </asp:SqlDataSource>
            </div>
                <div class="contentBlockTopRight">
                    <div class="contentBlockContents">
                <img src="/images/recycle64.png" class="pageTitleIcon" alt="recycle icon" />
                <asp:Label ID="Label2" runat="server" Text="Leader Board" CssClass="pageTitle" />
                        <asp:Repeater ID="Repeater3" runat="server" DataSourceID="SqlDataSource3">
                    <HeaderTemplate>
                        <table width="310px" style="font: 8pt verdana">
                            <tr style="background-color: DFA894">
                                <th align="left">
                                    <b>Name</b>
                                </th>
                                <th align="left">
                                    <b>Recycled</b>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #FFECD8">
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "lastName")%>, <%#DataBinder.Eval(Container.DataItem, "firstName")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "count")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:CN_INSIDER %>"
                    ProviderName="" SelectCommand="SELECT LastName, firstName, COUNT(parent_comment_id) AS count FROM dbo.comments left join dbo.contact on dbo.comments.userID =dbo.contact.contactID where (comments.topicId = 4 or comments.topicId =3) GROUP BY LastName, firstName order by count DESC">
                </asp:SqlDataSource>
                    </div>
                </div>

        </div>
        <customcontrols:ctlContentFooter ID="ctlContentFooter" runat="server"/>
        </form>
    </div>
</body>
</html>
