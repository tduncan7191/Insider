<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ThankYous" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Thank You" />
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Thank You" />
        <div id="main">
            <div class="contentBlock">
                <img src="/images/stop-to-say-thank-you.jpg" class="pageTitleIcon" alt="recycle icon" />
                <asp:Label ID="commenttypeHeader" runat="server" Text="Employee Thank Yous"
                    CssClass="pageTitle" />
                <p>
                    Thank a fellow Employee for a job well done or for helping you out with an important task!
                </p>
                <asp:Label ID="alert1" runat="server" Text="Label" Visible="false" CssClass="nomDropdownlist"
                    Style="color: Red;"></asp:Label>
                <img src="/images/stop-to-say-thank-you.jpg" style="margin: 0 5px 0 5px;float:left"/>
                <div style="float: left" />
                <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="False"
                    DataKeyNames="nomId" DataSourceID="SqlDataSource1" EnableModelValidation="True"
                    DefaultMode="Insert" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DetailView_ItemCommand"
                    OnItemInserted="DetailView_ItemInserted">
                    <AlternatingRowStyle BackColor="White" />
                    <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
                    <Fields>
                        <asp:TemplateField HeaderText="Nominee" SortExpression="NomineeId" ShowHeader="False">
                            <InsertItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2"
                                    DataValueField="NomineeId" DataTextField="name" AppendDataBoundItems="True" SelectedValue='<%# Bind("NomineeId") %>'>
                                    <asp:ListItem Value="0" Selected="True">Select Person to Thank</asp:ListItem>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ControlStyle CssClass="nomDropdownlist" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="message" ShowHeader="False" SortExpression="message">
                            <InsertItemTemplate>
                                <asp:TextBox ID="message" runat="server" Text='<%# Bind("message") %>' TextMode="MultiLine"></asp:TextBox>
                            </InsertItemTemplate>
                            <ControlStyle CssClass="nomDropdownlist" Height="90px" Width="400px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="nomDate" SortExpression="nomDate" Visible="False">
                            <InsertItemTemplate>
                                <asp:TextBox ID="nomDate" runat="server" Text='<% System.DateTime.Now %>'></asp:TextBox>
                            </InsertItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowInsertButton="True" ButtonType="Button" InsertText="add Thank You">
                            <ControlStyle CssClass="nomDropdownlist" />
                        </asp:CommandField>
                    </Fields>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                </asp:DetailsView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CN_INSIDER %>"
                    InsertCommand="INSERT INTO [thankyou] ([RecipientID], [NominatorId], [message], [thkDate]) VALUES ( @NomineeId, @NominatorId, @message, getDate())">
                    <InsertParameters>
                        <asp:Parameter Name="NomineeId" Type="Int32" />
                        <asp:Parameter Name="message" Type="String" />
                        <asp:Parameter Name="thkDate" Type="DateTime" />
                    </InsertParameters>
                </asp:SqlDataSource>
				<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CN_INSIDER %>"
						SelectCommand="SELECT [contactID] as NomineeId, LTrim(RTrim([lastName])) + ', ' + LTrim(RTrim([firstName])) as name FROM [contact]where status=0 OR (contactid IN (73,74,75,79,225) and status = 1) order by lastname">
				</asp:SqlDataSource>
            </div>
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </div>
        </form>
    </div>
</body>
</html>