<%@ Application Language="C#" %>

<script runat="server">
	
	protected void Application_BeginRequest(Object sender, EventArgs e)
	{
		clsRPlus.Main();
			
		/*	
		//JMW: Force maintenance message display to avoid data changes during maintenance windows.
		// 0 = No maintenance
		// 1 = Major maintenance (no access is allowed)
		// 2 = Minor maintenance (no access other than SKUP is allowed)
		Int32 iMaintenanceType = 0;

		//JMW: Redirect to maintenance message if required.
		if ((iMaintenanceType != 0) && clsRPlus.fnGetURL_Clean().IndexOf("maintenance") == -1)
		{
			if (iMaintenanceType == 1 || (iMaintenanceType == 2 && clsRPlus.fnGetURL_Clean().IndexOf("the-skup") == -1))
			{
				Response.Redirect("/maintenance/");
			}
		}

		//JMW: Force sign in required.
		Boolean bUserAuth = false;

		//JMW: Force correct URL and SSL.
		if (Request.ServerVariables["server_name"] != "insider.redemptionplus.com")
		{
			Response.Redirect("https://insider.redemptionplus.com" + clsRPlus.fnGetURL_CleanQuery());
		}
		else if (this.Request.IsSecureConnection != true)
		{
			Response.Redirect("https://" + Request.ServerVariables["server_name"] + clsRPlus.fnGetURL_CleanQuery());
		}

		if (Request.Cookies["sessionid"] != null)
		{

			//Create and store new unique id for this user's session.
			System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand();
			oCMD.Connection = clsRPlus.fnOpenCN_SQL(clsRPlus.enumDBs.INSIDER);
			oCMD.CommandText = "UPDATE contact SET dtSessionExpires = DATEADD(mm, 12, GetDate()) WHERE sSessionID = @sSessionID";
			oCMD.Parameters.AddWithValue("@sSessionID", clsJW_Encrypt.fnDecrypt(Server.UrlDecode(Request.Cookies["sessionid"].Value)));
			oCMD.ExecuteNonQuery();
			oCMD.Dispose();

			ADODB.Connection oCN = clsRPlus.fnOpenCN_ADO(clsRPlus.enumDBs.INSIDER);
			ADODB.Recordset oRS = new ADODB.Recordset();

			oRS.Open("SELECT COUNT(contactid) FROM contact" +
						" WHERE sSessionID = '" + clsJW_Encrypt.fnDecrypt(Server.UrlDecode(Request.Cookies["sessionid"].Value)) + "'" +
						" AND dtSessionExpires > GetDate()", oCN);

			if (Convert.ToInt32(oRS.Fields[0].Value) > 0) bUserAuth = true;

			clsRPlus.fnKillRS(oRS);
			clsRPlus.fnKillCN_ADO(oCN);
		}
		if (bUserAuth == false && clsRPlus.fnGetURL_Clean().IndexOf("sign-in") != 1)
		{
			Response.Redirect("/sign-in/?m=reset");
		}
		*/
	}

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }
	
    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
