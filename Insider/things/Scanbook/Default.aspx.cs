using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class _Default //: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        /* try
        {
            string str = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(str)) 
			using(SqlCommand cmd  = new SqlCommand("in_GenerateScanbook", conn))
			{
				conn.Open();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@masNumber", txtParam.Text);
				cmd.ExecuteNonQuery();		
			}            
        }
        catch (Exception ex)
        {
            lblResult.Text = "error: " + ex.Message.ToString();
        } */
    }
    private void SendToEmail()
    {
        
    }

/*     public void createPages()
    {
        Guid guid;
        if (!Guid.TryParse(Request.QueryString["id"], out guid))
            return;

        DEG.AdoNet.Dbase_Connection dbase = new DEG.AdoNet.Dbase_Connection();
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        TinyKernel kernel = new TinyKernel();
        try {
            var path = Server.MapPath("/_FileLibrary/") + "\\..\\Scanbooks\\" + Request.QueryString["id"] + "\\scanbook.pdf";
            WebSupergoo.ABCpdf8.XSettings.InstallLicense("WuJbSzVR9+1XpyYSM/zSObfMSeMZVIAgpcu2nr6xQh5YqX6WOi69N5FqZD08mwcIbtYz9TtFriEd1aEmSLZ3xzkZWATfWHBOrCgxVcOyR0LmwU3x9M4aXJfjaBLxIF4pW6l3XI55+wQ1SwZ6xUy5JUcLViefT3+rpuQ39V0mW//Ox7xtlSbi8WapyYBMjmxsNkYV621EjlbIOUMILr9GtWVM9hLCbaE=");
            WebSupergoo.ABCpdf8.Doc doc = new WebSupergoo.ABCpdf8.Doc();
            try {
                int pageId = 0;
                doc.HtmlOptions.FontEmbed = true;
                doc.EmbedFont("3 of 9 Barcode");
                doc.EmbedFont("Arial");
                doc.MediaBox.String = "0 0 612 792";
                doc.SetInfo(doc.Page, "/MediaBox:Rect", doc.MediaBox.String);
                doc.Rect.Width = (int)doc.MediaBox.Width;
                doc.Rect.Height = (int)doc.MediaBox.Height;
                doc.Rect.Position(5, 0);
                doc.HtmlOptions.PageCacheEnabled = false;
                
                var id = doc.AddImageUrl(Config_WebRoot + "/account/scanbookdata.asp?aid=" + Request.QueryString["id"], true, 0, true);
                while (doc.Chainable(id))
                {
                    doc.Page = doc.AddPage();
                    id = doc.AddImageToChain(id);
                }
                //+
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                
                using (var stream = System.IO.File.Create(path))
                    doc.Save(stream);
            } finally {
                doc.Dispose();
            }
            //new Util.NotifyService().Send(string xFrom, string xTo, string xCc, string xBcc, string cSubject, string cBody, string[] cAttachmentArray) {
        } finally {
            if (dbase != null) {
                dbase.Close(); dbase = null;
            }
        }
    }

    public void create pageData()
    {
        <%
	Response.Expires = -1
	Response.CacheControl = "no-cache"
	Response.AddHeader "Pragma", "no-cache"
	'- Head -'
	Instinct_Head
	'+ database
	Dim oDbase: Set oDbase = m_oInstinct.Load("DBase")
	oDbase.Add("").SqlServer Config_DbaseServer, Config_DbaseUser, Config_DbasePassword, Config_Dbase
   '+
	Dim oHtml: Set oHtml = m_oInstinct.Load("HTML")
	Dim cId : cId = Request.QueryString("aid")
	'+
	Dim rData : Set rData = oDbase("").Interface("ProductGroup", "Hash:ScanBook~x=AccountId", Null, cId)
%>
<!DOCTYPE html>
<html>
<head>
	<title>Digital Scan Book</title>
	<style>
		html {
		  font-family: serif;
		  -webkit-text-size-adjust: 100%;
			  -ms-text-size-adjust: 100%;
		}
		body {
		  margin: 0;
		}
		div.logo {
			text-align:center;
			margin:1em 0;
		}
		div.page {
			width: 8in;
			height: 10.5in;
			page-break: after;
			position: relative;
		}
		div.footer {
			position: absolute;
			bottom: 1em;
			left: 40%;
			font-size: 0.8em;
		}
		table {
			width: 100%;
			border-collapse: collapse;
			margin:0;
		}
		td, th {
			border: 2px solid #000;
			text-align: center;
			font-size: 1em;
			font-family: sans-serif;
			padding: 3px;
		}
		span.barcode {
			font-family: '3 of 9 Barcode';
			font-size: 2em;
		}
		td img {
			max-width: 100px;
			max-height: 100px;
		}
	</style>
</head>
<body>
<%
	Dim nCount : nCount = 0
	Do While (rData.Eof = False)
		nCount = 0
%>
	<div class="page">
		<div class="logo"><img src="/_project/_block/frame/2004logo200.jpg" alt="logo" /></div>
		<table>
		<tr>
			<th width="30%">Image</th>
			<th>Item<br/>Number</th>
			<th>Description</th>
			<th>Ticket<br/>Value</th>
			<th width="25%">Barcode</th>
		</tr>
<%
		Do While (rData.Eof = False And nCount < 7)
			nCount = nCount + 1
%>
		
		<tr>
			<td><img src="/_Sku/<%= rData("Id") %>.jpg" /></td>
			<td>#<%= rData("Id") %></td>
			<td><%= rData("Name") %></td>
			<td><%= m_oInstinct.Lng(rData("TicketValue"), 0) %></td>
			<td><span class="barcode">*<%= rData("Id") %>*</span></td>
		</tr>
<%
			rData.MoveNext
		Loop
%>
		</table>
		<div class="footer">RedemptionPlus.com 888.564.7587</div>
	</div>
<%
	Loop
%>
</body>
</html>
<%
	rData.Close() : Set rData = Nothing
	'+
	m_oInstinct.Free oHtml
	m_oInstinct.Free oDbase
	'- Foot -'
	Instinct_Foot
%>
    }

    public void Process(int orderlogKey)
	{
		//var stage = ConfigurationManager.AppSettings["ApplicationStage"] ?? "DEV";
		var notifyService = new Util.NotifyService();
		//var errorEmail = ConfigurationManager.AppSettings["ErrorNotifyEmail"];
		//var appDomain = ConfigurationManager.AppSettings["ApplicationDomain"];
		var securePath = ConfigurationManager.AppSettings["ScanbookPath"];
		//_sforce.NotificationEmail = errorEmail;
		
		try
		{
			var url = "http://redemptionplus.com/account/scanbookpdf.aspx?id=";
			using (var table = _dbase.Interface("OrderLog", "Item~n=Key", orderlogKey, DBNull.Value))
			using (var client = new WebClient())
			{
				var id = table.Rows[0].Field<Guid>("AccountId").ToString();
				var sforceId = table.Rows[0].Field<string>("AccountUid2");
				var email = table.Rows[0].Field<string>("AccountContactEmail");
								
				client.DownloadString(url + id);
				var path = securePath + id + "\\scanbook.pdf";
				
				if (!_sforce.UploadFileToAccount(sforceId, path, string.Format("scanbook_{0}.pdf", DateTime.Now.ToString("yyyy-MM-dd"))))
					throw new ApplicationException("Failed to upload file to Salesforce.");

				try {
					notifyService.Send("noreply@redemptionplus.com", email, null, null, "Your new scan book is attached", @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
                    <html><head>
                    <title>Redemption Plus - Growing your business one smile at a time</title>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=ISO-8859-1"">
                    <style type=""text/css"">
                    body {font-size:10px; font-family:Verdana;}
                    td {font-size:10px; font-family:Verdana;}
                    </style>
                    </head>
                    <body style=""font-family:Verdana;"">
                    <table align=""center"" bgcolor=""bbd533"" cellpadding=""5"" cellspacing=""25"" style=""border-radius:10px; width:700px"">
                    <tbody>
                        <tr>
                            <td bgcolor=""white"" style=""border-radius:10px; width:200px""><img alt=""Redemption Plus - Redemption Toys and Incentive Merchandise"" src=""https://ci4.googleusercontent.com/proxy/VaucwX0mGM4vjlmclPgV2bjzfYZquWTabWh3ViSrX8Wi2agHVT_V-b_9b08_I2qktinLMwig8oSA0KkbIL77vs27gJrX_HRoCdCZar6aI9LZW8soQIVda0JgfjXxSiE=s0-d-e1-ft#http://www.redemptionplus.com/_PROJECT/_Block/Frame/Head/WebHeadLeft2.jpg"" style=""border-radius:10px"">
                            </td>
                        </tr>
                        <tr>
                            <td><hr bgcolor=""392b82"" border=""0"" color=""392b82"" size=""7"" style=""border-radius:10px"">
                            </td>
                        </tr>
                        <tr style=""width: 00px;"">
                            <td bgcolor=""white"" style=""border-radius:10px;"">
                                <br><span style=""font-size:16px""><span style=""font-family:verdana,geneva,sans-serif"">
                                <span style=""color:#222222;background-color:#ffffff"">
                                <p>The Redemption Plus team is here to support your business needs, so you can focus on what matters most - guest experience. One way we do this is by helping with inventory management and accuracy. A new scanbook will be generated if your center places an order with a new sku that is displayed in a bin or basket or is only sold in an inner quantity.</p>

                    <p>You may be asking, &quot;what do I do with this now?&quot; We recommend an easy 3-step process:</p>
                    <ol>
                    <li>Print the attached scan book in color</li>
                    <li>Recycle your current scan book to eliminate confusion</li>
                    <li>Add your new scan book to a designated 3-ring binder with protective sheets</li>
                    </ol>
                    <p>As always, we're here listening with open ears and an open mind if you see opportunities to improve this process. We highly value the input of those on the front lines! </p>
                    <p>Redemption Plus</p></span></span>
                                <br><hr>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr bgcolor=""392b82"" border=""0"" color=""392b82"" size=""7"" style=""border-radius:10px"">
                            </td>
                        </tr>
                    </tbody>
                    </table>
                    <br>
                    <table cellpadding=""0"" cellspacing=""0"" width=""100%"">
                    <tr>
                    <td>
                        Do not reply to this email.<br>
                        Please contact your account representative with any questions or comments.</td>
                    </tr>
                    </table>
                    </body>
                    </html>", new[] { path }, System.Web.Mail.MailFormat.Html);
				}
				catch (Exception ex)
				{
				}
			} */
}