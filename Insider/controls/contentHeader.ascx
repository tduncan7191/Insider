<%@ Control Language="C#" AutoEventWireup="true" CodeFile="contentHeader.ascx.cs" Inherits="controls_WebUserControl" %>
<script runat="server">
    public string heading = null;
</script>
<asp:Panel ID="Panel1" runat="server" BackImageUrl="/images/headerBlank1.jpg" style="height:200px;width:1024px;margin:0 auto;">
    <div style="float: left; font-size: larger; font-weight: bold;margin:150px 0 0 170px">
        <p>
		<%
			if (heading == null) Response.Write(this.Page.Title);
			else Response.Write(heading);
		%></p>
    </div>
</asp:Panel>
<asp:Panel ID="menuPanel1" runat="server" style="width:1024px;margin:0 auto;">
    <div id="mnuHeader">
		<% if (Request.Cookies["sessionid"] != null) { %>
			<div class="usertext">
				Welcome, <%=Request.Cookies["firstname"].Value%> <%=Request.Cookies["lastname"].Value%>
				&nbsp;&nbsp;
				<a href="/sign-in/?m=reset">Sign Out</a>
			</div>
		<% } %>
		<ul>
			<li><a href="/">Home</a></li>
			<!--<li><a href="/answers/">Answers</a></li>-->
			<li class="drop">People
				<ul>
					<!--<li class="pop">Text Boards
						<ul>
							<li><a href="/people/text-boards/self-improvement/">Improve Your Work</a></li>
							<li><a href="/people/text-boards/department-improvement/">Improve Your Department's Work</a></li>
						</ul>
					</li>-->
					<li><a href="/people/95er-of-the-month/">95er of the Month</a></li>
					<li><a href="/people/thank-yous/">Thank Yous</a></li>
					<li><a href="/people/contact-list/">Contact List</a></li>
				</ul>
			</li>
			<li class="drop">Places
				<ul>
					<li><a href="http://www.redemptionplus.com">SMILE</a></li>
					<li><a href="http://www.redemptionplus.com/degsms">SMILE SMS</a></li>
					<li><a href="http://dashboards.redemption-plus.com/Dashboard/Login.aspx">R+ Dashboards</a></li>
					<li><a href="http://www.bigripper.com/">Big Ripper</a></li>
					<li><a href="http://www.cosmoglo.net/">CosmoGlo</a></li>
					<li><a href="http://www.emeraldtoy.com/">Emerald Toy</a></li>
					<li><a href="http://www.grouppinnacle.com/">Group Pinnacle</a></li>
					<li><a href="https://mail.google.com">Google Mail</a></li>
					<li><a href="https://na2.salesforce.com/">Salesforce</a></li>
				</ul>
			</li>
			<li class="drop">Things
				<ul>
					<li><a href="/things/mission-vision-values/">Mission Vision Values</a></li>
					<li class="pop">Data Management
						<ul>
							<li><a href="/things/data-alerts/">Data Alerts</a></li>
							<li><a href="/things/the-skup/2.0/">The SKUP 2.0</a></li>
							<li><a href="/things/truck-entry/">Truck Entry</a></li>
						</ul>
					</li>
					<!--<li><a href="/things/benefits/">Benefits</a></li>-->
					<li><a href="/things/egos-and-excuses/">Egos and Excuses</a></li>
					<li><a href="/downloads/EmployeeHandbook.pdf">Employee Handbook</a></li>
					<li><a href="/things/job-opportunities/">Job Opportunities</a></li>
                    <li><a href="/things/email-sig-gen/">Email Signature Generator</a></li> 
                    <li><a href="/things/recover-cart/">Recover Lost Cart</a></li>                                        
                    <li><a href="/things/reprint/">Regenerate Display Labels</a></li>                                   
                    <li><a href="/things/Scanbook/">Scanbook</a></li>                                        
                    <li><a href="/things/FakeDPL/">FakeDPL</a></li>         
				</ul>
			</li>
		</ul>
		<div class="clearboth">&nbsp;</div>
    </div>
</asp:Panel>