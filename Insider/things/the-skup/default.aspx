<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="skup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Inventory Gap Analysis " />
    <script runat="server">
	   void Page_Load(object sender, EventArgs e) {
		   skuNumber.Attributes.Add("onkeyup", "fnSKU_Blur(event);");
	   }
	</script>
	<script type="text/javascript">
		function fnSKU_Blur(e) {
			var unicode = e.keyCode ? e.keyCode : e.charCode
			if (unicode > 32) document.getElementById('sADUOverride').value = '';
		}
	</script>
	<style type="text/css">
		.skup-DataCell {padding:0 5px;text-align:center;}
		.skup-Cell100w {display:inline-block;width:100px;}
		.skup-CellTitle {text-align:center;background-color:#ccc;opacity:.7;}
	</style>
</head>
<body>
    <div id="container" class="skup-wrapper">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Inventory Gap Analysis" />
        <div id="main">
            <div class="contentBlockFull skup-header" style="padding-bottom:0;margin-bottom:0;">
                <div style="padding: 10px 0 0 0; float: left;">
                    <asp:Label ID="EnterSku" runat="server" Text="Enter Sku: " CssClass="skup-label"></asp:Label>
                    <asp:TextBox ID="skuNumber" runat="server"></asp:TextBox>
					<br />
					<asp:Label ID="ADUOverride" runat="server" Text="ADU Override: " CssClass="skup-label"></asp:Label>
					<asp:TextBox ID="sADUOverride" runat="server"></asp:TextBox>
					<br />
					<asp:Label ID="CheckMonthly" runat="server" Text="Monthly Results: " CssClass="skup-label"></asp:Label>
					<asp:CheckBox ID="bMonthly" runat="server" />
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
				<div style="padding:10px 0 0 10px; float:left;">
					<asp:HyperLink ID="ForecastsHyperLink" runat="server" NavigateUrl="forecasts/" Target="_blank">Edit Forecasting</asp:HyperLink>
					<br /><br />
					<asp:HyperLink ID="FutureUsageHyperLink" runat="server" NavigateUrl="future-usage/" Target="_blank" Visible="false">Edit Future Usage</asp:HyperLink>
					<br /><br />
					<asp:HyperLink ID="Legacy2011HyperLink" runat="server" NavigateUrl="legacy/2011/" Target="_blank">Legacy SKUP 2011</asp:HyperLink>
					<br />
					<asp:HyperLink ID="Legacy2012HyperLink" runat="server" NavigateUrl="legacy/2012/" Target="_blank">Legacy SKUP 2012</asp:HyperLink>
				</div>
                <div style="clear: both">&nbsp;</div>
            </div>
			<div class="contentBlockFull" style="overflow:auto;overflow-y:hidden;-ms-overflow-y:hidden;scrollbar-base-color:#ffeaff;padding:0;margin:0;">
                <asp:GridView ID="grdSKUP" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                    Font-Size="12px"
					HeaderStyle-BackColor="green"
					RowStyle-BackColor="#cc99ff"
					AlternatingRowStyle-BackColor="#c0c0c0"
                    EnableModelValidation="True"
					EnableEventValidation="false">
                    <Columns>
						<asp:BoundField DataField="iYear" HeaderText="Year">
                            <ItemStyle CssClass="skup-DataCell skup-CellTitle"></ItemStyle>
                        </asp:BoundField>
						<asp:BoundField DataField="sValName" HeaderText="Data">
                            <ItemStyle CssClass="skup-DataCell skup-Cell100w skup-CellTitle"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
			<div class="contentBlockFull" style="text-align:center;">
				<asp:Button ID="Exportbtn" runat="server" Text="Export to Excel" onclick="btnExportToExcel_Click" Visible="false" width="150" />
				&nbsp;
				<asp:Label ID="lblPageLoadTime" runat="server"></asp:Label>
			</div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
    </div>
</body>
</html>
