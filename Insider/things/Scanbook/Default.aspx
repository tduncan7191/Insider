<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="recoverCart.css" rel="Stylesheet" type="text/css" />
    <customcontrols:ctlpagehead id="ctlPageHead" runat="server" title="R+ Insider - Scanbooks" />
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
            var controlId = document.getElementById("<%=txtMasNo.ClientID %>").value;

            if (controlId == "") {

                alert("Please enter a MAS number");
                return false;
            }
            generate();
            //document.getElementById("btnGenerate").Text = "Completed";
			//alert("Scanbook Generated");                
            return true;
        }
        function generate() {
            var sfID = "";

            var generateScanbookData = {
                "txtMasNo": "0001832",
                "txtStartDate": "1/1/2018",
                "txtEndDate": "8/24/2018",
                "IsAllItems": true
            };

            $.ajax({
                type: "POST",
                url: "Scanbook.asmx/GenerateScanbook",
                cache: false,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(generateScanbookData),
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    alert(data[1]);
                    var pdfBody = data[0];
                    var sfId = data[1];
                    
                    var sfData = {
                        "Name": "scanbook.pdf",
                        "Body": pdfBody,
                        "parentId": sfId
                    };
                    $.ajax({
                        url: "https://redemptionplus.my.salesforce.com/services/data/v37.0/sobjects/Attachment",
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": "Bearer " + "872016005920900701"
                        },
                        data: JSON.stringify(sfData),
                        success: function (msg) {
                            var data = msg.d;
                            alert(data);
                        },
                        error: function (jqXHR, exception) {
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not connect.\n Verify Network.';
                            } else if (jqXHR.status == 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status == 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                            alert(msg);
                        },
                    });
                },
                error: function (jqXHR, exception) {
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Not connect.\n Verify Network.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                },
            });        
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
                        <asp:TextBox ID="txtMasNo" runat="server"></asp:TextBox>
                        <br />
                        <br />                        
						<label>Start Date</label>
						<asp:TextBox ID="txtStartDate" runat="server"/>
						<br />
						<br />						
						<label>End Date</label>
						<asp:TextBox ID="txtEndDate" runat="server"/>
                        <br />
                        <br />
                        <label>Include all items</label>
                        <asp:CheckBox ID="chkIsAllItems" runat="server"></asp:CheckBox>
                        <br />
                        <br />
                        <asp:Label ID="lblResult" runat="server" Text="" Font-Size="13px"></asp:Label>
                        <br />
                        <br />
                        <input type="button" onclick="completedTask()" value="Generate Scanbook" />
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
