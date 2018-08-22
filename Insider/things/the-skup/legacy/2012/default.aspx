<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="skup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Import Inventory Gap Analysis " />
    <script language="javascript" type="text/javascript">

    </script>
</head>
<body>
    <div id="container" class="skup-wrapper">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Import Inventory Gap Analysis" />
        <h2>Legacy SKUP (2012 Logic v1)</h2>
		<div id="main">
            <div class="contentBlockFull skup-header">
                <div style="padding: 10px 0 0 0; float: left;">
                    <asp:Label ID="EnterSku" runat="server" Text="Enter Sku: " CssClass="skup-label"></asp:Label>
                    <asp:TextBox ID="skuNumber" runat="server"></asp:TextBox>
					<br />
					<asp:Label ID="lblADUOverride" runat="server" Text="ADU Override: " CssClass="skup-label"></asp:Label>
                    <asp:TextBox ID="ADUOverride" runat="server"></asp:TextBox>
					<br />
					<div class="skup-submit">
						<asp:Button ID="EnterSkuButton" runat="server" Text="Submit" OnClick="EnterSkuButton_Click" />
					</div>
                </div>
				<div style="text-align:center;padding: 10px 0 0 5px; float: left; width: 200px">
                    <asp:Label ID="DescriptionBox" runat="server" Width="145px" BorderStyle="None" CssClass="Skup-ProductName"></asp:Label>
					<br />
					<asp:Image ID="Image1" runat="server" />
                </div>
                <div style="padding: 10px 0 0 0; float: left;">
                    <asp:Label ID="Label7" runat="server" Text="Active: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="ActiveTextBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label8" runat="server" Text="Re-order: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="ReorderTextBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label9" runat="server" Text="Main Category: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="MainCategoryBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label10" runat="server" Text="First Sold Date: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="First_DATE_SOLDBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label11" runat="server" Text="Avg Cost: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="AvgCostBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label12" runat="server" Text="SBP1: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="SB1Box" runat="server" ReadOnly="True"></asp:TextBox>
                </div>
                <div style="padding: 10px 0 0 0; float: left;">
                    <asp:Label ID="Label1" runat="server" Text="Vendor: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="VendorNumberBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label2" runat="server" Text="Last Leadtime: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="LeadTimeBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label5" runat="server" Text="Standard Leadtime: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="StandardLeadTimeBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label3" runat="server" Text="Class: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="ClassBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label4" runat="server" Text="MOQ: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="MOQBox" runat="server" ReadOnly="True"></asp:TextBox>
					<br />
                    <asp:Label ID="Label6" runat="server" Text="ADU: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="adu" runat="server" ReadOnly="True"></asp:TextBox>
                </div>
                <asp:TextBox ID="Debug" runat="server" Visible="false"></asp:TextBox>
                <div style="clear: both">
                </div>
            </div>
            <div class="contentBlockFull" style="overflow:auto;overflow-y:hidden;-ms-overflow-y:hidden;scrollbar-base-color:#ffeaff;padding:0;margin:0;">
                <asp:Button ID="Exportbtn" runat="server" Text="export" onclick="btnExportToExcel_Click" Visible="false"/>
                <!--Explicit table creation -->
                <div>
                    <br />
                    <asp:GridView ID="grdSKUP" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                        Font-Size="12px" HeaderStyle-BackColor="green" AllowSorting="True"
                        EnableModelValidation="True" EnableEventValidation="false">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="175px" DataField="Code" HeaderText="TransactionType">
                                <ItemStyle Width="175px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date1" HeaderText="Date1">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date2" HeaderText="Date2">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date3" HeaderText="Date3">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date4" HeaderText="Date4">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date5" HeaderText="Date5">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date6" HeaderText="Date6">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date7" HeaderText="Date7">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date8" HeaderText="Date8">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date9" HeaderText="Date9">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date10" HeaderText="Date10">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date11" HeaderText="Date11">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date12" HeaderText="Date12">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date13" HeaderText="Date13">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date14" HeaderText="Date14">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date15" HeaderText="Date15">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date16" HeaderText="Date16">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date17" HeaderText="Date17">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date18" HeaderText="Date18">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date19" HeaderText="Date19">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date20" HeaderText="Date20">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date21" HeaderText="Date21">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date22" HeaderText="Date22">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date23" HeaderText="Date23">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date24" HeaderText="Date24">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date25" HeaderText="Date25">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date26" HeaderText="Date26">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date27" HeaderText="Date27">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date28" HeaderText="Date28">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date29" HeaderText="Date29">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date30" HeaderText="Date30">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date31" HeaderText="Date31">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date32" HeaderText="Date32">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date33" HeaderText="Date33">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date34" HeaderText="Date34">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date35" HeaderText="Date35">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date36" HeaderText="Date36">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date37" HeaderText="Date37">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date38" HeaderText="Date38">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date39" HeaderText="Date39">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date40" HeaderText="Date40">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date41" HeaderText="Date41">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date42" HeaderText="Date42">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date43" HeaderText="Date43">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date44" HeaderText="Date44">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date45" HeaderText="Date45">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date46" HeaderText="Date46">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date47" HeaderText="Date47">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date48" HeaderText="Date48">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date49" HeaderText="Date49">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date50" HeaderText="Date50">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date51" HeaderText="Date51">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Date52" HeaderText="Date52">
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <br />
                </div>
                <!--Dynamic table creation -->
                <asp:PlaceHolder ID="GridHolder" runat="server"></asp:PlaceHolder>
            </div>
            
            <div class="contentBlockFull">
                <asp:HyperLink ID="FutureUsageHyperLink" runat="server" NavigateUrl="../future-usage/" Target="_blank" Visible="false">Edit Future Usage</asp:HyperLink>
                <br />
                <asp:HyperLink ID="ForecastsHyperLink" runat="server" NavigateUrl="../forecasts/" Target="_blank">Edit Forecasting</asp:HyperLink>
				<br /><br />
				<asp:Label ID="lblPageLoadTime" runat="server"></asp:Label>
            </div>
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
    </div>
</body>
</html>
