<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Calendar_View" %>

<!DOCTYPE>
<html>
<head runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Calender of Events" />
    <link href="/scripts/fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>
    <script src="/scripts/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="/scripts/calendarscript.js" type="text/javascript"></script>
    <script src="/scripts/jquery-ui-timepicker-addon-0.6.2.min.js" type="text/javascript"></script>
    <style type='text/css'>
        body
        {
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande" ,Helvetica,Arial,Verdana,sans-serif;
        }
        #calendar
        {
            width: 900px;
            margin: 0 auto;
        }
        /* css for timepicker */
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .style1
        {
            width: 100%;
        }
        
        /* table fields alignment*/
        .alignRight
        {
        	text-align:right;
        	padding-right:10px;
        	padding-bottom:10px;
        }
        .alignLeft
        {
        	text-align:left;
        	padding-bottom:10px;
        }
    </style>
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Employee Contact List" />
        <div id="main">
            <div class="contentBlockFull">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    
    <div id="calendar">
    </div>
   <!-- <div id="updatedialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px;"
        title="Update or Delete Event">
        <table cellpadding="0" class="style1">
            <tr>
                <td class="alignRight">
                    name:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    start:</td>
                <td class="alignLeft">
                    <span id="eventStart"></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    end: </td>
                <td class="alignLeft">
                    <span id="eventEnd"></span><input type="hidden" id="eventId" /></td>
            </tr>
        </table>
    </div>
    <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px;" title="Add Event">
    <table cellpadding="0" class="style1">
            <tr>
                <td class="alignRight">
                    name:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="50" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    start:</td>
                <td class="alignLeft">
                    <span id="addEventStartDate" ></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    end:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate" ></span></td>
            </tr>
        </table>
        
    </div>-->
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    </div>
            <!-- End Content -->
        </div>
        <!--End Main-->
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </form>
    </div>
</body>
</html>
