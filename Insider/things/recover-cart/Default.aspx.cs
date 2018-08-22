using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Default2 : System.Web.UI.Page
{
    

    private class Uid
    {
        public string userID { get; set; }
        public string accountID { get; set; }
        public string acctConID { get; set; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        hdnState.Value = restored.Attributes["class"];
      


    }


    protected void gvAbandonedCarts_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSelectedCart.Visible = true;
        btnRecover.Visible = true;
        dvCartInfo.Visible = true;

    }
    protected void gvAbandonedCarts_PageIndexChanged(object sender, EventArgs e)
    {
        gvSelectedCart.Visible = false;
        btnRecover.Visible = false;
        dvCartInfo.Visible = false;
        
    }
    protected void btnRecover_Click(object sender, EventArgs e)
    {


        string cartKey = gvAbandonedCarts.SelectedValue.ToString();
        SqlConnection con = new SqlConnection(dsAbandonedCarts.ConnectionString);
        SqlCommand cmd = new SqlCommand("prc_Insider_RecoverCart", con);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        SqlParameter nKey = cmd.Parameters.Add("@nKey", System.Data.SqlDbType.Int);
        nKey.Direction = System.Data.ParameterDirection.Input;
        nKey.Value = gvAbandonedCarts.SelectedValue;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Uid sfKeys = new Uid();

        using (con)
        {
            con.Open();

            using (SqlCommand cmd2 = new SqlCommand("Exec prc_Insider_GetKeys @nKey", con))
            {
                cmd2.Parameters.AddWithValue("@nKey", gvAbandonedCarts.SelectedValue);
                using (SqlDataReader rdr = cmd2.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        sfKeys.accountID = rdr["AcctID"].ToString();
                        sfKeys.acctConID = rdr["ContactID"].ToString();
                        sfKeys.userID = rdr["UserID"].ToString();

                    }

                }


            }




        }
        string href="<a href='http://www.redemptionplus.com/Login/Salesforce.asp?Sfuid=";
     
        ltlLink.Text = href + sfKeys.userID + "-" + sfKeys.accountID + "-" + sfKeys.acctConID + "' target='_blank' class='smileLink'>Click here</a> to log in to SMILE ";
        
        
            restored.Attributes["class"] = "restored";
          


    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        restored.Attributes["class"] = "hidden";
        gvAbandonedCarts.DataBind();
        gvSelectedCart.Visible = false;
        btnRecover.Visible = false;
        dvCartInfo.Visible = false;
    }
}
