using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NinetyFiver_of_the_Month : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "myKey", "return fn95erVerify();");
		SqlDataSource1.InsertParameters.Add("NominatorId", ((Request.Cookies["contactid"] == null) ? "" : Request.Cookies["contactid"].Value));
    }
    protected void DetailView_ItemInserted(Object sender, DetailsViewInsertedEventArgs e)
    {
        if (e.Exception == null && e.AffectedRows == 1)
        {
            alert1.Text = "Your Nomination has been added. You can add another.";
            alert1.Visible = true;
        }
    }
    protected void DetailView_ItemCommand(Object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
			Response.Redirect("~/");
        }
    }
}
