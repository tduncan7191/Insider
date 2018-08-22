using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_WebUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		//JMW: Force sign in required.
		Boolean bUserAuth = false;

		if (ConfigurationManager.AppSettings["bDevMode"] != "1" && Request.ServerVariables["server_name"].ToLower().IndexOf("dev.redemption-plus.com") == -1)
		{
			if (Request.ServerVariables["server_name"] != "insider.redemptionplus.com")
			{
				Response.Redirect("https://insider.redemptionplus.com" + clsRPlus.fnGetURL_CleanQuery());
			}
			else if (this.Request.IsSecureConnection != true)
			{
				Response.Redirect("https://" + Request.ServerVariables["server_name"] + clsRPlus.fnGetURL_CleanQuery());
			}
		}

		if (Request.Cookies["sessionid"] != null)
		{
			//Create and store new unique id for this user's session.
			System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand();
			System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER);
			oCMD.Connection = oCN;
			oCMD.CommandText = "UPDATE contact SET dtSessionExpires = DATEADD(mm, 12, GetDate()) WHERE sSessionID = @sSessionID";
			oCMD.Parameters.AddWithValue("@sSessionID", clsJW_Encrypt.fnDecrypt(Server.UrlDecode(Request.Cookies["sessionid"].Value)));
			oCMD.ExecuteNonQuery();

			oCMD.CommandText = "SELECT contactid FROM contact" +
						" WHERE sSessionID = @sSessionID" +
						" AND dtSessionExpires > GetDate()";
			using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
			{
				if (oDR.HasRows == true) bUserAuth = true;
			}

			oCMD.Dispose();
			clsRPlus.fnKillCN(oCN);
		}

		if (bUserAuth == false && clsRPlus.fnGetURL_Clean().IndexOf("sign-in") != 1)
		{
			Response.Redirect("/sign-in/?m=reset");
		}
    }
}