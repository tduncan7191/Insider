<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Enter Truck" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 385px;
        }
        .style2
        {
            width: 115px;
        }
    </style>
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="F/A - Enter Truck" />
        <div id="main">
            <div class="contentBlock">
                <div>
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:Label ID="messageLabel1" runat="server" Text="Label" Visible="false"></asp:Label><asp:Label
                            ID="messageLabel2" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Button ID="editOrderButton" runat="server" Text="Edit Order" Visible="false"
                            OnClick="editOrderButton_Click" />
                    </asp:Panel>
                    <!--Top portion-->
                    <table>
                        <tr>
                            <td class="style2">
                                <span>Order Number:</span>
                            </td>
                            <td class="style1">
                                <asp:TextBox runat="server" ID="Order_Number"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <span>Shipping Company:</span>
                            </td>
                            <td class="style1">
                                <asp:DropDownList ID="Truck_CO" runat="server" DataSourceID="SqlDataSource4" DataTextField="Description"
                                    DataValueField="Company">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <span>Ship Date:</span>
                            </td>
                            <td class="style1">
                                <asp:TextBox runat="server" ID="ShipMonth" Width="20px" Height="20px"></asp:TextBox><span>/</span><asp:TextBox
                                    runat="server" ID="ShipDay" Width="20px" Height="20px"></asp:TextBox><span>/</span><asp:TextBox
                                        runat="server" ID="ShipYear" Width="35px" Height="20px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <span>Tracking Number:</span>
                            </td>
                            <td class="style1">
                                <asp:TextBox runat="server" ID="Tracking_number"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <span>Truck FOB:</span>
                            </td>
                            <td class="style1">
                                <asp:TextBox runat="server" ID="truck_FOB"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <!--Lower portion-->
                    <table>
                        <!--Packer label row 1-->
                        <tr>
                            <td>
                            </td>
                            <td>
                                <span>Packer 1</span>
                            </td>
                            <td>
                                <span>Packer 2</span>
                            </td>
                            <td>
                                <span>Packer 3</span>
                            </td>
                        </tr>
                        <!--Packer entry row 2-->
                        <tr>
                            <td>
                                <span>Packer:</span><br />
                                <span>Lines:</span><br />
                                <span>Tags:</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="Packer1" runat="server" DataSourceID="SqlDataSource2" DataTextField="name"
                                    DataValueField="PACKER">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" ID="linesPacker1"></asp:TextBox><br />
                                <asp:TextBox runat="server" ID="tagsPacker1"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="Packer2" runat="server" DataSourceID="SqlDataSource2" DataTextField="name"
                                    DataValueField="PACKER">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" ID="linesPacker2"></asp:TextBox><br />
                                <asp:TextBox runat="server" ID="tagsPacker2"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="Packer3" runat="server" DataSourceID="SqlDataSource2" DataTextField="name"
                                    DataValueField="PACKER">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" ID="linesPacker3"></asp:TextBox><br />
                                <asp:TextBox runat="server" ID="tagsPacker3"></asp:TextBox>
                            </td>
                        </tr>
                        <!--Packer label row 3-->
                        <tr>
                            <td>
                            </td>
                            <td>
                                Packer 4
                            </td>
                            <td>
                                Packer 5
                            </td>
                            <td>
                                Packer 6
                            </td>
                        </tr>
                        <!--Packer entry row 4-->
                        <tr>
                            <td>
                                <span>Packer:</span><br />
                                <span>Lines:</span><br />
                                <span>Tags:</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="Packer4" runat="server" DataSourceID="SqlDataSource2" DataTextField="name"
                                    DataValueField="PACKER">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" ID="linesPacker4"></asp:TextBox><br />
                                <asp:TextBox runat="server" ID="tagsPacker4"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="Packer5" runat="server" DataSourceID="SqlDataSource2" DataTextField="name"
                                    DataValueField="PACKER">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" ID="linesPacker5"></asp:TextBox><br />
                                <asp:TextBox runat="server" ID="tagsPacker5"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="Packer6" runat="server" DataSourceID="SqlDataSource2" DataTextField="name"
                                    DataValueField="PACKER">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" ID="linesPacker6"></asp:TextBox><br />
                                <asp:TextBox runat="server" ID="tagsPacker6"></asp:TextBox>
                            </td>
                        </tr>
                        <!--Packer button row-->
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="Update" runat="server" Text="Update" Visible="false" OnClick="Update_Click" />
                                <asp:Button ID="Enter" runat="server" Text="Enter" OnClick="Enter_Click" />
                                <asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CN_UPS %>"
                    ProviderName="" SelectCommand="SELECT [Order_Number], [Truck_CO], [ShipMonth], [ShipDay], [ShipYear], [Packer], [Lines], [Tags], [Tracking_number] FROM [TRUCK]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CN_UPS %>"
                    DeleteCommand="DELETE FROM [PACKER] WHERE [PACKERID] = @PACKERID" InsertCommand="INSERT INTO [PACKER] ([PACKER], [PACKER_FIRSTNAME], [PACKER_LASTNAME], [RATE], [STATUS], [TYPE]) VALUES (@PACKER, @PACKER_FIRSTNAME, @PACKER_LASTNAME, @RATE, @STATUS, @TYPE)"
                    ProviderName="" SelectCommand="SELECT PACKERID, PACKER, { fn CONCAT(PACKER, { fn CONCAT('- ', { fn CONCAT(PACKER_FIRSTNAME, PACKER_LASTNAME) }) }) } AS name, RATE, STATUS, TYPE FROM dbo.PACKER WHERE (STATUS = 'Y') ORDER BY name"
                    UpdateCommand="UPDATE [PACKER] SET [PACKER] = @PACKER, [PACKER_FIRSTNAME] = @PACKER_FIRSTNAME, [PACKER_LASTNAME] = @PACKER_LASTNAME, [RATE] = @RATE, [STATUS] = @STATUS, [TYPE] = @TYPE WHERE [PACKERID] = @PACKERID">
                    <DeleteParameters>
                        <asp:Parameter Name="PACKERID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="PACKER" Type="String" />
                        <asp:Parameter Name="PACKER_FIRSTNAME" Type="String" />
                        <asp:Parameter Name="PACKER_LASTNAME" Type="String" />
                        <asp:Parameter Name="RATE" Type="Decimal" />
                        <asp:Parameter Name="STATUS" Type="String" />
                        <asp:Parameter Name="TYPE" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="PACKER" Type="String" />
                        <asp:Parameter Name="PACKER_FIRSTNAME" Type="String" />
                        <asp:Parameter Name="PACKER_LASTNAME" Type="String" />
                        <asp:Parameter Name="RATE" Type="Decimal" />
                        <asp:Parameter Name="STATUS" Type="String" />
                        <asp:Parameter Name="TYPE" Type="String" />
                        <asp:Parameter Name="PACKERID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:CN_MAS_RDP %>"
                    ProviderName=""
                    SelectCommand="SELECT [SalesOrderNumber], [ML_UDF_SOH_REJECT_REASON], [ML_UDF_SOH_SHIP_OTHER_CARR], [ML_UDF_SOH_SHIP_OTH_ACC_BR], [ML_UDF_SOH_SHIP_OTH_ACC_NU], [ML_UDF_SOH_SHIP_OTH_BROKER], [ML_UDF_SOH_CC_NUM], [ML_UDF_SOH_CC_PREAUTH_NUM], [ML_UDF_SOH_CC_EXPDATE], [ML_UDF_SOH_CC_TYPE], [ML_UDF_SOH_OUTBOUND_SHIP], [ML_UDF_SOH_CURRENCY_NAME], [ML_UDF_SOH_ORD_EMAIL_ADD], [CB_UDF_SOH_PICK_TICK_PRINT], [CB_UDF_SOH_PRINT_TAGS], [ML_UDF_SOH_SHIP_NOTES], [ML_UDF_SOH_SHIP_PHONE], [ML_UDF_SOH_TAG_DESCRIPTION], [ML_UDF_SOH_SO_SHIP_TERMS], [ML_UDF_SOH_CONTRACT_ID], [ML_UDF_SOH_GROUP_ID], [ML_UDF_SOH_STUDENT_ID] FROM [TruckEntry]">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:CN_UPS %>"
                    ProviderName="" SelectCommand="SELECT [Company], [Description] FROM [Shipping_Co] ORDER BY [Company]">
                </asp:SqlDataSource>
            </div>
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
    </div>
</body>
</html>