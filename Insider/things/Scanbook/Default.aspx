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
            var controlId = document.getElementById("<%=txtParam.ClientID %>").value;

            if (controlId == "") {

                alert("Please enter a MAS number");
                return false;
            }
			document.getElementById("btnSubmit").Text = "Completed";
			alert("Scanbook Generated");                
            return true;
        }
        function sendToSalesforce()
        {
            var root = "https://redemptionplus.my.salesforce.com/"
            var OAuthToken = "";
            var url = root+"services/data/v37.0/sobjects/Attachment"

            var data = {
            "Name" : "demoAttachment.pdf",
            "Body": "Base64Encoded Attachment",
            "parentId": "00658000004kGVRAA2" //the id of the opportunity
            }

            $.ajax({
            url: url,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer "+OAuthToken
            },
            data: JSON.stringify(data)
            })
        }
    </script>

</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customcontrols:ctlcontentheader id="ctlContentHeader1" runat="server" heading="Regenerate Display Labels" />
        <asp:MultiView ID="mvLoggedIn" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwLoggedIn" runat="server">
                <div id="main">
                    <div class="contentBlock">
                        <h3>
                            Enter the customer's MAS number and click "Generate Scanbook" to produce a scanbook on demand</h3>
                        <br />
                        <asp:TextBox ID="txtParam" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblResult" runat="server" Text="" Font-Size="13px"></asp:Label>
                        <br />
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Generate Scanbook" OnClick="btnGenerate_Click"
                            OnClientClick="completedTask()" />
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
