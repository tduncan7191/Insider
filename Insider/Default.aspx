<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - HomePage" />
    
    <script type="text/javascript">
    	$(document).ready(function () {
    		$("#featured").tabs({ fx: { opacity: "toggle" } });
    		$("#featured").tabs().tabs("rotate", 6500, true);
    	});
    </script>
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Home" />
        <div id="main">
            <div class="contentBlockFull" width="1024px">
                <customControls:QuoteWidget ID="QuoteWidget2" runat="server" Width="980px" BackColor="White"
                    QuoteColor="Black" AuthorColor="Green" QuoteFontName="Times New Roman" AuthorFontName="Times New Roman"
                    QuoteFontSize="Medium" AuthorFontSize="Medium" QuoteFontStyle="Italic" AuthorFontStyle="Bold"
                    Selection="Random" />
            </div>
            <div class="contentBlockFull" style="height: 266px">
                <div>
                    <div id="featured">
                        <ul class="ui-tabs-nav">
                            <li class="ui-tabs-nav-item ui-tabs-selected" id="Li1"><a href="#fragment-1">
                                <img src="/images/text-board-icon.png" alt="" /><span>Text Boards</span></a></li>
                            <li class="ui-tabs-nav-item" id="nav-fragment-2"><a href="#fragment-2">
                                <img src="/images/image2-small.jpg" alt="" /><span>95er Nomination</span></a></li>
                            <!--<li class="ui-tabs-nav-item" id="nav-fragment-3"><a href="#fragment-3">
                                <img src="/images/answers-icon.png" alt="" /><span>Answers</span></a></li>-->
                            <li class="ui-tabs-nav-item ui-tabs-selected" id="nav-fragment-4"><a href="#fragment-4">
                                <img src="/images/image1-small.jpg" alt="" /><span>Mission, Vision, Values</span></a></li>
                        </ul>
                            
                        <!-- Second Content -->
                        <div id="fragment-1" class="ui-tabs-panel ui-tabs-hide" style="">
                            <img src="/images/text-board-slide.png" alt="" />
                            <div class="info">
                                <h2>Text Boards</h2>
								<p style="text-align:center;font-weight:bold;">
									<a href="/people/text-boards/self-improvement/">Click here for ideas to improve your work.</a>
									<br />
									<a href="/people/text-boards/department-improvement/">Click here for ideas to improve your department's work.</a></p>
                            </div>
                        </div>
						<!-- Second Content -->
                        <div id="fragment-2" class="ui-tabs-panel ui-tabs-hide" style="">
                            <a href="/people/pcs-nomination/"><img src="/images/image2.jpg" alt="" /></a>
                            <div class="info">
                                <h2><a href="/people/pcs-nomination/">95er of the Month</a></h2>
                                <p>Submit your a nomination for who demonstrated the qualities of a 95er...<a href="/people/95er-of-the-month/">nominate someone</a></p>
                            </div>
                        </div>
                        <!-- Third Content -->
                       <!--<div id="fragment-3" class="ui-tabs-panel ui-tabs-hide" style="">
                            <a href="/answers/"><img src="/images/answers-slide.png" alt="" /></a>
                            <div class="info">
                                <h2><a href="/answers/">Redemption Plus Answers</a></h2>
                                <p>Find answers to common questions asked by Redemption Plus team members...<a href="/answers/">read more</a></p>
                            </div>
                        </div>-->
						<!-- Fourth Content -->
                        <div id="fragment-4" class="ui-tabs-panel" style="">
                            <a href="/things/mission-vision-values/"><img src="/images/image1.jpg" alt="" /></a>
                            <div class="info">
                                <h2><a href="/things/mission-vision-values/">Mission, Vision, Values</a></h2>
                                <p>These are the principles we operate by. Learn them, know them, live them...<a href="/things/mission-vision-values/">read more</a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="contentBlockFull">
                <script type="text/javascript">
                    var defaultText = "Enter Sku";
                    function WaterMark(txt, evt) {
                    	if (txt.value.length == 0 && evt.type == "blur") {
                    		txt.style.color = "gray";
                    		txt.value = defaultText;
                    	}
                    	if (txt.value == defaultText && evt.type == "focus") {
                    		txt.style.color = "black";
                    		txt.value = "";
                    	}
                    }
                </script>
                <img src="/images/PriceCheck.png" class="pageTitleIcon" alt="recycle icon" />
                <asp:Label ID="commenttypeHeader" runat="server" Text="Employee Price Check" CssClass="pageTitle" />
                <div class="nomDropdownlist">
                <asp:ScriptManager ID="MainScriptManager" runat="server" />
                <asp:UpdatePanel ID="pnlHelloWorld" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="enteredSkuBox" runat="server" Text="Enter Sku" ForeColor="Gray"
                            onblur="WaterMark(this, event);" onfocus="WaterMark(this, event);"></asp:TextBox>
                        <asp:Button runat="server" ID="btnHelloWorld" OnClick="getPrice_Click" Text="Check Price" /><asp:Label
                            ID="alert1" runat="server" Text="Label" Visible="false"></asp:Label>
                        </br>
                            </br></br>
                            <asp:PlaceHolder ID="GridHolder" runat="server"></asp:PlaceHolder>
                        </br>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
    </form>
</body>
</html>
