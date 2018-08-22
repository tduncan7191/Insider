using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {

            string prm = string.Empty;
            string frstprm = txtParam.Text;
            int count = 0;
            //Seprate the text box value in single value and store in string type array
            string[] words = frstprm.Split(',');

            //Loop through the Array Element
            foreach (string word in words)
            {
                string w = word;
                if (count == 0)
                {
                    w = w.Trim();
                    prm += "'" + w + "'";
                }
                else
                {
                    w = w.Trim();
                    prm += "," + "'" + w + "'";
                }
                count++;
            }
            //Getting connection string from database
            string str = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            //Open connection
            con.Open();
            string sql = "update [SMILEWEB_live].[dbo].[TaskQueue] set IsExecuted = null from [SMILEWEB_live].[dbo].[TaskQueue] TQ inner join [SMILEWEB_live].[dbo].[ProductLabelRequest] PLR on PLR.[key] = TQ.[Param0] inner join SMILEWEB_live..OrderLog OL on OL.[key] = PLR.OrderLogKey where TaskId = 'ProductLabel::GeneratePdf' and OrderId in" + "(" + prm + ")";
            SqlCommand cmd = new SqlCommand(sql, con);
            //Executing Commnad
            int i = cmd.ExecuteNonQuery();

            //Print result on lable with number of rows effected.
            lblResult.Text = "Query Executed Successfully " + i + " Rows effected";
            
        }
        catch (Exception ex)
        {
            lblResult.Text = ex.Message.ToString();

        }
    }
}