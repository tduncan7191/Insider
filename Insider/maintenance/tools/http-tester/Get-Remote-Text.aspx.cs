using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

public partial class Get_Remote_Response : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string sURL = "";
		string sResponse = "";

		if (HttpContext.Current.Request.QueryString["url"] != null) sURL = HttpContext.Current.Request.QueryString["url"].ToString();

		if (sURL.Length > 0)
		{
			try
			{
				// Create the web request  
				WebRequest oRequest = WebRequest.Create(sURL) as HttpWebRequest;

				// Get response
				using (WebResponse oResponse = oRequest.GetResponse() as HttpWebResponse)
				{
					HttpWebResponse oWR = (HttpWebResponse)oResponse;
					// Get the response stream  
					StreamReader oReader = new StreamReader(oWR.GetResponseStream());

					// Read the whole contents and return as a string  
					HttpContext.Current.Response.Write(oReader.ReadToEnd());
				}
			}
			catch (WebException ex)
			{
				if ((HttpWebResponse)ex.Response != null)
				{
					sResponse = ((HttpWebResponse)ex.Response).StatusCode.ToString();
				}
				else sResponse = "NotFound";
			}
		}
		else sResponse = "No URL";

		HttpContext.Current.Response.Write(sResponse);
    }
}