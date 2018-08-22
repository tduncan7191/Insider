<%@ Control Language="C#" ClassName="ctlAnswers_SubmitQuestion" %>

<h2>Submit a Question</h2>

<script type="text/javascript">
	var sAnswersForm_Default_Subject = "Enter question subject/title...";
	var sAnswersForm_Default_Question = "Enter detailed question text...";

	window.onload = function () {
		document.getElementById('AnswersForm_sSubject').value = sAnswersForm_Default_Subject;
		document.getElementById('AnswersForm_sQuestion').value = sAnswersForm_Default_Question;
	};

	function fnAnswersForm_Validate(o) {
		try {
			$('#AnswersForm_sSubject').blur();
			$('#AnswersForm_sQuestion').blur();

			var sMsg = null;

			if (o.AnswersForm_iCatID.value == '') {
				sMsg = 'You must select a Category before submitting.';
			}
			else if (o.AnswersForm_sSubject.value == '') {
				sMsg = 'You must enter a Subject before submitting.';
			}
			else if (o.AnswersForm_sQuestion.value == '') {
				sMsg = 'You must enter a Question before submitting.';
			}

			if (sMsg != null) {
				$('#AnswersForm_sSubject').blur();
				$('#AnswersForm_sQuestion').blur();
				alert(sMsg);
			}
			else fnAJAXForm_Post(o, document.getElementById('AnswersForm_Response'));
		}
		catch (err) {
			alert(err);
		}
    }
</script>
<div id="AnswersForm_Response" class="AJAXForm_Response">&nbsp;</div>
<form method="post" action="/ajax-data-services/answers/" onsubmit="fnAnswersForm_Validate(this);return false;" id="AnswersForm" class="RPlus_Form center">
<input type="hidden" name="AnswersForm_sFormType" id="AnswersForm_sFormType" value="new-question" />
	<div class="Category">
        <label class="Required">Category:</label>
        <select id="AnswersForm_iCatID" name="AnswersForm_iCatID" class="Field SelectBox">
            <option value="">Select an Option</option>
        <%
            using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
			{
				using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
				{
					oCMD.Connection = oCN;
					oCMD.CommandText = "SELECT c.iCatID, c.sName FROM qa_category c ORDER BY sName";
					
					int iCatID = 0;
					string sSelected = null;

					if (Request.QueryString["iCatID"] != null) iCatID = Convert.ToInt32(clsRPlus.fnVal(Request.QueryString["iCatID"]));

					oCMD.CommandText = "SELECT c.iCatID, c.sName FROM qa_category c ORDER BY sName";
					using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
					{
						while (oDR.Read())
						{
							if (iCatID == Convert.ToInt32(clsRPlus.fnVal(oDR["iCatID"].ToString()))) sSelected = " selected=\"selected\"";
							else sSelected = "";
        %>
            <option value="<%=oDR["iCatID"].ToString()%>"<%=sSelected%>><%=oDR["sName"].ToString()%></option>
        <%
							}
					}
				}
            }
        %>
        </select>
    </div>
	<input type="text" maxlength="50" name="AnswersForm_sSubject" id="AnswersForm_sSubject" class="Field SubjectBox" onfocus="fnDynamicFormText_Toggle(this, sAnswersForm_Default_Subject);" onblur="fnDynamicFormText_Toggle(this, sAnswersForm_Default_Subject);" />
	<textarea name="AnswersForm_sQuestion" id="AnswersForm_sQuestion" class="Field CommentBox" rows="12" cols="50" onfocus="fnDynamicFormText_Toggle(this, sAnswersForm_Default_Question);" onblur="fnDynamicFormText_Toggle(this, sAnswersForm_Default_Question);"></textarea>
	<br />
	<input type="submit" name="btnSubmit" id="btnSubmit" value="Submit Question" />
</form>