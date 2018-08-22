/*
Redemption Plus
The SKUP
Version 1.0
Last modified on Dec. 19, 2012
Created by Jon Willis
Dependencies:
	- jQuery 1.8+
	- jQuery UI 1.9+
	- jquery-numberformatter-1.2.3.min
	- jquery-tojson.js
	- jquery-throttle-1.0.js
	- jquery-md5-1.2.1
	- jquery-cookie-1.3
	- jshashtable-2.1.js
	- rp-1.0.1.js+

Changelog:
*/
$(document).ready(function () {
	skup.Init.Defaults();
});

/* Created SKUP class and debugging value */
var skup = function () {}
skup.bDebug = true; //Set to false to prevent console logging (saves resources)

skup.Init = function () {}
skup.Init.Defaults = function () {	
	//Ensure browsers doesn't cache AJAX responses
	$.ajaxSetup({
		type: 'POST',
		headers: { "cache-control": "no-cache" }
	});
}

/* SKUP UI Functions */
skup.UI = function () {}
skup.UI.PleaseWait = function () {
	//Uses jQuery blockui plugin with custom loading image and formatting
	$("#main .contentBlockFull").first().block({message:"<img src=\"/images/loading-pacman.gif\" style=\"margin:15px;\" />"
									, css: {border:'2px solid #f1913b', borderRadius:'8px', backgroundColor:'#fff'}
		});
}
skup.UI.ResponseText = function(s) {
	$("#skup2 #output h2").html(s);
}
skup.UI.Unblock = function() {
	$("#main .contentBlockFull").first().unblock();
}
skup.UI.Error = function(jsData) {
	$("#skup2 #output h2").html("Sorry, an error occurred while getting the data.");
	$("#skup2 #output h2").append("<div>Source: " + jsData.src + "</div>");
	$("#skup2 #output h2").append("<div>Message: " + jsData.ex.message + "</div>");
	$("#skup2 #output h2").append("<div>Description: " + jsData.ex.description + "</div>");
	$("#skup2 #output h2").append("<div>Number: " + jsData.ex.line + "</div>");
}

