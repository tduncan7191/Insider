<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="MissionVisionValues" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="Mission Vision Values" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Mission Vision Values" />
        <div id="main">
            <div id="columns">
                <ul id="column1" class="column">
                    <li class="widget" id="widget1">
                        <div class="widget-content">
							<h2>Mission/Vision/Values</h2>
                            <img src="/images/mission-vision-values/mission-vision-values.jpg?v=2013" />
                        </div>
                    </li>
                </ul>
                <ul id="column2" class="column">
                    <li class="widget" id="widget4">
                        <div class="widget-content">
							<h2>People Strategy</h2>
                            <img src="/images/mission-vision-values/people-strategies.jpg" />
                        </div>
                    </li>
                </ul>
            <ul id="column3" class="column">
                <li class="widget" id="widget5">
                    <div class="widget-content">
						<h2>Finance Strategy</h2>
                        <img src="/images/mission-vision-values/finance-strategies.jpg" />
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
    </form> </div>
</body>
</html>
