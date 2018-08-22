<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="controls_EmployeeContactList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="R+ Insider - Employee Contact List" />
</head>
<body>
<div id="container">
    <form id="form1" runat="server">
    <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="Employee Contact List" />
    <div id="main">
        <div class="contentBlockFull">
			<style type="text/css">
				#contact-us {margin:0 auto;width:800px;font-family:verdana, arial, sans-serif;}
				#contact-us h2 {margin:25px 0 0;color:#00388f;text-shadow:1px 1px 2px #cfdc35;}
				#contact-us a.top {display:block;text-decoration:none;color:#00388f;font-size:11px;}
				#contact-us a.top:hover {color:#ccc;}

				#contact-us .contact-wrap {position:relative;display:inline-block;width:245px;min-height:115px;margin:5px 0px;padding:5px;background-image:url('http://www.redemptionplus.com/_FileLibrary/FileImage/bg-Contact-Box.jpg');border:2px solid #51b6e2;border-radius:15px;box-shadow: 2px 2px 4px #eee;}
				#contact-us .contact-wrap:hover {background-image:url('http://www.redemptionplus.com/_FileLibrary/FileImage/bg-Contact-Box-Hover.jpg');border-color:#cfdc35}
				#contact-us .contact-wrap img {width:80px;height:106px;margin-right:10px;border-radius:15px;box-shadow:2px 2px 4px #ddd;}
				#contact-us .contact-wrap img.no-fun {display:inline-block;zoom:1;*display:inline;}
				#contact-us .contact-wrap img.fun {display:none;}
				#contact-us .contact-wrap:hover img.no-fun {display:none;}
				#contact-us .contact-wrap:hover img.fun {display:inline-block;zoom:1;*display:inline;}
	
				#contact-us .contact-info {z-index:10;vertical-align:top;position:relative;display:inline-block;zoom:1;*display:inline;}
				#contact-us .contact-info ul {display:inline-block;list-style-type:none;margin:0;padding:0;zoom:1;*display:inline;}
				#contact-us .contact-info ul li {font-size:11px;}
				#contact-us .contact-info ul li.name {font-size:13px;color:#00388f;font-weight:bold;text-shadow:1px 1px 0px #cfdc35;}
				#contact-us .contact-info ul li.title {width:140px;margin-bottom:5px;color:#00388f;font-size:10px;font-weight:bold;border-bottom:2px solid #333;}
				#contact-us .contact-info ul li.email {margin-top:3px;}
				#contact-us .contact-info ul li.vcard img {margin:-20px 0 0 0;padding:0;border:0;width:25px;height:25px;box-shadow:none;}
				#contact-us .contact-info ul li a {display:block;margin:0;color:#00388f;text-decoration:none;}
				#contact-us .contact-info ul li a:hover {color:#aaa;}

				#contact-us .contact-popup {z-index:50;display:none;position:absolute;width:185px;left:245px;top:10px;padding:0 10px 15px 10px;background-color:#f6f6f6;border:1px solid #ccc;border-radius:8px;box-shadow:0px 0px 3px #ccc;}
				#contact-us .contact-popup h3 {margin:15px 0 5px 0;padding:0px;font-size:11px;line-height:13px;font-weight:bold;color:#361683;}
				#contact-us .contact-popup div {font-size:10px;color:#6e0561;}
				#contact-us .contact-popup img {display:block;margin:10px auto 0;width:165px;height:165px;border:none;border-radius:0;box-shadow:none;}
				#contact-us .contact-popup span {display:block;text-align:center;font-size:10px;font-weight:normal;color:#000;}

				#contact-us .contact-quick {margin:0 auto;padding:0;text-align:center;}
				#contact-us .contact-quick .header {margin:0;padding:2px;border-style:solid;border-width:2px 1px 3px 3px;border-color:#cfdc35;border-radius:7px;box-shadow:-3px 3px 5px #00388f;}
				#contact-us .contact-quick a {display:block;margin:10px;padding:2px 5px;text-decoration:none;color:#00388f;font-size:10px;box-shadow:-1px 1px 1px 2px #00388f;border-radius:5px;}
				#contact-us .contact-quick a:hover {background-color:#cfdc35;}

				#contact-us .contact-wrap:hover {background-color:#f1f1f1;}
				#contact-us .contact-wrap:hover .contact-popup {display:block;}
				#contact-us .contact-popup:hover {display:block;}

				#contact-us .address-info {margin:0;padding:15px;font-size:11px;color:#777;}
				#contact-us .address-info h3 {margin:0;color:#00388f;font-size:12px;font-weight:bold;}
				#contact-us .address-info div div {margin-bottom:10px;}
				#contact-us .address-info div a {color:#00388f;}

				#div-fraContact {z-index:100;position:absolute;display:none;width:310px;background-color:#51b6e2;border:2px solid #cfdc35;border-radius:8px;box-shadow:2px 2px 5px #999;}
				#div-fraContact div {float:right;cursor:pointer;width:75px;padding:5px;color:#fff;text-align:center;font-size:10px;font-family:Verdana, Arial, Sans-serif;font-weight:bold;background-color:#cc0000;border-radius:0 5px 0 5px;}
				#fraContact {width:310px;height:325px;}

				.clear {clear:both;line-height:0;height:0;font-size:0;}
			</style>
			<script type="text/javascript">
				function ObjectPosition(obj) {
					var curleft = 0;
					var curtop = 0;
					if (obj.offsetParent) {
						do {
							curleft += obj.offsetLeft;
							curtop += obj.offsetTop;
						} while (obj = obj.offsetParent);
					}
					return [curleft, curtop];
				}
			</script>
			<div id="contact-us">
				<div class="address-info">
					<div style="float:left;margin:0 25px 15px 0;">
						<h3>Toll-Free: </h3>
						<div>(888) 564-7587</div>
						<h3>Local Phone: </h3>
						<div>(913) 563-4300</div>
						<h3>Fax: </h3>
						<div>(913) 563-4301</div>
						<h3>Email: </h3>
						<div><a href="mailto:info@redemptionplus.com">Click here to email us</a></div>
						<h3>Address:</h3>
						<div>9829 Commerce Parkway<br>Lenexa, Kansas 66219-2401<br><a href="http://maps.google.com/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=Redemption+Plus,+Commerce+Parkway,+Lenexa,+KS&amp;aq=0&amp;oq=Redemption+Plus,+&amp;sll=37.0625,-95.677068&amp;sspn=57.553742,79.013672&amp;ie=UTF8&amp;hq=Redemption+Plus,&amp;hnear=Commerce+Pkwy,+Lenexa,+Johnson,+Kansas+66219&amp;t=m&amp;cid=7896531052624277436&amp;ll=38.964748,-94.764633&amp;spn=0.106778,0.20668&amp;z=11" target="_blank">View Map</a></div>
					</div>
					<div style="float:right;">
						<div class="contact-quick">
							<div class="header">
								<a href="#Sales">Sales</a>
								<a href="#Pinnacle">Pinnacle Entertainment Advisors</a>
								<a href="#Marketing">Marketing</a>
								<a href="#PS">Product Solutions</a>
								<a href="#Finance">Finance &amp; Administration</a>
								<a href="#IT">Information Technology</a>
								<a href="#Distribution">Distribution</a>
							</div>
						</div>
					</div>
					<div class="clear">&nbsp;</div>
				</div>
	
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ron-hill.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ron-hill-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Ron Hill</li>
							<li class="title">President - CEO</li>
							<li>Phone: (913) 563-4302</li>
							<li>Fax: (913) 273-1968</li>
							<li class="email"><a href="mailto:ronh" onclick="fraEmailClick(this);return false;">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/ron-hill-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why?</h3>
						<div>Mark Spitz.  Watching him win those 7 gold medals inspired me to become a swimmer which made a profound impact on my life.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ron-hill-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>

				<!--
				***** CONTACT TEMPLATE *****
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/first-last.jpg" class="no-fun" />
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/first-last-fun.jpg" class="fun" />
					<div class="contact-info">
						<ul>
							<li class="name">Name</li>
							<li class="title">Title</li>
							<li>Phone: (913) 563-4300</li>
							<li>Cell: (913) 563-4300</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:flast@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/first-last-vcard-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png" /></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>Question?</h3>
						<div>Answer</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/first-last-qr.jpg" />
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				***** END CONTACT TEMPLATE *****
				-->
				<h2 id="Sales">Sales</h2>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jason-patterson.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jason-patterson-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Jason Patterson</li>
							<li class="title">Director of Customer Success</li>
							<li>Phone: (877) 563-3543</li>
							<li>Cell: (816) 824-6282</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:jpatterson@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/jason-patterson-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why?</h3>
						<div>Albert Einstein. I would like to experience his humor first hand.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jason-patterson-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/justin-michaels_2.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/justin-michaels-fun_2.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Justin Michaels</li>
							<li class="title">Account Executive</li>
							<li>Phone: (877) 563-4374</li>
							<li>Cell: (913) 207-7938</li>
							<li>Fax: (866) 422-1347</li>
							<li class="email"><a href="mailto:jmichaels@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/justin-michaels-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>Christmas at my Grandmother's Family Entertainment Center.  She would turn on all the games in the arcade and give us as many tokens as we wanted.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/justin-michaels-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/rachelle-granger.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/rachelle-granger-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Rachelle Granger</li>
							<li class="title">Account Manager</li>
							<li>Phone: (877) 563-4347</li>
							<li>Cell: (913) 271-6011</li>
							<li>Fax: (866) 306-5631</li>
							<li class="email"><a href="mailto:rgranger@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/rachelle-granger-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>Staying at my grandparent's house and spending the night.  In the morning I would wake up to the smell of breakfast with the radio in the background, and I would  hear my grandmother and grandfather talking in the kitchen.  Such a simple pleasure but such a good one!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/rachelle-granger-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ann-krull.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ann-krull-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Ann Krull</li>
							<li class="title">Account Executive</li>
							<li>Phone: (877) 563-4375</li>
							<li>Cell: (913) 207-4129</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:akrull@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/ann-krull-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>I would want to meet my grandfather. I heard he was a wonderful man.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ann-krull-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/josh-gedminas.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/josh-gedminas-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Josh Gedminas</li>
							<li class="title">Account Manager</li>
							<li>Phone: (877) 563-4378</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:jgedminas@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/josh-gedminas-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Winston Churchill. I think he would have great stories to tell.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/josh-gedminas-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/keitha-mcbride.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/keitha-mcbride-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Keitha McBride</li>
							<li class="title">Account Executive</li>
							<li>Phone: (877) 563-4316</li>
							<li>Cell: (214) 998-0277</li>
							<li>Fax: (866) 679-2523</li>
							<li class="email"><a href="mailto:kmcbride@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/keitha-mcbride-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Elvis-he was an amazing performer and was not afraid to pursue his dream.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/keitha-mcbride-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/stacey-weingarten.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/stacey-weingarten-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Stacey Weingarten</li>
							<li class="title">Account Manager</li>
							<li>Phone: (877) 563-4312</li>
							<li>Fax: (866) 419-7772</li>
							<li class="email"><a href="mailto:sweingarten@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/stacey-weingarten-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>The summer I turned 10. My parents told us we were going to visit my grandparents. My sister and I fell asleep in the car and when we woke up a few hours later, we found out we were going to Six Flags Great America instead! It was the best surprise ever and  we went to my grandparents afterwards!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/stacey-weingarten-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/felicity-colson.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/felicity-colson-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Felicity Colson</li>
							<li class="title">Account Manager</li>
							<li>Phone: (877) 563-3547</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:fcolson@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/felicity-colson-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>Summer vacations with my family. Go-karts, water parks, and arcades for 2 weeks straight!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/felicity-colson-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Anthony-Boyer.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Anthony-Boyer-Fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Anthony Boyer</li>
							<li class="title">Account Executive</li>
							<li>Phone: (913) 563-4383</li>
							<li>Cell: (913) 209-7572</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:aboyer@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/Anthony-Boyer-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why?</h3>
						<div>Steve McQueen. I grew up watching his movies and trying to do some of his stunts when I probably shouldn't have.  He got me in trouble on several occasions.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Anthony-Boyer-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/howard-mcauliffe.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/howard-mcauliffe-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Howard McAuliffe</li>
							<li class="title">Crane &amp; Merchandiser Specialist</li>
							<li>Phone: (913) 563-4369</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:hmcauliffe@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/howard-mcauliffe-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory?</h3>
						<div>Thanks to my mom and dad I have many, but if I had to pick one it would be camping with the whole family at Kerr Dam.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/howard-mcauliffe-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
					<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Ron-Levi.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Ron-Levi-fun_2.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Ron Levi</li>
							<li class="title">Amusement Park Specialist</li>
											<li>Phone: (913) 563-4368</li>
							<li>Cell: (913) 307-6491</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:rlevi@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/Ron-Levi-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why?</h3>
						<div>Abraham Lincoln because he was one of the most influential presidents in U.S. history.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Ron-Levi-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Kyler-Tarwater.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Kyler-Tarwater-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Kyler Tarwater</li>
							<li class="title">VP Sales Aquabi</li>
							<li>Phone: (855) 206-2717</li>
							<li>Cell: (515) 664-6759</li>
							<li>Fax: (866) 728-4823</li>
							<li class="email"><a href="mailto:ktarwater@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/Kyler-Tarwater-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What’s your favorite childhood memory?</h3>
						<div>My older brother used to always win in everything. During a game of one-on-one, I had 9 points and he had 7. We were playing "make it - take it"; he missed a shot and I got the rebound. I scored, which put me 1 point away from victory. I faked to the basket, he stumbled and I shot from the outside. I heard an uncomfortable laugh from my brother as my shot sailed towards the bucket. Sure enough, it went in. I beat my older brother for the first time in history.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Kyler-Tarwater-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
					<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Nancy-Berg.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Nancy-Berg-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Nancy Berg</li>
							<li class="title">Director of Fundraising</li>
							<li>Phone: (888) 273-6885</li>
							<li>Cell: (770) 598-2407</li>
							<li>Fax: (231) 972-0144</li>
							<li class="email"><a href="mailto:nberg@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/Nancy-Berg-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>Duncan Yo Yo! &nbsp; I was a Yo Yo Queen!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Nancy-Berg-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/shelly-mumper.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/shelly-mumper-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Shelly Mumper</li>
							<li class="title">Sales Administrator</li>
							<li>Phone: (877) 563-3549</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:smumper@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/shelly-mumper-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Luciana, an eight-year-old girl in Mexico that my husband and I have sponsored since she was four.  We have written a lot of letters to each other and she has drawn many pictures for us.  I would like to meet her and give her a great big hug!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/shelly-mumper-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<a href="#" class="top">Back to top</a>
	
				<!-- Redemption Entertainment Advisors -->
				<h2 id="Pinnacle">Pinnacle Entertainment Advisors</h2>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/george-mcauliffe_2.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/george-mcauliffe-fun_2.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">George McAuliffe</li>
							<li class="title">President - Pinnacle Entertainment</li>
							<li>Phone: (913) 563-4370</li>
							<li>Cell: (314) 422-7197</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:gmcauliffe@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/george-mcauliffe-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Pete Hamill, a great writer in NYC.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/george-mcauliffe-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jim-kipper.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jim-kipper-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Jim Kipper</li>
							<li class="title">Family Entertainment Specialist</li>
							<li>Phone: (877) 563-4372</li>
							<li>Cell: (913) 207-1398</li>
							<li>Fax: (866) 422-2611</li>
							<li class="email"><a href="mailto:jkipper@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/jim-kipper-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>Everything that came from the bottom of a cereal or Cracker Jack box.  I still look on the back of every box before I buy.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jim-kipper-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<a href="#" class="top">Back to top</a>
	
				<h2 id="Marketing">Marketing</h2>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/courtney-german.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/courtney-german-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Courtney German</li>
							<li class="title">Marketing Specialist</li>
							<li>Phone: (913) 563-4320</li>
							<li>Fax: (913) 563-4301</li>
							<li class="email"><a href="mailto:cgerman@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/courtney-german-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Tina Fey. She's smart, funny, strong, and, from best I can tell, has stayed real despite being a celebrity.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/courtney-german-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/julie-annett_2.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/julie-annett-fun_2.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Julie Annett</li>
							<li class="title">Marketing Admin</li>
							<li>Phone: (913) 563-4321</li>
							<li>Fax: (913) 273-1857</li>
							<li class="email"><a href="mailto:jannett@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/julie-annett-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>Anything "Peanuts" or "Snoopy".  I have been an avid collector of all things Snoopy since grade school.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/julie-annett-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<a href="#" class="top">Back to top</a>
	
				<h2 id="PS">Product Solutions</h2>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/mark-hollywood.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/mark-hollywood-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Mark Hollywood</li>
							<li class="title">V.P. of Product Solutions</li>
							<li>Phone: (913) 563-4342</li>
							<li>Fax: (913) 273-1873</li>
							<li class="email"><a href="mailto:mhollywood@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/mark-hollywood-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Richard Branson.  He’s an innovative and successful entrepreneur, who seems to live and enjoy life to the fullest extent possible.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/mark-hollywood-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/barb-suter.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/barb-suter-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Barb Suter</li>
							<li class="title">Director of Merchandise</li>
							<li>Phone: (913) 563-4343</li>
							<li>Fax: (913) 273-1970</li>
							<li class="email"><a href="mailto:bsuter@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/barb-suter-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>Living in Colorado and seeing Donna Fargo sing "I'm the happiest girl in the whole USA" at the rodeo.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/barb-suter-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/brenda-tietze.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/brenda-tietze-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Brenda Tietze</li>
							<li class="title">Creative Merchandiser</li>
							<li>Phone: (913) 563-4323</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:btietze@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/brenda-tietze-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Cartoonist Bill Watterson.  We could talk about the Transmogrifier and The Horrendous Space Kablooie.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/brenda-tietze-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/lisa-aguirre.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/lisa-aguirre-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Lisa Aguirre</li>
							<li class="title">Import Buyer</li>
							<li>Phone: (913) 563-4341</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:laguirre@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/lisa-aguirre-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>She-Ra Princess of Power!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/lisa-aguirre-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ashley-penland.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ashley-penland-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Ashley Penland</li>
							<li class="title">Domestic Buyer</li>
							<li>Phone: (913) 563-4346</li>
							<li>Fax: (913) 273-1971</li>
							<li class="email"><a href="mailto:apenland@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/ashley-penland-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>My Great Grandparents to see what they were like.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/ashley-penland-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/kara-kokoruda.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/kara-kokoruda-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Kara Kokoruda</li>
							<li class="title">Kit Solutions Buyer</li>
							<li>Phone: (913) 563-4340</li>
							<li>Fax: (913) 273-1860</li>
							<li class="email"><a href="mailto:kkokoruda@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/kara-kokoruda-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>Watching the Wizard of Oz everytime it was on t.v. with my family.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/kara-kokoruda-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
			<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/austin-suter.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/austin-suter-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Austin Suter</li>
							<li class="title">Kit Administrator</li>
							<li>Phone: (913) 563-4337</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:asuter@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/austin-suter-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy? </h3>
						<div>The Yomega Yo-Yo.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/austin-suter-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<a href="#" class="top">Back to top</a>
	
				<h2 id="Finance">Finance &amp; Administration</h2>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/steve-jordan.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/steve-jordan-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Steve Jordan</li>
							<li class="title">Sr. V.P. - CFO</li>
							<li>Phone: (913) 563-4310</li>
							<li>Fax: (913) 563-4399</li>
							<li class="email"><a href="mailto:sjordan@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/steve-jordan-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>My Johnny Eagle M-14 rifle, with sound effects and plastic bullets that you could load in a magazine and shoot.  It definitely gave me superior firepower over the other kids on my block.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/steve-jordan-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/sheila-colby.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/sheila-colby-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Sheila Colby</li>
							<li class="title">Accounts Receivable Specialist</li>
							<li>Phone: (913) 563-4306</li>
							<li>Fax: (913) 273-1973</li>
							<li class="email"><a href="mailto:scolby@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/sheila-colby-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Betty White. She's awesome. I admire all the work she does for animals. </div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/sheila-colby-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/leslie-holway.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/leslie-holway-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Leslie Holway</li>
							<li class="title">Financial Analyst</li>
							<li>Phone: (913) 563-4338</li>
							<li>Fax: (913) 563-4399</li>
							<li class="email"><a href="mailto:lholway@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/leslie-holway-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>First time I rode a roller coaster - Loved it!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/leslie-holway-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/kathy-stites.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/kathy-stites-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Kathy Stites</li>
							<li class="title">Accounts Payable Specialist</li>
							<li>Phone: (913) 563-4303</li>
							<li>Fax: (913) 563-4399</li>
							<li class="email"><a href="mailto:kstites@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/kathy-stites-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>My bicycle!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/kathy-stites-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/staci-jepsen.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/staci-jepsen-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Staci Jepsen</li>
							<li class="title">Exec. Support Manager</li>
							<li>Phone: (913) 563-4317</li>
							<li>Fax: (913) 273-1975</li>
							<li class="email"><a href="mailto:sjepsen@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/staci-jepsen-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>Barbie doll house.  It had 3 stories with an "elevator"!</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/staci-jepsen-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
					<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Travis-Privat.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Travis-Privat-Fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Travis Privat</li>
							<li class="title">Accountant</li>
							<li>Phone: (913) 563-4307</li>
							<li>Fax: (913) 563-4399</li>
							<li class="email"><a href="mailto:tprivat@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/Travis-Privat-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory?</h3>
						<div>Going to see the Cardinals play every summer with my family.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/Travis-Privat-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<a href="#" class="top">Back to top</a>
	
				<h2 id="IT">Information Technology</h2>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/matt-czugala.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/matt-czugala-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Matt Czugala</li>
							<li class="title">IT Manager</li>
							<li>Phone: (913) 563-4322</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:mczugala@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/matt-czugala-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Nikola Tesla.  Imagine what he could change today.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/matt-czugala-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jon-willis.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jon-willis-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Jon Willis</li>
							<li class="title">Web Developer</li>
							<li>Phone: (913) 563-4332</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:jwillis@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/jon-willis-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your favorite childhood memory? </h3>
						<div>Flying my grandfather's airplane when I was six.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/jon-willis-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<a href="#" class="top">Back to top</a>
	
				<h2 id="Distribution">Distribution</h2>
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/cheryl-wood.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/cheryl-wood-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">Cheryl Wood</li>
							<li class="title">Director of Distribution</li>
							<li>Phone: (913) 563-4326</li>
							<li>Fax: (913) 273-0251</li>
							<li class="email"><a href="mailto:cwood@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/cheryl-wood-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>If you could meet anyone in the world, dead or alive, who would it be and why? </h3>
						<div>Steve Jobs...I would love to see him at work.  I wonder if he could hold a simple conversation with simple people.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/cheryl-wood-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
			<!-- brandon gone	<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/brandon-smith.jpg" class="no-fun" />
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/brandon-smith-fun.jpg" class="fun" />
					<div class="contact-info">
						<ul>
							<li class="name">Brandon Smith</li>
							<li class="title">Distribution Supervisor</li>
							<li>Phone: (913) 563-4324</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:bsmith@redemptionplus.com">Send email</a></li>
							<li class="vcard">&nbsp;</li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3></h3>
						<div></div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/brandon-smith-qr.jpg" />
						<span>Scan QR code for vCard data</span>
					</div>
				</div>-->
				<div class="contact-wrap">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/john-wagner.jpg" class="no-fun">
					<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/john-wagner-fun.jpg" class="fun">
					<div class="contact-info">
						<ul>
							<li class="name">John Wagner</li>
							<li class="title">Receiving Supervisor</li>
							<li>Phone: (913) 563-4324</li>
							<li>Fax: (913) 563-4333</li>
							<li class="email"><a href="mailto:jwagner@redemptionplus.com">Send email</a></li>
							<li class="vcard"><a href="http://www.redemptionplus.com/_FileLibrary/FileImage/john-wagner-vcard.vcf">Download vCard&nbsp;<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/vcard_add.png"></a></li>
						</ul>
					</div>
					<div class="contact-popup">
						<h3>What's your all-time favorite toy?</h3>
						<div>A stuffed elephant that I had as a kid.  His name was Peanuts.</div>
						<img src="http://www.redemptionplus.com/_FileLibrary/FileImage/john-wagner-qr.jpg">
						<span>Scan QR code for vCard data</span>
					</div>
				</div>
				<a href="#" class="top">Back to top</a>
			</div>
		</div>
	</div>
    <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
    </form>
</div>
</body>
</html>