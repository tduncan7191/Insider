/*
Redemption Plus
Data Alerts
Version 1.0
Last modified on Dec. 24, 2013
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
	- rp.js

Changelog:
*/
$(document).ready(function () {
	da.Init.Defaults();
});

/**************
Empty object to be extended with class methods
**************/
var da = {};

/**************
Alert Classes
Define all new alert classes here, and they will be accessible automatically by the other methods.
Step 1: Define js class
Step 2: Add HTML template for editor with description block
**************/
da.AlertTypes = {};
/************** NEW CLASS TEMPLATE
da.AlertTypes.NewClassName = {};
da.AlertTypes.NewClassName.Class = "NewClassName";
da.AlertTypes.NewClassName.Name = "New Class Display String";
da.AlertTypes.NewClassName.Bindings = function () { //binds HTML editor actions
}
da.AlertTypes.NewClassName.ParamJSON = function () { //builds Parameter JSON from HTML editor
	return JSON.stringify({});
} 
da.AlertTypes.NewClassName.PreLoad = function() { //populates HTML data before loading alert params
}
da.AlertTypes.NewClassName.Load = function(p) { //populates class fields in HTML editor from ParamJSON (p) as passed from web service
}
da.AlertTypes.NewClassName.Validate = function() { //performs class-specific HTML validation. Return true if no class validation required.
	return true;
}
**************/
da.AlertTypes.UserDefined = {};
da.AlertTypes.UserDefined.Class = "UserDefined";
da.AlertTypes.UserDefined.Name = "User Defined";
da.AlertTypes.UserDefined.Bindings = function () { //binds HTML editor actions
}
da.AlertTypes.UserDefined.ParamJSON = function () { //builds Parameter JSON from HTML editor
	return JSON.stringify({"ViewName":$("#UserDefined input[name='ViewName']").val()});
}  
da.AlertTypes.UserDefined.PreLoad = function() { //populates HTML data before loading alert params
}
da.AlertTypes.UserDefined.Load = function(p) { //populates class fields in HTML editor from ParamJSON (p) as passed from web service
	$("#UserDefined input[name='ViewName']").val(p.ViewName);
}
da.AlertTypes.UserDefined.Validate = function() { //performs class-specific HTML validation. Return true if no class validation required.
	var j = $.parseJSON(this.ParamJSON());
	if (j.ViewName.length==0) { alert("View Name is required."); return true; }
	else return true;
}
da.AlertTypes.UsageSpike = {};
da.AlertTypes.UsageSpike.Class = "UsageSpike";
da.AlertTypes.UsageSpike.Name = "Usage Spike";
da.AlertTypes.UsageSpike.Bindings = function () {
	$("#UsageSpike a[class='clear_pc']").bind('click', function() {$.each($("#UsageSpike input[name='ProductClass']"),function() { $(this).removeAttr("checked",""); }); return false; });
	$("#UsageSpike a[class='select_pc']").bind('click', function() {$.each($("#UsageSpike input[name='ProductClass']"),function() { $(this).attr("checked",""); }); return false; });
	$("#UsageSpike input[name='RangeDays']").bind('keyup', function() {$(this).formatNumber({ format: '##0', locale: 'us' });});
	$("#UsageSpike input[name='RangeThreshold']").bind('keyup', function() {$(this).formatNumber({ format: '###0', locale: 'us' });});
}
da.AlertTypes.UsageSpike.ParamJSON = function() {
	var pc = "";
	$.each($("#UsageSpike input[name='ProductClass']"), function () {
		if ($(this).is(":checked")) {
			if (pc.length>0) pc += ",";
			pc += $(this).val();
		}
	});

	//Build JSON parameters string from form fields.
	return JSON.stringify({"ProductClasses":(pc.length==0) ? null : pc
		,"ActiveItems":($("#UsageSpike select[name='ActiveItems'] option:selected").val()=="") ? null : Boolean($("#UsageSpike select[name='ActiveItems'] option:selected").val())
		,"ReorderItems":($("#UsageSpike select[name='ReorderItems'] option:selected").val()=="") ? null : Boolean($("#UsageSpike select[name='ReorderItems'] option:selected").val())
		,"Top100Items":($("#UsageSpike select[name='Top100Items'] option:selected").val()=="") ? null : Boolean($("#UsageSpike select[name='Top100Items'] option:selected").val())
		,"Include004":Boolean($("#UsageSpike select[name='Include004'] option:selected").val())
		,"IgnoreZeroQOH":Boolean($("#UsageSpike select[name='IgnoreZeroQOH'] option:selected").val())
		,"LeadtimeStockout":($("#UsageSpike select[name='LeadtimeStockout'] option:selected").val()=="") ? null : Boolean($("#UsageSpike select[name='LeadtimeStockout'] option:selected").val())
		,"Range":($("#UsageSpike select[name='Range'] option:selected").val()=="") ? null : Boolean($("#UsageSpike select[name='Range'] option:selected").val())
		,"RangeDays":$("#UsageSpike input[name='RangeDays']").val()
		,"RangeThreshold":$("#UsageSpike input[name='RangeThreshold']").val()
	});
}
da.AlertTypes.UsageSpike.PreLoad = function() {
	//Get latest Product Classes with selections
	$.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "ws-data-alerts.asmx/LoadProductClasses",
		data: JSON.stringify({"SessionID":$.cookie("sessionid")}),
		dataType: "json",
		async:false,
		complete: function (jqXHR, textStatus) {
			try {
				var oJSON = $.parseJSON(jqXHR.responseText);
				da.ValidateSession(oJSON.d);		
				oJSON = $.parseJSON(oJSON.d);
				
				if (!oJSON.Success) { da.UI.ShowResponse('Error loading alert. Refresh the page and try again.'); }
				else {
					var s = oJSON.Message.split(',');
					
					//iterate through product classes and add with selections
					var pc = "";
					for (var i=0;i<s.length;i++) {
						pc += '<span class="productclass"><input type="checkbox" value="' + s[i] + '" name="ProductClass" id="chk' + s[i] + '" />&nbsp;<label for="chk' + s[i] + '">' + s[i] + '</label></span>'
					}
					$("#UsageSpike .pc").html(pc);
				}
			}
			catch (ex) {
				da.UI.ShowResponse('Unhandled exception occurred while loading product classes.<br /><br />' + ex.toString());
			}
		}
	});
}
da.AlertTypes.UsageSpike.Load = function(p) {
	if (p==null) var p = {};
	else {
		p = $.parseJSON(p);

		if (p.ActiveItems==true) $("#UsageSpike select[name='ActiveItems'] option[value='true']").attr("selected","selected");
		else if (p.ActiveItems==false) $("#UsageSpike select[name='ActiveItems'] option[value='false']").attr("selected","selected");
		else $("#UsageSpike select[name='ActiveItems'] option[value='']").attr("selected","selected");

		if (p.ReorderItems==true) $("#UsageSpike select[name='ReorderItems'] option[value='true']").attr("selected","selected");
		else if (p.ReorderItems==false) $("#UsageSpike select[name='ReorderItems'] option[value='false']").attr("selected","selected");
		else $("#UsageSpike select[name='ReorderItems'] option[value='']").attr("selected","selected");

		if (p.Include004==true) $("#UsageSpike select[name='Include004'] option[value='true']").attr("selected","selected");
		else if (p.Include004==false) $("#UsageSpike select[name='Include004'] option[value='false']").attr("selected","selected");
		else $("#UsageSpike select[name='Include004'] option[value='']").attr("selected","selected");

		if (p.IgnoreZeroQOH==true) $("#UsageSpike select[name='IgnoreZeroQOH'] option[value='true']").attr("selected","selected");
		else if (p.IgnoreZeroQOH==false) $("#UsageSpike select[name='IgnoreZeroQOH'] option[value='false']").attr("selected","selected");

		if (p.LeadtimeStockout==true) $("#UsageSpike select[name='LeadtimeStockout'] option[value='true']").attr("selected","selected");
		else if (p.LeadtimeStockout==false) $("#UsageSpike select[name='LeadtimeStockout'] option[value='false']").attr("selected","selected");
		else $("#UsageSpike select[name='LeadtimeStockout'] option[value='']").attr("selected","selected");

		if (p.Range==true) $("#UsageSpike select[name='Range'] option[value='true']").attr("selected","selected");
		else if (p.Range==false) $("#UsageSpike select[name='Range'] option[value='false']").attr("selected","selected");

		$("#UsageSpike input[name='RangeDays']").val(p.RangeDays);
		$("#UsageSpike input[name='RangeThreshold']").val(p.RangeThreshold);

		var pc = p.ProductClasses.split(',');
		for (var i=0;i<pc.length;i++) {
			$("#UsageSpike input[id='chk" + pc[i] + "']").attr("checked","checked");
		}
	}

	da.AlertTypes.UsageSpike.Bindings();
}
da.AlertTypes.UsageSpike.Validate = function() {
	//get json
	var j = $.parseJSON(this.ParamJSON());

	//make sure range numbers exist if selected
	if (j.Range!=null)
	{
		if (j.RangeDays==0 || j.RangeThreshold==0)
		{ alert("Range Days and Range Threshold must be greater than zero."); return false; }
	}

	//product class
	if (((j.ProductClasses==null) ? 0 : j.ProductClasses.length)==0) 
		{ alert("At least one Product Class is required."); return false; }
	
	//warn user about broad params
	if (j.ActiveItems==null && j.ReorderItems==null && j.Top100Items==null && j.IgnoreZeroQOH==false && j.Range==null)
		{ alert("The values selected may result in a large number of items being returned.\n\nThe alert will be saved, but you might want to consider making it more strict."); }

	return true;
}
da.AlertTypes.RBDCItems = {};
da.AlertTypes.RBDCItems.Class = "RBDCItems";
da.AlertTypes.RBDCItems.Name = "Reward Board Discontinued Items";
da.AlertTypes.RBDCItems.Bindings = function() {
}
da.AlertTypes.RBDCItems.ParamJSON = function() {
	return JSON.stringify({});
}
da.AlertTypes.RBDCItems.PreLoad = function() {
}
da.AlertTypes.RBDCItems.Load = function(p) {
}
da.AlertTypes.RBDCItems.Validate = function() {
	return true;
}

