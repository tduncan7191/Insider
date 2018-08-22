<%@ Page Title="" Language="C#" MasterPageFile="~/templates/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="tools_password_encryption_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody1" Runat="Server">
<%
	if (Request.Cookies["contactid"].Value != "229" && Request.Cookies["contactid"].Value != "208" && Request.Cookies["contactid"].Value != "320")
	{
		Response.Write("You do not have the proper credentials to use this page.");
		Response.Write(" User ID: " + Request.Cookies["contactid"].Value);
		
	}
	else if (Request.Form.Count == 0)
	{
	%>
		<form action="" method="post">
		User to encrypt password for:
		<select name="iContactID" id="iContactID" class="Field SelectBox">
			<option value="">Select a User</option>
			<%
			//Wrap it up B! "using" ensures all objects are closed/disposed automatically.
		using (System.Data.SqlClient.SqlConnection oCN = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
			{
				oCN.Open();
				using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
				{
					oCMD.Connection = oCN;
					oCMD.CommandText = "SELECT contactID, firstName, lastName FROM contact"
								+ " WHERE status IN  (0,1)"
								//+ " OR contactid IN (73,74,75,79,225)" //Special warehouse/other employees to allow.
								+ " ORDER BY firstName, lastName";

					using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
					{
						while (oDR.Read())
						{
							Response.Write("<option value=\"" + oDR["contactID"].ToString() + "\">" + oDR["firstName"].ToString() + " " + oDR["lastName"].ToString() + "</option>");
						}
					}
				}
			}
			%>
		</select>
		<input type="submit" value="submit" />
		</form>
	<%
	}
	else
	{
		//JMW: Update, Uncomment, and then Run this code (load the Sign In form) to encrypt a user password in the DB.
		//Wrap it up B! "using" ensures all objects are closed/disposed automatically.
		using (System.Data.SqlClient.SqlConnection oCN = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
		{
			oCN.Open();
			using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
			{
				oCMD.Connection = oCN;
				oCMD.CommandText = "SELECT contactID, password, firstName, lastName FROM contact"
							+ " WHERE contactID = @iContactID";
				oCMD.Parameters.AddWithValue("@iContactID", Request.Form["iContactID"].ToString());
				using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
				{
					if (oDR.HasRows == true)
					{
						oDR.Read();
						using (System.Data.SqlClient.SqlCommand oCMD2 = new System.Data.SqlClient.SqlCommand())
						{
							oCMD2.Connection = oCN;
							oCMD2.CommandText = "UPDATE contact"
												+ " SET password = @sPassword"	
												+ " WHERE contactID = @iContactID";
							oCMD2.Parameters.AddWithValue("@iContactID", Request.Form["iContactID"].ToString());
							oCMD2.Parameters.AddWithValue("@sPassword", clsJW_Encrypt.fnEncrypt(oDR["password"].ToString()));
							oDR.Close();
							oCMD2.ExecuteNonQuery();
						}
						%><h1>Contact ID <%=Request.Form["iContactID"].ToString()%>'s password has been encrypted.</h1><%
					}
					else
					{
						%><h1>Contact ID not found, or no user selected.</h1><%
					}
				}
			}
		}
	}
%>
</asp:Content>

