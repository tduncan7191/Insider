<%@ Page Title="ADU Adjustments - The SKUP 2.0" Language="C#" MasterPageFile="~/templates/SKUP.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="things_the_skup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SKUPHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SKUPPageHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SKUPTitle1" Runat="Server">ADU Adjustments</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SKUPBody1" Runat="Server">
	<p style="font-weight:bold;color:red;">Adjustments made on this page apply to all inventory reports that use the standard ADU formulas.</p>
	<form id="frmParams">
		<div id="params">
			<div class="param">
				<label class="req">SKU</label>
				<input type="text" name="sSKU" value="<% Response.Write(HttpContext.Current.Request.QueryString["sSKU"]); %>"/>
			</div>
			<button class="search" id="btnRun">Load Adjustments</button>
			<div class="clearboth">&nbsp;</div>
		</div>
	</form>

	<form id="frmADUAdj">
		<div id="ADUAdj">
			<div class="msg"></div>
			<div class="results">
				<table class="tbl1">
					<tr class="hdr">
						<th>Item Code</th>
						<th>Effective Date</th>
						<th>Warehouse</th>
						<th>Adj. Qty</th>
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

	<div id="templates" class="results">
		<div class="ADUAdj">
			<table class="tbl1">
				<tr class="data">
					<td><input type="text" maxlength="10" name="jsAdj[$iNum][sItemCode]" value="$sItemCode" readonly="readonly" tabindex="-1" /></td>
					<td><input type="text" maxlength="10" name="jsAdj[$iNum][dtEffective]" value="$dtEffective" />
						<input type="hidden" name="jsAdj[$iNum][dtEffective_Init]" value="$dtEffective" />
						<input type="hidden" name="jsAdj[$iNum][iID]" value="$iID" tabindex="-1" />
					</td>
					<td><input type="text" maxlength="3" name="jsAdj[$iNum][sWarehouse]" value="$sWarehouse" />
						<input type="hidden" name="jsAdj[$iNum][sWarehouse_Init]" value="$sWarehouse" />
					</td>
					<td><input type="text" maxlength="8" name="jsAdj[$iNum][iUnits]" value="$iUnits" />
						<input type="hidden" name="jsAdj[$iNum][iUnits_Init]" value="$iUnits" />
					</td>
					<td><textarea name="jsAdj[$iNum][sNotes]" rows="2" cols="12">$sNotes</textarea>
						<textarea name="jsAdj[$iNum][sNotes_Init]" style="display:none;">$sNotes</textarea>
					</td>
					<td class="del"><input type="checkbox" name="jsAdj[$iNum][bDelete]" value="1" /></td>
				</tr>
			</table>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SKUPNoWrap" Runat="Server">
	<script type="text/javascript">
		$(document).ready(function () {
			skup.ADUAdj.Init();
		});
    </script>
</asp:Content>