/**************
Initializes the methods on the page
**************/
da.Init = {};
da.Init.Defaults = function () {	
	//Ensure browsers doesn't cache AJAX responses
	$.ajaxSetup({
		type: 'POST',
		headers: { "cache-control": "no-cache" }
	});
	
	//Add AlertTypes to drop-down
	$.each(da.AlertTypes, function () {
		$('#AlertEditor select[name="AlertTypeClass"]').append($("<option></option").val(this.Class).html(this.Name));
	});
	
	da.Init.Bindings();

	da.Instances.Load();

	/* Extend jQuery with custom .center() functions */
	if (jQuery) {
		jQuery.fn.centerBoth = function () {
			$(this).centerHorizontal();
			$(this).centerVertical();
			return this;
		}
		jQuery.fn.centerHorizontal = function () {
			var iLeft = (($(window).width() / 2) - ($(this).outerWidth() / 2)) + $(window).scrollLeft();
			
			iLeft = (iLeft < $(window).scrollLeft()) ? $(window).scrollLeft() : iLeft;

			this.css("position","absolute");
			this.css("left", iLeft + "px");
			return this;
		}
		jQuery.fn.centerVertical = function () {
			var iTop = (($(window).height() / 2) - ($(this).outerHeight() / 2)) + $(window).scrollTop();
			
			iTop = (iTop < $(window).scrollTop()) ? $(window).scrollTop() : iTop;

			this.css("position","absolute");
			this.css("top", iTop + "px");
			return this;
		}
	}
}
da.Init.Bindings = function() {
	$('#ServerResponse a').bind("click", function() { $("#ServerResponse").hide(); return false;});
	$('#AlertInstances button[name="btnNew"]').bind("click", function () { da.Editor.Load(0); });
	$('#AlertEditor select[name="AlertTypeClass"]').bind("change", function() { da.Editor.TypeChanged(); });
	$('#AlertEditor button[name="btnSave"]').bind("click", function() { da.Editor.Save(); });
	$('#AlertEditor button[name="btnCancel"]').bind("click", function() {
		da.UI.HideDialog();
		$("html, body").scrollTop($("#AlertInstances").offset().top);
	});
}

