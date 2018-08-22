using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class EgosAndExcuses_reply : System.Web.UI.Page
{
    int commentTypeReturn = 1;
    int replyType = 1;
    string[] commentTypeArray = { "Excuse", "Ego", "Recycled Excuse", "Recycled Ego" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Request.QueryString["id"] == null))
        {
            loadComment(Request.QueryString["id"]);
        }
        else
        {
            Response.Redirect("~//things/egos-and-excuses/");
        }
        if (!IsPostBack)
        {
            PopulateNames();
        }
        string s = commentTypeArray[commentTypeReturn - 1] + ":";
        commenttypeHeader.Text = commentTypeArray[commentTypeReturn - 1] + ":";

    }

    //new code for this box 
    private void PopulateNames()
    {
        //new code for this box

		using (DataClassesDataContext context = new DataClassesDataContext())
        {
            var contactQuery =
				from ic in context.Contacts
                where ic.status.Value == 0
                orderby ic.lastName
                select new { name = ic.lastName + ", " + ic.firstName, ic.contactID };
            
            DropDownList1.DataSource = contactQuery;
            DropDownList1.DataTextField = "name";
            DropDownList1.DataValueField = "contactID";
            DropDownList1.DataBind();
            System.Web.UI.WebControls.ListItem liFirst = new ListItem("Select Your Name", "");
            DropDownList1.AppendDataBoundItems = true;
            DropDownList1.Items.Insert(0, liFirst);
            DropDownList1.SelectedIndex = 0;

        }
    }
    //new code for this box
    private void SaveComment()
    {
        //new code for this box


        string s = DropDownList1.SelectedValue;
        if (DropDownList1.SelectedItem.Value != "")
        {
            if (ReplyComment.Text != "")
            {
                alert1.Visible = false;
				using (DataClassesDataContext context = new DataClassesDataContext())
                {
					comment newComment = new comment()
                    {
                        userid = Convert.ToInt32(DropDownList1.SelectedItem.Value),
						comment1 = ReplyComment.Text,
                        parent_comment_id = Convert.ToInt32(Request.QueryString["id"]),
                        topicId = replyType,
                        ts = DateTime.Now
                    };

					context.comments.InsertOnSubmit(newComment);
                    context.SubmitChanges();
                }
                ClearForm();
                loadComment(Request.QueryString["id"]);
            }
            else
            {
                alert1.Text = "Please enter text to recycle the " + commentTypeArray[commentTypeReturn - 1] +".";
                alert1.Visible = true;
            }
        }
        else
        {
            {
                alert1.Text = "You must select your Name.";
                alert1.Visible = true;
            }
        }
    }

    public void ClearForm()
    {
        CommentToReplyTo.Text = "";
        replyType = 0;
        ReplyComment.Text = "";
    }

    //new code for this box
    public void loadComment(String parameter)
    {
        //new code for this box
		using (DataClassesDataContext context = new DataClassesDataContext())
        {
			comment comment = (from p in context.comments
                                       where p.id == Convert.ToInt32(parameter)
                                       select p).First();

			CommentToReplyTo.Text = comment.comment1.ToString();
            commentTypeReturn = Convert.ToInt32(comment.topicId);
            if (commentTypeReturn == 1)
            {
                replyType = 3;
            }
            else
            {
                replyType = 4;
            }
			
			var commentReplies = from p in context.comments
								 join ct in context.comment_types on p.topicId equals ct.comment_type_id
								 join cont in context.Contacts on p.userid equals cont.contactID
                                 where p.parent_comment_id == Convert.ToInt32(parameter)
								 select new { p.comment2, ct.Comment_type1, cont.firstName, cont.lastName };

            commentRepliesRepeater.DataSource = commentReplies;
            commentRepliesRepeater.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SaveComment();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}