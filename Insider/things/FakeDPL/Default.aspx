<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="recoverCart.css" rel="Stylesheet" type="text/css" />
    <customcontrols:ctlpagehead id="ctlPageHead" runat="server" title="R+ Insider - Regenerate Display Labels" />
    <link href="reprint.css" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript">
		function completedTask() {
			var userid;
            var txtMasNoControlId = document.getElementById("<%=txtMasNo.ClientID %>").value;
            var txtOrderNumberControlId = document.getElementById("<%=txtOrderNumber.ClientID %>").value;

            if (txtMasNoControlId == "" && txtOrderNumberControlId == "") {

                alert("Please enter a MAS number and Order Number");
                return false;
            }
			//document.getElementById("btnSubmit").Text = "Completed";
			//alert("Scanbook Generated");                
            return true;
        }
    </script>

</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customcontrols:ctlcontentheader id="ctlContentHeader1" runat="server" heading="Generate DPL" />
        <asp:MultiView ID="mvLoggedIn" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwLoggedIn" runat="server">
                <div id="main">
                    <div class="contentBlock" style="width:100%">
						<br />
                        <input type="text" ID="txtMasNo" runat="server" placeholder="Mas Number"/>
                        <br />
                        <br />					
						<input type="text" ID="txtOrderNumber" runat="server" placeholder="Order Number"/>
						<br />
						<br />	
						<asp:DropDownList id="DPLFormat" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DPLFormat_Change">
							<asp:ListItem Selected="True" Value="DPL"> Standard </asp:ListItem>
							<asp:ListItem Value="EPL"> EPL </asp:ListItem>
							<asp:ListItem Value="Intercard"> Intercard </asp:ListItem>
						</asp:DropDownList>
                        <br />
						<br />
						<label>Start Date</label>
						<asp:TextBox ID="startDate" runat="server"/>
						<br />
						<br />						
						<label>End Date</label>
						<asp:TextBox ID="endDate" runat="server"/>
						<br />
						<br />					
						<input type="text" ID="addItem" runat="server" placeholder="Item Code"/>
                        <asp:Button ID="btnAddItem" runat="server" Text="Add Item" OnClick="btnAddItem_Click" />						
						<br />
						<br />
                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" OnClientClick="completedTask()"/>
                        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click"/>
                        <asp:Label ID="lblResult" runat="server" Text="" Font-Size="13px"></asp:Label>
                    </div>
					<div class="contentBlock" style="width:100%">
                        <asp:GridView ID="gvDPL" runat="server" Width="100%" EmptyDataText="There are no data records to display."></asp:GridView>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                <customcontrols:ctlcontentfooter id="ctlContentFooter" runat="server" />
            </asp:View>
            <asp:View ID="View1" runat="server">
                <customcontrols:ctlcontentheader id="ctlContentHeader2" runat="server" heading="Email Signature Generator" />
                <div id="Div1">
                    <div class="contentBlock">
                        <h3>
                            Members Only!</h3>
                        <h4>
                            For some reason, you aren't logged in. Please return to the login screen.</h4>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        </form>
    </div>
</body>
</html>
