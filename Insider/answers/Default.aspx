<%@ Page Title="Answers" Language="C#" MasterPageFile="~/templates/Answers.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AnswersHeader1" Runat="Server">
<title>R+ Answers</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AnswersPageHeader1" runat="server">
    R+ Answers
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AnswersBody1" Runat="Server">
		<h2>Unanswered Questions</h2>
        <%
		using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
		{
			using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
			{
				oCMD.Connection = oCN;
				oCMD.CommandText = "SELECT TOP 10 COUNT(a.iAID) As iCnt, q.iQID, q.sSubject, CAST(q.sQuestion As varchar(50)) As sQuestion, c.sName As sCategory"
								+ " FROM qa_question q LEFT JOIN qa_answers a ON q.iQID = a.iQID"
								+ " LEFT JOIN qa_category c ON c.iCatID = q.iCatID"
								+ " WHERE a.sAnswer IS NULL"
								+ " GROUP BY q.iQID, q.sSubject, CAST(q.sQuestion As varchar(50)), c.sName"
								+ " ORDER BY iCnt DESC";

				using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
				{
					if (oDR.HasRows)
					{
						while (oDR.Read())
						{
        %>
            <div class="Answer_ListItem" onclick="window.location=this.childNodes[1].href;">
                <a href="/answers/question/?iQID=<%=oDR["iQID"].ToString() %>"><%=oDR["sSubject"].ToString()%></a>
				<div class="small"><%=oDR["iCnt"].ToString()%> Answers&nbsp;&nbsp;&nbsp;&nbsp;Category: <%=oDR["sCategory"].ToString() %></div>
                <%=clsRPlus.fnMid(oDR["sQuestion"].ToString(), 0, 50) + "..."%>
            </div>
        <%
						}
					}
					else
					{
						%>
						No answers found.
						<%
					}
				}
        %>
		
		<h2>Most Answered Questions</h2>
        <%
				oCMD.CommandText = "SELECT TOP 10 COUNT(a.iAID) As iCnt, a.iQID, q.sSubject, CAST(q.sQuestion As varchar(50)) As sQuestion, c.sName As sCategory"
								+ " FROM qa_answers a LEFT JOIN qa_question q ON q.iQID = a.iQID"
								+ " LEFT JOIN qa_category c ON c.iCatID = q.iCatID"
								+ " WHERE q.sSubject IS NOT NULL"
								+ " GROUP BY a.iQID, q.sSubject, CAST(q.sQuestion As varchar(50)), c.sName"
								+ " ORDER BY iCnt DESC";

				using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
				{
					if (oDR.HasRows)
					{
						while (oDR.Read())
						{
        %>
            <div class="Answer_ListItem" onclick="window.location=this.childNodes[1].href;">
                <a href="/answers/question/?iQID=<%=oDR["iQID"].ToString() %>"><%=oDR["sSubject"].ToString()%></a>
				<div class="small"><%=oDR["iCnt"].ToString()%> Answers&nbsp;&nbsp;&nbsp;&nbsp;Category: <%=oDR["sCategory"].ToString() %></div>
                <%=clsRPlus.fnMid(oDR["sQuestion"].ToString(), 0, 50) + "..."%>
            </div>
        <%
						}
					}
					else
					{
						%>
						No answers found.
						<%
					}
				}
        %>

        <h2>Recently Answered Questions</h2>
		<%
				oCMD.CommandText = "SELECT DISTINCT TOP 10 q.iQID, q.sSubject, CAST(q.sQuestion As varchar(50)) As sQuestion, (SELECT TOP 1 dtCreated FROM qa_answers WHERE iQID = q.iQID ORDER BY dtCreated DESC) As dtLast, c.sName As sCategory"
							+ " FROM qa_question q"
							+ " LEFT JOIN qa_category c ON c.iCatID = q.iCatID"
							+ " WHERE (SELECT TOP 1 dtCreated FROM qa_answers WHERE iQID = q.iQID ORDER BY dtCreated DESC) IS NOT NULL"
							+ " ORDER BY dtLast DESC";
					
				using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
				{
					if (oDR.HasRows)
					{
						while (oDR.Read())
						{
        %>
            <div class="Answer_ListItem" onclick="window.location=IIF(this.childNodes[1].href==null,this.childNodes[0].href,this.childNodes[1].href);">
                <a href="question/?iQID=<%=oDR["iQID"].ToString() %>"><%=oDR["sSubject"].ToString()%></a>
				<div class="Date NoBefore"><%=oDR["dtLast"].ToString()%>&nbsp;&nbsp;&nbsp;&nbsp;Category: <%=oDR["sCategory"].ToString() %></div>
                <%=clsRPlus.fnMid(oDR["sQuestion"].ToString(), 0, 50) + "..." %>
            </div>
        <%
						}
					}
					else {
						%>
						No questions found.
						<%
					}
				}
			}
		}
        %>
        <customControls:ctlAnswers_SubmitQuestion ID="ctlAnswers_SubmitQuestion1" runat="server" />
</asp:Content>

