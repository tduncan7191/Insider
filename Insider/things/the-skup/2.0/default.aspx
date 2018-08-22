<%@ Page Title="The SKUP 2.0" Language="C#" MasterPageFile="~/templates/SKUP.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="things_the_skup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SKUPHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SKUPPageHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SKUPTitle1" Runat="Server">Main Page</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SKUPBody1" Runat="Server">
    <form id="frmParams" onsubmit="return false;">
	<div id="params">
		<div class="param">
			<label class="req">SKU</label>
			<input type="text" name="sSKU" />
		</div>
		
		<div class="param" id="StartDate">
			<label class="req">From</label>
			<input type="text" name="dtFrom" title="" value="01/01/<%Response.Write(System.DateTime.Now.AddYears(-1).Year);%>"/>
		</div>

		<div class="param" id="EndDate">
			<label class="req">To</label>
			<input type="text" name="dtTo" title="" value="12/31/<%Response.Write(System.DateTime.Now.AddYears(1).Year);%>"/>
		</div>

		<div class="param">
			<label class="req">View</label>
			<select name="sView">
				<option value="Weekly">Weekly</option>
				<option value="Monthly">Monthly</option>
			</select>
		</div>

		<script type="text/javascript">
			function fnSourceChanged() {
				if ($("select[name$='sSource'] option:selected").val() == "Live") {
					$('#ADUOverride').show();
					$('#DWUpdate').show();
				}
				else {
					$('#ADUOverride').hide();
					$('#DWUpdate').hide();
				}
			}
		</script>
		<div class="param">
			<label class="req">Source</label>
			<select name="sSource" onchange="fnSourceChanged(this);">
				<option value="DW">Data Warehouse (faster)</option>
				<option selected="selected" value="Live">Live Data (slower)</option>
			</select>
		</div>

		<div class="param">
			<label class="req">Results</label>
			<select name="sType">
				<option value="Actual" title="Shows the actual usage trends based on current ADU. No forecasted values are used.">Actuals</option>
				<option selected value="Forecast" title="Shows traditional SKUP data. Uses forecasts and future usage.">Forecasted</option>
				<option value="Auto" title="Shows auto-balanced usage based on forecasts. Future usage is not applied unless 'Auto Lock' is selected.">Auto Balanced</option>
			</select>
		</div>

		<div class="param" id="ADUOverride">
			<label>ADU Override</label>
			<input type="text" name="dADU" title="The current calculated ADU may be overridden for a single execution when 'Live Data' is selected."/>
		</div>

		<div class="param" id="DWUpdate">
			<label>Update DW</label>
			<select name="bDWUpdate" title="Select whether or not the Data Warehouse should be updated based on the new results. If ADU Override is used SKUP related reports will only reflect the updated data until The SKUP's next scheduled auto-run.">
				<option value="0" title="Existing Data Warehouse records will be affected.">No</option>
				<option value="1" title="Existing Data Warehouse records for selected SKU will be overwritten with the new results.">Yes</option>
			</select>
		</div>

		<div class="clearboth">&nbsp;</div>

		<button class="run" id="btnRun">Run</button>
	</div>
	</form>

	<div id="output" class="results">
		<h2>&nbsp;</h2>
		<div class="SKUHeader"></div>
		<div class="SKUHistory"></div>
		
	</div>

	<div id="templates" class="results">
		<div class="SKUHeader">
			<h3>$sSKU - $sName</h3>
			<img src="http://www.redemptionplus.com/_SKU/$sSKU.jpg" class="pic" />
			<div class="wrap">
				<div class="val"><label>Active</label>&nbsp;<span>$sActive</span></div>
				<div class="val"><label>Re-order</label>&nbsp;<span>$sReorder</span></div>
				<div class="val"><label>Top 100</label>&nbsp;<span>$sCategory4</span></div>
				<div class="val"><label>Main Category</label>&nbsp;<span>$sCategory1</span></div>
				<div class="val"><label>Class</label>&nbsp;<span>$sClass</span></div>
				<div class="val"><label>First Sold</label>&nbsp;<span>$sFirstDateSold</span></div>
				<div class="val"><label>Vendor</label>&nbsp;<span>$sVendor</span></div>
				<div class="clearboth">&nbsp;</div>
				<div class="val"><label>Standard Leadtime</label>&nbsp;<span>$sLeadTime</span></div>
				<div class="val"><label>Last Leadtime</label>&nbsp;<span>$sLastLeadTime</span></div>
				<div class="val"><label>Avg. Cost</label>&nbsp;<span>$sAvgCost</span></div>
				<div class="val"><label>Silver Price</label>&nbsp;<span>$sPrice_Silver</span></div>
				<div class="val"><label>Gold Price</label>&nbsp;<span>$sPrice_Gold</span></div>
				<div class="val"><label>Diamond Price</label>&nbsp;<span>$sPrice_Diamond</span></div>
				<div class="val"><label>MOQ</label>&nbsp;<span>$sMOQ</span></div>
				<div class="val"><label>ADU</label>&nbsp;<span>$sADU</span></div>
				<div class="val"><label>000 QOH</label>&nbsp;<span>$s000QOH</span></div>
				<div class="val"><label>004 QOH</label>&nbsp;<span>$s004QOH</span></div>
				<div class="clearboth">&nbsp;</div>
			</div>
			<div class="clearboth">&nbsp;</div>
		</div>
		<div class="weekly table">
			<table class="tbl1" cellspacing="0">
				<tr class="hdr">
					<th>Data</th>
				</tr>
				<tr class="data">
					<td>$sData</td>
				</tr>
			</table>
			<table class="tbl2" cellspacing="0">
				<tr class="hdr">
					<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th><th>29</th><th>30</th><th>31</th><th>32</th><th>33</th><th>34</th><th>35</th><th>36</th><th>37</th><th>38</th><th>39</th><th>40</th><th>41</th><th>42</th><th>43</th><th>44</th><th>45</th><th>46</th><th>47</th><th>48</th><th>49</th><th>50</th><th>51</th><th>52</th><th>53</th>
				</tr>
				<tr class="data">
					<td>$s1</td><td>$s2</td><td>$s3</td><td>$s4</td><td>$s5</td><td>$s6</td><td>$s7</td><td>$s8</td><td>$s9</td><td>$s10</td><td>$s11</td><td>$s12</td><td>$s13</td><td>$s14</td><td>$s15</td><td>$s16</td><td>$s17</td><td>$s18</td><td>$s19</td><td>$s20</td><td>$s21</td><td>$s22</td><td>$s23</td><td>$s24</td><td>$s25</td><td>$s26</td><td>$s27</td><td>$s28</td><td>$s29</td><td>$s30</td><td>$s31</td><td>$s32</td><td>$s33</td><td>$s34</td><td>$s35</td><td>$s36</td><td>$s37</td><td>$s38</td><td>$s39</td><td>$s40</td><td>$s41</td><td>$s42</td><td>$s43</td><td>$s44</td><td>$s45</td><td>$s46</td><td>$s47</td><td>$s48</td><td>$s49</td><td>$s50</td><td>$s51</td><td>$s52</td><td>$s53</td>
				</tr>
			</table>
		</div>
		<div class="monthly table">
			<table class="tbl1" cellspacing="0">
				<tr class="hdr">
					<th>Data</th>
				</tr>
				<tr class="data">
					<td>$sData</td>
				</tr>
			</table>
			<table class="tbl2" cellspacing="0">
				<tr class="hdr">
					<th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th>
				</tr>
				<tr class="data">
					<td>$s1</td><td>$s2</td><td>$s3</td><td>$s4</td><td>$s5</td><td>$s6</td><td>$s7</td><td>$s8</td><td>$s9</td><td>$s10</td><td>$s11</td><td>$s12</td>
				</tr>
			</table>
		</div>
		<div class="future-usage">
			<h3>Future Usage Entries</h3>
			<form id="FutureUsageTmpID">
			<table>
				<tr class="hdr">
					<th>Tran. Date</th>
					<th>Req. Date</th>
					<th>Type</th>
					<th>Qty</th>
					<th title="By checking the &quot;Auto&quot; checkbox the future usage entry will be included when the Auto-Generated POs are calculated.">Auto</th>
					<th>Del</th>
				</tr>
				<tr class="data">
					<td><input type="hidden" name="Auto[$iNum][sSKU]" value="$sSKU" /><input type="text" maxlength="10" name="Auto[$iNum][dtTran]" value="$dtTran" /></td>
					<td><input type="text" maxlength="10" name="Auto[$iNum][dtRequired]" value="$dtRequired" /></td>
					<td><select name="Auto[$iNum][sType]"><option value="FUPO" selected="$sType_PO">Purchase Order</option><option value="FTSU" selected="$sType_SU">Sales Units</option></select></td>
					<td><input type="text" maxlength="10" name="Auto[$iNum][iQty]" value="$iQty" /></td>
					<td><input type="checkbox" name="Auto[$iNum][bLock]" checked="$bLock" value="1" /></td>
					<td><div class="del">-</div></td>
				</tr>
			</table>
			</form>
			<button class="save">Save Changes</button><button class="cancel">Cancel Changes</button><button class="add">New Entry</button>
			<div class="reset"></div>
		</div>
		<div class="auto-usage">
			<h3>Auto-Generated POs</h3>
			<table>
				<tr class="hdr">
					<th>Tran. Date</th>
					<th>Req. Date</th>
					<th>Type</th>
					<th>Qty</th>
				</tr>
				<tr class="data">
					<td>$dtTran</td>
					<td>$dtRequired</td>
					<td>PO</td>
					<td>$iQty</td>
				</tr>
			</table>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SKUPNoWrap" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			skup.Main.Init();
		});
	</script>
</asp:Content>