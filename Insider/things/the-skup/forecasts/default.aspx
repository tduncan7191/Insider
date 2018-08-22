<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Forecasts" %>

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
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" EnableModelValidation="True"
                AutoGenerateColumns="False" DataKeyNames="id" ShowFooter="True" AllowPaging="True"
                AllowSorting="True" OnRowCommand="GridView1_RowCommand" CellPadding="4" ForeColor="#333333"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="idTextBox" InsertVisible="False" SortExpression="id"
                        Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Width="25px"></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="idLabel" runat="server" Text='<%# Bind("id") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Width="25px"></asp:Label>
                        
                    </EditItemTemplate>
                    <ItemTemplate>
                            <asp:Label ID="idLabel" runat="server" Text='<%# Bind("id") %>' Width="25px"></asp:Label>
                        
                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" SortExpression="forecastYear">
                        <EditItemTemplate>
                            <asp:TextBox ID="forecastYearTextBox" runat="server" Text='<%# Bind("forecastYear") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="forecastYearLabel" runat="server" Text='<%# Bind("forecastYear") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="forecastYearTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type" SortExpression="forecastCategory">
                        <EditItemTemplate>
                            <asp:DropDownList ID="forecastCategoryDropDownList" runat="server" Text='<%# Bind("forecastCategory") %>'>
                                <asp:ListItem>IS</asp:ListItem>
                                <asp:ListItem>STAPLE</asp:ListItem>
                                <asp:ListItem>CO</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="forecastCategoryLabel" runat="server" Text='<%# Bind("forecastCategory") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="forecastCategoryTextBoxEmpty" runat="server">
                                <asp:ListItem>IS</asp:ListItem>
                                <asp:ListItem>STAPLE</asp:ListItem>
                                <asp:ListItem>CO</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jan" SortExpression="Jan">
                        <EditItemTemplate>
                            <asp:TextBox ID="JanTextBox" runat="server" Text='<%# Bind("Jan") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="JanLabel" runat="server" Text='<%# Bind("Jan") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="JanTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feb" SortExpression="Feb">
                        <EditItemTemplate>
                            <asp:TextBox ID="FebTextBox" runat="server" Text='<%# Bind("Feb") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="FebLabel" runat="server" Text='<%# Bind("Feb") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="FebTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mar" SortExpression="Mar">
                        <EditItemTemplate>
                            <asp:TextBox ID="MarTextBox" runat="server" Text='<%# Bind("Mar") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="MarLabel" runat="server" Text='<%# Bind("Mar") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="MarTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Apr" SortExpression="Apr">
                        <EditItemTemplate>
                            <asp:TextBox ID="AprTextBox" runat="server" Text='<%# Bind("Apr") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="AprLabel" runat="server" Text='<%# Bind("Apr") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="AprTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="May" SortExpression="May">
                        <EditItemTemplate>
                            <asp:TextBox ID="MayTextBox" runat="server" Text='<%# Bind("May") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="MayLabel" runat="server" Text='<%# Bind("May") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="MayTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jun" SortExpression="Jun">
                        <EditItemTemplate>
                            <asp:TextBox ID="JunTextBox" runat="server" Text='<%# Bind("Jun") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="JunLabel" runat="server" Text='<%# Bind("Jun") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="JunTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jul" SortExpression="Jul">
                        <EditItemTemplate>
                            <asp:TextBox ID="JulTextBox" runat="server" Text='<%# Bind("Jul") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="JulLabel" runat="server" Text='<%# Bind("Jul") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="JulTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aug" SortExpression="Aug">
                        <EditItemTemplate>
                            <asp:TextBox ID="AugTextBox" runat="server" Text='<%# Bind("Aug") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="AugLabel" runat="server" Text='<%# Bind("Aug") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="AugTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sep" SortExpression="Sep">
                        <EditItemTemplate>
                            <asp:TextBox ID="SepTextBox" runat="server" Text='<%# Bind("Sep") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="SepLabel" runat="server" Text='<%# Bind("Sep") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="SepTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Oct" SortExpression="Oct">
                        <EditItemTemplate>
                            <asp:TextBox ID="OctTextBox" runat="server" Text='<%# Bind("Oct") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="OctLabel" runat="server" Text='<%# Bind("Oct") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="OctTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nov" SortExpression="Nov">
                        <EditItemTemplate>
                            <asp:TextBox ID="NovTextBox" runat="server" Text='<%# Bind("Nov") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="NovLabel" runat="server" Text='<%# Bind("Nov") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NovTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dec" SortExpression="Dec">
                        <EditItemTemplate>
                            <asp:TextBox ID="DecTextBox" runat="server" Text='<%# Bind("Dec") %>' Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DecLabel" runat="server" Text='<%# Bind("Dec") %>' Width="25px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="DecTextBoxNew" runat="server" Width="25px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <FooterTemplate>
                            <asp:LinkButton ID="btnNew" runat="server" CommandName="New" Text="Insert New" />
                        </FooterTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="btnNew" runat="server" CommandName="New" 
                                Text="Insert New" />
                        
</FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <EmptyDataTemplate>
                    Year:<asp:TextBox ID="forecastYearTextBoxEmpty" runat="server" Text='<%# Bind("forecastYear") %>' Width="25px"></asp:TextBox>
                    Type:<asp:DropDownList ID="forecastCategoryTextBoxEmpty" runat="server" Text='<%# Bind("forecastCategory") %>'>
                        <asp:ListItem>IS</asp:ListItem>
                        <asp:ListItem>STAPLE</asp:ListItem>
                        <asp:ListItem>CO</asp:ListItem>
                    </asp:DropDownList>
                    Jan:<asp:TextBox ID="JanTextBoxEmpty" runat="server" Text='<%# Bind("JAN") %>' Width="25px"></asp:TextBox>
                    Feb:<asp:TextBox ID="FebTextBoxEmpty" runat="server" Text='<%# Bind("Feb") %>' Width="25px"></asp:TextBox>
                    Mar:<asp:TextBox ID="MarTextBoxEmpty" runat="server" Text='<%# Bind("Mar") %>' Width="25px"></asp:TextBox>
                    Apr:<asp:TextBox ID="AprTextBoxEmpty" runat="server" Text='<%# Bind("Apr") %>' Width="25px"></asp:TextBox>
                    May:<asp:TextBox ID="MayTextBoxEmpty" runat="server" Text='<%# Bind("May") %>' Width="25px"></asp:TextBox>
                    Jun:<asp:TextBox ID="JunTextBoxEmpty" runat="server" Text='<%# Bind("Jun") %>' Width="25px"></asp:TextBox>
                    Jul:<asp:TextBox ID="JulTextBoxEmpty" runat="server" Text='<%# Bind("Jul") %>' Width="25px"></asp:TextBox>
                    Aug:<asp:TextBox ID="AugTextBoxEmpty" runat="server" Text='<%# Bind("Aug") %>' Width="25px"></asp:TextBox>
                    Sep:<asp:TextBox ID="SepTextBoxEmpty" runat="server" Text='<%# Bind("Sep") %>' Width="25px"></asp:TextBox>
                    Oct:<asp:TextBox ID="OctTextBoxEmpty" runat="server" Text='<%# Bind("Oct") %>' Width="25px"></asp:TextBox>
                    Nov:<asp:TextBox ID="NovTextBoxEmpty" runat="server" Text='<%# Bind("Nov") %>' Width="25px"></asp:TextBox>
                    Dec:<asp:TextBox ID="DecTextBoxEmpty" runat="server" Text='<%# Bind("Dec") %>' Width="25px"></asp:TextBox>
                    <asp:LinkButton runat="server" ID="NoDataInsert" CommandName="EmptyNew" Text="Insert" />
                </EmptyDataTemplate>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString=""
                DeleteCommand="DELETE FROM [SkupForecasts] WHERE [id] = @id" InsertCommand="INSERT INTO [SkupForecasts] ( [forecastYear], [forecastCategory], [Jan], [Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec]) VALUES (@forecastYear, @forecastCategory, @Jan, @Feb, @Mar, @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Nov, @Dec)"
                ProviderName="" SelectCommand="SELECT * FROM [SkupForecasts]"
                UpdateCommand="UPDATE [SkupForecasts] SET [forecastYear] = @forecastYear, [forecastCategory] = @forecastCategory, [Jan] = @Jan, [Feb] = @Feb,[Mar] = @Mar,[Apr] = @Apr,[May] = @May,[Jun] = @Jun,[Jul] = @Jul,[Aug] = @Aug,[Sep] = @Sep,[Oct] = @Oct,[Nov] = @Nov,[Dec] = @Dec WHERE [id] = @id">
                <DeleteParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                    <asp:Parameter Name="forecastYear" Type="Int32" />
                    <asp:Parameter Name="forecastCategory" Type="String" />
                    <asp:Parameter Name="Jan" Type="Decimal" />
                    <asp:Parameter Name="Feb" Type="Decimal" />
                    <asp:Parameter Name="Mar" Type="Decimal" />
                    <asp:Parameter Name="Apr" Type="Decimal" />
                    <asp:Parameter Name="May" Type="Decimal" />
                    <asp:Parameter Name="Jun" Type="Decimal" />
                    <asp:Parameter Name="Jul" Type="Decimal" />
                    <asp:Parameter Name="Aug" Type="Decimal" />
                    <asp:Parameter Name="Sep" Type="Decimal" />
                    <asp:Parameter Name="Oct" Type="Decimal" />
                    <asp:Parameter Name="Nov" Type="Decimal" />
                    <asp:Parameter Name="Dec" Type="Decimal" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                    <asp:Parameter Name="forecastYear" Type="Int32" />
                    <asp:Parameter Name="forecastCategory" Type="String" />
                    <asp:Parameter Name="Jan" Type="Decimal" />
                    <asp:Parameter Name="Feb" Type="Decimal" />
                    <asp:Parameter Name="Mar" Type="Decimal" />
                    <asp:Parameter Name="Apr" Type="Decimal" />
                    <asp:Parameter Name="May" Type="Decimal" />
                    <asp:Parameter Name="Jun" Type="Decimal" />
                    <asp:Parameter Name="Jul" Type="Decimal" />
                    <asp:Parameter Name="Aug" Type="Decimal" />
                    <asp:Parameter Name="Sep" Type="Decimal" />
                    <asp:Parameter Name="Oct" Type="Decimal" />
                    <asp:Parameter Name="Nov" Type="Decimal" />
                    <asp:Parameter Name="Dec" Type="Decimal" />
                </UpdateParameters>
            </asp:SqlDataSource>
            </div>
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
    </div>
</body>
</html>
