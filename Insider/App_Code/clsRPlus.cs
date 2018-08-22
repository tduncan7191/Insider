using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Class1
/// </summary>
public class clsRPlus
{
	public enum enumDBs { INSIDER, MAS_RDP, SALES, UPS };

	public static string sCN_INSIDER = "";
	public static string sCN_MAS_RDP = "";
	public static string sCN_SALES = "";
	public static string sCN_UPS = "";

	public static void Main()
	{
		//JMW: Add our connectiong strings specific to the server we are running on (live or dev)
		ConnectionStringSettings oCN = new ConnectionStringSettings();
		
		sCN_INSIDER = System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString;
		sCN_MAS_RDP = System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString;
		sCN_SALES = System.Configuration.ConfigurationManager.ConnectionStrings["CN_SALES"].ConnectionString;
		sCN_UPS = System.Configuration.ConfigurationManager.ConnectionStrings["CN_UPS"].ConnectionString;
	}

	public static int fnVal(string s)
	{
		try
		{
			int l = Convert.ToInt32(s);
			return l;
		}
		catch (Exception ex)
		{
			return 0;
		}
	}

	public static string fnHTML_ReplaceLineBreaks(string blockOfText)
	{
		return blockOfText.Replace("\r\n", "<br />").Replace("\n", "<br />").Replace("\r", "<br />");
	}

	public static SqlConnection fnOpenCN(enumDBs oDB) {
		SqlConnection oCN = new SqlConnection() ;
		
		//Replace "Provider" from connection string because it is not supported in ADO.NET connections.
		switch (oDB.ToString())
		{
			case "INSIDER":
				oCN.ConnectionString = sCN_INSIDER;
				break;
			case "MAS_RDP":
				oCN.ConnectionString = sCN_MAS_RDP;
				break;
			case "SALES":
				oCN.ConnectionString = sCN_SALES;
				break;
			case "UPS":
				oCN.ConnectionString = sCN_UPS;
				break;
		}
		oCN.Open();
		
		return oCN;
	}

	//OPEN SQl Client connection for use with ADO.NET.
	public static void fnKillCN(System.Data.SqlClient.SqlConnection oCN_SQL)
	{
		if (oCN_SQL.State == System.Data.ConnectionState.Open) oCN_SQL.Close();
		oCN_SQL.Dispose();
		oCN_SQL = null;
	}

	public static void fnKillRS(ADODB.Recordset oRS)
	{
		if (oRS.State == 1) oRS.Close();
		oRS = null;
	}

	public static Boolean fnSendMail(string sSubject, string sBody, string sTo, string sCC = null, string sBCC = null)
	{
		try
		{
			System.Net.Mail.MailMessage oMsg = new System.Net.Mail.MailMessage();
			
			oMsg.To.Add(sTo);
			if (sCC != null) oMsg.CC.Add(sCC);
			if (sBCC != null) oMsg.CC.Add(sBCC);
			
			oMsg.Subject = "Test Subject";
			oMsg.Body = "Test message body.";

			//JMW: Credentials are set in web.config for each FROM address.
			oMsg.From = new System.Net.Mail.MailAddress("jwillis@redemptionplus.com");
			System.Net.Mail.SmtpClient oSMTP = new System.Net.Mail.SmtpClient("smtp.gmail.com");
			oSMTP.Send(oMsg);

			return true;
		}
		catch (Exception ex)
		{
			//Just exit returning false on error
			return false;
		}
	}

    public static string fnMid(string s, int iStart, int iEnd)
    {
        if (iStart >= s.Length) return "";
        else if (iEnd > s.Length) iEnd = s.Length;
        
        return s.Substring(iStart, iEnd);
    }

	public static string fnGetURL_Clean()
	{
		return HttpContext.Current.Request.Url.AbsolutePath.ToLower().Replace("default.aspx", "");
	}

	public static string fnGetURL_CleanQuery()
	{
		if (HttpContext.Current.Request.Url.Query != "")
		{
			return HttpContext.Current.Request.Url.AbsolutePath.ToLower().Replace("default.aspx", "") + "?" + HttpContext.Current.Request.Url.Query;
		}
		else return HttpContext.Current.Request.Url.AbsolutePath.ToLower().Replace("default.aspx", "");
	}

	public static string fnGetURL_Full()
	{
		return HttpContext.Current.Request.Url.AbsolutePath + "?" + HttpContext.Current.Request.Url.Query;
	}

