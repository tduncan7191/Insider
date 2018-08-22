/*******************
JMW: These are old functions that will need to be deprecated eventually
There are still some known pages that still use these methods.
*******************/
/* 'o' = Field Object, 's' = Default Text */
function fnDynamicFormText_Toggle(o, s) {
	if (o.value == s) {
		o.style.color = '#000';
		o.value = '';
	}
	else if (o.value == '') {
		o.style.color = '';
		o.value = s;
	}
}

/* XMLHTTP Function */
function fnCreateHTTP() {
    var oHTTP = false;
    if (window.XMLHttpRequest) { // Mozilla, Safari, ...
        oHTTP = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) { // IE
        try {
            oHTTP = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                oHTTP = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) { }
        }
    }
    return oHTTP
}

/****************************
AJAX Form Handlers (Requires JQuery)
****************************/
function fnAJAXForm_ShowForm(bReset, oF, oR) {
	oR.style.display = 'none';
	oR.innerHTML = '';
	if (bReset) oF.reset();
	for (var i = 0; i < oF.length; i++) {
		try {
			$("#" + oF.elements[i].id).blur();
		}
		catch (err) { continue; }
	}
	oF.style.display = 'block';
}

//Requires 'oF' (Form Object), and 'oR' (HTML Response Container Object).
//Optionally accepts 'sDo' as JavaScript code that will be executed after a successful response is received.
function fnAJAXForm_Post(oF, oR, sDo) {
	var oHTTP = fnCreateHTTP();
	if (!oHTTP) {
		//alert('Unable to save form data.\n\nIf this message persists please contact the site administrator.');
		fnAJAXForm_ProcessResponse('', oF, oR);
	}
	else {
		oHTTP.onreadystatechange = function () {
			if (oHTTP.readyState == 4) {
				if (oHTTP.status == 200) {
					//alert(oHTTP.responseText);
					fnAJAXForm_ProcessResponse(oHTTP.responseText, oF, oR);
					if (sDo != null) eval(sDo);
				} else {
					//alert(oHTTP.responseText);
					fnAJAXForm_ProcessResponse(oHTTP.responseText, oF, oR);
				}
			}
		};
		oHTTP.open('POST', oF.action, true);
		oHTTP.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		oHTTP.send($(oF).serialize());
	}
}

function fnAJAXForm_ProcessResponse(s, oF, oR) {
	if (s != '') {
		oF.style.display = 'none';
		oR.innerHTML = s;
		oR.style.display = 'block';
	}
	else {
		oF.style.display = 'none';
		oR.innerHTML = '<div class="AJAXForm_Error">Sorry, an unexpected problem occurred. <a href="" onclick="fnAJAXForm_ShowForm(false,document.forms[\'' + oF.id + '\'],document.getElementById(\'' + oR.id + '\'));return false;">Click here</a> to go back to the form.<br /><br />If this message persists please contact the site administrator.</div>';
		oR.style.display = 'block';
	}
}