/* Functions that process data returned by web service */
/* Functions that determine how to handle JSON results and when to display/hide HTML elements */
skup.Main = function() {}
skup.Main.Init = function() {
	//Don't let submit elements or buttons post
	$("input[name$=dtFrom]" ).datepicker();
	$("input[name$=dtTo]" ).datepicker();
	$("input[type$=submit]").bind("click", function () { return false; });
	$("button").bind("click", function () { return false; });
	$("input[name$=sSKU]").focus();

	//Execute SKUP scripts when Run button is clicked
	$("#btnRun").bind("click", function () {
		//execute SKUP code
		skup.Main.Run();
	});
}
skup.Main.Tasks = function () {};
skup.Main.Tasks.SKUHeader = {done:false,name:"details"};
skup.Main.Tasks.SKUHistory = {done:false,name:"dataset"};
skup.Main.Tasks.FutureUsage = {done:false,name:"future-usage"};
skup.Main.Tasks.AutoUsage= {done:false,name:"auto-usage"};
skup.Main.Tasks.NoData = {done:false,name:"no-data"};
skup.Main.Tasks.Cancel = {done:false,name:"cancel"};
skup.Main.Tasks.Error = {done:false,name:"error"};
skup.Main.Tasks.Reset = function() {
	//reset completed tasks
	skup.Main.Tasks.SKUHeader.done = false;
	skup.Main.Tasks.SKUHistory.done = false;
	skup.Main.Tasks.FutureUsage.done = false;
	skup.Main.Tasks.AutoUsage.done = false;
	skup.Main.Tasks.NoData.done = false;
	skup.Main.Tasks.Cancel.done = false;
	skup.Main.Tasks.Error.done = false;
}
skup.Main.Run = function() {
	var oParams = $("#frmParams").toJSON();
			
	skup.Main.Tasks.Reset();

	//validate parameters
	if (oParams.sSKU.length == 0) { alert("Please enter a SKU."); return; }

	//Hide results content and let the user know what's up
	skup.Main.Reset();
	$("#main .contentBlockFull").first().block({message:"<img src=\"/images/loading-pacman.gif\" style=\"margin:15px;\" />"
								, css: {border:'2px solid #f1913b', borderRadius:'8px', backgroundColor:'#fff'}
	});
			
	skup.Main.Data.SKUHeader();
	skup.Main.Data.SKUHistory();
}
skup.Main.Reset = function () {
	$("#skup2 #output").hide();
	$("#skup2 #output").children().html("");
}
//callback handler for AJAX requests
skup.Main.ProcessJSON = function(sType, jsData) {
	try {
		//process the JSON error
		if (sType==skup.Main.Tasks.Error.name && skup.Main.Tasks.Error.done==false) {
			//set our completed tasks Error value so we only show the error message once
			skup.Main.Tasks.Error.done = true;
			skup.Main.Reset();

			skup.UI.Error(jsData);
			
			setTimeout(function () {skup.Main.Show();}, 500);
		}
		else if (sType==skup.Main.Tasks.NoData.name && skup.Main.Tasks.Cancel.done==false) {
			skup.Main.Tasks.Cancel.done = true;

			skup.Main.Reset();
			skup.UI.ResponseText("Sorry, no data was returned.<br /><br />Please check the parameters and try again.");
			setTimeout(function () {skup.Main.Show();}, 500);
		}
		else if (sType==skup.Main.Tasks.Cancel.name && skup.Main.Tasks.Cancel.done==false) {
			skup.Main.Tasks.Cancel.done = true;

			skup.Main.Reset();
			skup.UI.ResponseText("Operation cancelled.");
			setTimeout(function () {skup.Main.Show();}, 500);
		}
		//if we made it this far then process the response data by type
		else if (skup.Main.Tasks.Cancel.done == false && skup.Main.Tasks.Error.done==false) {
			if (sType == skup.Main.Tasks.SKUHeader.name) { skup.Main.SKUHeader(jsData); }
			else if (sType == skup.Main.Tasks.SKUHistory.name) { skup.Main.SKUHistory(jsData); }
				
			//if we finished all processes with no errors then show results
			if (skup.Main.Tasks.SKUHeader.done==true && skup.Main.Tasks.SKUHistory.done==true) {
				skup.Main.Show();
			}
		}
	}
	catch (ex) {
		skup.Main.ProcessJSON(skup.Main.Tasks.Error.name, {'src':'ProcessJSON','ex':ex});
	}
}
skup.Main.Show = function() {
	skup.Main.Tasks.Reset();
	$("#main .contentBlockFull").first().unblock();

	//Fade in the output content with timeout to avoid flickering
	$("#skup2 #output").fadeIn();
}
skup.Main.SKUHeader = function(jsData) {
	//Clone details template to results
	$("#skup2 #templates .SKUHeader").clone().appendTo("#skup2 #output .SKUHeader");

	//Loop data values for details and replace template placeholders
	$.each(jsData, function (key, value) {
		$("#skup2 #output .SKUHeader").html($("#skup2 #output .SKUHeader").html().split("$" + key).join(value));
	});

	skup.Main.Tasks.SKUHeader.done = true;
}
skup.Main.SKUHistory = function(jsData) {
	if (jsData.length<=0) {
		skup.Main.ProcessJSON(skup.Main.Tasks.NoData.name, {});
		return;
	}

	//get the requested view type so we can get the correct template
	var sView = $("#frmParams").toJSON().sView.toLowerCase();

	//append data table template to results section
	$("#skup2 #templates ." + sView).clone().appendTo("#skup2 #output .SKUHistory");
				
	$("#skup2 #output .SKUHistory .tbl1 .data").remove();
	$("#skup2 #output .SKUHistory .tbl2 .data").remove();
				
	var s = "";
	var i = 1;

	$.each(jsData, function (key, value) {
		//append new rows to the data tables
		$("#skup2 #templates ." + sView + " .tbl1 .data").clone().appendTo("#skup2 #output .SKUHistory .tbl1");
		$("#skup2 #templates ." + sView + " .tbl2 .data").clone().appendTo("#skup2 #output .SKUHistory .tbl2");
		
		//apply even/odd row formatting
		if (i == 0) {
			i = 1;
			$("#skup2 #output .SKUHistory .tbl1 .data").last().addClass('even');
			$("#skup2 #output .SKUHistory .tbl2 .data").last().addClass('even');
		}
		else {
			i = 0;
			$("#skup2 #output .SKUHistory .tbl1 .data").last().addClass('odd');
			$("#skup2 #output .SKUHistory .tbl2 .data").last().addClass('odd');
		}

		//loop each value in current row and replace template's placeholder
		$.each(value, function (key, value) {
			//special formatting for date rows
			if (value == "Week Start" || value == "Week End" || value == "Month Start" || value == "Month End") {
				$("#skup2 #output .SKUHistory .tbl1 .data").last().removeClass('odd');
				$("#skup2 #output .SKUHistory .tbl2 .data").last().removeClass('odd');
				$("#skup2 #output .SKUHistory .tbl1 .data").last().removeClass('even');
				$("#skup2 #output .SKUHistory .tbl2 .data").last().removeClass('even');
				$("#skup2 #output .SKUHistory .tbl1 .data").last().addClass('new');
				$("#skup2 #output .SKUHistory .tbl2 .data").last().addClass('new');
			}

			//replace placeholder values with actual data in each table accordingly
			if (key == 'sData') {
				s = $("#skup2 #output .SKUHistory .tbl1 .data").last().html();
				s = s.replace("$" + key, value);
				$("#skup2 #output .SKUHistory .tbl1 .data").last().html(s)
			}
			else {
				s = $("#skup2 #output .SKUHistory .tbl2 .data").last().html();
				s = s.replace("$" + key, value);
				$("#skup2 #output .SKUHistory .tbl2 .data").last().html(s)
			}
		});
	});

	skup.Main.Tasks.SKUHistory.done = true;
}

