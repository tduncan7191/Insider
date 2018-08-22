<%@ Page Title="Job Opportunities" Language="C#" MasterPageFile="~/templates/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="JobOpportunities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader1" Runat="Server">
	<style type="text/css">
		.jobs li { margin: 15px 0; }
		.jobs li h3 { display: inline-block; }
		.jobs li h3:after { content: ' - '; }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody1" Runat="Server">
	<h1>Job Opportunities at Redemption Plus</h1>
	<ul class="jobs">
		<li>
			<h3>Shipping Supervisor</h3>
			<a href="/downloads/jobs/Job - Shipping Supervisor - July 2013.pdf" target="_blank">Click here to view job description</a>
		</li>	
		
	</ul>
</asp:Content>

