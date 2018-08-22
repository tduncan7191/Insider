using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using RP_WebClasses;

[WebService(Namespace = "http://insider.redemptionplus.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class wsSKUP2 : System.Web.Services.WebService {
	
	public string sInvalidSession = "Invalid Session";

	/* ADU Adjustment DATA AND METHOD */
	public class ADUAdjData
	{
		public string iID;
		public string sItemCode;
		public string dtEffective;
		public string dtEffective_Init;
		public string sWarehouse;
		public string sWarehouse_Init;
		public string iUnits;
		public string iUnits_Init;
		public string sNotes;
		public string sNotes_Init;
		public string bDelete;
	}
	public class ADUAdjResult
	{
		public bool bSuccess { get; set; }
		public string sMessage { get; set; }
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadADUAdj(string sSKU, string sSessionID)
	{
		try
		{
			//don't allow iOS 6 (or any browsers) to cache responses
			System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			
			if (!ValidateSession(sSessionID)) return sInvalidSession;

			//Convert JSON string to object list
			List<ADUAdjData> oADUAdjList = new List<ADUAdjData>();
			ADUAdjData oADUAdj = null;

			using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_RP_DW"].ConnectionString))
			{
				oCN.Open();
				using (SqlCommand oCMD = new SqlCommand("", oCN))
				{
					oCMD.CommandText = "SELECT iID, sItemCode, sWarehouseCode, CONVERT(varchar(10), dtAdj, 101) As dtAdj, iUnits, sNotes"
						+ " FROM Inventory_ADU_Adj"
						+ " WHERE sItemCode = @sSKU"
						+ " ORDER BY Inventory_ADU_Adj.dtAdj";
					oCMD.Parameters.AddWithValue("@sSKU", sSKU);

					using (SqlDataReader oDR = oCMD.ExecuteReader())
					{
						while (oDR.Read())
						{
							oADUAdj = new ADUAdjData();
							oADUAdj.iID = oDR["iID"].ToString();
							oADUAdj.sItemCode = oDR["sItemCode"].ToString();
							oADUAdj.dtEffective = oDR["dtAdj"].ToString();
							oADUAdj.dtEffective_Init = oADUAdj.dtEffective;
							oADUAdj.sWarehouse = oDR["sWarehouseCode"].ToString();
							oADUAdj.sWarehouse_Init = oADUAdj.sWarehouse;
							oADUAdj.iUnits = oDR["iUnits"].ToString();
							oADUAdj.iUnits_Init = oADUAdj.iUnits;
							oADUAdj.sNotes = oDR["sNotes"].ToString();
							oADUAdj.sNotes_Init = oADUAdj.sNotes;
							oADUAdjList.Add(oADUAdj);
						}
					}
				}
			}

			return new JavaScriptSerializer().Serialize(oADUAdjList);
		}
		catch (Exception ex)
		{
			return "WS Error: " + ex.Message.ToString();
		}
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string SaveADUAdj(List<ADUAdjData> jsAdj, string sSessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		if (!ValidateSession(sSessionID)) return sInvalidSession;

		ADUAdjResult oResult = new ADUAdjResult();
		oResult.bSuccess = false;

		//validate user can save
		string[] arrUsers = { "229", "208", "213", "260", "275", "274", "278" };
		if (!arrUsers.Contains(System.Web.HttpContext.Current.Request.Cookies["contactid"].Value)) oResult.sMessage = "Permission denied.";
		else
		{
			try
			{
				//Convert JSON string to object list
				using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_RP_DW"].ConnectionString))
				{
					oCN.Open();
					using (SqlCommand oCMD = new SqlCommand())
					{
						oCMD.Connection = oCN;
						oResult.sMessage = jsAdj.Count.ToString();

						foreach (ADUAdjData oADUAdj in jsAdj)
						{
							oCMD.CommandText = "";
							oCMD.Parameters.Clear();

							//delete existing if selected to be deleteed
							if ((oADUAdj.bDelete ?? "0") == "1" && oADUAdj.iID.Length > 0)
							{
								oCMD.CommandText = "DELETE FROM Inventory_ADU_Adj WHERE iID = @iID";
								oCMD.Parameters.AddWithValue("@iID", oADUAdj.iID);
							}
							//update existing if changes were made
							else if (oADUAdj.iID.Length > 0 && (oADUAdj.iUnits != oADUAdj.iUnits_Init || oADUAdj.sWarehouse != oADUAdj.sWarehouse_Init || oADUAdj.dtEffective != oADUAdj.dtEffective_Init))
							{
								oCMD.CommandText = "UPDATE Inventory_ADU_Adj"
									+ " SET sItemCode = @sItemCode"
									+ ", sWarehouseCode = @sWarehouse"
									+ ", dtAdj = @dtEffective"
									+ ", iUnits = @iUnits"
									+ ", sNotes = @sNotes"
									+ " WHERE iID = @iID";
								oCMD.Parameters.AddWithValue("@iID", oADUAdj.iID);
								oCMD.Parameters.AddWithValue("@sItemCode", oADUAdj.sItemCode);
								oCMD.Parameters.AddWithValue("@sWarehouse", oADUAdj.sWarehouse);
								oCMD.Parameters.AddWithValue("@dtEffective", oADUAdj.dtEffective);
								oCMD.Parameters.AddWithValue("@iUnits", oADUAdj.iUnits);
								oCMD.Parameters.AddWithValue("@sNotes", oADUAdj.sNotes);
							}
							//insert new
							else if ((oADUAdj.bDelete ?? "0") == "0" && oADUAdj.iID.Length == 0)
							{
								oCMD.CommandText = "INSERT INTO Inventory_ADU_Adj (sItemCode, sWarehouseCode, dtAdj, iUnits, sNotes)"
									+ " VALUES (@sItemCode, @sWarehouse, @dtEffective, @iUnits, @sNotes)";
								oCMD.Parameters.AddWithValue("@sItemCode", oADUAdj.sItemCode);
								oCMD.Parameters.AddWithValue("@sWarehouse", oADUAdj.sWarehouse);
								oCMD.Parameters.AddWithValue("@dtEffective", oADUAdj.dtEffective);
								oCMD.Parameters.AddWithValue("@iUnits", oADUAdj.iUnits);
								oCMD.Parameters.AddWithValue("@sNotes", oADUAdj.sNotes);
							}

							//only execute if we have command text
							if (oCMD.CommandText.Length > 0) oResult.sMessage = oCMD.ExecuteNonQuery().ToString() + " records affected.";
							oResult.bSuccess = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				oResult.bSuccess = false;
				oResult.sMessage = "WS Error: " + ex.Message.ToString();
			}
		}

		//return proper status
		return new JavaScriptSerializer().Serialize(oResult);
	}

	/* Usage Entries DATA AND METHOD */
	public class UsageData
	{
		public string iID;
		public string sItemCode;
		public string dtTran;
		public string dtTran_Init;
		public string dtRequired;
		public string dtRequired_Init;
		public string sType;
		public string sType_Init;
		public string iUnits;
		public string iUnits_Init;
		public string sNotes;
		public string sNotes_Init;
		public string bDelete;
	}
	public class UsageResult
	{
		public bool bSuccess { get; set; }
		public string sMessage { get; set; }
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadUsage(string sSKU, string sSessionID)
	{
		try
		{
			//don't allow iOS 6 (or any browsers) to cache responses
			System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

			if (!ValidateSession(sSessionID)) return sInvalidSession;

			//Convert JSON string to object list
			List<UsageData> oUsageList = new List<UsageData>();
			UsageData oUsage = null;

			using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
			{
				oCN.Open();
				using (SqlCommand oCMD = new SqlCommand("", oCN))
				{
					oCMD.CommandText = "SELECT iID, sItemCode, sType, CONVERT(varchar(10), dtTran, 101) As dtTran, CONVERT(varchar(10), dtRequired, 101) As dtRequired, iUnits, sNotes"
						+ " FROM SKUP2_FutureUsage"
						+ " WHERE sItemCode = @sSKU"
						+ " ORDER BY SKUP2_FutureUsage.dtTran";
					oCMD.Parameters.AddWithValue("@sSKU", sSKU);

					using (SqlDataReader oDR = oCMD.ExecuteReader())
					{
						while (oDR.Read())
						{
							oUsage = new UsageData();
							oUsage.iID = oDR["iID"].ToString();
							oUsage.sItemCode = oDR["sItemCode"].ToString();
							oUsage.dtTran = oDR["dtTran"].ToString();
							oUsage.dtTran_Init = oUsage.dtTran;
							oUsage.dtRequired = oDR["dtRequired"].ToString();
							oUsage.dtRequired_Init = oUsage.dtRequired;
							oUsage.sType = oDR["sType"].ToString();
							oUsage.sType_Init = oUsage.sType;
							oUsage.iUnits = oDR["iUnits"].ToString();
							oUsage.iUnits_Init = oUsage.iUnits;
							oUsage.sNotes = oDR["sNotes"].ToString();
							oUsage.sNotes_Init = oUsage.sNotes;
							oUsageList.Add(oUsage);
						}
					}
				}
			}

			return new JavaScriptSerializer().Serialize(oUsageList);
		}
		catch (Exception ex)
		{
			return "WS Error: " + ex.Message.ToString();
		}
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string SaveUsage(List<UsageData> jsUsage, string sSessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		if (!ValidateSession(sSessionID)) return sInvalidSession;

		UsageResult oResult = new UsageResult();
		oResult.bSuccess = false;

		try
		{
			//Convert JSON string to object list
			using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
			{
				oCN.Open();
				using (SqlCommand oCMD = new SqlCommand())
				{
					oCMD.Connection = oCN;
					oResult.sMessage = jsUsage.Count.ToString();

					foreach (UsageData oUsage in jsUsage)
					{
						oCMD.CommandText = "";
						oCMD.Parameters.Clear();

						//delete existing if selected to be deleteed
						if ((oUsage.bDelete ?? "0") == "1" && oUsage.iID.Length > 0)
						{
							oCMD.CommandText = "DELETE FROM SKUP2_FutureUsage WHERE iID = @iID";
							oCMD.Parameters.AddWithValue("@iID", oUsage.iID);
						}
						//update existing if changes were made
						else if (oUsage.iID.Length > 0 && (oUsage.iUnits != oUsage.iUnits_Init || oUsage.sType != oUsage.sType_Init || oUsage.dtTran != oUsage.dtTran_Init || oUsage.dtRequired != oUsage.dtRequired_Init))
						{
							oCMD.CommandText = "UPDATE SKUP2_FutureUsage"
								+ " SET sItemCode = @sItemCode"
								+ ", sType = @sType"
								+ ", dtTran = @dtTran"
								+ ", dtRequired = @dtRequired"
								+ ", iUnits = @iUnits"
								+ ", sNotes = @sNotes"
								+ " WHERE iID = @iID";
							oCMD.Parameters.AddWithValue("@iID", oUsage.iID);
							oCMD.Parameters.AddWithValue("@sItemCode", oUsage.sItemCode);
							oCMD.Parameters.AddWithValue("@sType", oUsage.sType);
							oCMD.Parameters.AddWithValue("@dtTran", oUsage.dtTran);
							oCMD.Parameters.AddWithValue("@dtRequired", oUsage.dtRequired);
							oCMD.Parameters.AddWithValue("@iUnits", oUsage.iUnits);
							oCMD.Parameters.AddWithValue("@sNotes", oUsage.sNotes);
						}
						//insert new
						else if ((oUsage.bDelete ?? "0") == "0" && oUsage.iID.Length == 0)
						{
							oCMD.CommandText = "INSERT INTO SKUP2_FutureUsage (sItemCode, sType, dtTran, dtRequired, iUnits, sNotes)"
								+ " VALUES (@sItemCode, @sType, @dtTran, @dtRequired, @iUnits, @sNotes)";
							oCMD.Parameters.AddWithValue("@sItemCode", oUsage.sItemCode);
							oCMD.Parameters.AddWithValue("@sType", oUsage.sType);
							oCMD.Parameters.AddWithValue("@dtTran", oUsage.dtTran);
							oCMD.Parameters.AddWithValue("@dtRequired", oUsage.dtRequired);
							oCMD.Parameters.AddWithValue("@iUnits", oUsage.iUnits);
							oCMD.Parameters.AddWithValue("@sNotes", oUsage.sNotes);
						}

						//only execute if we have command text
						if (oCMD.CommandText.Length > 0) oResult.sMessage = oCMD.ExecuteNonQuery().ToString() + " records affected.";
						oResult.bSuccess = true;
					}
				}
			}
		}
		catch (Exception ex)
		{
			oResult.bSuccess = false;
			oResult.sMessage = "WS Error: " + ex.Message.ToString();
		}

		//return proper status
		return new JavaScriptSerializer().Serialize(oResult);
	}

	/* PRODUCT CLASS CONFIG DATA AND METHOD */
	public class AutoClassConfigData
	{
		public string sClass { get; set; }
		public string sMinWeeks { get; set; }
		public string sMinWeeks_Init { get; set; }
		public string sReorderWeeks { get; set; }
		public string sReorderWeeks_Init { get; set; }
		public string sAutoRun { get; set; }
		public string sAutoRun_Init { get; set; }
		public string sExists { get; set; }
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string SaveAutoClassConfig(string jsData, string sSessionID)
	{
		try
		{
			//don't allow iOS 6 (or any browsers) to cache responses
			System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

			if (!ValidateSession(sSessionID)) return sInvalidSession;
			
			//Convert JSON string to object list
			List<AutoClassConfigData> oClasses = new JavaScriptSerializer().Deserialize<List<AutoClassConfigData>>(jsData);
			List<AutoClassConfigData> oClassesNew = new List<AutoClassConfigData>();
			using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
			{
				oCN.Open();
				foreach (AutoClassConfigData oClass in oClasses)
				{
					//normalize possible null boolean value to '0'
					if (oClass.sAutoRun == null) oClass.sAutoRun = "0";

					//add new class config values
					if (oClass.sExists == "0")
					{
						//update new class parameters before inserting
						oClass.sClass = oClass.sClass.Replace("*", "");
						oClass.sExists = "1";

						using (SqlCommand oCMD = new SqlCommand("", oCN))
						{
							oCMD.CommandText = "INSERT INTO SKUP2_Auto_ClassConfig (sCategory1, iMinWeeksLeft, iWeeksToOrder, bAutoRun)"
												+ " VALUES (@sClass, @sMinWeeksLeft, @sWeeksToOrder, @sAutoRun)";
							oCMD.Parameters.AddWithValue("@sClass", oClass.sClass);
							oCMD.Parameters.AddWithValue("@sMinWeeksLeft", oClass.sMinWeeks);
							oCMD.Parameters.AddWithValue("@sWeeksToOrder", oClass.sReorderWeeks);
							oCMD.Parameters.AddWithValue("@sAutoRun", oClass.sAutoRun);
							oCMD.ExecuteNonQuery();
						}
					}
					//update existing class config values if changed
					else if (oClass.sReorderWeeks != oClass.sReorderWeeks_Init
						|| oClass.sMinWeeks != oClass.sMinWeeks_Init
						|| oClass.sAutoRun != oClass.sAutoRun_Init)
					{
						using (SqlCommand oCMD = new SqlCommand("", oCN))
						{
							oCMD.CommandText = "UPDATE SKUP2_Auto_ClassConfig SET iMinWeeksLeft = @sMinWeeksLeft, iWeeksToOrder = @sWeeksToOrder, bAutoRun = @sAutoRun"
												+ " WHERE sCATEGORY1 = @sClass";
							oCMD.Parameters.AddWithValue("@sClass", oClass.sClass);
							oCMD.Parameters.AddWithValue("@sMinWeeksLeft", oClass.sMinWeeks);
							oCMD.Parameters.AddWithValue("@sWeeksToOrder", oClass.sReorderWeeks);
							oCMD.Parameters.AddWithValue("@sAutoRun", oClass.sAutoRun);
							oCMD.ExecuteNonQuery();
						}
					}

					//Update response values accordingly
					oClass.sMinWeeks_Init = oClass.sMinWeeks;
					oClass.sReorderWeeks_Init = oClass.sReorderWeeks;
					oClass.sAutoRun_Init = oClass.sAutoRun;

					//add class to response
					oClassesNew.Add(oClass);
				}
			}

			return new JavaScriptSerializer().Serialize(oClassesNew);
		}
		catch (Exception ex)
		{
			return "WS Error: " + ex.Message;
		}
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadAutoClassConfig(string sSessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		if (!ValidateSession(sSessionID)) return sInvalidSession;

		List<AutoClassConfigData> oDataRows = new List<AutoClassConfigData>();
		AutoClassConfigData oData = new AutoClassConfigData();

		using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
		{
			oCN.Open();
			using (SqlCommand oCMD = new SqlCommand("", oCN))
			{
				oCMD.CommandText = "SELECT sCATEGORY1, iMinWeeksLeft, iWeeksToOrder, bAutoRun, bExists"
									+ " FROM vw_SKUP2_Auto_ClassConfig_Controller ORDER BY sCATEGORY1";

				using (SqlDataReader oDR = oCMD.ExecuteReader())
				{
					while (oDR.Read())
					{
						oData = new AutoClassConfigData();
						oData.sClass = oDR["sCATEGORY1"].ToString();
						oData.sMinWeeks = oDR["iMinWeeksLeft"].ToString();
						oData.sReorderWeeks = oDR["iWeeksToOrder"].ToString();
						oData.sAutoRun = oDR["bAutoRun"].ToString();
						oData.sExists = oDR["bExists"].ToString();

						oData.sReorderWeeks_Init = oData.sReorderWeeks;
						oData.sMinWeeks_Init = oData.sMinWeeks;
						oData.sAutoRun_Init = oData.sAutoRun;

						oDataRows.Add(oData);
					}
				}
			}
		}

		return new JavaScriptSerializer().Serialize(oDataRows);
	}

	/* SKUDetails CLASS AND METHOD */
	public class SKUHeaderData
	{
		public string sSKU { get; set; }
		public string sName { get; set; }
		public string sActive { get; set; }
		public string sReorder { get; set; }
		public string sCategory1 { get; set; }
		public string sCategory4 { get; set; }
		public string sFirstDateSold { get; set; }
		public string sAvgCost { get; set; }
		public string sPrice_Silver { get; set; }
		public string sPrice_Gold { get; set; }
		public string sPrice_Diamond { get; set; }
		public string sVendor { get; set; }
		public string sLeadTime { get; set; }
		public string sLastLeadTime { get; set; }
		public string sClass { get; set; }
		public string sMOQ { get; set; }
		public string sADU { get; set; }
		public string s000QOH { get; set; }
		public string s004QOH { get; set; }
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadSKUHeader(string sSKU, string sSessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		if (!ValidateSession(sSessionID)) return sInvalidSession;

		SKUHeaderData oData = new SKUHeaderData();
		
		using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
		{
			oCN.Open();
			using (SqlCommand oCMD = new SqlCommand("",oCN)) {
				oCMD.CommandText = "SELECT CI.ITEMCODE As sSKU, CI.ITEMCODEDESC As sName, UDF_IS_ACTIVE As sActive, UDF_REORDER As sReorder, CATEGORY1 As sCategory1, CATEGORY4 As sCategory4"
									+ ", CONVERT(varchar(10), HSF.First_Date_Sold, 101) As sFirstDateSold, AVERAGEUNITCOST As sAvgCost"
									+ ", udf_Price_Silver As sPrice_Silver, udf_Price_Gold As sPrice_Gold, UDF_Price_Diamond As sPrice_Diamond"
									+ ", V.VENDORNAME As sVendor, IV.STANDARDLEADTIME As sLeadTime, IV.LASTLEADTIME As sLastLeadTime, UDF_MAIN_CATEGORY As sClass"
									+ ", CAST(IW.MINIMUMORDERQTY As int) As sMOQ, dbo.fn_Inventory_Item_ADU_ByWarehouseAndDateRange(CI.ITEMCODE, '000', dbo.fn_Inventory_Date_ReviewPeriod_Start(), dbo.fn_Inventory_Date_ReviewPeriod_End()) As sADU"
									+ ", ROUND(COALESCE(IW.QUANTITYONHAND, 0), 0) As s000QOH, ROUND(COALESCE(IM004.QUANTITYONHAND, 0), 0) As s004QOH"
									+ " FROM CI_ITEM CI"
									+ " INNER JOIN IM_ITEMWAREHOUSE IW"
										+ " ON IW.ITEMCODE = CI.ITEMCODE AND IW.WAREHOUSECODE = '000'"
									+ " INNER JOIN IM_ITEMVENDOR IV"
										+ " ON IV.VENDORNO = CI.PRIMARYVENDORNO"
									+ " INNER JOIN AP_VENDOR V"
										+ " ON V.VENDORNO = CI.PRIMARYVENDORNO"
									+ " INNER JOIN SALES..his_std_fds HSF"
										+ " ON HSF.ITEMCODE = CI.ITEMCODE"
									+ " LEFT JOIN IM_ITEMWAREHOUSE IM004"
										+ " ON IM004.ITEMCODE = CI.ITEMCODE AND IM004.WAREHOUSECODE = '004'"
									+ " WHERE CI.ITEMCODE = @sSKU";
				oCMD.Parameters.Add("@sSKU", sSKU);

				using (SqlDataReader oDR = oCMD.ExecuteReader())
				{
					while (oDR.Read())
					{
						oData.sSKU = oDR["sSKU"].ToString();
						oData.sName = oDR["sName"].ToString();
						oData.sActive = oDR["sActive"].ToString();
						oData.sReorder = oDR["sReorder"].ToString();
						oData.sCategory1 = oDR["sCategory1"].ToString();
						oData.sCategory4 = oDR["sCategory4"].ToString();
						oData.sFirstDateSold = oDR["sFirstDateSold"].ToString();
						oData.sAvgCost = oDR["sAvgCost"].ToString();
						oData.sPrice_Silver = oDR["sPrice_Silver"].ToString();
						oData.sPrice_Gold = oDR["sPrice_Gold"].ToString();
						oData.sPrice_Diamond = oDR["sPrice_Diamond"].ToString();
						oData.sVendor = oDR["sVendor"].ToString();
						oData.sLeadTime = oDR["sLeadTime"].ToString();
						oData.sLastLeadTime = oDR["sLastLeadTime"].ToString();
						oData.sClass = oDR["sClass"].ToString();
						oData.sMOQ = oDR["sMOQ"].ToString();
						oData.sADU = oDR["sADU"].ToString();
						oData.s000QOH = Math.Round(Convert.ToDecimal(oDR["s000QOH"]), 0).ToString();
						oData.s004QOH = Math.Round(Convert.ToDecimal(oDR["s004QOH"]), 0).ToString();
					}
				}
			}
		}

		return new JavaScriptSerializer().Serialize(oData);
	}

	public class WeeklyHistory {
		public string sData {get; set;}
		public string s1 {get; set;}
		public string s2 {get; set;}
		public string s3 {get; set;}
		public string s4 {get; set;}
		public string s5 {get; set;}
		public string s6 {get; set;}
		public string s7 {get; set;}
		public string s8 {get; set;}
		public string s9 {get; set;}
		public string s10 {get; set;}
		public string s11 {get; set;}
		public string s12 {get; set;}
		public string s13 {get; set;}
		public string s14 {get; set;}
		public string s15 {get; set;}
		public string s16 {get; set;}
		public string s17 {get; set;}
		public string s18 {get; set;}
		public string s19 {get; set;}
		public string s20 {get; set;}
		public string s21 {get; set;}
		public string s22 {get; set;}
		public string s23 {get; set;}
		public string s24 {get; set;}
		public string s25 {get; set;}
		public string s26 {get; set;}
		public string s27 {get; set;}
		public string s28 {get; set;}
		public string s29 {get; set;}
		public string s30 {get; set;}
		public string s31 {get; set;}
		public string s32 {get; set;}
		public string s33 {get; set;}
		public string s34 {get; set;}
		public string s35 {get; set;}
		public string s36 {get; set;}
		public string s37 {get; set;}
		public string s38 {get; set;}
		public string s39 {get; set;}
		public string s40 {get; set;}
		public string s41 {get; set;}
		public string s42 {get; set;}
		public string s43 {get; set;}
		public string s44 {get; set;}
		public string s45 {get; set;}
		public string s46 {get; set;}
		public string s47 {get; set;}
		public string s48 {get; set;}
		public string s49 {get; set;}
		public string s50 {get; set;}
		public string s51 {get; set;}
		public string s52 {get; set;}
		public string s53 {get; set;}
	}

	public class MonthlyHistory
	{
		public string sData { get; set; }
		public string s1 { get; set; }
		public string s2 { get; set; }
		public string s3 { get; set; }
		public string s4 { get; set; }
		public string s5 { get; set; }
		public string s6 { get; set; }
		public string s7 { get; set; }
		public string s8 { get; set; }
		public string s9 { get; set; }
		public string s10 { get; set; }
		public string s11 { get; set; }
		public string s12 { get; set; }
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadSKUHistory(string sSKU, DateTime dtStart, DateTime dtEnd, string sSource, string sType, string sView, string sDWUpdate, long dADU, string sSessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
		
		if (!ValidateSession(sSessionID)) return sInvalidSession;

		//Ensure our view string matches the case of the values we use to compare
		sView = sView.ToLower();

		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		//Setup our view type objects
		List<WeeklyHistory> oWeeklyDataRows = new List<WeeklyHistory>();
		WeeklyHistory oWeeklyData = null;
		List<MonthlyHistory> oMonthlyDataRows = new List<MonthlyHistory>();
		MonthlyHistory oMonthlyData = null;

		using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
		{
			oCN.Open();
			using (SqlCommand oCMD = new SqlCommand("", oCN))
			{
				oCMD.CommandText = "EXEC prc_SKUP2 @dtStart, @dtEnd, @sSKU, @sSource, @sType, @sView, @sDWUpdate, @dADU";
				oCMD.Parameters.Add("@sSKU", sSKU);
				oCMD.Parameters.Add("@dtStart", Convert.ToDateTime(dtStart));
				oCMD.Parameters.Add("@dtEnd", Convert.ToDateTime(dtEnd));
				oCMD.Parameters.Add("@sSource", sSource);
				oCMD.Parameters.Add("@sType", sType);
				oCMD.Parameters.Add("@sView", sView);
				oCMD.Parameters.Add("@sDWUpdate", sDWUpdate);
				oCMD.Parameters.Add("@dADU", dADU);
				
				using (SqlDataReader oDR = oCMD.ExecuteReader())
				{
					while (oDR.Read())
					{
						//Add the data to the list for the specified view type
						if (sView == "weekly")
						{
							oWeeklyData = new WeeklyHistory();
							oWeeklyData.sData = oDR["sValName"].ToString();
							oWeeklyData.s1 = oDR["1"].ToString();
							oWeeklyData.s2 = oDR["2"].ToString();
							oWeeklyData.s3 = oDR["3"].ToString();
							oWeeklyData.s4 = oDR["4"].ToString();
							oWeeklyData.s5 = oDR["5"].ToString();
							oWeeklyData.s6 = oDR["6"].ToString();
							oWeeklyData.s7 = oDR["7"].ToString();
							oWeeklyData.s8 = oDR["8"].ToString();
							oWeeklyData.s9 = oDR["9"].ToString();
							oWeeklyData.s10 = oDR["10"].ToString();
							oWeeklyData.s11 = oDR["11"].ToString();
							oWeeklyData.s12 = oDR["12"].ToString();
							oWeeklyData.s13 = oDR["13"].ToString();
							oWeeklyData.s14 = oDR["14"].ToString();
							oWeeklyData.s15 = oDR["15"].ToString();
							oWeeklyData.s16 = oDR["16"].ToString();
							oWeeklyData.s17 = oDR["17"].ToString();
							oWeeklyData.s18 = oDR["18"].ToString();
							oWeeklyData.s19 = oDR["19"].ToString();
							oWeeklyData.s20 = oDR["20"].ToString();
							oWeeklyData.s21 = oDR["21"].ToString();
							oWeeklyData.s22 = oDR["22"].ToString();
							oWeeklyData.s23 = oDR["23"].ToString();
							oWeeklyData.s24 = oDR["24"].ToString();
							oWeeklyData.s25 = oDR["25"].ToString();
							oWeeklyData.s26 = oDR["26"].ToString();
							oWeeklyData.s27 = oDR["27"].ToString();
							oWeeklyData.s28 = oDR["28"].ToString();
							oWeeklyData.s29 = oDR["29"].ToString();
							oWeeklyData.s30 = oDR["30"].ToString();
							oWeeklyData.s31 = oDR["31"].ToString();
							oWeeklyData.s32 = oDR["32"].ToString();
							oWeeklyData.s33 = oDR["33"].ToString();
							oWeeklyData.s34 = oDR["34"].ToString();
							oWeeklyData.s35 = oDR["35"].ToString();
							oWeeklyData.s36 = oDR["36"].ToString();
							oWeeklyData.s37 = oDR["37"].ToString();
							oWeeklyData.s38 = oDR["38"].ToString();
							oWeeklyData.s39 = oDR["39"].ToString();
							oWeeklyData.s40 = oDR["40"].ToString();
							oWeeklyData.s41 = oDR["41"].ToString();
							oWeeklyData.s42 = oDR["42"].ToString();
							oWeeklyData.s43 = oDR["43"].ToString();
							oWeeklyData.s44 = oDR["44"].ToString();
							oWeeklyData.s45 = oDR["45"].ToString();
							oWeeklyData.s46 = oDR["46"].ToString();
							oWeeklyData.s47 = oDR["47"].ToString();
							oWeeklyData.s48 = oDR["48"].ToString();
							oWeeklyData.s49 = oDR["49"].ToString();
							oWeeklyData.s50 = oDR["50"].ToString();
							oWeeklyData.s51 = oDR["51"].ToString();
							oWeeklyData.s52 = oDR["52"].ToString();
							oWeeklyData.s53 = oDR["53"].ToString();
							oWeeklyDataRows.Add(oWeeklyData);
						}
						else
						{
							oMonthlyData = new MonthlyHistory();
							oMonthlyData.sData = oDR["sValName"].ToString();
							oMonthlyData.s1 = oDR["1"].ToString();
							oMonthlyData.s2 = oDR["2"].ToString();
							oMonthlyData.s3 = oDR["3"].ToString();
							oMonthlyData.s4 = oDR["4"].ToString();
							oMonthlyData.s5 = oDR["5"].ToString();
							oMonthlyData.s6 = oDR["6"].ToString();
							oMonthlyData.s7 = oDR["7"].ToString();
							oMonthlyData.s8 = oDR["8"].ToString();
							oMonthlyData.s9 = oDR["9"].ToString();
							oMonthlyData.s10 = oDR["10"].ToString();
							oMonthlyData.s11 = oDR["11"].ToString();
							oMonthlyData.s12 = oDR["12"].ToString();
							oMonthlyDataRows.Add(oMonthlyData);
						}
					}
				}
			}
		}

		//Return the requested data view object
		if (sView == "weekly") return new JavaScriptSerializer().Serialize(oWeeklyDataRows);
		else return new JavaScriptSerializer().Serialize(oMonthlyDataRows);
	}

	public bool ValidateSession(string sSessionID) {
		sSessionID = clsJW_Encrypt.fnDecrypt(sSessionID);
		
		if (sSessionID.Length == 0) return false;
		
		using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
		{
			oCN.Open();
			using (SqlCommand oCMD = new SqlCommand("", oCN))
			{
				oCMD.CommandText = "SELECT contactID As iCnt FROM Contact WHERE COALESCE(sSessionID, '') = @sSessionID AND dtSessionExpires > GetDate()";
				oCMD.Parameters.Add("@sSessionID", sSessionID.ToString());

				string sID = "";

				using (SqlDataReader oDR = oCMD.ExecuteReader())
				{
					while (oDR.Read())
					{
						sID = oDR["iCnt"].ToString();
					}
				}

				if (sID == "") return false;
			}
		}

		//if we get here then sessionid is valid
		return true;
	}
}
