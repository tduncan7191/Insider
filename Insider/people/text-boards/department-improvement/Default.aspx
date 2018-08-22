<%@ Page Title="Improve Your Department's Work" Language="C#" MasterPageFile="~/templates/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="people_text_board_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody1" Runat="Server">
	<script type="text/javascript">
		$(document).ready(function () {
			$.getScript('/scripts/rp-text-board.js');
		});
	</script>
	<form id="frmTB" action="" method="" onsubmit="return false;">	
	<input type="hidden" name="sBoard" value="department" />
	<input type="hidden" name="iContactID" value="<%=HttpContext.Current.Request.Cookies["contactid"].Value.ToString() %>" />
	<div id="TextBoard">
		<h2><img src="/images/text-board-icon.png" style="width:80px;margin:0 auto;" /> Improve Your Department's Work</h2>
		<p>Do you have an idea about how you can improve your department's work? Share it! Just type your idea in the box below, and then click 'Add Text' to share it with the rest of the Redemption Plus team.</p>
		<textarea name="sText"></textarea><br />
		<button id="btnSaveText">Add Text</button>
		<h2>Ideas to Improve Your Department's Work</h2>
		<div id="TB_Responses">&nbsp;</div>
	</div>
	</form>
</asp:Content>