/* Functions that get data from Web Services */
skup.Main.Data = function() {this.bError = false;}
skup.Main.Data.SKUHeader = function() {
	$.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "/things/the-skup/2.0/wsSKUP.asmx/LoadSKUHeader",
		data: JSON.stringify({"sSKU":$("#frmParams input[name$='sSKU']").val(),"sSessionID":$.cookie("sessionid")}),
		dataType: "json",
		complete: function (jqXHR, textStatus) {
			try {
				if (skup.bDebug == true) rp.Console("SKUHeader Resp: " + jqXHR.responseText);
				var oJSON = $.parseJSON(jqXHR.responseText);
					
				skup.ValidateSession(oJSON.d);
					
				oJSON = $.parseJSON(oJSON.d);

				skup.Main.ProcessJSON(skup.Main.Tasks.SKUHeader.name, oJSON);
			}
			catch (ex) 
			{
				skup.Main.ProcessJSON(skup.Main.Tasks.Error.name, {'src':'WebService.SKUHeader','ex':ex});
			}
		}
	});
}
skup.Main.Data.SKUHistory = function() {
	var sSKU = $("#frmParams input[name$='sSKU']").val();
	var dtStart = $("#frmParams input[name$='dtFrom']").val();
	var dtEnd = $("#frmParams input[name$='dtTo']").val();
	var sView = $("#frmParams select[name$='sView']").val();
	var sSource = $("#frmParams select[name$='sSource']").val();
	var sType = $("#frmParams select[name$='sType']").val();
	var sDWUpdate = $("#frmParams select[name$='bDWUpdate']").val();
	var dADU = ($("#frmParams input[name$='dADU']").val() != "") ? $("#frmParams input[name$='dADU']").val() : 0;
		
	$.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "/things/the-skup/2.0/wsSKUP.asmx/LoadSKUHistory",
		data: JSON.stringify({"sSKU":sSKU,"dtStart":dtStart,"dtEnd":dtEnd,"sView":sView,"sSource":sSource,"sType":sType,"sDWUpdate":sDWUpdate,"dADU":dADU,"sSessionID":$.cookie("sessionid")}),
		dataType: "json",
		complete: function (jqXHR, textStatus) {
			try {
				if (skup.bDebug == true) rp.Console("SKUHistory Resp: " + jqXHR.responseText);
				var oJSON = $.parseJSON(jqXHR.responseText);
					
				skup.ValidateSession(oJSON.d);
					
				oJSON = $.parseJSON(oJSON.d);
						
				skup.Main.ProcessJSON(skup.Main.Tasks.SKUHistory.name, oJSON);
			}
			catch (ex) 
			{
				skup.Main.ProcessJSON(skup.Main.Tasks.Error.name, {'src':'WebService.SKUHistory','ex':ex});
			}
		}
	});
}

