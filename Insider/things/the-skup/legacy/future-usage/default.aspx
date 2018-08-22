<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="FutureUsage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Import Inventory Gap Analysis " />
    <script language="javascript" type="text/javascript">

    </script>
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Import Inventory Gap Analysis" />
        <div id="main">
            <div class="contentBlockFull">
                <div>
                <h1>Future Usage Editor</h1>
                <span style="color:red; font-size:8px; padding:0 0 10px 0;">*Only shows entrys from the Sku you clicked from.</span>
                    <asp:Label ID="EnterSku" runat="server" Text="Enter Sku: " Visible="false"></asp:Label><br />
                    <asp:TextBox ID="skuNumber" runat="server" Width="60px" Visible="false"></asp:TextBox><asp:LinkButton
                        ID="LinkButton1" runat="server" Visible="false">LinkButton</asp:LinkButton>
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" EnableModelValidation="True"
                        AutoGenerateColumns="False" DataKeyNames="id" ShowFooter="True" AllowPaging="True"
                        AllowSorting="True" OnRowCommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333"
                        GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            <asp:TemplateField HeaderText="idTextBox" InsertVisible="False" SortExpression="id"
                                Visible="false">
                                <EditItemTemplate>
                                    <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="idLabel" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemCode" SortExpression="ItemCode">
                                <EditItemTemplate>
                                    <asp:TextBox ID="ItemCodeTextBox" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="ItemCodeLabel" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="ItemCodeTextBoxNew" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="transactiondate" SortExpression="transactiondate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="transactiondateTextBox" runat="server" Text='<%# Bind("transactiondate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="transactiondateLabel" runat="server" Text='<%# Bind("transactiondate") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="transactiondateTextBoxNew" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" SortExpression="Type">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="TypeDropDownList" runat="server" Text='<%# Bind("Type") %>'>
                                        <asp:ListItem>FSLSU</asp:ListItem>
                                        <asp:ListItem>FPO</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="TypeTextBoxEmpty" runat="server">
                                        <asp:ListItem>FSLSU</asp:ListItem>
                                        <asp:ListItem>FPO</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="value" SortExpression="value">
                                <EditItemTemplate>
                                    <asp:TextBox ID="valueTextBox" runat="server" Text='<%# Bind("value") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="valueLabel" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="valueTextBoxNew" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:LinkButton ID="btnNew" runat="server" CommandName="New" Text="Insert New" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <EmptyDataTemplate>
                            item code:<asp:TextBox ID="ItemCodeTextBoxEmpty" runat="server" Text='<%# Bind("ItemCode") %>'></asp:TextBox>
                            Date:<asp:TextBox ID="transactiondateTextBoxEmpty" runat="server" Text='<%# Bind("transactiondate") %>'></asp:TextBox>
                            type:<asp:DropDownList ID="TypeTextBoxEmpty" runat="server" Text='<%# Bind("Type") %>'>
                                <asp:ListItem>FSLSU</asp:ListItem>
                                <asp:ListItem>FPO</asp:ListItem>
                            </asp:DropDownList>
                            Qty:<asp:TextBox ID="valueTextBoxEmpty" runat="server" Text='<%# Bind("value") %>'></asp:TextBox>
                            <asp:LinkButton runat="server" ID="NoDataInsert" CommandName="EmptyNew" Text="Insert" />
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CN_SALES %>"
                        DeleteCommand="DELETE FROM [SkupFutureEntrys] WHERE [id] = @id" InsertCommand="INSERT INTO [SkupFutureEntrys] ( [ItemCode], [transactiondate], [Type], [value]) VALUES (@ItemCode, @transactiondate, @Type, @value)"
                        ProviderName="" SelectCommand="SELECT * FROM [SkupFutureEntrys] where ItemCode=@ItemCode"
                        UpdateCommand="UPDATE [SkupFutureEntrys] SET [ItemCode] = @ItemCode, [transactiondate] = @transactiondate, [Type] = @Type, [value] = @value WHERE [id] = @id"
                        OnSelecting="SqlDataSource1_Selecting">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                            <asp:Parameter Name="ItemCode" Type="String" />
                            <asp:Parameter Name="transactiondate" Type="DateTime" />
                            <asp:Parameter Name="Type" Type="String" />
                            <asp:Parameter Name="value" Type="Int32" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:Parameter Name="ItemCode" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ItemCode" Type="String" />
                            <asp:Parameter Name="transactiondate" Type="DateTime" />
                            <asp:Parameter Name="Type" Type="String" />
                            <asp:Parameter Name="value" Type="Int32" />
                            <asp:Parameter Name="id" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>

                </div>
            </div>
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
    </div>
</body>
</html>
