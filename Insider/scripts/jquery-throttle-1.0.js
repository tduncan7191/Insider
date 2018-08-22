/**
* Event Throttle
* @author Darcy Clarke
*
* Copyright (c) 2012 Darcy Clarke
* Dual licensed under the MIT and GPL licenses.
* http://darcyclarke.me/
*/

(function (a, b) { a.throttle = function (b, c) { var d = this; d.delay = 200; d.delay = typeof b == "function" ? d.delay : typeof b == "number" ? b : d.delay; d.cb = typeof b == "function" ? b : typeof c == "function" ? c : function () { }; if (a.throttle.timeout !== false) clearTimeout(a.throttle.timeout); a.throttle.timeout = setTimeout(function () { a.throttle.timeout = false; d.cb() }, d.delay) }; a.throttle.timeout = false })(jQuery, window)