/* Functions for loading and saving AutoClassConfig data */
skup.AutoClassConfig = function () {}
skup.AutoClassConfig.Init = function() {
	skup.UI.PleaseWait();

	$.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "/things/the-skup/2.0/wsSKUP.asmx/LoadAutoClassConfig",
		data: JSON.stringify({"sSessionID":$.cookie("sessionid")}),
		dataType: "json",
		complete: function (jqXHR, textStatus) {
			try {
				if (skup.bDebug == true) rp.Console("LoadAutoClassConfig Resp: " + jqXHR.responseText);
				var oJSON = $.parseJSON(jqXHR.responseText);
					
				skup.ValidateSession(oJSON.d);
					
				oJSON = $.parseJSON(oJSON.d);
							
				skup.AutoClassConfig.LoadUI(oJSON);
			}
			catch (ex) 
			{
				skup.Main.ProcessJSON(skup.Main.Tasks.Error.name, {'src':'WebService.LoadAutoClassConfig','ex':ex});
			}
		}
	});
}
skup.AutoClassConfig.LoadUI = function(jsData) {
	//clear any previous responses
	$("#skup2 #output .ClassConfig").html("");

	if (jsData.length<=0) {
		ProcessJSON(skup.Main.Tasks.NoData.name, {});
		return;
	}
	
	//append data table template to results section
	$("#skup2 #templates .class-config").clone().appendTo("#skup2 #output .ClassConfig");
	$("#skup2 #output .ClassConfig .data").remove();
		
	var s = "";
	var i = 0;
		
	$.each(jsData, function (key, value) {
		//append new rows to the data tables
		$("#skup2 #templates .class-config .data").clone().appendTo("#skup2 #output .ClassConfig table");
			
		//replace placeholder values with actual data in each table accordingly
		s = $("#skup2 #output .ClassConfig table .data").last().html();
		
		//set initial values
		s = s.replace(/\$iNum/g, i) //insert unique row id for json form parser
			.replace("$sClass", this.sClass + ((this.sExists.toString() == "1") ? "" : "*")) //show asterik for categories that are using defaults
			.replace("$sMinWeeks_Init", this.sMinWeeks)
			.replace("$sReorderWeeks_Init", this.sReorderWeeks)
			.replace("$sAutoRun_Init", ((this.sAutoRun.toString() == "1") ? 1 : 0))
			.replace("$sExists", this.sExists)
			.replace("$sMinWeeks", this.sMinWeeks)
			.replace("$sReorderWeeks", this.sReorderWeeks);

		//convert bool to checked input value
		if (this.sAutoRun.toString() == "1") s = s.replace("$sAutoRun", "\" checked=\"checked\"");
		else s = s.replace("$sAutoRun", "");
			
		$("#skup2 #output .ClassConfig table .data").last().html(s);

		i++;
	});
		
	$("#main .contentBlockFull").first().unblock();
}
skup.AutoClassConfig.Save = function() {
	skup.UI.PleaseWait();

	var oJSON = null;
	var s = JSON.stringify($("#frmClassConfig").toJSON());
	s = s.substring(13, s.length - 1);
		
	var oResp = $.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "/things/the-skup/2.0/wsSKUP.asmx/SaveAutoClassConfig",
		data: JSON.stringify({"jsData":s,"sSessionID":$.cookie("sessionid")}),
		dataType: "json"
	});
	
	oResp.done(function (data) {
		try {	
			if (skup.bDebug == true) rp.Console("SaveAutoClassConfig Resp: " + data.d);
			
			if (data.d.indexOf('WS Error: ') < 0) {
				skup.ValidateSession(data.d);
			
				oJSON = $.parseJSON(data.d);
				skup.AutoClassConfig.LoadUI(oJSON);
			}
			else alert(data.d);
		}
		catch (ex) {
			alert(ex);
		}
	});

	$("#main .contentBlockFull").first().unblock();
}