/**************
Editor Classes
**************/
da.Editor = {};
da.Editor.Bindings = function() {
	$("#AlertEditor select[name='Frequency']").bind('change', function() { da.Editor.FrequencyChanged(); });
}
da.Editor.Load = function(AlertID) {
	if (AlertID==null) var AlertID = 0;

	da.UI.PleaseWait();
	da.Editor.Bindings();

	//Reset form
	document.getElementById('AlertForm').reset();
	$("#AlertEditor input[name='AlertID']").val('');
	$("#AlertEditor select[name='AlertTypeClass'] option:selected").removeAttr("selected");
	$("#AlertEditor select[name='AlertTypeClass'] option[value='']").attr("selected","selected");
	$("#AlertEditor input[name='DaysToRun']").removeAttr("checked");
	$("#AlertEditor .AlertTypeTemplate").hide();
	
	//Find the AlertType and execute its Load() method
	$.each(da.AlertTypes, function() {
		this.PreLoad();
	});

	if (AlertID != 0) {
		$.ajax({
			type: "POST",
			async:false,
			contentType: "application/json; charset=utf-8",
			url: "ws-data-alerts.asmx/LoadAlert",
			data: JSON.stringify({"AlertID":AlertID,"SessionID":$.cookie("sessionid")}),
			dataType: "json",
			complete: function (jqXHR, textStatus) {
				try {
					var oJSON = $.parseJSON(jqXHR.responseText);
					da.ValidateSession(oJSON.d);		
					oJSON = $.parseJSON(oJSON.d);

					if (!oJSON.Success) { da.UI.ShowResponse('Error loading alert. Refresh the page and try again.'); }
					else {
						oJSON = $.parseJSON(oJSON.Message);
						$("#AlertEditor input[name='AlertID']").val(oJSON.AlertID);
						$("#AlertEditor input[name='Name']").val(oJSON.Name);
						$("#AlertEditor input[name='SendTo']").val(oJSON.SendTo);
						
						if (oJSON.Active==true) $("#AlertEditor select[name='Active'] option[value='true']").attr("selected","selected");
						else  $("#AlertEditor select[name='Active'] option[value='false']").attr("selected","selected");

						if (oJSON.DaysToRun.indexOf('Sunday')>=0) $("#AlertEditor input[id='Sunday']").attr("checked","checked");
						if (oJSON.DaysToRun.indexOf('Monday')>=0) $("#AlertEditor input[id='Monday']").attr("checked","checked");
						if (oJSON.DaysToRun.indexOf('Tuesday')>=0) $("#AlertEditor input[id='Tuesday']").attr("checked","checked");
						if (oJSON.DaysToRun.indexOf('Wednesday')>=0) $("#AlertEditor input[id='Wednesday']").attr("checked","checked");
						if (oJSON.DaysToRun.indexOf('Thursday')>=0) $("#AlertEditor input[id='Thursday']").attr("checked","checked");
						if (oJSON.DaysToRun.indexOf('Friday')>=0) $("#AlertEditor input[id='Friday']").attr("checked","checked");
						if (oJSON.DaysToRun.indexOf('Saturday')>=0) $("#AlertEditor input[id='Saturday']").attr("checked","checked");

						$("#AlertEditor select[name='Frequency'] option[value='" + oJSON.Frequency + "']").attr("selected","selected");
					
						//Set the AlertType and execute its Load() method
						$("#AlertEditor select[name='AlertTypeClass'] option[value='" + oJSON.AlertTypeClass + "']").attr("selected","selected");
						$.each(da.AlertTypes, function() {
							if (this.Class==$("#AlertEditor select[name='AlertTypeClass'] option:selected").val()) {
								this.Load($.parseJSON(oJSON.ParamJSON));
							}
						});
					}
				}
				catch (ex) {
					alert(ex.message);
					da.UI.ShowResponse('Unhandled exception occurred while deleting instance data.<br /><br />' + ex.toString());
				}
			}
		});
	}
	
	da.Editor.FrequencyChanged();
	da.Editor.TypeChanged();

	da.UI.HideDialog();
	da.UI.ShowDialog($("#AlertEditor"));
	$("html,body").scrollTop(0);
}
da.Editor.Save = function() {
	if (da.Editor.Validate())
	{
		da.UI.PleaseWait($("#AlertEditor"));
		
		var oResp = $.ajax({
			type: "POST",
			contentType: "application/json; charset=utf-8",
			url: "ws-data-alerts.asmx/SaveAlert",
			data: JSON.stringify({"jsAlert":da.Editor.BuildJSON(),"SessionID":$.cookie("sessionid")}),
			dataType: "json",
			async:false,
			complete: function (jqXHR, textStatus) {
				try {
					var oJSON = $.parseJSON(jqXHR.responseText);
					da.ValidateSession(oJSON.d);		
					oJSON = $.parseJSON(oJSON.d);
					da.UI.ShowResponse(oJSON.Message);
				}
				catch(ex) {
					da.UI.ShowResponse("Unhandled exception occurred while saving alert.<br /><br />" + ex.toString());
				}
			}
		});
		
		$("html,body").scrollTop($("#ServerResponse").offset().top);
		da.Instances.Load();
	}
}
da.Editor.BuildJSON = function() {
	var j = {};
	j.AlertID = $("#AlertEditor input[name='AlertID']").val();
	j.AlertID = (j.AlertID.length==0) ? 0 : j.AlertID;
	j.Name = $("#AlertEditor input[name='Name']").val();
	j.SendTo = $("#AlertEditor input[name='SendTo']").val();
	j.Active = $("#AlertEditor select[name='Active'] option:selected").val();

	j.DaysToRun = "";
	$.each($("#AlertEditor input[name='DaysToRun']"), function() {
		if ($(this).is(":checked")) j.DaysToRun += (j.DaysToRun=="") ? $(this).val() : "," + $(this).val();
	});
	
	j.Frequency = $("#AlertEditor select[name='Frequency'] option:selected").val();
	j.AlertTypeClass = $("#AlertEditor select[name='AlertTypeClass'] option:selected").val();

	//Get AlertType object so we can run class-specific Validate()
	var s = {};
	$.each(da.AlertTypes, function() {
		if (this.Class==$("#AlertEditor select[name='AlertTypeClass'] option:selected").val()) {
			s = this.ParamJSON();
		}
	});
	j.ParamJSON = s;
	
	return JSON.stringify(j);
}
da.Editor.Validate = function() { //expects a da.AlertType object
	try {
		var o = $.parseJSON(da.Editor.BuildJSON());
		
		if (o.Name.length==0) { alert("An Instance Name is required."); return false; }
	
		if (o.SendTo.length==0) { alert("Send To is required."); return false; }
		else {
			var e = o.SendTo.split(',');
			for (var i=0;i<e.length;i++) {
				var s = e[i];
				if (s.indexOf('.')<0 || s.indexOf('@')<0) { alert("An invalid email was found in Send To.\n\nPlease correct and try again."); return false; }
			}
		}

		if (o.DaysToRun.length==0) { alert("You must select at least one Day of Week."); return false; }

		if (o.AlertTypeClass.length==0) { alert("An Alert Class is required."); return false; }
		
		var v = false;
		$.each(da.AlertTypes, function() {
			if (this.Class==$("#AlertEditor select[name='AlertTypeClass'] option:selected").val()) {
				v = this.Validate();
			}
		});

		return v; //return class level validation result
	}
	catch (e) {
		alert('Validation error.\n\n'+ e.toString());
		return false;
	}
}
da.Editor.FrequencyChanged = function() {
	$("#AlertEditor .Frequency").hide();
	$("#AlertEditor .Frequency." + $("#AlertEditor select[name='Frequency'] option:selected").val()).show();
}
da.Editor.TypeChanged = function() {
	$.each(da.AlertTypes, function() {
		$("#AlertEditor #" + this.Class).hide();
	});
	var s = $("#AlertEditor select[name='AlertTypeClass'] option:selected").val();
	if (s.length>0) $("#AlertEditor #" + s).show();
}

