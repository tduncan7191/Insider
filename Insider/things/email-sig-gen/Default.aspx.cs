using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                tbFirstName.Text = Request.Cookies["firstname"].Value;
                tbLastName.Text = Request.Cookies["lastname"].Value;

            }
            catch (Exception)
            {


            }

        }



    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string eMail = Request.Cookies["email"].Value;
        string contactID = Request.Cookies["contactid"].Value;

        if (tbPhone.Text == "" || tbPhone.Text == " ")
        {
            ltlPhone.Text = "";
        }
        else
        {
            tbPhone.Text = tbPhone.Text.Replace(" ", "");
            tbPhone.Text = tbPhone.Text.Replace("-", ".");
            tbPhone.Text = tbPhone.Text.Replace("(", "");
            tbPhone.Text = tbPhone.Text.Replace(")", ".");
            tbPhone.Text = tbPhone.Text.Replace(".", "");

            string areaCode = tbPhone.Text.Substring(0, 3);
            string prefix = tbPhone.Text.Substring(3, 3);
            string extension = tbPhone.Text.Substring(6);
            string phoneValue = areaCode + prefix + extension;
            ltlPhone.Text = "T: <a href='tel:+1" + phoneValue + "' value='+1" + phoneValue + "' target='_blank'>" + areaCode + '.' + prefix + '.' + extension + "</a>";
            tbPhone.Text = areaCode + '.' + prefix + '.' + extension;
        }


        if (tbMobile.Text == "" || tbMobile.Text == " ")
        {
            ltlMobile.Text = "";
        }
        else
        {
            tbMobile.Text = tbMobile.Text.Replace(" ", "");
            tbMobile.Text = tbMobile.Text.Replace("-", "");
            tbMobile.Text = tbMobile.Text.Replace("(", "");
            tbMobile.Text = tbMobile.Text.Replace(")", "");
            tbMobile.Text = tbMobile.Text.Replace(".", "");

            string areaCode = tbMobile.Text.Substring(0, 3);
            string prefix = tbMobile.Text.Substring(3, 3);
            string extension = tbMobile.Text.Substring(6);
            string mobileValue = areaCode + prefix + extension;
            ltlMobile.Text = "M: <a href='tel:+1" + mobileValue + "' value='+1" + mobileValue + "' target='_blank'>" + areaCode + '.' + prefix + '.' + extension + "</a>";
            tbMobile.Text = areaCode + '.' + prefix + '.' + extension;
        }

        if (tbFax.Text == "" || tbFax.Text == " ")
        {
            ltlFax.Text = "";
        }
        else
        {
            tbFax.Text = tbFax.Text.Replace(" ", "");
            tbFax.Text = tbFax.Text.Replace("-", ".");
            tbFax.Text = tbFax.Text.Replace("(", "");
            tbFax.Text = tbFax.Text.Replace(")", ".");
            tbFax.Text = tbFax.Text.Replace(".", "");

            string areaCode = tbFax.Text.Substring(0, 3);
            string prefix = tbFax.Text.Substring(3, 3);
            string extension = tbFax.Text.Substring(6);
            string faxValue = areaCode + prefix + extension;
            ltlFax.Text = "F: <a href='tel:+1" + faxValue + "' value='+1" + faxValue + "' target='_blank'>" + areaCode + '.' + prefix + '.' + extension + "</a>";
            tbFax.Text = areaCode + '.' + prefix + '.' + extension;
        }
        if (tbLinkedIn.Text == "")
        {
            ltlLinkedIn.Text = "<a href='http://www.linkedin.com/company/redemption-plus' style='font-family: 'Times New Roman'; font-size: medium' target='_blank'>" +
                                        "<img src='http://redemptionplus.com/Images/LinkedIn-35w.png' width='25px' style='margin-top: 5px; margin-left:2px;'></a>";
        }

        else
        {
            ltlLinkedIn.Text = "<a href='http://www.linkedin.com/in/" + tbLinkedIn.Text + "' style='font-family: 'Times New Roman'; font-size: medium;' target='_blank'>" +
                                       " <img src='http://redemptionplus.com/Images/LinkedIn-35w.png' width='25px' style='margin-top: 5px; margin-left:2px;'></a>";
        }

        if (contactID == "11" || contactID == "65")
        {
            ltlPinnacle.Text = "<br /><a href='http://www.grouppinnacle.com/' style='font-size:1em;' target='_blank'>www.grouppinnacle.com</a>";

        }
        else
        {
            ltlPinnacle.Text = "";
        }

        ltlEmail.Text = "<br /><a href='mailto:" + eMail + " style='font-family: Verdana; ' target='_blank'>" + eMail + "</a>";
        lblTitle.Text = tbTitle.Text;



        lblFirstName.Text = tbFirstName.Text;




        lblFirstName.Text = tbFirstName.Text;
        lblLastName.Text = tbLastName.Text;



        pnlSignature.Visible = true;





    }
}