/* Functions for loading and saving ADU Adjustments */
skup.ADUAdj = function () {}
skup.ADUAdj.WarehouseList = ['000','002','004'];
skup.ADUAdj.Init = function() {
	$("button").bind("click", function () { return false; });
	$("#btnRun").bind('click', function () { skup.ADUAdj.Load(); return false; });
	$("#btnAdd").bind('click', function () { skup.ADUAdj.Add(); return false; });
	$("#btnSave").bind('click', function () { skup.ADUAdj.Save(); return false; });
	$("#btnCancel").bind('click', function () { skup.ADUAdj.Load(); return false;});
}
skup.ADUAdj.UpdateEvents = function () {
	//clear previous event bindings from adjustment fields
	$("input[name$='jsAdj']").unbind();

	//add bindings to adjustment fields
	$("input[name*='[dtEffective]']" ).removeClass('hasDatepicker');
	
	//loop all date fields and setup datepicker
	$.each($("#frmADUAdj").find("input[name*='[dtEffective]']"), function () { $(this).datepicker(); });

	$("input[name*='[sWarehouse]']" ).autocomplete({source:skup.ADUAdj.WarehouseList,minLength:0});
	$("input[name*='[sWarehouse]']" ).bind('click', function () {$(this).autocomplete("search","");});
	$("input[name*='[iUnits']").bind('blur', function () {
		$(this).val($.parseNumber($(this).val()));
		if ($(this).val() == 0) $(this).val('');
	});
	$("input[name*='[sWarehouse']").bind('blur', function () { 
		if (-1==$.inArray($(this).val(), skup.ADUAdj.WarehouseList)) $(this).val('');
	});
}
skup.ADUAdj.Load = function() {
	$("#ADUAdj .msg").hide();
	$("#ADUAdj .tbl1 .data").remove();

	if ($("input[name$=sSKU]").val()=="") skup.ADUAdj.Message('Please enter a SKU.');
	else {
		skup.UI.PleaseWait('Getting Data...');
		
		var oResp = $.ajax({
			type: "POST",
			contentType: "application/json; charset=utf-8",
			url: "/things/the-skup/2.0/wsSKUP.asmx/LoadADUAdj",
			data: JSON.stringify({"sSKU":$("input[name$=sSKU]").val(),"sSessionID":$.cookie("sessionid")}),
			dataType: "json"
		});
		
		oResp.done(function (data) {
			try {
				if (data.d.indexOf('WS Error: ') < 0) {
					skup.ValidateSession(data.d);
					
					oJSON = $.parseJSON(data.d);
					$.each(oJSON, function() {
						skup.ADUAdj.Add(this);
					});

					$("#ADUAdj .results").show();
				}
				else skup.ADUAdj.Message('Service error occurred.');
			}
			catch (ex) {
				skup.ADUAdj.Message('Unhandled exception occurred while loading data.');
			}

			skup.UI.Unblock();
		});
	}
}
skup.ADUAdj.Add = function(oJSON) {
	//if 'o' is empty then set to blank values
	if (oJSON == undefined) { 
		var dt = new Date();
		dt = ((dt.getUTCMonth()+1<10) ? '0' : '') + (dt.getUTCMonth()+1) + '/' + ((dt.getUTCDate()+1<10) ? '0' : '') + dt.getUTCDate() + '/' + dt.getUTCFullYear();
		oJSON = {sItemCode:$("input[name$=sSKU]").val(),dtEffective:dt,sWarehouse:'',iUnits:'',iID:'',sNotes:''};
	}
	//get template, clone contents, and set row object
	var oTmpl = $("#templates .ADUAdj .tbl1 .data");
	var oTbl = $("#ADUAdj .results .tbl1");
	oTmpl.clone().appendTo(oTbl);
	var oRow = $("#ADUAdj .results .tbl1 .data").last();
	
	//get current number of rows
	var iNum = $("#ADUAdj .results .tbl1 .data").length-1;

	//replace template contents with blank or existing row data
	oRow.html(oRow.html().replace(/\$dtEffective/g, oJSON.dtEffective)
		.replace(/\$sItemCode/g, oJSON.sItemCode)		
		.replace(/\$sWarehouse/g, oJSON.sWarehouse)
		.replace(/\$iUnits/g, oJSON.iUnits)
		.replace(/\$sNotes/g, oJSON.sNotes)
		.replace(/\$iID/g, oJSON.iID)
		.replace(/\$iNum/g, iNum)
	);

	//Re-initialize Field Events
	skup.ADUAdj.UpdateEvents();
}
skup.ADUAdj.Save = function() {
	var oJSON = $("#frmADUAdj").toJSON();
	
	//validate before saving
	var bValid = true;
	$.each(oJSON.jsAdj, function(o) {
		if (((this.bDelete==undefined) ? 0 : this.bDelete) != '1') {
			if (((this.sItemCode==undefined) ? 0 : this.sItemCode.length) == 0) {
				alert('Item Code is required for all rows');
				bValid = false;
				return false;
			}
			if (((this.dtEffective==undefined) ? 0 : this.dtEffective.length) == 0) {
				alert('Effective Date is required for all rows');
				bValid = false;
				return false;
			}
			if (((this.sWarehouse==undefined) ? 0 : this.sWarehouse.length) == 0) {
				alert('Warehouse is required for all rows');
				bValid = false;
				return false;
			}
			if (((this.iUnits==undefined) ? 0 : this.iUnits.length) == 0) {
				alert('Adj. Qty is required for all rows');
				bValid = false;
				return false;
			}
		}
	});
	if (bValid == false) return false;
	
	//Show loading overlay and prep JSON
	skup.UI.PleaseWait();
	var sJSON = JSON.stringify(oJSON);
	sJSON = sJSON.substring(1, sJSON.length - 1);
	sJSON = "{" + sJSON + ",sSessionID:\"" + $.cookie("sessionid") + "\"}";
	
	var oResp = $.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "/things/the-skup/2.0/wsSKUP.asmx/SaveADUAdj",
		data: sJSON,
		dataType: "json"
	});
	
	oResp.done(function (data) {
		var sMsg = 'There were problems saving one or more rows. If this problem persists please contact the site administrator.<br /><br />The message returned was:<br />';
		try {
			var oJSON = $.parseJSON(data.d);
			if (oJSON.bSuccess == true) {
				alert('Saved successfully.');
				skup.ADUAdj.Load();
			}
			else {
				skup.ADUAdj.Message(sMsg + oJSON.sMessage);
			}
		}
		catch (ex) {
			skup.ADUAdj.Message(sMsg + oJSON.sMessage);
		}

		skup.UI.Unblock();
	});
}
skup.ADUAdj.Message = function(s) {
	$("#ADUAdj .msg").html(s);
	$("#ADUAdj .msg").show();
}

