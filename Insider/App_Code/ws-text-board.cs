using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using RP_WebClasses;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://insider.redemptionplus.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {
	public string sAppPath = System.Web.HttpContext.Current.Server.MapPath("/");
	public string sFileDir = System.Web.HttpContext.Current.Server.MapPath("/files/");

	/* SaveText Method */
	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveText(string sBoard, string iContactID, string sText) {
		bool bSuccess = false;

		using (SqlConnection oCN = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
		{
			oCN.Open();
			using (SqlCommand oCMD = new SqlCommand())
			{
				oCMD.Connection = oCN;
				oCMD.CommandText = "INSERT INTO TextBoard (sBoard, sText, sSource, iContactID)"
									+ " VALUES (@sBoard, @sText, @sSource, @iContactID)";
				oCMD.Parameters.AddWithValue("@sBoard", sBoard);
				oCMD.Parameters.AddWithValue("@sText", sText);
				oCMD.Parameters.AddWithValue("@sSource", "Insider");
				oCMD.Parameters.AddWithValue("@iContactID", iContactID);

				int iRes = oCMD.ExecuteNonQuery();

				if (iRes > 0) bSuccess = true;
			}
		}

		return "{\"bSuccess\":\"" + bSuccess.ToString() + "\"}";
    }
	
	/* LoadText Method and Objects */
	public class LoadResult
	{
		public string sText { get; set; }
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadText(string sType)
	{
		List<LoadResult> oResults = new List<LoadResult>();

		using (SqlConnection oCN = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
		{
			oCN.Open();
			using (SqlCommand oCMD = new SqlCommand())
			{
				oCMD.Connection = oCN;
				oCMD.CommandText = "SELECT sText"
									+ " FROM TextBoard"
									+ " WHERE sBoard = @sBoard"
									+ " ORDER BY dtCreated ASC";
				oCMD.Parameters.AddWithValue("@sBoard", sType);

				using (SqlDataReader oDR = oCMD.ExecuteReader())
				{
					while (oDR.Read())
					{
						LoadResult oR = new LoadResult();
						oR.sText = oDR["sText"].ToString();
						oResults.Add(oR);
					}
				}
			}
		}

		return new JavaScriptSerializer().Serialize(oResults);
	}
}
