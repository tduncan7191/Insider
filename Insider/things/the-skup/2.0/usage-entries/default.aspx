<%@ Page Title="Usage Entries - The SKUP 2.0" Language="C#" MasterPageFile="~/templates/SKUP.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="things_the_skup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SKUPHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SKUPPageHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SKUPTitle1" Runat="Server">Usage Entries</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SKUPBody1" Runat="Server">
	<form id="frmParams">
		<div id="params">
			<div class="param">
				<label class="req">SKU</label>
				<input type="text" name="sSKU" value="<% Response.Write(HttpContext.Current.Request.QueryString["sSKU"]); %>"/>
			</div>
			<button class="search" id="btnRun">Load Usage Entries</button>
			<div class="clearboth">&nbsp;</div>
		</div>
	</form>

	<form id="frmUsage">
		<div id="Usage">
			<div class="msg"></div>
			<h2>Future Usage Entries</h2>
			<p>Future Usage applies to forecasted and auto balanced SKUP results.</p>
			<div class="results">
				<table class="tbl1">
					<tr class="hdr">
						<th>Item Code</th>
						<th>Type</th>
						<th>Tran. Date</th>
						<th>Req. Date</th>
						<th>Qty</th>
						<th>Notes</th>
						<th>Delete</th>
					</tr>
				</table>
				<button id="btnSave">Save</button>
				<button id="btnCancel">Cancel</button>
				<button id="btnAdd">Add New</button>
			</div>
		</div>
	</form>
	<form id="frmAutoUsage">
		<div id="AutoUsage">
			<h2>Auto Usage Entries</h2>
			<p>Auto Usage applies to auto balanced SKUP results only, and all entries are re-generated nightly.</p>
			<div class="results">
				<table class="tbl1">
					<tr class="hdr">
						<th>Item Code</th>
						<th>Type</th>
						<th>Tran. Date</th>
						<th>Req. Date</th>
						<th>Qty</th>
					</tr>
				</table>
			</div>
		</div>
	</form>

	<div id="templates" class="results">
		<div class="Usage">
			<table class="tbl1">
				<tr class="data">
					<td><input type="text" maxlength="10" name="jsUsage[$iNum][sItemCode]" value="$sItemCode" readonly="readonly" tabindex="-1" />
						<input type="hidden" name="jsUsage[$iNum][iID]" value="$iID" tabindex="-1" />
					</td>
					<td><input type="text" maxlength="3" name="jsUsage[$iNum][sType]" value="$sType" />
						<input type="hidden" name="jsUsage[$iNum][sType_Init]" value="$sType" />
					</td>
					<td><input type="text" maxlength="10" name="jsUsage[$iNum][dtTran]" value="$dtTran" />
						<input type="hidden" name="jsUsage[$iNum][dtTran_Init]" value="$dtTran" />
					</td>
					<td><input type="text" maxlength="10" name="jsUsage[$iNum][dtRequired]" value="$dtRequired" />
						<input type="hidden" name="jsUsage[$iNum][dtRequired_Init]" value="$dtRequired" />
					</td>
					<td><input type="text" maxlength="8" name="jsUsage[$iNum][iUnits]" value="$iUnits" />
						<input type="hidden" name="jsUsage[$iNum][iUnits_Init]" value="$iUnits" />
					</td>
					<td><textarea name="jsUsage[$iNum][sNotes]" rows="2" cols="12">$sNotes</textarea>
						<textarea name="jsUsage[$iNum][sNotes_Init]" style="display:none;">$sNotes</textarea>
					</td>
					<td class="del"><input type="checkbox" name="jsUsage[$iNum][bDelete]" value="1" /></td>
				</tr>
			</table>
		</div>
		<div class="AutoUsage">
			<table class="tbl1">
				<tr class="data">
					<td><input type="text" maxlength="10" name="jsAutoUsage[$iNum][sItemCode]" value="$sItemCode" readonly="readonly" tabindex="-1" /></td>
					<td><input type="text" maxlength="3" name="jsAutoUsage[$iNum][sType]" value="$sType" readonly="readonly" /></td>
					<td><input type="text" maxlength="10" name="jsAutoUsage[$iNum][dtTran]" value="$dtTran" readonly="readonly" /></td>
					<td><input type="text" maxlength="10" name="jsAutoUsage[$iNum][dtRequired]" value="$dtRequired" readonly="readonly" /></td>
					<td><input type="text" maxlength="8" name="jsAutoUsage[$iNum][iUnits]" value="$iUnits" readonly="readonly" /></td>
				</tr>
			</table>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SKUPNoWrap" Runat="Server">
	<script type="text/javascript">
		$(document).ready(function () {
			skup.Usage.Init();
		});
    </script>
</asp:Content>