/* Functions for loading and saving ADU Adjustments */
skup.Usage = function () {}
skup.Usage.TypeList = ['FUPO', 'FUSU'];
skup.Usage.Init = function() {
	$("button").bind("click", function () { return false; });
	$("#btnRun").bind('click', function () { skup.Usage.Load(); return false; });
	$("#btnAdd").bind('click', function () { skup.Usage.Add(); return false; });
	$("#btnSave").bind('click', function () { skup.Usage.Save(); return false; });
	$("#btnCancel").bind('click', function () { skup.Usage.Load(); return false;});
}
skup.Usage.UpdateEvents = function () {
	//clear previous event bindings from adjustment fields
	$("#frmUsage input[name$='jsUsage']").unbind();

	//add bindings to adjustment fields
	$("#frmUsage input[name*='[dtTran]']" ).removeClass('hasDatepicker');
	$("#frmUsage input[name*='[dtRequired]']" ).removeClass('hasDatepicker');

	//loop all date fields and setup datepicker
	$.each($("#frmUsage").find("input[name*='[dtTran]']"), function () { $(this).datepicker(); });
	$.each($("#frmUsage").find("input[name*='[dtRequired]']"), function () { $(this).datepicker(); });

	$("#frmUsage input[name*='[sType]']" ).autocomplete({source:skup.Usage.TypeList,minLength:0});
	$("#frmUsage input[name*='[sType]']" ).bind('click', function () {$(this).autocomplete("search","");});
	$("#frmUsage input[name*='[iUnits']").bind('blur', function () {
		$(this).val($.parseNumber($(this).val()));
		if ($(this).val() == 0) $(this).val('');
	});
	$("#frmUsage input[name*='[sType']").bind('blur', function () { 
		if (-1==$.inArray($(this).val(), skup.Usage.TypeList)) $(this).val('');
	});
}
skup.Usage.Load = function() {
	$("#Usage .msg").hide();
	$("#Usage .tbl1 .data").remove();
	$("#AutoUsage .tbl1 .data").remove();

	if ($("input[name$=sSKU]").val()=="") skup.Usage.Message('Please enter a SKU.');
	else {
		skup.UI.PleaseWait('Getting Data...');
		
		var oResp = $.ajax({
			type: "POST",
			contentType: "application/json; charset=utf-8",
			url: "/things/the-skup/2.0/wsSKUP.asmx/LoadUsage",
			data: JSON.stringify({"sSKU":$("input[name$=sSKU]").val(),"sSessionID":$.cookie("sessionid")}),
			dataType: "json"
		});
		
		oResp.done(function (data) {
			try {
				if (data.d.indexOf('WS Error: ') < 0) {
					skup.ValidateSession(data.d);
					
					oJSON = $.parseJSON(data.d);
					$.each(oJSON, function() {
						skup.Usage.Add(this);
					});

					$("#Usage .results").show();
					$("#AutoUsage .results").show();
				}
				else skup.Usage.Message('Service error occurred.');
			}
			catch (ex) {
				skup.Usage.Message('Unhandled exception occurred while loading data.');
			}

			skup.UI.Unblock();
		});
	}
}
skup.Usage.Add = function(oJSON) {
	//if 'o' is empty then set to blank values
	if (oJSON == undefined) { 
		var dt = new Date(Date.now());
		dt = ((dt.getMonth()<10) ? '0' : '') + (dt.getMonth()+1) + '/' + ((dt.getDay()<10) ? '0' : '') + dt.getDay() + '/' + dt.getFullYear();
		oJSON = {sItemCode:$("input[name$=sSKU]").val(),dtTran:dt,dtRequired:dt,sType:'',iUnits:'',iID:'',sNotes:''};
	}

	//add Auto Usage line
	if (oJSON.sType == 'APO') {
		//get template, clone contents, and set row object
		var oTmpl = $("#templates .AutoUsage .tbl1 .data");
		var oTbl = $("#AutoUsage .results .tbl1");
		oTmpl.clone().appendTo(oTbl);
		var oRow = $("#AutoUsage .results .tbl1 .data").last();
	
		//get current number of rows
		var iNum = $("#AutoUsage .results .tbl1 .data").length-1;

		//replace template contents with blank or existing row data
		oRow.html(oRow.html().replace(/\$sItemCode/g, oJSON.sItemCode)
			.replace(/\$dtTran/g, oJSON.dtTran)
			.replace(/\$dtRequired/g, oJSON.dtRequired)
			.replace(/\$sType/g, oJSON.sType)
			.replace(/\$iUnits/g, oJSON.iUnits)
			.replace(/\$sNotes/g, oJSON.sNotes)
			.replace(/\$iNum/g, iNum)
		);
	}
	//add Future Usage line
	else {
		//get template, clone contents, and set row object
		var oTmpl = $("#templates .Usage .tbl1 .data");
		var oTbl = $("#Usage .results .tbl1");
		oTmpl.clone().appendTo(oTbl);
		var oRow = $("#Usage .results .tbl1 .data").last();
		
		//get current number of rows
		var iNum = $("#Usage .results .tbl1 .data").length-1;

		//replace template contents with blank or existing row data
		oRow.html(oRow.html().replace(/\$sItemCode/g, oJSON.sItemCode)
			.replace(/\$dtTran/g, oJSON.dtTran)
			.replace(/\$dtRequired/g, oJSON.dtRequired)
			.replace(/\$sType/g, oJSON.sType)
			.replace(/\$iUnits/g, oJSON.iUnits)
			.replace(/\$sNotes/g, oJSON.sNotes)
			.replace(/\$iID/g, oJSON.iID)
			.replace(/\$iNum/g, iNum)
		);

		//Re-initialize Field Events
		skup.Usage.UpdateEvents();
	}
}
skup.Usage.Save = function() {
	var oJSON = $("#frmUsage").toJSON();
	
	//validate before saving
	var bValid = true;
	$.each(oJSON.jsUsage, function(o) {
		if (((this.bDelete==undefined) ? 0 : this.bDelete) != '1') {
			if (((this.sItemCode==undefined) ? 0 : this.sItemCode.length) == 0) {
				alert('Item Code is required for all rows');
				bValid = false;
				return false;
			}
			if (((this.dtTran==undefined) ? 0 : this.dtTran.length) == 0) {
				alert('Tran. Date is required for all rows');
				bValid = false;
				return false;
			}
			if (((this.dtRequired==undefined) ? 0 : this.dtRequired.length) == 0) {
				alert('Req. Date is required for all rows');
				bValid = false;
				return false;
			}
			if (((this.sType==undefined) ? 0 : this.sType.length) == 0) {
				alert('Type is required for all rows');
				bValid = false;
				return false;
			}
			if (((this.iUnits==undefined) ? 0 : this.iUnits.length) == 0) {
				alert('Qty is required for all rows');
				bValid = false;
				return false;
			}
		}
	});
	if (bValid == false) return false;
	
	//Show loading overlay and prep JSON
	skup.UI.PleaseWait();
	var sJSON = JSON.stringify(oJSON);
	sJSON = sJSON.substring(1, sJSON.length - 1);
	sJSON = "{" + sJSON + ",sSessionID:\"" + $.cookie("sessionid") + "\"}";
	
	var oResp = $.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "/things/the-skup/2.0/wsSKUP.asmx/SaveUsage",
		data: sJSON,
		dataType: "json"
	});
	
	oResp.done(function (data) {
		var sMsg = 'There were problems saving one or more rows. If this problem persists please contact the site administrator.<br /><br />The message returned was:<br />';
		try {
			var oJSON = $.parseJSON(data.d);
			if (oJSON.bSuccess == true) {
				alert('Saved successfully.');
				skup.Usage.Load();
			}
			else {
				skup.Usage.Message(sMsg + oJSON.sMessage);
			}
		}
		catch (ex) {
			skup.Usage.Message(sMsg + oJSON.sMessage);
		}

		skup.UI.Unblock();
	});
}
skup.Usage.Message = function(s) {
	$("#Usage .msg").html(s);
	$("#Usage .msg").show();
}

/* Validates Web Service response to ensure session is not invalid */
skup.ValidateSession = function(s) {
	if (s == "Invalid Session") {
		alert("Not logged in or session timed out.\n\nRedirecting to Sign In page.");
		window.location.href = "/";
		return false;
	}
	else return true;
}