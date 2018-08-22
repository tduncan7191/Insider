<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ajax_data_services_sign_in_Default" %>
<%
if (Request.Form.Count != 0)
{
	//Process the submitted Sign In form parameters.
	if (Request.Form["SignInForm_sUser"] != null)
	{
		using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
		{
			using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
			{
				oCMD.Connection = oCN;
				oCMD.CommandText = "SELECT sSessionID, contactID, firstName, lastName, email, password FROM contact"
									+ " WHERE contactid = @iContactID";
				oCMD.Parameters.AddWithValue("@iContactID", clsRPlus.fnVal(Request.Form["SignInForm_sUser"]).ToString());
				
				using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
				{
					if (oDR.HasRows)
					{
						while (oDR.Read())
						{
							if (clsJW_Encrypt.fnDecrypt(oDR["password"].ToString()) == Request.Form["SignInForm_sPassword"])
							{
								oDR.Close();
								
								oCMD.CommandText = "UPDATE contact SET sSessionID = NewID(), dtSessionExpires = DATEADD(mm, 12, GetDate()) WHERE contactid = @iContactID";
								oCMD.ExecuteNonQuery();

								oCMD.CommandText = "SELECT sSessionID, contactID, firstName, lastName, email, password FROM contact"
													+ " WHERE contactid = @iContactID";

								using (System.Data.SqlClient.SqlDataReader oDR2 = oCMD.ExecuteReader())
								{
									while (oDR2.Read())
									{
										Boolean bRemember = (Request.Form["SignInForm_bRemember"] == "1");
							
										//JMW: Setup user cookie values
										HttpCookie oCookie = new HttpCookie("sessionid");
										oCookie.Value = Server.UrlEncode(clsJW_Encrypt.fnEncrypt(oDR2["sSessionID"].ToString()));
										if (bRemember == true) oCookie.Expires = System.DateTime.Now.AddYears(25);
										Response.SetCookie(oCookie);
				
										oCookie = new HttpCookie("contactid");
										oCookie.Value = oDR2["contactID"].ToString();
										if (bRemember == true) oCookie.Expires = System.DateTime.Now.AddYears(25);
										Response.SetCookie(oCookie);

										oCookie = new HttpCookie("firstname");
										oCookie.Value = oDR2["firstName"].ToString();
										if (bRemember == true) oCookie.Expires = System.DateTime.Now.AddYears(25);
										Response.SetCookie(oCookie);

										oCookie = new HttpCookie("lastname");
										oCookie.Value = oDR2["lastName"].ToString();
										if (bRemember == true) oCookie.Expires = System.DateTime.Now.AddYears(25);
										Response.SetCookie(oCookie);

										oCookie = new HttpCookie("email");
										oCookie.Value = oDR2["email"].ToString();
										if (bRemember == true) oCookie.Expires = System.DateTime.Now.AddYears(25);
										Response.SetCookie(oCookie);

										if (bRemember == true)
										{
											oCookie = new HttpCookie("rememberme");
											oCookie.Value = "1";
											oCookie.Expires = System.DateTime.Now.AddYears(25);
											Response.SetCookie(oCookie);
										}
										else Response.Cookies["rememberme"].Expires = DateTime.Now.AddDays(-1);

										oCookie = null;
									}
								}

								//Refresh to access new cookie data.
								Response.Redirect(clsRPlus.fnGetURL_CleanQuery());
							}
							else
							{
							%>
							<h2>The User and Password entered do not match.</h2>
							<div><a href="" onclick="fnAJAXForm_ShowForm(false,document.getElementById('SignIn_Form'),document.getElementById('SignIn_Response'));return false;">Click here to try again.</a></div>
							<%
							Response.End();
							}
						}
					}
				}
			}
		}
	}
}
else if (Request.Cookies["sessionid"] != null)
{
	%><customControls:ctlRPlus_SignIn_Form ID="ctlRPlus_SignIn_Form" runat="server" /><%
}
%>