<%@ Page Title="95er of the Month" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="NinetyFiver_of_the_Month" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <customControls:ctlPageHead ID="ctlPageHead" runat="server" title="95er of the Month - R+ Insider" />
	<script>
		var sDefaultTxt = "Please tell us why...";
		$(document).ready(function() {
			$("textarea").hide();
			$("textarea").html(sDefaultTxt);
			$("input[type$='checkbox']").bind("click", function() { 
				if ($(this).is(":checked")) $("#" + this.id.replace("highway","message")).show();
				else $("#" + this.id.replace("highway","message")).hide();
			});
			$("textarea").bind("focus", function() { 
				if ($(this).html()=="Please tell us why...") $(this).html('');
			});
		});
		function fn95erVerify() {
			if ($("#DetailsView1_DropDownList1 option:selected").val()=="0") {
				alert("Please select a nominee.");
				return false;
			}
			
			var bValidHighways = false;
			$("input[type$='checkbox']").each(function() {
				if ($(this).is(":checked")) {
					bValidHighways = true;
					return false;
				}
			});
			if (!bValidHighways) {
				alert("Please select at least one highway.");
				return false;
			}

			var bValidComments = false;
			$("textarea").each(function() {
				if ($("#" + this.id.replace("message","highway")).is(":checked")) {
					var sTxt = $(this).html();
					
					if (sTxt != sDefaultTxt) bValidComments = true;
					else bValidComments = false;
				}
			});
			if (!bValidComments) {
				alert("Please tell us why for each highway selected.");
				return false;
			}

			//return true if everything passed
			return true;
		}
	</script>
