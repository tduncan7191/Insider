using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
	public string sCategory = null;    
	public int iCatID = 0;
	public int iPageNum = 1;
	public int iMaxRecords = 10;
	
	protected void Page_Load(object sender, EventArgs e)
    {
		if (Request.QueryString["iCatID"] != null) iCatID = Convert.ToInt32(clsRPlus.fnVal(Request.QueryString["iCatID"]));
		if (Request.QueryString["iPageNum"] != null) iPageNum = Convert.ToInt32(clsRPlus.fnVal(Request.QueryString["iPageNum"]));
		if (iCatID > 0)
		{
			using (System.Data.SqlClient.SqlConnection oCN = clsRPlus.fnOpenCN(clsRPlus.enumDBs.INSIDER))
			{
				using (System.Data.SqlClient.SqlCommand oCMD = new System.Data.SqlClient.SqlCommand())
				{
					oCMD.Connection = oCN;
					oCMD.CommandText = "SELECT sName FROM qa_category"
										+ " WHERE iCatID = @iCatID";
					oCMD.Parameters.AddWithValue("@iCatID", Request.QueryString["iCatID"].ToString());

					using (System.Data.SqlClient.SqlDataReader oDR = oCMD.ExecuteReader())
					{
						while (oDR.Read())
							sCategory = oDR["sName"].ToString();
					}
				}
			}
		}
    }
}