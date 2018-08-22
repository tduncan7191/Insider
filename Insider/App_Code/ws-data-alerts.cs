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
public class ws_data_alerts : System.Web.Services.WebService
{
	public string sInvalidSession = "Invalid Session";

	string[] AuthorizedUserIDs = { "229", "208", "213" };

	public class AlertInstance
	{
		public int AlertID { get; set; }
		public string Name { get; set; }
		public string AlertTypeClass { get; set; }
		public string AlertTypeName { get; set; }
		public bool Active { get; set; }
		public string SendTo { get; set; }
		public string Frequency { get; set; }
		public string DaysToRun { get; set; }
		public string LastRun { get; set; }
		public string NextRun { get; set; }
		public string ParamJSON { get; set; }
	}
	public class AlertResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadProductClasses(string SessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		AlertResult Result = new AlertResult();
		Result.Success = false;

		//first, validate session
		if (!ValidateSession(SessionID)) Result.Message = sInvalidSession;
		else
		{
			try
			{
				using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
				{
					oCN.Open();
					using (SqlCommand oCMD = new SqlCommand("", oCN))
					{
						oCMD.CommandText = "SELECT CATEGORY1 FROM CI_ITEM WHERE UDF_IS_ACTIVE = 'y' AND COALESCE(CATEGORY1, '') <> '' GROUP BY CATEGORY1 ORDER BY COUNT(*) DESC";

						using (SqlDataReader oDR = oCMD.ExecuteReader())
						{
							Result.Message = "";

							while (oDR.Read())
							{
								Result.Message += (Result.Message.ToString()=="") ? oDR["CATEGORY1"].ToString() : "," + oDR["CATEGORY1"].ToString();
							}
						}
						
						Result.Success = true;
						Result.Message = Result.Message;
					}
				}
			}
			catch (Exception ex)
			{ return ExHandler(ex); }
		}

		return new JavaScriptSerializer().Serialize(Result);
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadInstances(string SessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		AlertResult Result = new AlertResult();
		Result.Success = false;

		//first, validate session
		if (!ValidateSession(SessionID)) Result.Message = sInvalidSession;
		else
		{
			List<AlertInstance> AlertInstances = new List<AlertInstance>();

			try
			{
				using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
				{
					oCN.Open();
					using (SqlCommand oCMD = new SqlCommand("", oCN))
					{
						oCMD.CommandText = "SELECT AlertID, DA.Name, DAT.Name As AlertTypeName, DAT.AlertClass As AlertTypeClass, DA.AlertTypeID, Active, SendTo, Frequency, DaysToRun, '' As LastRun, '' As NextRun, ParamJSON"
											+ " FROM DataAlerts_Alert DA"
											+ " INNER JOIN DataAlerts_AlertType DAT"
												+ " ON DAT.AlertTypeID = DA.AlertTypeID"
											+ " ORDER BY AlertID";

						using (SqlDataReader oDR = oCMD.ExecuteReader())
						{
							while (oDR.Read())
							{
								AlertInstance Alert = new AlertInstance();
								Alert.AlertID = Convert.ToInt32(oDR["AlertID"]);
								Alert.Name = oDR["Name"].ToString();
								Alert.AlertTypeClass = oDR["AlertTypeClass"].ToString();
								Alert.AlertTypeName = oDR["AlertTypeName"].ToString();
								Alert.Active = Convert.ToBoolean(oDR["Active"]);
								Alert.SendTo = oDR["SendTo"].ToString();
								Alert.Frequency = oDR["Frequency"].ToString();
								Alert.DaysToRun = oDR["DaysToRun"].ToString();
								Alert.LastRun = oDR["LastRun"].ToString();
								Alert.NextRun = oDR["NextRun"].ToString();
								Alert.ParamJSON = oDR["ParamJSON"].ToString();
								AlertInstances.Add(Alert);
							}
						}

						Result.Success = true;
						Result.Message = new JavaScriptSerializer().Serialize(AlertInstances);
					}
				}
			}
			catch (Exception ex)
			{ return ExHandler(ex); }
		}

		return new JavaScriptSerializer().Serialize(Result);
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string LoadAlert(string AlertID, string SessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		AlertResult Result = new AlertResult();
		Result.Success = false;

		//first, validate session
		if (!ValidateSession(SessionID)) Result.Message = sInvalidSession;
		else
		{
			try
			{
				using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
				{
					oCN.Open();
					using (SqlCommand oCMD = new SqlCommand("", oCN))
					{
						oCMD.CommandText = "SELECT AlertID, DA.Name, DAT.Name As AlertTypeName, DAT.AlertClass As AlertTypeClass, DA.AlertTypeID, Active, SendTo, Frequency, DaysToRun, '' As LastRun, '' As NextRun, ParamJSON"
											+ " FROM DataAlerts_Alert DA"
											+ " INNER JOIN DataAlerts_AlertType DAT"
												+ " ON DAT.AlertTypeID = DA.AlertTypeID"
											+ " WHERE AlertID = @AlertID"
											+ " ORDER BY AlertID";
						oCMD.Parameters.Add("@AlertID", AlertID);
						
						AlertInstance Alert = new AlertInstance();

						using (SqlDataReader oDR = oCMD.ExecuteReader())
						{
							while (oDR.Read())
							{
								Alert.AlertID = Convert.ToInt32(oDR["AlertID"].ToString());
								Alert.Name = oDR["Name"].ToString();
								Alert.AlertTypeClass = oDR["AlertTypeClass"].ToString();
								Alert.AlertTypeName = oDR["AlertTypeName"].ToString();
								Alert.Active = Convert.ToBoolean(oDR["Active"]);
								Alert.SendTo = oDR["SendTo"].ToString();
								Alert.Frequency = oDR["Frequency"].ToString();
								Alert.DaysToRun = oDR["DaysToRun"].ToString();
								Alert.LastRun = oDR["LastRun"].ToString();
								Alert.NextRun = oDR["NextRun"].ToString();
								Alert.ParamJSON = oDR["ParamJSON"].ToString();
							}
						}

						Result.Success = true;
						Result.Message = new JavaScriptSerializer().Serialize(Alert);
					}
				}
			}
			catch (Exception ex)
			{ return ExHandler(ex); }
		}

		return new JavaScriptSerializer().Serialize(Result);
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string SaveAlert(string jsAlert, string SessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
		
		AlertInstance a = new JavaScriptSerializer().Deserialize<AlertInstance>(jsAlert);

		AlertResult Result = new AlertResult();
		Result.Success = false;

		//first, validate session
		if (!ValidateSession(SessionID)) Result.Message = sInvalidSession;
		else
		{
			//validate user can save
			if (!ValidateUser()) Result.Message = "Permission denied.";
			else
			{
				try
				{
					//Convert JSON string to object list
					using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
					{
						oCN.Open();
						using (SqlCommand oCMD = new SqlCommand())
						{
							oCMD.Connection = oCN;
							oCMD.Parameters.Add("@AlertID", a.AlertID);
							oCMD.Parameters.Add("@Name", a.Name);
							oCMD.Parameters.Add("@SendTo", a.SendTo);
							oCMD.Parameters.Add("@Active", a.Active);
							oCMD.Parameters.Add("@DaysToRun", a.DaysToRun);
							oCMD.Parameters.Add("@Frequency", a.Frequency);
							oCMD.Parameters.Add("@AlertTypeClass", a.AlertTypeClass);
							oCMD.Parameters.Add("@ParamJSON", a.ParamJSON);
							oCMD.Parameters.Add("@SessionID", clsJW_Encrypt.fnDecrypt(SessionID));

							if (a.AlertID==0)
							{
								oCMD.CommandText = "INSERT INTO DataAlerts_Alert ("
										+ "Name,SendTo,Active,DaysToRun,Frequency,AlertTypeID,ParamJSON,CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate"
									+ ")"
									+ " SELECT @Name,@SendTo,@Active,@DaysToRun,@Frequency"
									+ ",(SELECT AlertTypeID FROM DataAlerts_AlertType WHERE AlertClass = @AlertTypeClass)"
									+ ",@ParamJSON"
									+ ",Contact.ContactID,GetDate(),Contact.ContactID,GetDate()"
									+ " FROM Contact"
									+ " WHERE CAST(Contact.sSessionID As nvarchar(max)) = @SessionID";
							}
							else
							{
								oCMD.CommandText = "UPDATE DataAlerts_Alert"
									+ " SET Name=@Name"
										+ ",SendTo=@SendTo"
										+ ",Active=@Active"
										+ ",DaysToRun=@DaysToRun"
										+ ",Frequency=@Frequency"
										+ ",AlertTypeID=DataAlerts_AlertType.AlertTypeID"
										+ ",ParamJSON=@ParamJSON"
										+ ",LastModifiedBy=(SELECT contactID FROM Contact WHERE CAST(Contact.sSessionID As nvarchar(max)) = @SessionID)"
										+ ",LastModifiedDate=GetDate()"
									+ " FROM DataAlerts_AlertType"
									+ " WHERE AlertID = @AlertID"
									+ " AND DataAlerts_AlertType.AlertClass = @AlertTypeClass";
							}
							int ResultRows = oCMD.ExecuteNonQuery();

							Result.Success = (ResultRows == 0) ? false : true;
							if (a.AlertID != 0)
								Result.Message = (ResultRows == 0) ? "Alert ID could not be found." : "Alert saved successfully.";
							else
								Result.Message = (ResultRows == 0) ? "New Alert could not be added." : "Alert saved successfully";
						}
					}
				}
				catch (Exception ex)
				{ return ExHandler(ex); }
			}
		}

		//return proper status
		return new JavaScriptSerializer().Serialize(Result);
	}

	[WebMethod(EnableSession = true)]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public string DeleteAlert(string AlertID, string SessionID)
	{
		//don't allow iOS 6 (or any browsers) to cache responses
		System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		AlertResult Result = new AlertResult();
		Result.Success = false;

		//first, validate session
		if (!ValidateSession(SessionID)) Result.Message = sInvalidSession;
		else
		{
			//validate user can save
			if (!ValidateUser()) Result.Message = "Permission denied.";
			else
			{
				try
				{
					//Convert JSON string to object list
					using (SqlConnection oCN = new SqlConnection(ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
					{
						oCN.Open();
						using (SqlCommand oCMD = new SqlCommand())
						{
							oCMD.Connection = oCN;
							oCMD.CommandText = "DELETE FROM DataAlerts_Alert WHERE AlertID = @AlertID";
							oCMD.Parameters.Add("@AlertID", AlertID);
							
							int iAffected = oCMD.ExecuteNonQuery();

							if (iAffected > 0) Result.Success = true;
						}
					}
				}
				catch (Exception ex)
				{ return ExHandler(ex); }
			}
		}

		//return proper status
		return new JavaScriptSerializer().Serialize(Result);
	}

	private string ExHandler(Exception ex)
	{
		AlertResult Result = new AlertResult();
		Result.Success = false;
		Result.Message = "WS Error: " + ex.Message.ToString();

		return new JavaScriptSerializer().Serialize(Result);
	}

	private bool ValidateUser()
	{
		return AuthorizedUserIDs.Contains(System.Web.HttpContext.Current.Request.Cookies["contactid"].Value);
	}

	private bool ValidateSession(string sSessionID)
	{
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