/* UI Functions */
da.UI = function () {}
da.UI.PleaseWait = function (o) {
	//set default blocking if no object passed
	if (o==undefined) o = $("#DataAlerts").parent();

	//Uses jQuery blockui plugin with custom loading image and formatting
	o.block({message:"<img src=\"/images/loading-pacman.gif\" style=\"margin:15px;\" />"
								, css: {border:'2px solid #f1913b', borderRadius:'8px', backgroundColor:'#fff'}
	});
}
da.UI.ShowDialog = function(oDiag,iFade) {
	if (iFade == undefined) var iFade = 0;
	
	$.blockUI({message:oDiag
		,fadeIn:iFade
		,css:{cursor:'default',position:'absolute'}
		,themedCSS:{cursor:'default',position:'absolute',top:'50px',width:'760px',left:'0',right:'0'}
		,overlayCSS:{cursor:'default',position:'absolute'}
		,theme:true
		,draggable:false
		,centerX:false
		,centerY:false
		,allowBodyStretch:true
		,bindEvents:false
	});

}
da.UI.HideDialog = function() {
	if (iFade == undefined) var iFade = 0;
	
	//unblock all possible blocked elements
	$.unblockUI();
	$("*").unblock();
}
da.UI.ShowResponse = function(s) {
	da.UI.HideDialog();
	$("#ServerResponse div").html(s);
	$("#ServerResponse").show();
	$("html,body").scrollTop($("#ServerResponse").offset().top);
}

