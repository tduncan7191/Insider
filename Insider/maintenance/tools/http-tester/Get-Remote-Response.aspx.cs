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

		HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

		if (HttpContext.Current.Request.QueryString["url"] != null) sURL = HttpContext.Current.Request.QueryString["url"].ToString();

		if (sURL.Length > 0 && sURL != "http://" && sURL != "https://")
		{
			try
			{
				// Create the web request  
				WebRequest oRequest = WebRequest.Create(sURL) as HttpWebRequest;
				oRequest.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
				// Get response
				using (WebResponse oResponse = oRequest.GetResponse() as HttpWebResponse)
				{
					HttpWebResponse oWR = (HttpWebResponse)oResponse;

					// Read the whole contents and return as a string  
					HttpContext.Current.Response.Write(oWR.StatusCode);
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