<%@ Page Title="Product Class Config. - The SKUP 2.0" Language="C#" MasterPageFile="~/templates/SKUP.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="things_the_skup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SKUPHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SKUPPageHeader1" Runat="Server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SKUPTitle1" Runat="Server">Product Class Configuration</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SKUPBody1" Runat="Server">
	<form id="frmClassConfig">
	<div id="output" class="results">
		<div class="ClassConfig"></div>
	</div>
	</form>

	<div id="templates" class="results">
		<div class="class-config">
			<p>* indicates a class that has not been manually configured yet</p>
			<table class="tbl1">
				<tr class="hdr">
					<th>Class</th>
					<th>Min. Weeks of Supply</th>
					<th>Weeks to Reorder</th>
					<th>Auto Run</th>
				</tr>
				<tr class="data">
					<td><input type="text" readonly="readonly" maxlength="10" name="jsClasses[$iNum][sClass]" value="$sClass" tabindex="-1" /></td>
					<td><input type="text" maxlength="10" name="jsClasses[$iNum][sMinWeeks]" value="$sMinWeeks" />
						<input type="hidden" name="jsClasses[$iNum][sMinWeeks_Init]" value="$sMinWeeks_Init" />
					</td>
					<td><input type="text" maxlength="10" name="jsClasses[$iNum][sReorderWeeks]" value="$sReorderWeeks" />
						<input type="hidden" name="jsClasses[$iNum][sReorderWeeks_Init]" value="$sReorderWeeks_Init" />
					</td>
					<td><input type="checkbox" name="jsClasses[$iNum][sAutoRun]$sAutoRun" value="1" />
						<input type="hidden" name="jsClasses[$iNum][sAutoRun_Init]" value="$sAutoRun_Init" />
						<input type="hidden" name="jsClasses[$iNum][sExists]" value="$sExists" />
					</td>
				</tr>
			</table>
			<button class="save" onclick="SaveConfig(); return false;">Save Changes</button><button class="cancel" onclick="if (confirm('Are you sure?\n\nAll changes will be reverted if you click \'OK\'.') == true) { window.history.go(0); }return false;">Cancel Changes</button>
			<div class="reset"></div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SKUPNoWrap" Runat="Server">
	<script type="text/javascript">
		$(document).ready(function () {
			skup.AutoClassConfig.Init();
		});

		function SaveConfig() {
			skup.AutoClassConfig.Save();
		}
    </script>
</asp:Content>