</head>
<body>
    <div id="container">
        <form id="form1" runat="server">
        <customControls:ctlContentHeader ID="ctlContentHeader1" runat="server" heading="95er of the Month" />
        <div id="main">
            <div class="contentBlock">
                <img src="/images/Eom_64_th.jpg" class="pageTitleIcon" alt="recycle icon" />
                <asp:Label ID="commenttypeHeader" runat="server" Text="95er of the Month"
                    CssClass="pageTitle" />
                <p>Here is your chance to recognize your co-workers for 95'ing!</p>
                <asp:Label ID="alert1" runat="server" Text="Label" Visible="false" CssClass="nomDropdownlist"
                    Style="color: Red;"></asp:Label>
                <img src="/images/golden_lizard249h.jpg" style="margin: 0 5px 0 5px;float:left"/>
                <div style="float: left" />
                <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="425px" AutoGenerateRows="False"
                    DataKeyNames="nomId" DataSourceID="SqlDataSource1" EnableModelValidation="True"
                    DefaultMode="Insert" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DetailView_ItemCommand"
                    OnItemInserted="DetailView_ItemInserted">
                    <AlternatingRowStyle BackColor="White" />
                    <CommandRowStyle BackColor="#f5d101" Font-Bold="True" />
                    <EditRowStyle BackColor="#cfdc35" />
                    <FieldHeaderStyle BackColor="#f5d101" Font-Bold="True" />
                    <Fields>
                        <asp:TemplateField HeaderText="Nominee" SortExpression="NomineeId" ShowHeader="False">
                            <InsertItemTemplate>
								<div style="margin:0 0 5px 10px;">Select a nominee and all highways they have demonstrated this month. Include your comments below to complete your nomination.</div>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2"
                                    DataValueField="NomineeId" DataTextField="name" AppendDataBoundItems="True" SelectedValue='<%# Bind("NomineeId") %>'>
                                    <asp:ListItem Value="0" Selected="True">Select Nominee</asp:ListItem>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ControlStyle CssClass="nomDropdownlist" />
                        </asp:TemplateField>
						<asp:TemplateField HeaderText="highways" ShowHeader="false" SortExpression="highways">
							<InsertItemTemplate>
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway1") %>' ID="highway1" Text="Highway 1: Get the Picture" /><br />
								<asp:TextBox ID="message1" ClientIDMode="Static" runat="server" Text='<%# Bind("message1") %>' TextMode="MultiLine" Width="400px"></asp:TextBox><br />
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway2") %>' ID="highway2" Text="Highway 2: Risk" /><br />
								<asp:TextBox ID="message2" ClientIDMode="Static" runat="server" Text='<%# Bind("message2") %>' TextMode="MultiLine" Width="400px"></asp:TextBox><br />
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway3") %>' ID="highway3" Text="Highway 3: Full Responsibility" /><br />
								<asp:TextBox ID="message3" ClientIDMode="Static" runat="server" Text='<%# Bind("message3") %>' TextMode="MultiLine" Width="400px"></asp:TextBox><br />
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway4") %>' ID="highway4" Text="Highway 4: Feel All Your Feelings" /><br />
								<asp:TextBox ID="message4" ClientIDMode="Static" runat="server" Text='<%# Bind("message4") %>' TextMode="MultiLine" Width="400px"></asp:TextBox><br />
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway5") %>' ID="highway5" Text="Highway 5: Honest Communication" /><br />
								<asp:TextBox ID="message5" ClientIDMode="Static" runat="server" Text='<%# Bind("message5") %>' TextMode="MultiLine" Width="400px"></asp:TextBox><br />
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway6") %>' ID="highway6" Text="Highway 6: Forgiveness of the Past" /><br />
								<asp:TextBox ID="message6" ClientIDMode="Static" runat="server" Text='<%# Bind("message6") %>' TextMode="MultiLine" Width="400px"></asp:TextBox><br />
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway7") %>' ID="highway7" Text="Highway 7: Gratitude for the Present" /><br />
								<asp:TextBox ID="message7" ClientIDMode="Static" runat="server" Text='<%# Bind("message7") %>' TextMode="MultiLine" Width="400px"></asp:TextBox><br />
								<asp:CheckBox runat="server" ClientIDMode="Static" Checked='<%# Bind("highway8") %>' ID="highway8" Text="Highway 8: Hope for the Future" /><br />
								<asp:TextBox ID="message8" ClientIDMode="Static" runat="server" Text='<%# Bind("message8") %>' TextMode="MultiLine" Width="400px"></asp:TextBox>
							</InsertItemTemplate>
							<ControlStyle CssClass="chkhighways" />
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="nomDate" SortExpression="nomDate" Visible="False">
                            <InsertItemTemplate>
                                <asp:TextBox ID="nomDate" runat="server" Text='<% System.DateTime.Now %>'></asp:TextBox>
                            </InsertItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowInsertButton="True" ButtonType="Button" InsertText="Add Nomination">
                            <ControlStyle CssClass="nomDropdownlist" />
                        </asp:CommandField>
                    </Fields>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                </asp:DetailsView>
				<!--
					OLD MESSAGE BOX
					<asp:TemplateField HeaderText="message" ShowHeader="False" SortExpression="message">
                            <InsertItemTemplate>
                                <asp:TextBox ID="message" runat="server" Text='<%# Bind("message") %>' TextMode="MultiLine"></asp:TextBox>
                            </InsertItemTemplate>
                            <ControlStyle CssClass="nomDropdownlist" Height="90px" Width="400px" />
                        </asp:TemplateField>
				-->
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CN_INSIDER %>"
                    InsertCommand="INSERT INTO [Nomination] ([NomineeId], [NominatorId], [message], [nomDate]) VALUES ( @NomineeId, @NominatorId
									, 'Selected highways: ' + Char(10) + Char(13)
										+ CASE WHEN COALESCE(CAST(@highway1 As bit), 0) = 1 THEN '1 (Get the Picture); Comment: ' + COALESCE(@message1, '') + Char(10) + Char(13) ELSE '' END
										+ CASE WHEN COALESCE(CAST(@highway2 As bit), 0) = 1 THEN '2 (Risk); Comment: ' + COALESCE(@message2, '') + Char(10) + Char(13) ELSE '' END
										+ CASE WHEN COALESCE(CAST(@highway3 As bit), 0) = 1 THEN '3 (Full Responsibility); Comment: ' + COALESCE(@message3, '') + Char(10) + Char(13) ELSE '' END
										+ CASE WHEN COALESCE(CAST(@highway4 As bit), 0) = 1 THEN '4 (Feel All Your Feelings); Comment: ' + COALESCE(@message4, '') + Char(10) + Char(13) ELSE '' END
										+ CASE WHEN COALESCE(CAST(@highway5 As bit), 0) = 1 THEN '5 (Honest Communication); Comment: ' + COALESCE(@message5, '') + Char(10) + Char(13) ELSE '' END
										+ CASE WHEN COALESCE(CAST(@highway6 As bit), 0) = 1 THEN '6 (Forgiveness of the Past); Comment: ' + COALESCE(@message6, '') + Char(10) + Char(13) ELSE '' END
										+ CASE WHEN COALESCE(CAST(@highway7 As bit), 0) = 1 THEN '7 (Gratitude for the Present); Comment: ' + COALESCE(@message7, '') + Char(10) + Char(13) ELSE '' END
										+ CASE WHEN COALESCE(CAST(@highway8 As bit), 0) = 1 THEN '8 (Hope for the Future); Comment: ' + COALESCE(@message8, '') + Char(10) + Char(13) ELSE '' END
									 , getDate())">
                    <InsertParameters>
                        <asp:Parameter Name="NomineeId" Type="Int32" />
						<asp:Parameter Name="nomDate" Type="DateTime" />
						<asp:Parameter Name="message" Type="String" />
						<asp:Parameter Name="highway1" Type="String" />
						<asp:Parameter Name="highway2" Type="String" />
						<asp:Parameter Name="highway3" Type="String" />
						<asp:Parameter Name="highway4" Type="String" />
						<asp:Parameter Name="highway5" Type="String" />
						<asp:Parameter Name="highway6" Type="String" />
						<asp:Parameter Name="highway7" Type="String" />
						<asp:Parameter Name="highway8" Type="String" />
					</InsertParameters>
                </asp:SqlDataSource>
				<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CN_INSIDER %>"
                                    SelectCommand="SELECT [contactID] as NomineeId, LTrim(RTrim([lastName])) + ', ' + LTrim(RTrim([firstName])) as name FROM [Contact]where status=0 OR (contactid IN (73,74,75,79,225) and status = 1) order by lastname">
                                </asp:SqlDataSource>
				<!-- NominatorId Parameter is set via Page_Load() -->
            </div>
            <img src="/images/golden_lizard249h.jpg" style="margin: 0 5px 0 5px; float: left" />
        </div>
        <customControls:ctlContentFooter ID="ctlContentFooter" runat="server" />
        </div>
        </form>
    </div>
</body>
</html>