/* Functions for loading and saving alert instances */
da.Instances = function () {}
da.Instances.Bindings = function() {
	$('#AlertInstances button[name="btnEdit"]').bind('click', function () { da.Editor.Load($(this).parent().find("input[name='AlertID']").val()); });
	$('#AlertInstances button[name="btnDelete"]').bind('click', function () { da.Instances.Delete($(this).parent()); });
}
da.Instances.Load = function() {
	da.UI.PleaseWait();
	
	var oResp = $.ajax({
		type: "POST",
		contentType: "application/json; charset=utf-8",
		url: "ws-data-alerts.asmx/LoadInstances",
		data: JSON.stringify({"SessionID":$.cookie("sessionid")}),
		dataType: "json"
	});
		
	oResp.done(function (data) {
		try {
			da.ValidateSession(data.d);		
			oJSON = $.parseJSON(data.d);
			if (!oJSON.Success) { da.UI.ShowResponse('Unexpected response from Web service.') }
			else {
				var c = $('#AlertInstances .data');
				c.html('');

				//loop each alert and add to instances
				$.each($.parseJSON(oJSON.Message), function() {
					var sHTML = $('#AlertInstances .template').first().html();
					c.append(sHTML.replace(/\$Name/g, this.Name)
								.replace(/\$AlertID/g, this.AlertID)
								.replace(/\$AlertTypeName/g, this.AlertTypeName)	
								.replace(/\$AlertTypeClass/g, this.AlertTypeClass)
								.replace(/\$Active/g, this.Active)
								.replace(/\$Frequency/g, this.Frequency)
								.replace(/\$DaysToRun/g, (this.DaysToRun=="") ? "" : "(" + this.DaysToRun + ")")
								.replace(/\$LastRun/g, this.LastRun)
								.replace(/\$NextRun/g, this.NextRun)
								.replace(/\$SendTo/g, this.SendTo));
				});

				da.Instances.Bindings();
			}
		}
		catch (ex) {
			da.UI.ShowResponse('Unhandled exception occurred while loading data.<br /><br />' + ex.toString());
		}

		da.UI.HideDialog();
	});
}
da.Instances.Delete = function(a) {
	if (confirm("Are you sure you want to delete the '" + $(a).find('h3').html() + "' instance?\n\nThis action cannnot be undone. Click \'OK\' to delete or 'Cancel' to abort."))
	{
		da.UI.PleaseWait();
	
		var oResp = $.ajax({
			type: "POST",
			contentType: "application/json; charset=utf-8",
			url: "ws-data-alerts.asmx/DeleteAlert",
			data: JSON.stringify({"AlertID":$(a).find('input[name="AlertID"]').val(),"SessionID":$.cookie("sessionid")}),
			dataType: "json"
		});
		
		oResp.done(function (data) {
			try {
				da.ValidateSession(data.d);		
				oJSON = $.parseJSON(data.d);
				if (!oJSON.Success) { da.UI.ShowResponse('Error occurred.<br /><br />Refresh the page to confirm whether or not the instance was deleted.'); }
				else {
					da.UI.ShowResponse('Instance deleted successfully.')
					$(a).remove();
				}
			}
			catch (ex) {
				da.UI.ShowResponse('Unhandled exception occurred while deleting instance data.<br /><br />' + ex.toString());
			}

			da.UI.HideDialog();
		});
	}
}

/* Validates Web Service response to ensure session is not invalid */
da.ValidateSession = function(s) {
	if (s == "Invalid Session") {
		alert("Not logged in or session timed out.\n\nRedirecting to Sign In page.");
		window.location.href = "/";
		return false;
	}
	else return true;
}