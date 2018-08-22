using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;


public partial class _Default : System.Web.UI.Page
{
						private DataTable DT
						{
						get { return ViewState["DataTable"] as DataTable ?? new DataTable(); }
						set { ViewState["DataTable"] = value; }
						}

						protected void Page_Load(object sender, EventArgs e)
						{

						}

						protected void btnDownload_Click(object sender, EventArgs e)
						{		
						StringBuilder sb = new StringBuilder();

						switch(DPLFormat.SelectedItem.Value)
						{
							case "EPL":
								sb.Append("," + txtOrderNumber.Value + "\r\n");
								for(int i = 0; i < gvDPL.HeaderRow.Cells.Count; i++)
								{
									sb.Append(gvDPL.HeaderRow.Cells[i].Text + ",");
								}
								sb = new StringBuilder(sb.ToString().Trim().TrimEnd(','));
								sb.Append("\r\n");
								break;
							default:
								sb.Append("," + txtOrderNumber.Value + "\r\n");
								break;
						}	
						foreach (GridViewRow row in gvDPL.Rows)  
						{  
							foreach (TableCell cell in row.Cells)  
							{  
								string txt = RemoveSpecialCharacters(cell.Text);
								sb.Append(txt + ',');
							}
							sb = new StringBuilder(sb.ToString().Replace("&nbsp;", "").Trim().TrimEnd(','));
							sb.Append("\r\n");
						}
						Response.Clear();
						Response.Buffer = true;
						if(DPLFormat.SelectedItem.Value == "EPL")
						{
							Response.AddHeader("content-disposition",
							 "attachment;filename=" + txtOrderNumber.Value + ".EPL");							
						}
						else
						{
							Response.AddHeader("content-disposition",
							 "attachment;filename=" + txtOrderNumber.Value + ".DPL");							
						}
						Response.Charset = "";
						Response.ContentType = "application/text";
						Response.Output.Write(sb);
						Response.Flush();
						Response.End();
						}

						protected void btnGenerate_Click(object sender, EventArgs e)
						{
						DT = new DataTable();
						BindData(string.Empty);
						}

						protected void btnAddItem_Click(object sender, EventArgs e)
						{
						BindData(addItem.Value);
						}

						protected void DPLFormat_Change(object sender, EventArgs e)
						{
						DT = new DataTable();
						}

						private static string RemoveSpecialCharacters(string input)
						{
						input = input.Replace("&#174;", string.Empty); //Copyright
						input = input.Replace("\u2122", string.Empty); //Trademark
						input = input.Replace("&#169;", string.Empty); //Registered Trademark
						input = input.Replace("&amp;", "and"); //Ampersand
						return input;
						}

						protected void BindData(string itemCode)
						{
						try
						{
							using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString)) 
							{
								conn.Open();
								SqlCommand cmd  = new SqlCommand("in_FakeDPL", conn);
								cmd.CommandType = CommandType.StoredProcedure;
								cmd.Parameters.AddWithValue("@MasNo", txtMasNo.Value);
								cmd.Parameters.AddWithValue("@startDate", startDate.Text);
								cmd.Parameters.AddWithValue("@endDate", endDate.Text);
								cmd.Parameters.AddWithValue("@DPLFormat", DPLFormat.SelectedItem.Value);
								cmd.Parameters.AddWithValue("@itemCode", itemCode);
								var reader = cmd.ExecuteReader();
								
								DataTable dataTable = DT;
								if(dataTable.Columns.Count == 0)
								{
									dataTable.Load(reader);
								}
								else
								{	
									while(reader.Read())
									{						
										DataRow dataRow = dataTable.NewRow();
										foreach (DataColumn col in dataTable.Columns)
										{
											dataRow[col.ColumnName] = reader[col.ColumnName];
										}			
										dataTable.Rows.Add(dataRow);			
									}
								}	
													
								DT = dataTable;
								gvDPL.DataSource = dataTable;
								gvDPL.DataBind();  
							}          
						}
						catch (Exception ex)
						{
							lblResult.Text = "error: " + ex.Message.ToString();
						} 
						}
}