using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forecasts2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
        {
            try
            {
                if (e.CommandName.Equals("New"))
                {
                    LinkButton btnNew = e.CommandSource as LinkButton;
                    GridViewRow row = btnNew.NamingContainer as GridViewRow;
                    //System.Data.SqlClient.SqlDataReader reader = null;
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    if (row == null)
                    {
                        return;
                    }
                    TextBox txtforecastYear = GridView1.FooterRow.FindControl("forecastYearTextBoxNew") as TextBox;
                    DropDownList txtforecastCategory = GridView1.FooterRow.FindControl("forecastCategoryTextBoxEmpty") as DropDownList;
                    TextBox txtJan = GridView1.FooterRow.FindControl("JanTextBoxNew") as TextBox;
                    TextBox txtFeb = GridView1.FooterRow.FindControl("FebTextBoxNew") as TextBox;
                    TextBox txtMar = GridView1.FooterRow.FindControl("MarTextBoxNew") as TextBox;
                    TextBox txtApr = GridView1.FooterRow.FindControl("AprTextBoxNew") as TextBox;
                    TextBox txtMay = GridView1.FooterRow.FindControl("MayTextBoxNew") as TextBox;
                    TextBox txtJun = GridView1.FooterRow.FindControl("JunTextBoxNew") as TextBox;
                    TextBox txtJul = GridView1.FooterRow.FindControl("JulTextBoxNew") as TextBox;
                    TextBox txtAug = GridView1.FooterRow.FindControl("AugTextBoxNew") as TextBox;
                    TextBox txtSep = GridView1.FooterRow.FindControl("SepTextBoxNew") as TextBox;
                    TextBox txtOct = GridView1.FooterRow.FindControl("OctTextBoxNew") as TextBox;
                    TextBox txtNov = GridView1.FooterRow.FindControl("NovTextBoxNew") as TextBox;
                    TextBox txtDec = GridView1.FooterRow.FindControl("DecTextBoxNew") as TextBox;
					String query = "INSERT INTO [SKUP2_ForecastAdj] ( [forecastYear], [forecastCategory], [Jan], [Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec]) VALUES (@forecastYear, @forecastCategory, @Jan, @Feb, @Mar, @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Nov, @Dec)";

                    cmd.CommandText = query;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("forecastYear", txtforecastYear.Text);
                    cmd.Parameters.AddWithValue("forecastCategory", txtforecastCategory.Text);
                    cmd.Parameters.AddWithValue("Jan", txtJan.Text);
                    cmd.Parameters.AddWithValue("Feb", txtFeb.Text);
                    cmd.Parameters.AddWithValue("Mar", txtMar.Text);
                    cmd.Parameters.AddWithValue("Apr", txtApr.Text);
                    cmd.Parameters.AddWithValue("May", txtMay.Text);
                    cmd.Parameters.AddWithValue("Jun", txtJun.Text);
                    cmd.Parameters.AddWithValue("Jul", txtJul.Text);
                    cmd.Parameters.AddWithValue("Aug", txtAug.Text);
                    cmd.Parameters.AddWithValue("Sep", txtSep.Text);
                    cmd.Parameters.AddWithValue("Oct", txtOct.Text);
                    cmd.Parameters.AddWithValue("Nov", txtNov.Text);
                    cmd.Parameters.AddWithValue("Dec", txtDec.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Cancel();
                    cmd.Dispose();
                    conn.Close();
                    Response.AppendHeader("Refresh", "0,URL=");
                }
                if (e.CommandName.Equals("EmptyNew"))
                {
                    LinkButton btnNew = e.CommandSource as LinkButton;
                    GridViewRow row = btnNew.NamingContainer as GridViewRow;
                    System.Data.SqlClient.SqlDataReader reader = null;
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    if (row == null)
                    {
                        return;
                    }
                    TextBox txtforecastYear = GridView1.Controls[0].Controls[0].FindControl("forecastYearTextBoxEmpty") as TextBox;
                    DropDownList txtforecastCategory = GridView1.Controls[0].Controls[0].FindControl("forecastCategoryTextBoxEmpty") as DropDownList;
                    TextBox txtJan = GridView1.Controls[0].Controls[0].FindControl("JanTextBoxEmpty") as TextBox;
                    TextBox txtFeb = GridView1.Controls[0].Controls[0].FindControl("FebTextBoxEmpty") as TextBox;
                    TextBox txtMar = GridView1.Controls[0].Controls[0].FindControl("MarTextBoxEmpty") as TextBox;
                    TextBox txtApr = GridView1.Controls[0].Controls[0].FindControl("AprTextBoxEmpty") as TextBox;
                    TextBox txtMay = GridView1.Controls[0].Controls[0].FindControl("MayTextBoxEmpty") as TextBox;
                    TextBox txtJun = GridView1.Controls[0].Controls[0].FindControl("JunTextBoxEmpty") as TextBox;
                    TextBox txtJul = GridView1.Controls[0].Controls[0].FindControl("JulTextBoxEmpty") as TextBox;
                    TextBox txtAug = GridView1.Controls[0].Controls[0].FindControl("AugTextBoxEmpty") as TextBox;
                    TextBox txtSep = GridView1.Controls[0].Controls[0].FindControl("SepTextBoxEmpty") as TextBox;
                    TextBox txtOct = GridView1.Controls[0].Controls[0].FindControl("OctTextBoxEmpty") as TextBox;
                    TextBox txtNov = GridView1.Controls[0].Controls[0].FindControl("NovTextBoxEmpty") as TextBox;
                    TextBox txtDec = GridView1.Controls[0].Controls[0].FindControl("DecTextBoxEmpty") as TextBox;
					String query = "INSERT INTO [SKUP2_ForecastAdj] ( [forecastYear], [forecastCategory], [Jan], [Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec]) VALUES (@forecastYear, @forecastCategory, @Jan, @Feb, @Mar, @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Nov, @Dec)";

                    cmd.CommandText = query;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("forecastYear", txtforecastYear.Text);
                    cmd.Parameters.AddWithValue("forecastCategory", txtforecastCategory.Text);
                    cmd.Parameters.AddWithValue("Jan", txtJan.Text);
                    cmd.Parameters.AddWithValue("Feb", txtFeb.Text);
                    cmd.Parameters.AddWithValue("Mar", txtMar.Text);
                    cmd.Parameters.AddWithValue("Apr", txtApr.Text);
                    cmd.Parameters.AddWithValue("May", txtMay.Text);
                    cmd.Parameters.AddWithValue("Jun", txtJun.Text);
                    cmd.Parameters.AddWithValue("Jul", txtJul.Text);
                    cmd.Parameters.AddWithValue("Aug", txtAug.Text);
                    cmd.Parameters.AddWithValue("Sep", txtSep.Text);
                    cmd.Parameters.AddWithValue("Oct", txtOct.Text);
                    cmd.Parameters.AddWithValue("Nov", txtNov.Text);
                    cmd.Parameters.AddWithValue("Dec", txtDec.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Cancel();
                    cmd.Dispose();
                    conn.Close();
                    Response.AppendHeader("Refresh", "0,URL=");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
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
}