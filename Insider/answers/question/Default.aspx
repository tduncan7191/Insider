<%@ Page Title="Answers" Language="C#" MasterPageFile="~/templates/Answers.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="answers_category_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AnswersHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AnswersPageHeader1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AnswersBody1" Runat="Server">
	<h2 id="Question_Title" runat="server"></h2>
	
	<div id="Question" class="Question" runat="server">
		<div id="Question_Author" class="Author" runat="server"></div>
		<div id="Question_Text" class="Text" runat="server"></div>
		<div id="Question_Date" class="Date" runat="server"></div>
	</div>

	<h2>Answers</h2>
	<div id="Answers" class="Answers">
		<span id="Answers_Server" runat="server"></span>
	</div>
    <customControls:ctlAnswers_SubmitAnswer ID="ctlAnswers_SubmitAnswer1" runat="server" />
</asp:Content>

