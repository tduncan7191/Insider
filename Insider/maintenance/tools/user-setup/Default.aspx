<%@ Page Title="" Language="C#" MasterPageFile="~/templates/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="tools_user_setup_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody1" Runat="Server">
	<form id="form1" runat="server">
<%
	if (Request.Cookies["contactid"].Value != "229" && Request.Cookies["contactid"].Value != "208")
	{
		Response.Write("You do not have the proper credentials to use this page.");
		Response.Write(" User ID: " + Request.Cookies["contactid"].Value);
	}
	else
	{
	%>
	<asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" 
		AutoGenerateEditButton="True" DataSourceID="SqlDataSource1">
		<Columns>
			<asp:BoundField DataField="companyID" HeaderText="companyID" 
				SortExpression="companyID" />
			<asp:BoundField DataField="firstName" HeaderText="firstName" 
				SortExpression="firstName" />
			<asp:BoundField DataField="lastName" HeaderText="lastName" 
				SortExpression="lastName" />
			<asp:BoundField DataField="middleName" HeaderText="middleName" 
				SortExpression="middleName" />
			<asp:BoundField DataField="nickname" HeaderText="nickname" 
				SortExpression="nickname" />
			<asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
			<asp:BoundField DataField="address" HeaderText="address" 
				SortExpression="address" />
			<asp:BoundField DataField="city" HeaderText="city" SortExpression="city" />
			<asp:BoundField DataField="stateID" HeaderText="stateID" 
				SortExpression="stateID" />
			<asp:BoundField DataField="zip" HeaderText="zip" SortExpression="zip" />
			<asp:BoundField DataField="postionName" HeaderText="postionName" 
				SortExpression="postionName" />
			<asp:BoundField DataField="status" HeaderText="status" 
				SortExpression="status" />
			<asp:BoundField DataField="dateOfBirth" HeaderText="dateOfBirth" 
				SortExpression="dateOfBirth" />
			<asp:BoundField DataField="annDate" HeaderText="annDate" 
				SortExpression="annDate" />
			<asp:BoundField DataField="InactiveDate" HeaderText="InactiveDate" 
				SortExpression="InactiveDate" />
			<asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
			<asp:BoundField DataField="password" HeaderText="password" 
				SortExpression="password" />
			<asp:BoundField DataField="departmentID" HeaderText="departmentID" 
				SortExpression="departmentID" />
		</Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
		ConnectionString="<%$ ConnectionStrings:cnSQL_INSIDER %>" 
		SelectCommand="SELECT [companyID], [firstName], [lastName], [middleName], [nickname], [email], [address], [city], [stateID], [zip], [postionName], [status], [dateOfBirth], [annDate], [InactiveDate], [type], [password], [departmentID] FROM [Contact] WHERE [Status] ">
	</asp:SqlDataSource>
	<%
	}
%>
	</form>
</asp:Content>

