using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
using IronPdf;
using System.Web.Script.Services;
using System.IO;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[ScriptService]
public class Scanbook : System.Web.Services.WebService
{
    string scanbookPath = ConfigurationManager.AppSettings["ScanbookPath"];

    public Scanbook()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<string> GenerateScanbook(string txtMasNo, string txtStartDate, string txtEndDate, bool IsAllItems)
    {
        DateTime endDate = DateTime.Now;
        DateTime startDate = DateTime.Now;
        DateTime.TryParse(txtStartDate, out startDate);
        DateTime.TryParse(txtEndDate, out endDate);

        UserInfo userInfo = GetUserInfo(txtMasNo);
        List<Product> scanbookProducts = GetScanbookItems(txtMasNo, startDate, endDate, IsAllItems);
        List<string> returnStrings = new List<string>();
        if (IsAllItems)
        {
            byte[] bytePDF = GeneratePDF(scanbookProducts, userInfo);
            returnStrings.Add(Convert.ToBase64String(bytePDF));
            //SendToEmail(txtMasNo.Text, UserInfo);
        }
        //test
        userInfo = GetUserInfo("0005390");
        userInfo.Id = "c3d508db-5ae4-4b44-a917-937f99778eca";
        SendToEmail("0005390", userInfo);
        returnStrings.Add(userInfo.SforceId);
        return returnStrings;
    }
    
    private UserInfo GetUserInfo(string masNo)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SMILE"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select top 1 AccountId, AccountUid2, AccountContactEmail FROM [dbo].[OrderLog] where accountuid = @masNumber", conn);

            cmd.Parameters.AddWithValue("@masNumber", masNo);
            var reader = cmd.ExecuteReader();

            UserInfo userInfo = new UserInfo();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userInfo.Id = reader["AccountId"].ToString();
                    userInfo.SforceId = reader["AccountUid2"].ToString();
                    userInfo.Email = reader["AccountContactEmail"].ToString();
                }
            }
            return userInfo;
        }
    }
    
    public List<Product> GetScanbookItems(string masNo, DateTime startDate, DateTime endDate, bool IsAllItems)
    {
        List<Product> lstProducts = new List<Product>();

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SMILE"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("in_GenerateScanbook", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (IsAllItems)
            {
                cmd.Parameters.AddWithValue("@cMethod", "Hash:ScanBook");
            }
            else
            {
                cmd.Parameters.AddWithValue("@cMethod", "Exec:ScanbooksTaskQueue");
            }
            cmd.Parameters.AddWithValue("@masNumber", masNo);
            cmd.Parameters.AddWithValue("@startDate", startDate.Date.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@endDate", endDate.Date.ToString("yyyyMMdd"));
            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        Sku = reader["Id"].ToString(),
                        Description = reader["Name"].ToString(),
                        TicketValue = reader["TicketValue"].ToString(),
                        Image = "http://redemptionplus.com/_Sku/" + reader["Id"] + ".jpg"
                    };
                    lstProducts.Add(product);
                }
            }
        }
        return lstProducts;
    }
    
    public byte[] GeneratePDF(List<Product> scanbookProducts, UserInfo userInfo)
    {
        string html =
            "<!DOCTYPE html><html><head>" +
            "<title> Digital Scan Book</title>" +
            "<style>" +
                "html { font-family: serif; -webkit-text-size-adjust: 100 %; -ms-text-size-adjust: 100 %; } " +
                "body { margin: 0; } " +
                "div.logo { text-align:center; margin: 1em 0; } " +
                "div.page { width: 8in; height: 10.5in; page-break: after; position: relative; } " +
                "div.footer { position: absolute; bottom: 1em; left: 40%; font-size: 0.8em; } " +
                "table { width: 100 %; border-collapse: collapse; margin: 0; } " +
                "td, th {border: 2px solid #000;text-align: center;font-size: 1em; font-family: sans-serif;padding: 3px; } " +
                "span.barcode { font-family: '3 of 9 Barcode'; font-size: 2em; } " +
                "td img { max-width: 100px; max-height: 100px; } " +
            "</style>" +
            "</head>" +
            "<body>" +
            "<div class='page'>" +
            "<div class='logo'><img src = 'http://redemptionplus.com/_project/_block/frame/RPLogoSM.png' alt='logo' /></div>" +
            "<table>" +
                "<tr>" +
                "<th width='30%'>Image</th>" +
                "<th>Item<br/>Number</th>" +
                "<th>Description </th>" +
                "<th>Ticket<br/>Value</th>" +
                "<th width='25%'>Barcode</th>" +
                "</tr>";
        foreach (var product in scanbookProducts)
        {
            html +=
                "<tr>" +
                    "<td><img src = " + product.Image + " /></td>" +
                    "<td>#" + product.Sku + "</td>" +
                    "<td>" + product.Description + "</td>" +
                    "<td>" + product.TicketValue + "</td>" +
                    "<td><span class='barcode'>*" + product.Sku + "*</span></td>" +
                "</tr>";
        }
        html +=
            "</table>" +
            "<div class='footer'>RedemptionPlus.com 888.564.7587</div>" +
            "</div></body></html>";
        HtmlToPdf Renderer = new HtmlToPdf();
        if (Renderer.RenderHtmlAsPdf(html).TrySaveAs(scanbookPath + "\\" + userInfo.Id + "\\scanbook_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf"))
        {
            Renderer.RenderHtmlAsPdf(html).SaveAs(scanbookPath + "\\" + userInfo.Id + "\\scanbook_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
        }
        else
        {
            //Directory.CreateDirectory(scanbookPath + "\\" + userInfo.Id);
            //Renderer.RenderHtmlAsPdf(html).SaveAs(scanbookPath + "\\" + userInfo.Id + "\\scanbook_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
        }
        return Renderer.RenderHtmlAsPdf(html).BinaryData;
    }
    
    private void SendToEmail(string masNumber, UserInfo userInfo)
    {
        string html =
            @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
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
                    <td bgcolor=""white"" style=""border-radius:10px; width:200px""><img alt=""Redemption Plus - Redemption Toys and Incentive Merchandise"" src=""https://ci4.googleusercontent.com/proxy/VaucwX0mGM4vjlmclPgV2bjzfYZquWTabWh3ViSrX8Wi2agHVT_V-b_9b08_I2qktinLMwig8oSA0KkbIL77vs27gJrX_HRoCdCZar6aI9LZW8soQIVda0JgfjXxSiE=s0-d-e1-ft#http://www.redemptionplus.com/_PROJECT/_Block/Frame/RPLogoSM.png"" style=""border-radius:10px"">
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
            </html>";

        MailMessage mail = new MailMessage("noreply@redemptionplus.com", userInfo.Email);
        SmtpClient client = new SmtpClient
        {
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["EmailPassword"]),
            Host = "smtp.gmail.com"
        };
        mail.Subject = "Your new scan book is attached";
        mail.Body = html;
        mail.IsBodyHtml = true;
        mail.Attachments.Add(new Attachment(scanbookPath + "\\" + userInfo.Id + "\\scanbook_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf"));
        client.Send(mail);
    }
}

public class Product
{
    public string Image { get; set; }
    public string Sku { get; set; }
    public string Description { get; set; }
    public string TicketValue { get; set; }
}

public class UserInfo
{
    public string Id { get; set; }
    public string SforceId { get; set; }
    public string Email { get; set; }
}
