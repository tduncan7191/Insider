/*
Redemption Plus
Common JS Library
Version 1.1
Created by Jon Willis
Dependencies:
	- jQuery 1.8+
	- jquery-numberformatter-1.2.3.min
	- jquery-tojson.js
	- jquery-throttle-1.0.js
	- jquery-md5-1.2.1
	- jquery-cookie-1.3
	- jshashtable-2.1.js

Changelog:
JMW 18Dec2012: Add RP_ContentOverlay class with support for full page or content section overlays
*/

$(document).ready(function () {
	rp.Init();
});

var rp = new clsRPlus();
function clsRPlus() {
	this.IIF = IIF;
	this.trim = trim;
	this.GetCookie = GetCookie;
	this.Console = Console;
	this.ContentOverlay = ContentOverlay;
	this.Init = Init;

	function Init() {
		//Bind objects with 'rp-clickable' CSS Class to click first anchor link
		$('.rp-clickable').css('cursor','pointer');
		$('.rp-clickable').bind('click', function () {
			$(this).children('a')[0].click();
		});
	}

	function IIF(i,j,k) {
		if (i) return j; else return k;
	}

	function trim(stringToTrim) {
		return stringToTrim.replace(/^\s+|\s+$/g, "");
	}

	function Console(s) {
		if (!window.console) window.console = {};
		if (!window.console.log) window.console.log = function () { };
		window.console.log(s);
	}

	function GetCookie(c_name) {
		var i, x, y, ARRcookies = document.cookie.split(";");
		for (i = 0; i < ARRcookies.length; i++) {
			x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
			y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
			x = x.replace(/^\s+|\s+$/g, "");
			if (x == c_name) {
				return unescape(y);
			}
		}
	}

	function ContentOverlay(jsOptions) {
		/*
		jsOptions = Optional, allows options below to be passed via JSON object
			content = String or HTML Object to display in Overlay Content (i.e."Please wait...", "<div id='Div1'>Please wait...</div>")
			parent = Parent Object to overlay. If none, then the full page is overlayed.
			width (integer) = Sets the overlay's content width (this does not set the overlay height)
			height (integer) = Sets the overlay's content height (this does not set the overlay height)
			clicktohide (bool) = Toggles whether the overlay can be closed by clicking outside of the content box.

		Sample uses
		var oPrg = new rp.ContentOverlay({content:"Loading...",clicktohide:false); //Overlay Full Page, don't allow user to hide on click
		var oPrg = new rp.ContentOverlay({content:"Loading...",parent:$("#MyDivID")); //Overlay Page Section
		oPrg.Remove(); //Hide Overlay
		*/
		this.Remove = Remove;
		
		//Create a new overlay object
		var o = new Overlay();
		var oParent = jsOptions.parent;
		var oContent = jsOptions.content;

		//Detect and set parent object accordingly, then toggle object's "fullpage" property
		o.parent = (oParent) ? oParent : ($("body")) ? $("body") : $(document);
		o.fullpage = o.parent.is($("body"));

		//Add New Overlay Background to parent and update object
		o.parent.append('<div class="rp-overlay" style="display:none;">&nbsp;</div>');
		o.overlay = o.parent.children('.rp-overlay').last();
		
		//Update/Create Overlay Content Object
		o.isobj = ($.type(oContent) != "string");
		var sHTML = null;
		if (o.isobj) {
			o.content = $(oContent);
		}
		else {
			o.parent.append('<div class="rp-overlay-content" style="display:none;">' + oContent + '</div>');
			o.content = o.parent.children('.rp-overlay').last().next();
		}
		
		var z = $('.rp-overlay').length;
		o.overlay.css('z-index',z * 100);
		o.content.css('z-index',(z * 100) + 10);
		o.customwidth = jsOptions.width;
		o.customheight = jsOptions.height;
		o.clickhide = jsOptions.clicktohide;
		if (jsOptions.delay != undefined) o.delay = jsOptions.delay;
		
		//Now that we know our objects, set the size and position
		SizeAndPosition();

		o.overlay.fadeIn(750);
		o.content.fadeIn(750);

		$(window).bind('resize',function () {WindowResized();});
		if (o.clickhide) $(o.overlay).bind('click',function () {Remove()});
			
		//Adjust overlay content position on scroll if overlaying entire page
		if (o.fullpage) $(window).bind('scroll',function () {Scroll(); });
		
		function Overlay() {
			var ob = null; //stores overlay background object
			var op = null; //stores overlay parent object
			var oc = null; //stores overlay content object
			var w = 0; //stores default overlay content width
			var cw = 0; //stores custom overlay content width, if applicable
			var h = 0; //store default overlay content height
			var ch = 0; //stores custom overlay content height, if applicable
			var ph = 0; //stores original parent height, if applicable
			var t = 0; //default top position overlay
			var l = 0; //default left position overlay
			var r = 0; //default right position overlay
			var fullpage = true; //ref full page overlay
			var margin = 25; //overlay content container margin
			var delay = 0; //fade in/out delay time in ms
			var transparency = 20; //overlay background transparency (100 = fully transparent; 0 = background hides underlying content);
			var clickhide = true; //sets whether overlay can be hidden
			var isobj = false; //sets whether overlay content is passed as string (false) or object (true)
			var obj = null; //stores original object reference if passed

			//make properties public
			this.parent = op;
			this.overlay = ob;
			this.content = oc;
			this.width = w;
			this.customwidth = cw;
			this.height = h;
			this.customheight = ch;
			this.parentheight = ph;
			this.top = t;
			this.left = l;
			this.right = r;
			this.fullpage = fullpage;
			this.margin = margin;
			this.delay = delay;
			this.transparency = transparency;
			this.clickhide = clickhide;
			this.isobj = isobj;
		}

		function SizeAndPosition() {
			//no parent object, init to overlay entire page
			if (o.fullpage) {
				o.width = $(document).width();
				o.height = $(document).height();
				o.top = 0;
				o.left = 0;
				o.right = 'auto';
			}
			//parent object, only overlay parent's content
			else {
				o.width = o.parent.width();
				o.parentheight = o.parent.height();
				o.height = o.parent.height();
				o.width = o.parent.width();
				o.top = o.parent.offset().top + 'px';
				o.left = o.parent.offset().left + 'px';
				o.right = 'auto';
			}

			//initial position for transparent overlay
			o.overlay.css('position','absolute');
			o.overlay.css('top', o.top);
			o.overlay.css('left', o.left);
			o.overlay.css('right', o.right);
			o.overlay.css('height', o.height);
			o.overlay.css('width', o.width);
			o.overlay.css('position','absolute');
			o.overlay.css('margin','0 auto');
			o.overlay.css('background-color','#999');
			o.overlay.css('opacity',(100 - o.transparency) / 100);

			if (o.customwidth == null) o.customwidth = o.width * .75;
			else if (o.customwidth != $.parseNumber($.toString(o.customwidth))) o.customwidth = o.customwidth + 'px';

			if (o.customheight == null) iContentHeight = o.height * .75;
			else if (o.customheight != $.parseNumber($.toString(o.customheight))) o.customheight = o.customheight + 'px';
				
			//initial positioning for overlay content container
			o.content.css('position','absolute');
			o.content.css('text-align','center');
			o.content.css('margin','0 auto');
			o.content.css('position','absolute');
			o.content.css('margin','0 auto');
			o.content.css('width',o.customwidth);
			o.content.css('height',o.customheight);
			if (o.fullpage) o.content.css('top',o.content.css('top',$(document).scrollTop() + o.margin));
			else o.content.css('top',$.parseNumber(o.top.toString()) + o.margin + 'px');
			o.content.css('left','0');
			o.content.css('right','0');
			o.content.css('opacity','1');
			o.content.css('border','2px solid #f1913b');
			o.content.css('border-radius','8px');
			o.content.css('background-color','#fff');
			
			//resize parent if smaller than overlay, then update overlay's left position in case browser scrollbars appear
			if (!o.fullpage) {
				if (o.parentheight < o.content.height() || o.parentheight < (o.content.height() + o.margin * 2) || o.parentheight < o.customheight) {
					o.height = o.content.height() + (o.margin * 2);
					
					o.overlay.css('height',o.height);
					
					o.left = o.parent.offset().left;
					o.overlay.css('left',o.left);
				}
			}
		}
		function Remove(oEvent) {
			o.content.fadeOut(o.delay);
			o.overlay.fadeOut(o.delay);
			
			if (o.parentheight < o.parent.height()) setTimeout(function () {o.parent.css('height','auto')},o.delay);
			
			//remove overlay from HTML after fade
			setTimeout(function () {o.overlay.remove()}, o.delay + 50);
			
			if (o.isobj) setTimeout(function () {o.content.hide()}, o.delay + 50);
			else setTimeout(function () {o.content.remove()}, o.delay + 50);
		}
		function Scroll() {
			if (o.fullpage) o.content.css('top',o.content.css('top',$(document).scrollTop() + o.margin));
			else o.content.css('top',$(o.overlay).css('top') + o.margin);
		}
		function WindowResized() {
			if (o.parent.is($('body'))) {
				o.overlay.css('height',$(document).height());
				o.overlay.css('width',$(document).width());
			}
			else {
				o.overlay.css('top',o.parent.offset().top + 'px');
				o.overlay.css('left',o.parent.offset().left + 'px');
			}
		}
	}
}