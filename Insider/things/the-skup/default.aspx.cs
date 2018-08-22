using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;

public partial class skup : System.Web.UI.Page
{
    List<SqlParameter> insertParameters = new List<SqlParameter>();

    String enteredSku = "0";
	DateTime firstDateSold = System.DateTime.Now;

	#region Page Init
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Image1.ImageUrl = "http://www.redemptionplus.com/_FileLibrary/FileImage/redemption_news.jpg";
        Image1.Attributes.Remove("Width");
        Image1.Attributes.Remove("Height");
        Image1.Attributes.Add("Width", "132");
        Image1.Attributes.Add("Height", "84");
    }
    #endregion

    #region Servercalls
	//Get the basic Item Setup information from the database.
	public Boolean fillItemsInfo(String EnteredSku)
	{
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(clsRPlus.sCN_MAS_RDP))
		{
			//int loopCounter = 0;
			System.Data.SqlClient.SqlDataReader reader2 = null;
			System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

			String query = "SELECT * FROM vw_Item_Setup_SKUP where ItemNumber=@Entered_Sku";
			cmd2.CommandTimeout = 300;
			cmd2.CommandText = query;
			cmd2.CommandType = System.Data.CommandType.Text;
			cmd2.Connection = conn;
			cmd2.Parameters.AddWithValue("@Entered_Sku", EnteredSku);
			conn.Open();
			reader2 = cmd2.ExecuteReader();

			while (reader2.Read())
			{
				if (reader2["First_DATE_SOLD"] != DBNull.Value)
				{
					firstDateSold = Convert.ToDateTime(reader2["First_DATE_SOLD"]);
				}
				else firstDateSold = Convert.ToDateTime("01/01/1900");

				VendorNumberBox.Text = (String)(reader2["VendorNumber"]);
				DescriptionBox.Text = (String)(reader2["ItemDescription"]);
				ActiveTextBox.Text = (String)(reader2["Active"]);
				ReorderTextBox.Text = (String)(reader2["Reorder"]);
				if (!((reader2["MainCategory"]).Equals(System.DBNull.Value)))
				{
					MainCategoryBox.Text = (String)(reader2["MainCategory"]);
				}
				AvgCostBox.Text = System.Convert.ToString(Math.Round((decimal)(reader2["AveCost"]) * 1000) / 1000);
				SB1Box.Text = System.Convert.ToString(Math.Round((decimal)(reader2["SBP1"]) * 1000) / 1000);
				LeadTimeBox.Text = System.Convert.ToString((int)(decimal)(reader2["LastLeadTime"]));
				StandardLeadTimeBox.Text = System.Convert.ToString((int)(decimal)(reader2["StandardLeadTime"]));
				ClassBox.Text = (String)(reader2["Class"]);
				MOQBox.Text = System.Convert.ToString((int)(decimal)(reader2["MinimumOrderQty"]));
				adu.Text = System.Convert.ToString(reader2["DAILYUSAGE"]);
			}
			reader2.Close();
			cmd2.Parameters.Clear();
			cmd2.Cancel();
			
			//GET SKUP DATA
			Int32 iStartYear = System.DateTime.Now.AddYears(-1).Year;
			Int32 iEndYear = System.DateTime.Now.AddYears(1).Year;
			Int32 iCol = 0;
			Int32 iColMax = 0;


			Double dADU = 0;
			String sADU = "default";

			if (double.TryParse(sADUOverride.Text, out dADU)) sADU = sADUOverride.Text;

			if (bMonthly.Checked)
			{
				iColMax = 12;
				query = "SELECT * FROM dbo.fn_Inventory_SKUP_Monthly_ByItemAndYearRange(@Entered_Sku, @dtStart, @dtEnd, " + sADU + ", default)";
			}
			else
			{
				iColMax = 53;
				query = "SELECT * FROM dbo.fn_Inventory_SKUP_ByItemAndYearRange(@Entered_Sku, @dtStart, @dtEnd, " + sADU + ", default)";
			}

			//Setup the columns needed for the data being returned
			BoundField bndcCol;
			for (iCol = grdSKUP.Columns.Count - 1; iCol > 1; iCol--)
			{
				grdSKUP.Columns.RemoveAt(iCol);
			}
			for (iCol = 1; iCol <= iColMax; iCol++)
			{
				bndcCol = new BoundField();
				bndcCol.DataField = iCol.ToString();
				bndcCol.HeaderText = iCol.ToString();
				bndcCol.ItemStyle.CssClass = "skup-DataCell";
				if (iColMax == 12) bndcCol.ShowHeader = true;

				grdSKUP.Columns.Add(bndcCol);
			}

			cmd2.Parameters.Clear();
			cmd2.CommandText = query;
			cmd2.Parameters.AddWithValue("@Entered_Sku", EnteredSku);
			cmd2.Parameters.AddWithValue("@dtStart", iStartYear);
			cmd2.Parameters.AddWithValue("@dtEnd", iEndYear);
			reader2 = cmd2.ExecuteReader();

			grdSKUP.DataSource = reader2;
			grdSKUP.DataBind();
			grdSKUP.Visible = true;
			Session.Add("dt",reader2);
			Exportbtn.Visible = true;

			cmd2.Parameters.Clear();
			cmd2.Cancel();

			cmd2.Dispose();
			conn.Close();

			return true;
		}
	}

	public string GetConnectionString(string name)
	{
		//bool _status = false;
		String _message = "";
		try
		{
			//variable to hold our connection string for returning it
			string connString = string.Empty;
			//check to see if the user provided a connection string name
			//this is for if your application has more than one connection string
			if (!string.IsNullOrEmpty(name)) //a connection string name was provided
			{
				//get the connection string by the name provided
				connString = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
			}
			else //no connection string name was provided
			{
				//get the default connection string
				connString = System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
			}
			//_status = true;
			//return the connection string to the calling method
			return connString;
		}
		catch (Exception ex)
		{
			_message = ex.Message;
			//_status = false;
			return string.Empty;
		}
	}
    #endregion

    #region ClickActions

    protected void editOrderButton_Click(object sender, EventArgs e)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(clsRPlus.sCN_UPS))
        {
            int loopCounter = 0;
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
            String query = "SELECT * FROM [ROReport_ProductArrival]";
			cmd.CommandTimeout = 300;
			cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            //cmd.Parameters.AddWithValue("@Order_Number", messageLabel1.Text);
            Console.Write("Executing Reader");
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                loopCounter++;
            }
            reader.Close();
            cmd.Parameters.Clear();
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();
        }
    }

    protected void EnterSkuButton_Click(object sender, EventArgs e)
    {
		if (skuNumber.Text.Length == 0)
		{
			EnterSku.Style.Add(HtmlTextWriterStyle.Color, "red");
		}
		else
		{
			DateTime dtBegin = System.DateTime.Now;

			EnterSku.Style.Add(HtmlTextWriterStyle.Color, "");
			enteredSku = skuNumber.Text;
			
			Image1.ImageUrl = "http://www.redemptionplus.com/_Sku/" + enteredSku + ".jpg";
			Image1.Attributes.Remove("Width");
			Image1.Attributes.Remove("Height");
			Image1.Attributes.Add("Width", "140");
			Image1.Attributes.Add("Height", "140");

			fillItemsInfo(enteredSku);
			
			FutureUsageHyperLink.Visible = true;
			Session["ItemCode"] = skuNumber.Text;

			lblPageLoadTime.Text = "Data Speed: " + Math.Round(System.DateTime.Now.Subtract(dtBegin).TotalSeconds,0).ToString() + " sec.";
		}
    }
    #endregion

    #region export
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
		Response.ClearContent();
		Response.AddHeader("content-disposition", "attachment; filename=SKU-Export-" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "@" + DateTime.Now.Hour + DateTime.Now.Minute + ".xls");
		Response.ContentType = "application/excel";
		System.IO.StringWriter sw = new System.IO.StringWriter();
		HtmlTextWriter htw = new HtmlTextWriter(sw);
		grdSKUP.RenderControl(htw);
		Response.Write(sw.ToString());
		Response.End();
    }
	public override void VerifyRenderingInServerForm(Control control)
	{
	}
    #endregion
}