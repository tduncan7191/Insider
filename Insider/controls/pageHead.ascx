<%@ Control Language="C#" ClassName="ctlPageHead" %>
<script runat="server">
	public string title = clsRPlus.fnMid(clsRPlus.fnGetURL_Clean(), 1, 6);
</script>
	<title><%=title%></title>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="shortcut icon" href="/images/favicon.ico" />
	
    <link rel="stylesheet" type="text/css" href="/css/styles.css" />
	<link rel="stylesheet" type="text/css" href="/css/rplus-default.css" />

    <script type="text/javascript" src="/scripts/jquery-1.8.2.min.js"></script>
	<script type="text/javascript" src="/scripts/jquery-ui-1.9.0.custom.min.js"></script>
	<script type="text/javascript" src="/scripts/jquery-ui-tabs-rotate.js"></script>
    <script type="text/javascript" src="/scripts/rplus-common-1.0.0.js"></script>
    