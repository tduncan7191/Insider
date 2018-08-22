using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class EgosAndExcuses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
	/*
    public void LoadData()
    {
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM comments",
            GetConnectionString("cnSQL_SALES_SQLClient"));
        DataTable table = new DataTable();
        adapter.Fill(table);
        Repeater1.DataSource = table;
        Repeater1.DataBind();
    }

    public string GetConnectionString(string name)
    {
        //bool _status = false;
        String _message = "";
        try
        {
            //variable to hold our connection string for returning it
            string connString = string.Empty;
            //check to see if the user provided a connection string name
            //this is for if your application has more than one connection string
            if (!string.IsNullOrEmpty(name)) //a connection string name was provided
            {
                //get the connection string by the name provided
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            else //no connection string name was provided
            {
                //get the default connection string
                connString = System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            }
            //_status = true;
            //return the connection string to the calling method
            return connString;
        }
        catch (Exception ex)
        {
            _message = ex.Message;
            //_status = false;
            return string.Empty;
        }
    }


    public int findIndex(string k)
    {
        using (System.Data.SqlClient.SqlConnection connIndex = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_UPS"].ConnectionString))
        {
            int Index = 0;

            System.Data.SqlClient.SqlDataReader rdr = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
            String query = "SELECT packerid From Packer where Packer =@Packer";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = connIndex;
            connIndex.Open();
            cmd.Parameters.AddWithValue("@Packer", k);
            Console.Write("Executing Reader");
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Index = (int)rdr["packerid"];
            }
            return Index;
        }
    }







    /*
     * Page code
     * 
     * <!-- start parent repeater -->
<asp:repeater id="parentRepeaterControl" runat="server">
   <itemtemplate>
      <b><%# DataBinder.Eval(Container.DataItem,"au_id") %></b><br>

      <!-- start child repeater -->
      <asp:repeater id="childRepeater" datasource='<%# ((DataRowView)Container.DataItem)
      .Row.GetChildRows("myrelation") %>' runat="server">

         <itemtemplate>
            <%# DataBinder.Eval(Container.DataItem, "[\"title_id\"]")%><br>
         </itemtemplate>
      </asp:repeater>
      <!-- end child repeater -->

   </itemtemplate>
</asp:repeater>
<!-- end parent repeater -->
     * 
     * 
     * 
     * end page code
    protected System.Web.UI.WebControls.Repeater parentRepeater;

     public EgosAndExcuses()
      {
         Page.Init += new System.EventHandler(Page_Init);
      }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Create the connection and DataAdapter for the Authors table.
         SqlConnection cnn = new SqlConnection("server=(local);database=pubs; Integrated Security=SSPI ;");
         SqlDataAdapter cmd1 = new SqlDataAdapter("select * from authors",cnn);

         //Create and fill the DataSet.
         DataSet ds = new DataSet();
         cmd1.Fill(ds,"authors");

         //Create a second DataAdapter for the Titles table.
         SqlDataAdapter cmd2 = new SqlDataAdapter("select * from titleauthor",cnn);
         cmd2.Fill(ds,"titles");

         //Create the relation bewtween the Authors and Titles tables.
         ds.Relations.Add("myrelation",
         ds.Tables["authors"].Columns["au_id"],
         ds.Tables["titles"].Columns["au_id"]);
         parentRepeaterControl.DataSource= ds.Tables["authors"];
         //Bind the Authors table to the parent Repeater control, and call DataBind.
         
         Page.DataBind();

         //Close the connection.
         cnn.Close();
      }
      private void Page_Init(object sender, EventArgs e)
      {
         InitializeComponent();
      }
      private void InitializeComponent()
      {    
         this.Load += new System.EventHandler(this.Page_Load);
      }

    }
*/
}