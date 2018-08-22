<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="emailGen.css" rel="Stylesheet" type="text/css" />
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Email Signature Generator" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1 {
            width: 385px;
        }

        .style2 {
            width: 115px;
        }
    </style>
</head>
<body>

   
    <div id="container">
        <form id="form1" runat="server">
             <asp:MultiView ID="mvLoggedIn" runat="server" ActiveViewIndex="0"><asp:View ID="vwLoggedIn" runat="server">
            <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Email Signature Generator" />
            <div id="main">
                <div class="contentBlock">
                    <h3>Name and Job Title</h3>
                    <asp:TextBox ID="tbFirstName" runat="server" PlaceHolder="First Name" CssClass="buffer"></asp:TextBox>
                    <asp:TextBox ID="tbLastName" runat="server" PlaceHolder="Last Name" CssClass="buffer"></asp:TextBox>


                    <asp:TextBox ID="tbTitle" runat="server" Placeholder="Title" CssClass="buffer"></asp:TextBox><br />
                    <h3>Phone Numbers <span style="font-size: .75em;">(913.563.xxxx)</span></h3>
                    <asp:TextBox ID="tbPhone" runat="server" placeholder="Direct Line" CssClass="buffer"></asp:TextBox>
                    
                    <asp:TextBox ID="tbMobile" runat="server" Placeholder="Mobile" CssClass="buffer"></asp:TextBox>
                    <asp:TextBox ID="tbFax" runat="server" PlaceHolder="Fax" CssClass="buffer"></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="regexPhone" runat="server" ValidationExpression="^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$" ControlToValidate="tbPhone" ErrorMessage="Invalid Phone Number" Display="Dynamic"></asp:RegularExpressionValidator>
                
                    <asp:RegularExpressionValidator ID="regexMobile" runat="server" ValidationExpression="^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$" ControlToValidate="tbMobile" ErrorMessage="<br />Invalid Mobile Number" Display="Dynamic"></asp:RegularExpressionValidator>
                 
                     <asp:RegularExpressionValidator ID="regexFax" runat="server" ValidationExpression="^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$" ControlToValidate="tbFax" ErrorMessage="<br />Invalid Fax Number" Display="Dynamic"></asp:RegularExpressionValidator>
                    
                    <h3>LinkedIn.com/in/</h3>

                    <asp:TextBox ID="tbLinkedIn" runat="server" Placeholder="LinkedIn Address" CssClass="buffer"></asp:TextBox>

                    <asp:Button ID="btnSubmit" runat="server" Text="submit" OnClick="btnSubmit_Click" />
                    <br />
                    <br />
                    <asp:Panel ID="pnlSignature" runat="server" Visible="false">
                        <h3>Copy and paste into the signature area of your email settings.</h3>
                        <br />
                        <div>
                            <table id="sig">

                                <tbody>
                                    <tr>
                                        <td style="background-color: #AFDA0F; border-radius: 10px 0 0 0; ">
                                            <table style="background-color: white; margin-top: 4px; margin-left: 4px; ">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <b>
                                                                <asp:Label ID="lblFirstName" runat="server" Text="" CssClass="labels"></asp:Label>
                                                                <asp:Label ID="lblLastName" runat="server"  Text=""></asp:Label>
                                                            </b>


                                                            <div class="title">
                                                                <asp:Label ID="lblTitle" runat="server" Text="" CssClass="labels"></asp:Label>
                                                            </div>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="contact">
                                                                <asp:Literal ID="ltlPhone" runat="server" ></asp:Literal>
                                                                <asp:Literal ID="ltlMobile" runat="server"></asp:Literal>
                                                                <asp:Literal ID="ltlFax" runat="server"></asp:Literal>



                                                                <!--<asp:Literal ID="ltlEmail" runat="server"></asp:Literal>-->

<br />
                                                                <a href="http://www.redemptionplus.com" style="font-size:1em;" target="_blank">www.redemptionplus.com</a><asp:Literal ID="ltlPinnacle" runat="server"></asp:Literal>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Literal ID="ltlLinkedIn" runat="server" ></asp:Literal>
                    	<a href="http://tinyurl.com/hvtamwe" style="font-family: 'Times New Roman'; font-size: medium" target="_blank"><img src="http://redemptionplus.com/Images/Fbook-35w.png" width="25px" style="margin-top: 5px"></a>
					<%--<a href="http://www.twitter.com/redemptionplus" style="font-family: 'Times New Roman'; font-size: medium" target="_blank"><img src="http://redemptionplus.com/Images/twitter_logo.jpg" width="25px" style="margin-top: 5px"></a>--%>
						<a href="http://rtown.redemptionplus.com" style="font-family: 'Times New Roman'; font-size: medium" target="_blank"><img src="http://www.redemptionplus.com/Images/Rtown-35w.png" width="25px" style="margin-top: 5px"></a>
                    <%--<a href="http://www.facebook.com/pages/Redemption-Plus/103633231297" style="font-family: 'Times New Roman'; font-size: medium" target="_blank"><img src="http://redemptionplus.com/Images/Fbook-35w.png" width="25px" style="margin-top: 5px"></a>--%>
					<%--<a href="http://www.pinterest.com/redemptionplus" style="font-family: 'Times New Roman'; font-size: medium" target="_blank"><img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Pinterest35x.png" width="25px" style="margin-top: 5px"></a>--%>
				    <%--<a href="https://instagram.com/redemptionplus/" style="font-family: 'Times New Roman'; font-size: medium" target="_blank"><img src="https://s3-us-west-2.amazonaws.com/smilesite/images/Instagram_Icon_sm.png" width="25px" style="margin-top: 5px"></a>--%>
				    <%--<a href="http://www.tibpp.org/" style="font-family: 'Times New Roman'; font-size: medium" target="_blank"><img src="https://s3-us-west-2.amazonaws.com/smilesite/images/tibpp.png" width="25px" style="margin-top: 5px"></a>--%>
                        

                                                        </td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                                                    </tbody>
                            </table>
                            <span style="float:right; width:300px;margin-right: 25px;margin-top: -10px;" >
                            <--- Click and drag from here to the top left corner to copy.</span>
                        </div>
                    </asp:Panel>




                </div>


                <div class="directionsContainer">
                    <div class="directions">
                        <h3>Basic Instructions</h3>
                        <ul class="directions">
                            <li class="directions">Tell 'em your name.</li>
                            <li class="directions">Tell 'em what you do here.</li>
                            <li class="directions">Give 'em your number. <em>(At least one)</em></li>
                            <li class="directions">Let 'em know you're on LinkedIn.
                                <br />
                                <em>(If left blank, they'll still get a link to our company page.)</em></li>

                        </ul>
                        <b>Side note-</b> If you're using the Esna for Cisco plugin, you may want to turn it off during this process.
                    </div>
                </div>
                <div style="clear: both;"></div>
            </div>
            <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
                 </asp:View>
    <asp:View ID="vwNotLoggedIn" runat="server">
         <customControls:ctlContentHeader ID="ctlContentHeader2" runat="server" heading="Email Signature Generator" />
        <div id="main2">
                <div class="contentBlock">
                    <h3>Members Only!</h3>                    
                    <h4>For some reason, you aren't logged in. Please return to the login screen.</h4>
                
                </div>
        </div>


    </asp:View></asp:MultiView>
        </form>
    </div>
</body>
</html>