	public static string fnShowPaging(int iTotal, int iPage, int iPerPage, string sQuery)
	{
		string sResult = null;

		int iFirstRecord = 1;
		int iLastRecord = iPerPage;
		int iLastPage = 0;
		int iNextPage = 0;
		int iStartPage = 1;

		//Sets the  maximum number of page links to display, starting at 0
		int iMaxDisplay = 4;

		if (iTotal == 0) iFirstRecord = 0;
		else iFirstRecord = (iPerPage * iPage) - iPerPage + 1;

		iLastRecord = iFirstRecord + iPerPage - 1;
		if (iLastRecord > iTotal) iLastRecord = iTotal;

		iLastPage = Convert.ToInt32(Math.Round((iTotal / Convert.ToDouble(iPerPage)) + .4999, 0));
		//sResult = iLastPage.ToString();
		//sResult = sResult + ":" + ((iTotal / iPerPage) + .4999).ToString();
		iNextPage = iPage + 1;
		if (iLastPage < iNextPage) iNextPage = iLastPage;

		if (iPage == 0) iPage = 1;
		int iEndPage = 1;

		if (iLastPage > 1)
		{
			if (iPage == 1 && iLastPage < iMaxDisplay + 1)
			{
				iStartPage = 1;
				iEndPage = iLastPage;
			}
			else if (iPage < 3 && iLastPage <= 5)
			{
				iStartPage = 1;
				iEndPage = iLastPage;
			}
			else if (iPage > Math.Round((iLastPage - iMaxDisplay / 2) + .4999, 0))
			{
				iStartPage = iLastPage - iMaxDisplay;
				if (iStartPage < 1) iStartPage = 1;
				iEndPage = iLastPage;
			}
			else if (iPage == iLastPage)
			{
				iStartPage = iPage - iMaxDisplay;
				if (iStartPage < 1) iStartPage = 1;
				iEndPage = iLastPage;
			}
			else
			{
				iStartPage = iPage - Convert.ToInt32(Math.Round((iMaxDisplay / 2) + .4999, 0));
				if (iStartPage < 1) iStartPage = 1;
				iEndPage = iStartPage + iMaxDisplay;
				if (iEndPage > iLastPage) iEndPage = iLastPage;
			}
		}
		
		sResult = sResult + "<div class=\"pagination\">";
		sResult = sResult + "<p>Viewing <strong>" + iFirstRecord.ToString() + " - " + iLastRecord.ToString() + "</strong> of <strong>" + iTotal.ToString() + "</strong></p>";

		if (iStartPage != iEndPage)
		{
			if (iPage > 1)
			{
				sResult = sResult + "<a href=\"?iPageNum=" + (iPage - 1).ToString() + "&" + sQuery + "\">prev</a>";
			}

			sResult = sResult + "<!-- PAGES BY NUMBER -->";
			sResult = sResult + "<ul>";

			if ((iPage - (iMaxDisplay / 2)) > 1 && iLastPage > iMaxDisplay)
			{
				sResult = sResult + "<li><a href=\"?iPageNum=1&" + sQuery + "\">1...</a>&nbsp;&nbsp;</li>";
			}

			if (iEndPage > 1)
			{
				int iCnt = iStartPage;

				while (iCnt <= iEndPage)
				{
					sResult = sResult + "<li><a href=\"?iPageNum=" + iCnt.ToString() + "&" + sQuery + "\"";
					if (iCnt == iPage)
					{
						sResult = sResult + " class=\"active\"";
					}
					sResult = sResult + ">" + iCnt.ToString() + "</a>&nbsp;&nbsp;</li>";
					iCnt = iCnt + 1;
				}
			}

			if (iLastPage - iPage > (iMaxDisplay / 2) && iLastPage > iMaxDisplay)
			{
				sResult = sResult + "<li><a href=\"?iPageNum=" + iLastPage.ToString() + "&" + sQuery + "\">..." + iLastPage.ToString() + "</a></li>";
			}
			sResult = sResult + "</ul>";
			sResult = sResult + "<!-- END PAGES BY NUMBER -->";

			if (iPage < iLastPage)
			{
				sResult = sResult + "<a href=\"?iPageNum=" + (iPage + 1).ToString() + "&" + sQuery + "\">next</a>";
			}
		}
		sResult = sResult + "</div>";

		return sResult;
	}
}