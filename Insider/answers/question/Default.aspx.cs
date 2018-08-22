using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class answers_category_Default : System.Web.UI.Page
{
	public int iQID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
		if (Request.QueryString["iQID"] != null) iQID = Convert.ToInt32(clsRPlus.fnVal(Request.QueryString["iQID"]));

		if (iQID > 0)
		{
			using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
			{
				using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
				{
					oCMD.Connection = oCN;
					oCMD.CommandText = "SELECT (ISNULL(c.firstName, '{First}') + ' ' + ISNULL(c.LastName, '{Last}')) As sAuthor, sSubject, sQuestion, dtCreated"
										+ " FROM qa_question q"
										+ " LEFT JOIN Contact c ON c.contactid = q.iContactID"
										+ " WHERE iQID = @iQID";
					oCMD.Parameters.AddWithValue("@iQID", iQID.ToString());

					using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
					{
						if (oDR.HasRows)
						{
							while (oDR.Read())
							{
								this.Page.Title = "Question: " + oDR["sSubject"].ToString();
								this.Question_Title.InnerHtml = "Question: " + oDR["sSubject"].ToString();
								this.Question_Author.InnerHtml = oDR["sAuthor"].ToString() + " asked:";
								this.Question_Text.InnerHtml = oDR["sQuestion"].ToString();
								this.Question_Date.InnerHtml = oDR["dtCreated"].ToString();
							}
						}
						else
						{
							this.Question_Title.Visible = false;
							this.Question_Author.Visible = false;
							this.Question_Text.InnerHtml = "<h1>Sorry, the question you are looking for no longer exists.<h1>";
							this.Question_Date.Visible = false;
							this.ctlAnswers_SubmitAnswer1.Visible = false;
						}
					}

					oCMD.CommandText = "SELECT (ISNULL(c.firstName, '{First}') + ' ' + ISNULL(c.LastName, '{Last}')) As sAuthor, sAnswer, dtCreated"
										+ " FROM qa_answers q"
										+ " LEFT JOIN Contact c ON c.contactid = q.iContactID"
										+ " WHERE iQID = @iQID"
										+ " ORDER BY dtCreated";

					string sAnswers = "";

					using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
					{
						if (oDR.HasRows)
						{
							while (oDR.Read())
							{
								sAnswers = sAnswers + "<div class=\"Answer\" class=\"Answer\">";
								sAnswers = sAnswers + "<div class=\"Author\">" + oDR["sAuthor"].ToString() + " said:</div>";
								sAnswers = sAnswers + "<div class=\"Text\">" + oDR["sAnswer"].ToString();
								sAnswers = sAnswers + "<div class=\"Date\">" + oDR["dtCreated"].ToString() + "</div>";
								sAnswers = sAnswers + "</div>";
								sAnswers = sAnswers + "</div>";
							}
							this.Answers_Server.InnerHtml = sAnswers;
						}
						else this.Answers_Server.InnerHtml = "No answers found.";
					}
				}
			}
		}
    }
}