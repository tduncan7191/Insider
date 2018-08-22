var cTB = null;

$(document).ready(function () {
	fnVerifyCompatibility();

	//load and initialize class
	cTB = new clsTextBoard();
	cTB.Bind.Init();
});

//checks browser version against provided list and notifies user about compatibility issues
function fnVerifyCompatibility() {
	if (window.location.search.indexOf('compat=0') == -1) {
		var sMessage = 'The detected Device or Web Browser may not be fully compatible with this page.\n\n'
						+ 'For the best experience the following Devices and Web Browsers are recommended: Android, Chrome, iPad/iPhone/iPod, Safari.'
						+ '\n\n' + navigator.userAgent.toLowerCase();

		var arrCompatibleBrowsers = new Array();
		arrCompatibleBrowsers[0] = 'ipad';
		arrCompatibleBrowsers[1] = 'iphone';
		arrCompatibleBrowsers[2] = 'android';
		arrCompatibleBrowsers[3] = 'blackberry';
		arrCompatibleBrowsers[4] = 'safari';
		arrCompatibleBrowsers[5] = 'chrome';
		arrCompatibleBrowsers[6] = 'firefox';
		arrCompatibleBrowsers[7] = 'msie 8';
		arrCompatibleBrowsers[8] = 'msie 9';
		arrCompatibleBrowsers[9] = 'msie 10';

		var bCompatible = 0;

		for (i = 0; i < arrCompatibleBrowsers.length - 1; i++) {
			if (navigator.userAgent.toLowerCase().indexOf(arrCompatibleBrowsers[i]) != -1) {
				bCompatible = 1;
			}
		}

		if (bCompatible == 0) { alert(sMessage); return false; }
		else return true;
	}
	else return true;
}

function clsTextBoard(sTB_Type) {
	var cBind = new cBind();
	this.Bind = cBind;
	var cPostData = new cPostData();
	this.PostData = cPostData;
	
	function cBind() {
		this.Init = Init;

		function Init() {
			$.ajaxSetup({
				type: 'POST',
				headers: { "cache-control": "no-cache" }
			});

			$("#btnSaveText").bind("click", function () { cPostData.SaveText(); });

			cPostData.LoadText();
		}
	}

	function cPostData() {
		this.SaveText = SaveText;
		this.LoadText = LoadText;

		function NextSide() {
			var sClass = $("#TB_Responses").children().first().attr('class');

			if (sClass == null) return "left";
			else {
				if (sClass == "right") return "left";
				else return "right";
			}
		}

		function AddText(s) {
			//add line breaks
			s = s.replace('\n', '<br />');

			var sText = "<div class=\"" + NextSide() + "\">" + s + "</div>";
			$("#TB_Responses").html(sText + $("#TB_Responses").html());
		}

		function SaveText() {
			
			$.ajax({
				async: false,
				type: "POST",
				contentType: "application/json; charset=utf-8",
				url: "/ws/text-board.asmx/SaveText",
				data: JSON.stringify($("#frmTB").toJSON()),
				dataType: "json",
				complete: function (jqXHR, textStatus) {
					rp.Console(jqXHR.responseText);
					var oJSON = $.parseJSON(jqXHR.responseText);
					var oJSON = $.parseJSON(oJSON.d);
					
					try {
						if (oJSON.bSuccess) {
							AddText($("textarea[name$='sText']").val());
							$("textarea[name$='sText']").val('');
						}
						else alert('Sorry, the text could not be added due to a service error.\n\nPlease try again, and if this error persists please contact the site administrator.');
					}
					catch (e) {
						alert('Sorry, the text could not be added due to a service error.\n\nPlease try again, and if this error persists please contact the site administrator.');
					}
				}
			});
		}

		function LoadText() {
			$.ajax({
				async: false,
				type: "POST",
				contentType: "application/json; charset=utf-8",
				url: "/ws/text-board.asmx/LoadText",
				data: JSON.stringify({ sType: $("input[name$='sBoard']").val() }),
				dataType: "json",
				complete: function (jqXHR, textStatus) {
					rp.Console(jqXHR.responseText);
					var oJSON = $.parseJSON(jqXHR.responseText);
					var oJSON = $.parseJSON(oJSON.d);
					var sText = "";

					$.each(oJSON, function () {
						AddText(this.sText);
					})
				}
			});
		}
	}
}