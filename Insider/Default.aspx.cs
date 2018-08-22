using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected System.Data.DataTable employeePriceTable = new System.Data.DataTable();
    GridView Grid = new GridView();
    /*
    protected System.Data.DataTable binLocationTable = new System.Data.DataTable();
     */
    GridView binGrid = new GridView();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!(IsPostBack))
        {
            employeePriceTable.Columns.Add("Sku", Type.GetType("System.String"));
            employeePriceTable.Columns.Add("Description", Type.GetType("System.String"));
            // employeePriceTable.Columns.Add("Vendor", Type.GetType("System.String"));
            //employeePriceTable.Columns.Add("CatId", Type.GetType("System.String"));
            //employeePriceTable.Columns.Add("NCat", Type.GetType("System.String"));
            employeePriceTable.Columns.Add("QAV", Type.GetType("System.String"));
            employeePriceTable.Columns.Add("Price", Type.GetType("System.String"));
            employeePriceTable.Columns.Add("Min", Type.GetType("System.String"));
            employeePriceTable.Columns.Add("Inner", Type.GetType("System.String"));
            employeePriceTable.Columns.Add("Case", Type.GetType("System.String"));
            employeePriceTable.Columns.Add("PLine", Type.GetType("System.String"));
            ViewState["employeePriceTable"] = employeePriceTable;
            /*
            binLocationTable.Columns.Add("Sku", Type.GetType("System.String"));
            binLocationTable.Columns.Add("BinLocation", Type.GetType("System.String"));
            binLocationTable.Columns.Add("quantityonhand", Type.GetType("System.String"));
            binLocationTable.Columns.Add("WarehouseCode", Type.GetType("System.String"));
            ViewState["binLocationTable"] = binLocationTable;
            */

        }
    }
    protected void getPrice_Click(object sender, EventArgs e)
    {
		try
		{
			String s = EmployeePriceInfo(enteredSkuBox.Text);

			if (s == null)
			{
				alert1.Text = "Pricing for SKU " + enteredSkuBox.Text + " is listed below.";
				alert1.Style.Remove("color");
				alert1.Style.Add("color", "green");
				enteredSkuBox.Text = "Enter Sku";
			}
			else
			{
				alert1.Text = s;
				alert1.Style.Remove("color");
				alert1.Style.Add("color", "red");
			}
		}
		catch (Exception ex) {
			alert1.Text = ex.Message;
			alert1.Style.Remove("color");
			alert1.Style.Add("color", "red");
		}
		alert1.Visible = true;
    }
    /*
    protected void getBinLocation_Click(object sender, EventArgs e)
    {
        Boolean s = FindBinLocation(BinLocationItemCode.Text);
        if (s == false)
        {
            alert2.Text = " Item not found";
            alert2.Style.Add("color", "red");
            alert2.Visible = true;
        }
        else
        {
            alert2.Visible = false;
        }
    }
    */
    public String EmployeePriceInfo(String EnteredSku)
    {
		try
		{
			String sResult = "";
			Grid.ID = "Grid";
			Grid.Caption = "Employee Price";
			Grid.AutoGenerateColumns = false;
			Grid.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
			Grid.ForeColor = System.Drawing.Color.Black;
			Grid.HeaderStyle.BackColor = System.Drawing.Color.Gray;
			Grid.BorderColor = System.Drawing.Color.Black;
			Grid.Width = 650;
			Grid.RowStyle.BackColor = System.Drawing.Color.Gainsboro;
			Grid.CellPadding = 4;
			Grid.CellSpacing = 4;

			Grid.ShowFooter = false;
			TemplateField tf = null;
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("Sku", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("Sku", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("Description", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("Description", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("QAV", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("QAV", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("Price", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("Price", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("Min", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("Min", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("Inner", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("Inner", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("Case", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("Case", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);
			tf = new TemplateField();
			tf.HeaderTemplate = new DynamicGridViewTextTemplate("PLine", DataControlRowType.Header);
			tf.ItemTemplate = new DynamicGridViewTextTemplate("PLine", DataControlRowType.DataRow);
			Grid.Columns.Add(tf);

			if (ViewState["employeePriceTable"] != null)
			{
				employeePriceTable = (System.Data.DataTable)ViewState["employeePriceTable"];
			}

			using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
			{
				System.Data.SqlClient.SqlDataReader reader2 = null;
				System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

				String query = "SELECT ItemNumber, ItemDescription, QtyOnHand, QtyOnSalesOrder, AveCost, QuotedEach, salesUnit, MinSalesUnitInner, MinSalesUnitPurchase, ProductLine FROM vw_Item_Setup where ItemNumber=@Entered_Sku and active ='Y' and ItemNumber like '3%' and  (Productline<>'dc' or Productline<>'hold')";
				cmd2.CommandText = query;
				cmd2.CommandType = System.Data.CommandType.Text;
				cmd2.Connection = conn;
				cmd2.Parameters.Add("@Entered_Sku", System.Data.SqlDbType.VarChar, 30);
				cmd2.Parameters["@Entered_Sku"].Value = EnteredSku;
				conn.Open();
				reader2 = cmd2.ExecuteReader();

				if (reader2.HasRows == true)
				{
					while (reader2.Read())
					{
						employeePriceTable.Rows.Add();
						if (!(reader2["ItemNumber"]).Equals(System.DBNull.Value))
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Sku"] = (String)(reader2["ItemNumber"]);
						}
						else
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["ItemNumber"] = "Not found";
						}
						if (!(reader2["ItemDescription"]).Equals(System.DBNull.Value))
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Description"] = (String)(reader2["ItemDescription"]);
						}
						else
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Description"] = "Not found";
						}

						if (!(reader2["QtyOnHand"]).Equals(System.DBNull.Value))
						{
							if (!(reader2["QtyOnSalesOrder"]).Equals(System.DBNull.Value))
							{
								employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["QAV"] = System.Convert.ToString(Math.Round((decimal)(reader2["QtyOnHand"])) - Math.Round((decimal)(reader2["QtyOnSalesOrder"])));
							}
							else
							{
								employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["QAV"] = System.Convert.ToString(Math.Round((decimal)(reader2["QtyOnHand"])));
							}
						}
						else
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["QAV"] = "Not found";
						}
						if (!(reader2["AveCost"]).Equals(System.DBNull.Value))
						{
							if ((decimal)(reader2["AveCost"]) >= 0)
							{
								employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Price"] = System.Convert.ToString((Math.Round((decimal)(reader2["AveCost"]) * 1000) / 1000) * 1.1m);
							}
							else
							{
								if (!(reader2["QuotedEach"]).Equals(System.DBNull.Value))
								{
									employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Price"] = System.Convert.ToString((Math.Round((decimal)(reader2["QuotedEach"]) * 1000) / 1000) * 1.1m);
								}
								else
								{
									employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Price"] = "Not found";
								}
							}
						}
						if (!(reader2["salesUnit"]).Equals(System.DBNull.Value))
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Min"] = (String)(reader2["salesUnit"]);
						}
						else
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Min"] = "Not found";
						}
						if (!(reader2["MinSalesUnitInner"]).Equals(System.DBNull.Value))
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Inner"] = System.Convert.ToString(Math.Round((decimal)(reader2["MinSalesUnitInner"])));
						}
						else
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Inner"] = "Not found";
						}
						if (!(reader2["MinSalesUnitPurchase"]).Equals(System.DBNull.Value))
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Case"] = System.Convert.ToString(Math.Round((decimal)(reader2["MinSalesUnitPurchase"])));
						}
						else
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["Case"] = "Not found";
						}
						if (!(reader2["ProductLine"]).Equals(System.DBNull.Value))
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["PLine"] = (String)(reader2["ProductLine"]);
						}
						else
						{
							employeePriceTable.Rows[employeePriceTable.Rows.Count - 1]["PLine"] = "Not found";
						}
					}
				}
				else sResult = "false";

				reader2.Close();
				cmd2.Parameters.Clear();
				cmd2.Cancel();
				cmd2.Dispose();
				conn.Close();

				Grid.DataSource = employeePriceTable;
				Grid.DataBind();
				GridHolder.Controls.Add(Grid);

				//An empty result string means we didn't fail.
				if (sResult == "")
				{
					ViewState["employeePriceTable"] = employeePriceTable;
					return null;
				}
				else
				{
					return "Sku not found.";
				}
			}
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
    }
    /*
    public Boolean FindBinLocation(String EnteredSku)
    {
        Boolean result = false;
        binGrid.ID = "binGrid";
        binGrid.Caption = "Bin Locations";
        binGrid.AutoGenerateColumns = false;
        binGrid.AlternatingRowStyle.BackColor = System.Drawing.Color.White;
        binGrid.ForeColor = System.Drawing.Color.Black;
        binGrid.HeaderStyle.BackColor = System.Drawing.Color.Gray;
        binGrid.BorderColor = System.Drawing.Color.Black;
        binGrid.Width = 650;
        binGrid.RowStyle.BackColor = System.Drawing.Color.Gainsboro;
        binGrid.CellPadding = 4;
        binGrid.CellSpacing = 4;

        binGrid.ShowFooter = false;
        TemplateField tf = null;
        tf = new TemplateField();
        tf.HeaderTemplate = new DynamicGridViewTextTemplate("Sku", DataControlRowType.Header);
        tf.ItemTemplate = new DynamicGridViewTextTemplate("Sku", DataControlRowType.DataRow);
        binGrid.Columns.Add(tf);
        tf = new TemplateField();
        tf.HeaderTemplate = new DynamicGridViewTextTemplate("BinLocation", DataControlRowType.Header);
        tf.ItemTemplate = new DynamicGridViewTextTemplate("BinLocation", DataControlRowType.DataRow);
        binGrid.Columns.Add(tf);
        tf = new TemplateField();
        tf.HeaderTemplate = new DynamicGridViewTextTemplate("quantityonhand", DataControlRowType.Header);
        tf.ItemTemplate = new DynamicGridViewTextTemplate("quantityonhand", DataControlRowType.DataRow);
        binGrid.Columns.Add(tf);
        tf = new TemplateField();
        tf.HeaderTemplate = new DynamicGridViewTextTemplate("WarehouseCode", DataControlRowType.Header);
        tf.ItemTemplate = new DynamicGridViewTextTemplate("WarehouseCode", DataControlRowType.DataRow);
        binGrid.Columns.Add(tf);
        if (ViewState["binLocationTable"] != null)
        {
            binLocationTable = (System.Data.DataTable)ViewState["binLocationTable"];
        }


        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
        {
            System.Data.SqlClient.SqlDataReader reader2 = null;
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

            String query = "SELECT * FROM IM_ITEMWAREHOUSE where ItemCode=@Entered_Sku";
            cmd2.CommandText = query;
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = conn;
            cmd2.Parameters.AddWithValue("@Entered_Sku", EnteredSku);
            conn.Open();
            reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                employeePriceTable.Rows.Add();
                if (!(reader2["ItemCode"]).Equals(System.DBNull.Value))
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["Sku"] = (String)(reader2["ItemCode"]);
                    result = true;
                }
                else
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["Sku"] = "Not found";
                }
                if (!(reader2["BinLocation"]).Equals(System.DBNull.Value))
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["BinLocation"] = (String)(reader2["BinLocation"]);
                }
                else
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["BinLocation"] = "Not found";
                }

                if (!(reader2["quantityonhand"]).Equals(System.DBNull.Value))
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["quantityonhand"] = System.Convert.ToString(Math.Round((decimal)(reader2["quantityonhand"])));
                }
                else
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["quantityonhand"] = "Not found";
                }
                if (!(reader2["WarehouseCode"]).Equals(System.DBNull.Value))
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["WarehouseCode"] = (String)(reader2["WarehouseCode"]);
                }
                else
                {
                    binLocationTable.Rows[binLocationTable.Rows.Count - 1]["WarehouseCode"] = "Not found";
                }
            }
            reader2.Close();
            cmd2.Parameters.Clear();
            cmd2.Cancel();
            cmd2.Dispose();
            conn.Close();
            binGrid.DataSource = binLocationTable;
            binGrid.DataBind();
            GridHolder2.Controls.Add(binGrid);
            ViewState["binLocationTable"] = binLocationTable;
            BinLocationItemCode.Text = "Enter Sku";
            return result;
        }
    }
    */
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
    public class DynamicGridViewTextTemplate : ITemplate
    {

        string _ColName;

        DataControlRowType _rowType;

        int _Count;

        public DynamicGridViewTextTemplate(string ColName, DataControlRowType RowType)
        {

            _ColName = ColName;

            _rowType = RowType;

        }

        public DynamicGridViewTextTemplate(DataControlRowType RowType, int ArticleCount)
        {

            _rowType = RowType;

            _Count = ArticleCount;

        }

        public void InstantiateIn(System.Web.UI.Control container)
        {

            switch (_rowType)
            {

                case DataControlRowType.Header:

                    Literal lc = new Literal();

                    lc.Text = "<b>" + _ColName + "</b>";

                    container.Controls.Add(lc);

                    break;

                case DataControlRowType.DataRow:

                    Label lbl = new Label();

                    lbl.DataBinding += new EventHandler(this.lbl_DataBind);

                    container.Controls.Add(lbl);

                    break;

                case DataControlRowType.Footer:

                    Literal flc = new Literal();

                    flc.Text = "<b>Total No of Articles:" + _Count + "</b>";

                    container.Controls.Add(flc);

                    break;

                default:

                    break;

            }

        }





        private void lbl_DataBind(Object sender, EventArgs e)
        {

            Label lbl = (Label)sender;

            GridViewRow row = (GridViewRow)lbl.NamingContainer;

            lbl.Text = DataBinder.Eval(row.DataItem, _ColName).ToString();

        }



    }
    public class DynamicGridViewURLTemplate : ITemplate
    {

        string _ColNameText;

        string _ColNameURL;

        DataControlRowType _rowType;



        public DynamicGridViewURLTemplate(string ColNameText, string ColNameURL, DataControlRowType RowType)
        {

            _ColNameText = ColNameText;

            _rowType = RowType;

            _ColNameURL = ColNameURL;

        }

        public void InstantiateIn(System.Web.UI.Control container)
        {

            switch (_rowType)
            {

                case DataControlRowType.Header:

                    Literal lc = new Literal();

                    lc.Text = "<b>" + _ColNameURL + "</b>";

                    container.Controls.Add(lc);

                    break;

                case DataControlRowType.DataRow:

                    HyperLink hpl = new HyperLink();

                    hpl.Target = "_blank";

                    hpl.DataBinding += new EventHandler(this.hpl_DataBind);

                    container.Controls.Add(hpl);

                    break;

                default:

                    break;

            }

        }



        private void hpl_DataBind(Object sender, EventArgs e)
        {

            HyperLink hpl = (HyperLink)sender;

            GridViewRow row = (GridViewRow)hpl.NamingContainer;

            hpl.NavigateUrl = DataBinder.Eval(row.DataItem, _ColNameURL).ToString();

            hpl.Text = "<div class=\"Post\"><div class=\"PostTitle\">" + DataBinder.Eval(row.DataItem, _ColNameText).ToString() + "</div></div>";

        }

    }

}
