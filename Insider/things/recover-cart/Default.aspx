<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="recoverCart.css" rel="Stylesheet" type="text/css" />
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Cart Recovery" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1 {
            width: 385px;
        }

        .style2 {
            width: 115px;
        }
    </style>
    <script type="text/javascript">
        function pageopen() {
            window.open('Default.aspx', '_self', '');
            window.close();
        }
  </script>

</head>
<body>


   

    <div id="container">

        <form id="form1" runat="server">
            <div class="hidden" id="restored" runat="server">
                <div class="restoredMessage">
                    <asp:Label ID="lblSuccess" runat="server" Text="Cart Restored" CssClass="success"></asp:Label>
                    <div class="restoredLink">
                        <asp:Literal ID="ltlLink" runat="server" Text="Look here!"></asp:Literal>
                        <br />  
                        <asp:Button ID="btnBack" runat="server" Text="Back to Carts" CssClass="restoredButtons" OnClick="btnBack_Click" />
                     
                    </div>
                </div>
            </div>
            <input type="hidden" runat="server" id="hdnState" value="" />

            <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Cart Recovery" />
            <asp:MultiView ID="mvLoggedIn" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwLoggedIn" runat="server">

                    <div id="main">

                        <div class="contentBlock">
                            <h3>Below are the Abandoned Carts for the Past 7 Days.</h3>
                            <br />
                            <asp:GridView ID="gvAbandonedCarts" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Key" DataSourceID="dsAbandonedCarts" Width="700px" OnSelectedIndexChanged="gvAbandonedCarts_SelectedIndexChanged" OnPageIndexChanged="gvAbandonedCarts_PageIndexChanged">

                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text='<%# Eval("Key") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Cart Name" NullDisplayText="Not Named" SortExpression="Name" />
                                    <asp:BoundField DataField="LastModifyDate" HeaderText="Last Modify Date" SortExpression="LastModifyDate" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="LastModifyBy" HeaderText="Last Modify By" SortExpression="LastModifyBy" />
                                    <asp:BoundField DataField="Account_Name" HeaderText="Account" SortExpression="Account_Name" />
                                    <asp:BoundField DataField="accountexecutive" HeaderText="Account Executive" SortExpression="accountexecutive" />
                                </Columns>
                                <PagerStyle Font-Size="Medium" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="dsAbandonedCarts" runat="server" ConnectionString="<%$ ConnectionStrings:SMILE %>" SelectCommand="SELECT DISTINCT abc.[Key], abc.LastModifyDate, abc.LastModifyBy, abc.Account_Name, a.SalesRepresentativeKey, a.AccountExecutive, abc.Name FROM AbandonCart AS abc INNER JOIN Account AS a ON a.Name = abc.Account_Name INNER JOIN AbandonCart_Item AS ACI ON ACI.AbandonCartKey = abc.[Key] WHERE (abc.AbandonBy = 'System - Expired') AND (DATEDIFF(d, abc.LastModifyDate, GETDATE()) &lt;= 8) AND (a.SalesRepresentativeKey IS NOT NULL) AND (a.AccountExecutive NOT LIKE 'None') ORDER BY abc.LastModifyDate DESC"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="dsSelectedCart" runat="server" ConnectionString="<%$ ConnectionStrings:SMILE %>" SelectCommand="SELECT * FROM [AbandonCart_Item] WHERE ([AbandonCartKey] = @AbandonCartKey)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="gvAbandonedCarts" Name="AbandonCartKey" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="dsCartInfo" runat="server" ConnectionString="<%$ ConnectionStrings:SMILE %>" SelectCommand="SELECT [Name], [Account_Name], [Key] FROM [AbandonCart] WHERE ([Key] = @Key)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="gvAbandonedCarts" Name="Key" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
                            <asp:DetailsView ID="dvCartInfo" runat="server" AutoGenerateRows="False" CssClass="cartInfo" DataKeyNames="Key" DataSourceID="dsCartInfo" HorizontalAlign="Center" Visible="False" Width="300px">
                                <FieldHeaderStyle Font-Bold="True" />
                                <Fields>
                                    <asp:BoundField DataField="Key" HeaderText="Key" InsertVisible="False" ReadOnly="True" SortExpression="Key" />
                                    <asp:BoundField DataField="Account_Name" HeaderText="Account" SortExpression="Account_Name" />
                                    <asp:BoundField DataField="Name" HeaderText="Cart Name" NullDisplayText="Not Named" SortExpression="Name" />
                                </Fields>
                                <HeaderStyle Font-Bold="False" />
                            </asp:DetailsView>
                         
                            <br />
                            <asp:GridView ID="gvSelectedCart" runat="server" AutoGenerateColumns="False" DataKeyNames="Key" DataSourceID="dsSelectedCart" Visible="False" Width="700px">
                                <Columns>
                                    <asp:BoundField DataField="ProductGroup_Id" HeaderText="Sku" SortExpression="ProductGroup_Id" />
                                    <asp:BoundField DataField="ProductGroup_Name" HeaderText="Product Name" SortExpression="ProductGroup_Name" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                </Columns>
                            </asp:GridView>
                            <br />  

                            <asp:Button ID="btnRecover" runat="server" Text="Recover Cart" OnClick="btnRecover_Click" Visible="False" />
                        </div>



                        <div class="directionsContainer">
                            <div class="directions">
                                <h3>Basic Instructions</h3>
                                <ul class="directions">
                                    <li class="directions">Sort any of the columns to find your customer's cart</li>
                                    <li class="directions">Click the cart key on the left</li>
                                    <li class="directions">Verify the contents of the cart <em>(if necessary)</em></li>
                                    <li class="directions">Click "Restore Cart" button</li>
                                    <li class="directions">Once the cart has been restored, there will be a link to login to SMILE as the customer and complete the order <em>(if necessary)</em></li>
                                </ul>

                            </div>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
                </asp:View>
                <asp:View ID="vwNotLoggedIn" runat="server">
                    <customControls:ctlContentHeader ID="ctlContentHeader2" runat="server" heading="Email Signature Generator" />
                    <div id="main2">
                        <div class="contentBlock">
                            <h3>Members Only!</h3>
                            <h4>For some reason, you aren't logged in. Please return to the login screen.</h4>

                        </div>
                    </div>


                </asp:View>
            </asp:MultiView>
        </form>
    </div>
</body>
</html>
