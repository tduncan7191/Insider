<%@ Page Title="Answers" Language="C#" MasterPageFile="~/templates/Answers.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AnswersHeader1" Runat="Server">
<title><%=sCategory%> Answers</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AnswersPageHeader1" runat="server">
    R+ Answers
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AnswersBody1" Runat="Server">
		<%
		if (sCategory != null)
		{
		%>
        <h2><%=sCategory%> Questions</h2>
			<%
			int iCatID = 0;
		 
			if (Request.QueryString["iCatID"] != null) iCatID = clsRPlus.fnVal(Request.QueryString["iCatID"]);
			
			using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
			{
				using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
				{
					oCMD.Connection = oCN;
					oCMD.CommandType = System.Data.CommandType.StoredProcedure;
					oCMD.CommandText = "prc_PagedResults";
					oCMD.Parameters.AddWithValue("@sPK", "iQID");
					oCMD.Parameters.AddWithValue("@sFields", "iQID, sSubject, dtCreated");
					oCMD.Parameters.AddWithValue("@sTables", "qa_question");
					oCMD.Parameters.AddWithValue("@sWhere", "iCatID = " + iCatID.ToString());
					oCMD.Parameters.AddWithValue("@sOrderBy", DBNull.Value);
					oCMD.Parameters.AddWithValue("@nPageNum", iPageNum);
					oCMD.Parameters.AddWithValue("@nMaxRecords", iMaxRecords);

					using (System.Data.SqlClient.SqlDataReader oReader = oCMD.ExecuteReader())
					{
						int iRecords = 0;
			
						oReader.Read();
						if (oReader.HasRows == true) iRecords = clsRPlus.fnVal(oReader["nRecCnt"].ToString());
						oReader.NextResult();

						if (oReader.HasRows == true) {
							string sHTML_Paging = null;
							if (iRecords != 0) sHTML_Paging = clsRPlus.fnShowPaging(iRecords, iPageNum, iMaxRecords, "iCatID=" + iCatID.ToString());

							Response.Write(sHTML_Paging);

							while (oReader.Read())
							{
					%>
						<div class="Answer_ListItem" onclick="window.location=this.childNodes[1].href;">
							<a href="/answers/question/?iQID=<%=oReader["iQID"] %>"><%=oReader["sSubject"] %></a>
							<div class="Date"><%=oReader["dtCreated"] %></div>
							<%=clsRPlus.fnMid(oReader["sSubject"].ToString(), 0, 50) + "..."%>
						</div>
					<%
							}

							Response.Write(sHTML_Paging);
						}
						else {
						%>
						<h3 class="center">No questions exist in the selected category.</h3>
						<%
						}
					}
				}
			}
		}
		else {
			%>
        <h2>Category Not Found</h2>
        <div class="center">No questions were found.</div>
        <%
        }
        %>
        <customControls:ctlAnswers_SubmitQuestion ID="ctlAnswers_SubmitQuestion1" runat="server" />
</asp:Content>
