<%@ Page Title="Data Alerts" Language="C#" MasterPageFile="~/templates/DataAlerts.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="things_data_alerts_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DAHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DAPageHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DABody1" Runat="Server">
<h2><%=this.Page.Title %></h2>
<div id="DataAlerts">
	<div id="ServerResponse">
		<div></div>
		<br />
		<a href="" onclick="return false;">Hide Message</a>
	</div>
	<div id="AlertInstances">
		<div class="header">
			<button name="btnNew">Create New</button>
			<h3>Instances</h3>
		</div>
		<div class="data"></div>
		<div class="template">
			<div class="AlertInstance">
				<h3>$Name</h3>
				<label>Type</label>&nbsp;$AlertTypeName<br />
				<label>Active</label>&nbsp;$Active<br />
				<label>Frequency</label>&nbsp;$Frequency<br />
				<label>Days to Run</label>&nbsp;$DaysToRun<br />
				<%--
				<label>Last Run</label>&nbsp;<input type="text" readonly="readonly" name="LastRun" value="$LastRun" /><br />
				<label>Next Run</label>&nbsp;<input type="text" readonly="readonly" name="NextRun" value="$NextRun" /><br />
				--%>
				<label>Send to:</label>&nbsp;$SendTo<br />
				<input name="AlertID" type="hidden" value="$AlertID" />
				<button name="btnEdit">Edit</button>
				<button name="btnDelete">Delete</button>
			</div>
		</div>
	</div>

	<div id="AlertEditor">
		<div class="data">
			<form id="AlertForm" onsubmit="return false;">
				<div class="AlertHeader">
					<input type="hidden" name="AlertID" id="AlertID" value="" />
					
					<div>Instance Name</div>
					<input type="text" size="40" maxlength="50" name="Name" value="" />
					
					<div>Send To</div>
					<span>Enter the emails this alert will send to; separated by a comma.</span>
					<input type="text" size="60" maxlength="50" name="SendTo" value="" />

					<div>Active</div>
					<select name="Active">
						<option value="true">Yes</option>
						<option value="false">No</option>
					</select>
					
					<div>Frequency</div>
					<select name="Frequency">
						<option value="Daily">Daily</option>
						<option value="Weekly">Weekly</option>
						<option value="Monthly">Monthly</option>
					</select><br />
					<span class="Frequency Daily">Daily alerts run on selected days only.</span>
					<span class="Frequency Weekly">Weekly alerts run on the first "Day of Week" selected each week.</span>
					<span class="Frequency Monthly">Monthly alerts run on the first "Day of Week" selected on or after the first day of each month.</span>
					
					<div>Day of Week</div>
					<input type="checkbox" name="DaysToRun" id="Sunday" value="Sunday" />&nbsp;<label for="Sunday">Sunday</label>
					<input type="checkbox" name="DaysToRun" id="Monday" value="Monday" />&nbsp;<label for="Monday">Monday</label>
					<input type="checkbox" name="DaysToRun" id="Tuesday" value="Tuesday" />&nbsp;<label for="Tuesday">Tuesday</label>
					<input type="checkbox" name="DaysToRun" id="Wednesday" value="Wednesday" />&nbsp;<label for="Wednesday">Wednesday</label>
					<input type="checkbox" name="DaysToRun" id="Thursday" value="Thursday" />&nbsp;<label for="Thursday">Thursday</label>
					<input type="checkbox" name="DaysToRun" id="Friday" value="Friday" />&nbsp;<label for="Friday">Friday</label>
					<input type="checkbox" name="DaysToRun" id="Saturday" value="Saturday" />&nbsp;<label for="Saturday">Saturday</label>

					<div>Alert Type</div>
					<select name="AlertTypeClass">
						<option value="">Select One</option>
					</select>
				</div>
				
				<div id="UsageSpike" class="AlertTypeTemplate">
					<h3>Usage Spike Filters</h3>
					<span>Only items that meet all filters will be included in this alert.</span>

					<div>Is an Active Item?</div>
					<select name="ActiveItems">
						<option value="true">Yes</option>
						<option value="false">No</option>
						<option value="">Any</option>
					</select>

					<div>Is a Reorder Item?</div>
					<select name="ReorderItems">
						<option value="1">Yes</option>
						<option value="0">No</option>
						<option value="">Any</option>
					</select>

					<div>Is Top 100?</div>
					<select name="Top100Items">
						<option value="">Any</option>
						<option value="true">Yes</option>
						<option value="false">No</option>
					</select>

					<div>Ignore Zero QOH</div>
					<select name="IgnoreZeroQOH">
						<option value="false">No</option>
						<option value="true">Yes</option>
					</select>

					<div>Has Leadtime Stockout?</div>
					<span>Formula: (QOH + NextPO) < (ADU * STANDARDLEADTIME)</span>
					<select name="LeadtimeStockout">
						<option value="">Any</option>
						<option value="true">Yes</option>
						<option value="false">No</option>
					</select>

					<div>Has Usage Spike?</div>
					<span>
						Formula: (QtyUsed in Last X Days) > ((ADU * X) * T)<br />
						X = # of Days; T = Threshold Percent;
					</span>
					<select name="Range">
						<option value="">Any</option>
						<option value="true">Yes</option>
						<option value="false">No</option>
					</select>
					<div># of Days</div>
					<input type="text" name="RangeDays" size="5" maxlength="3" value="90" />
					<div>Threshold</div>
					<input type="text" name="RangeThreshold" size="5" maxlength="4" value="150" />%

					<div>Product Classes</div>
					<a href="javascript:void(0);" class="select_pc">Select All</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" class="clear_pc">Clear All</a>
					<span class="pc"></span>
				</div>
				<div id="RBDCItems" class="AlertTypeTemplate">
					<h3>Reward Board Discontinued Items</h3>
					<span>Sends an alert for each item that is 'DC', is on a Reward Board, and still has QOH.</span>
				</div>
				<div id="UserDefined" class="AlertTypeTemplate">
					<h3>User Defined Alert</h3>
					<span>Send an alert email containing all records in the specified view.</span>

					<div>View Name</div>
					<input type="text" name="ViewName" />
				</div>
				<br /><br />
				<button name="btnSave">Save</button>
				&nbsp;&nbsp;
				<button name="btnCancel">Cancel</button>
			</form>
		</div>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="DANoWrap" Runat="Server">
</asp:Content>