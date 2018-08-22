<%@ Control Language="C#" ClassName="ctlAnswers_SubmitQuestion" %>

<h2>Submit an Answer</h2>

<script type="text/javascript">
	var sAnswersForm_Answer = "Enter your answer here...";
	window.onload = function () { document.getElementById('AnswersForm_sAnswer').value = sAnswersForm_Answer; };

	function fnDisplayAnswer() {
		var sAnswer = document.forms['AnswersForm'].AnswersForm_sAnswer.value;
		sAnswer = sAnswer.replace(/\n|\r/g, '<br />');
		
		var d = document.createElement('div');
		d.setAttribute('class', 'Answer');
		d.setAttribute('className', 'Answer')

		d.innerHTML = rp.GetCookie('firstname') + ' ' + rp.GetCookie('lastname') + ' said:';
		
		var d2 = document.createElement('div');
		d2.setAttribute('class', 'Text');
		d2.setAttribute('className', 'Text');
		d2.innerHTML = sAnswer;

		var d3 = document.createElement('div');
		d3.setAttribute('class', 'Date');
		d3.setAttribute('className', 'Date');

		var dt = new Date();
		d3.innerHTML = (dt.getUTCMonth() + 1) + '/' + dt.getUTCDate() + '/' + dt.getFullYear() + ' ' + dt.toLocaleTimeString();
		
		if (document.getElementById('Answers').childNodes[1].innerHTML == 'No answers found.') document.getElementById('Answers').childNodes[1].innerHTML = '';

		d2.appendChild(d3);
		d.appendChild(d2);
		
		document.getElementById('Answers').appendChild(d);
	}

	function fnAnswersForm_Validate(o) {
		try {
			$('#AnswersForm_sAnswer').blur();

			var sMsg = null;

			if (o.AnswersForm_sAnswer.value == '') {
				sMsg = 'You must enter an Answer before submitting.';
			}

			if (sMsg != null) {
				$('#AnswersForm_sAnswer').blur();
				alert(sMsg);
			}
			else fnAJAXForm_Post(o, document.getElementById('AnswersForm_Response'), 'fnDisplayAnswer();');
		}
		catch (ex) {
			//don't do anything, just don't fail.
		}
		return false;
    }
</script>
<div id="AnswersForm_Response" class="AJAXForm_Response">&nbsp;</div>
<form method="post" action="/ajax-data-services/answers/" onsubmit="fnAnswersForm_Validate(this);return false;" id="AnswersForm" class="RPlus_Form center">
	<input type="hidden" name="AnswersForm_sFormType" id="AnswersForm_sFormType" value="new-answer" />
	<input type="hidden" name="AnswersForm_iQID" id="AnswersForm_iQID" value="<%=Request.QueryString["iqid"]%>" />
	<textarea name="AnswersForm_sAnswer" id="AnswersForm_sAnswer" class="Field CommentBox" rows="12" cols="50" onfocus="fnDynamicFormText_Toggle(this,sAnswersForm_Answer);" onblur="fnDynamicFormText_Toggle(this,sAnswersForm_Answer);"></textarea>
	<br />
	<input type="submit" name="btnSubmit" id="btnSubmit" value="Submit Answer" />
</form>