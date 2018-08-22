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

public partial class FutureUsage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_SALES"].ConnectionString))
        {
            try
            {
                if (e.CommandName.Equals("New"))
                {
                    LinkButton btnNew = e.CommandSource as LinkButton;
                    GridViewRow row = btnNew.NamingContainer as GridViewRow;
                    System.Data.SqlClient.SqlDataReader reader = null;
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    if (row == null)
                    {
                        return;
                    }
                    TextBox txtItemCode = GridView1.FooterRow.FindControl("ItemCodeTextBoxNew") as TextBox;
                    TextBox txtDate = GridView1.FooterRow.FindControl("transactiondateTextBoxNew") as TextBox;
                    DropDownList txtTransactionType = GridView1.FooterRow.FindControl("TypeTextBoxEmpty") as DropDownList;
                    TextBox txtValue = GridView1.FooterRow.FindControl("valueTextBoxNew") as TextBox;
                    String query ="INSERT INTO [SkupFutureEntrys] ( [ItemCode], [transactiondate], [Type], [value]) VALUES (@ItemCode, @transactiondate, @Type, @value)";

                    cmd.CommandText = query;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("ItemCode", txtItemCode.Text);
                    cmd.Parameters.AddWithValue("transactiondate", txtDate.Text);
                    cmd.Parameters.AddWithValue("Type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("value", txtValue.Text);
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
                    TextBox txt = GridView1.Controls[0].Controls[0].FindControl("ItemCodeTextBoxEmpty") as TextBox;
                    TextBox txtItemCode = GridView1.Controls[0].Controls[0].FindControl("ItemCodeTextBoxEmpty") as TextBox;
                    TextBox txttransactiondate = GridView1.Controls[0].Controls[0].FindControl("transactiondateTextBoxEmpty") as TextBox;
                    DropDownList txtTransactionType = GridView1.Controls[0].Controls[0].FindControl("TypeTextBoxEmpty") as DropDownList;
                    TextBox txtValue = GridView1.Controls[0].Controls[0].FindControl("valueTextBoxEmpty") as TextBox;
                    String query = "INSERT INTO [SkupFutureEntrys] ( [ItemCode], [transactiondate], [Type], [value]) VALUES (@ItemCode, @transactiondate, @Type, @value)";

                    cmd.CommandText = query;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("ItemCode", txtItemCode.Text);
                    cmd.Parameters.AddWithValue("transactiondate", txttransactiondate.Text);
                    cmd.Parameters.AddWithValue("Type", txtTransactionType.Text);
                    cmd.Parameters.AddWithValue("value", txtValue.Text);
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

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        string s = Session["ItemCode"].ToString();

        if (s != null)
        {
            e.Command.Parameters["@ItemCode"].Value = s;
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