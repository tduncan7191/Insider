<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ajax_data_services_answers_Default" validateRequest="false" %>
<%
	/* THIS PAGE WILL AWAYS RETURN EMPTY STRING ("") ON ERROR */
	try
	{
		using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
		{
			using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
			{
				if (Request.Form["AnswersForm_sFormType"] == "new-question")
				{
					string sCMD = "INSERT INTO qa_question (iContactID, sSubject, sQuestion, iCatID) VALUES(";
					sCMD = sCMD + "@iContactID, @sSubject, @sQuestion, @iCatID)";
			
					string sQID = "";
					oCMD.Connection = oCN;
					oCMD.CommandText = "INSERT INTO qa_question (iContactID, sSubject, sQuestion, iCatID)"
										+ " VALUES(@iContactID, @sSubject, @sQuestion, @iCatID)";
					oCMD.Parameters.AddWithValue("@iContactID", Request.Cookies["contactid"].Value);
					oCMD.Parameters.AddWithValue("@sSubject", Request.Form["AnswersForm_sSubject"].ToString());
					oCMD.Parameters.AddWithValue("@sQuestion", clsRPlus.fnHTML_ReplaceLineBreaks(Request.Form["AnswersForm_sQuestion"]));
					oCMD.Parameters.AddWithValue("@iCatID", Request.Form["AnswersForm_iCatID"]);
					oCMD.ExecuteNonQuery();

					oCMD.CommandText = "SELECT @@IDENTITY As iQID";
			
					sQID = Convert.ToInt32(oCMD.ExecuteScalar()).ToString();
			%>
			<div class="center">
			<h3>Success! Your question has been saved.</h3>
			<br />
			To view the new question's page <a href="/answers/question/?iqid=<%=sQID%>">click here</a>.
			<br /><br />
			To submit another question <a href="" onclick="fnAJAXForm_ShowForm(true,document.forms['AnswersForm'],document.getElementById('AnswersForm_Response'));return false;">click here</a>.
			</div>
			<%
				}
				else if (Request.Form["AnswersForm_sFormType"] == "new-answer")
				{
					oCMD.Connection = oCN;
					oCMD.CommandText = "INSERT INTO qa_answers (iQID, iContactID, sAnswer)"
										+ " VALUES(@iQID, @iContactID, @sAnswer)";
					oCMD.Parameters.Add("@iQID", Request.Form["AnswersForm_iQID"]);
					oCMD.Parameters.Add("@iContactID", Request.Cookies["contactid"].Value);
					oCMD.Parameters.AddWithValue("@sAnswer", clsRPlus.fnHTML_ReplaceLineBreaks(Request.Form["AnswersForm_sAnswer"]));
					oCMD.ExecuteNonQuery();
					%>
					<div class="center">
						<h3>Success! Your answer has been saved.</h3>
						<p><a href="" onclick="window.location=window.location;return false;">Refresh this page</a> to view other new answers (if any).</p>
					</div>
					<%
				}
			}
		}
	}
	catch (Exception ex) { throw ex; }
%>