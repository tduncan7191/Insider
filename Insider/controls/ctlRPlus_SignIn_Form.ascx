<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlRPlus_SignIn_Form.ascx.cs" Inherits="controls_RPlus_SignIn_Form" %>
<div class="center">
<%
	if (Request.QueryString["m"] == "reset")
	{
		Response.Cookies["sessionid"].Expires = DateTime.Now.AddDays(-1);
		if (Request.Cookies["rememberme"] == null)
		{
			Response.Cookies["contactid"].Expires = DateTime.Now.AddDays(-1);
		}
		Response.Cookies["firstname"].Expires = DateTime.Now.AddDays(-1);
		Response.Cookies["lastname"].Expires = DateTime.Now.AddDays(-1);
		Response.Cookies["email"].Expires = DateTime.Now.AddDays(-1);
		Response.Redirect("/sign-in/");
	}
	else if (Request.Cookies["sessionid"] == null)
	{
%>
<script type="text/javascript">
	function fnValidate(o) {
		var sMsg = "";

		if (o.SignInForm_sUser.value == "") {
			sMsg = "You must select a User.";
		}
		else if (o.SignInForm_sPassword.value == "") {
			sMsg = "You must enter a Password.";
		}

		if (sMsg != "") {
			alert(sMsg);
		}
		else fnAJAXForm_Post(o, document.getElementById('SignIn_Response'));
	}
</script>
<div id="SignIn_Response" class="AJAXForm_Response">&nbsp;</div>
<form method="post" action="/ajax-data-services/sign-in/" onsubmit="fnValidate(this);return false;" id="SignIn_Form" class="RPlus_Form">
	<h2>Sign In to Insider</h2>
	<div style="margin-bottom:10px;">Please select your name and enter your password below.</div>
	<table cellpadding="0" cellspacing="0" style="width:250px;margin:0 auto;">
	<tr>
		<td style="text-align:right;">User:</td>
		<td>
			<select name="SignInForm_sUser" id="SignInForm_sUser" class="Field SelectBox">
				<option value="">Select Your Name</option>
				<%
				string sSelected = "";

				using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
				{
					using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
					{
						oCMD.Connection = oCN;
						oCMD.CommandText = "SELECT contactID, firstName, lastName FROM contact"
											+ " WHERE status IN (0,1)"
											//" OR contactid IN (73,74,75,79,225)" //Special warehouse/other employees to allow.
											+ " ORDER BY firstName, lastName";
						using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
						{
							while (oDR.Read())
							{
								if (Request.Cookies["contactid"] != null)
								{
									if (Request.Cookies["contactid"].Value.ToString() == oDR["contactid"].ToString())
									{
										sSelected = " selected=\"selected\"";
									}
								}
								Response.Write("<option value=\"" + oDR["contactID"].ToString() + "\"" + sSelected + ">" + oDR["firstName"].ToString() + " " + oDR["lastName"].ToString() + "</option>");
								sSelected = "";
							}
						}
					}
				}
				%>
			</select>
		</td>
	</tr>
	<tr>
		<td style="text-align:right;">Password:</td>
		<td><input type="password" name="SignInForm_sPassword" id="SignInForm_sPassword" class="Field" /></td>
	</tr>
	<tr>
		<td colspan="2" style="text-align:center;padding:10px 0;vertical-align:middle;">
			<%
			sSelected = "";
			if (Request.Cookies["rememberme"] != null)
			{
				if (Request.Cookies["rememberme"].Value == "1") sSelected = "checked=\"checked\"";
			}
			%>
			Remember me on this computer:&nbsp;<input type="checkbox" name="SignInForm_bRemember" id="SignInForm_bRemember" value="1" <%=sSelected%> />
		</td>
	</tr>
	</table>
    <input type="submit" name="SignInForm_btnSubmit" id="SignInForm_btnSubmit" value="Sign In" />
</form>
<%
	}
	else
	{
%>
	<h1>You are signed in!<br /></h1>
	<div style="margin-bottom:15px;">
		Name: <%=Request.Cookies["firstname"].Value%>&nbsp;<%=Request.Cookies["lastname"].Value%>
		<br />
		Email: <%=Request.Cookies["email"].Value%>
		<br /><br />
		If this information is incorrect please contact the site administrator.
	</div>
	<h3>You can <a href="?m=reset">click here</a> to sign out.</h3>
<%
	}
%>
</div>