using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class QuoteMaint : System.Web.UI.Page
{
    private DataSet ds = null;
    private string fileName = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        fileName = Server.MapPath("App_Data") + "\\Quotes.xml";

        dgQuotes.RowEditing += new GridViewEditEventHandler(dgQuotes_RowEditing);
        dgQuotes.RowUpdating += new GridViewUpdateEventHandler(dgQuotes_RowUpdating);
        dgQuotes.RowCancelingEdit += new GridViewCancelEditEventHandler(dgQuotes_RowCancelingEdit);
        dgQuotes.RowDeleting += new GridViewDeleteEventHandler(dgQuotes_RowDeleting);

        btnSubmit.Click += new EventHandler(btnSubmit_Click);

        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }

    void dgQuotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int quoteID = 0;
        if (int.TryParse(dgQuotes.Rows[e.RowIndex].Cells[0].Text, out quoteID))
        {
            DeleteQuote(quoteID);
            Response.Redirect(Request.RawUrl);
        }
    }

    void btnSubmit_Click(object sender, EventArgs e)
    {
        Quote quote = new Quote();
        quote.Text = txtQuote.Text;
        quote.Author = txtAuthor.Text;

        AddQuote(quote);
        Response.Redirect(Request.RawUrl);
    }

    void dgQuotes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    void dgQuotes_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Quote quote = new Quote();

        TextBox txtQuote = (TextBox)dgQuotes.Rows[e.RowIndex].Cells[1].Controls[0];
        quote.Text  = txtQuote.Text;

        TextBox txtAuthor = (TextBox)dgQuotes.Rows[e.RowIndex].Cells[2].Controls[0];
        quote.Author  = txtAuthor.Text;

        int id=0;
        string sId = dgQuotes.Rows[e.RowIndex].Cells[0].Text;

        if (int.TryParse(sId, out id))
        {
            quote.Id = id;
            UpdateQuote(quote);
        }

        Response.Redirect(Request.RawUrl);
    }

    void dgQuotes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgQuotes.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    private void BindGrid()
    {
        if (ds == null)
        {
            ds = new DataSet();  
            ds.ReadXml(fileName);
        }

        if (ds != null)
        {
            dgQuotes.DataSource = ds;
            dgQuotes.DataBind();
        }
    }

    private void UpdateQuote(Quote quote)
    {
        QuoteXml xml = new QuoteXml();
        xml.LoadXml(fileName);
        xml.UpdateQuote(quote);
    }

    private void AddQuote(Quote quote)
    {
        QuoteXml xml = new QuoteXml();
        xml.LoadXml(fileName);
        xml.AddQuote(quote);
    }

    private void DeleteQuote(int quoteID)
    {
        QuoteXml xml = new QuoteXml();
        xml.LoadXml(fileName);
        xml.DeleteQuote(quoteID);
    }

}
