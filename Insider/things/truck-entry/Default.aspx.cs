using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    string orderNumber;
    string truckcompany;
    string month;
    string day;
    string year;
    string trackingNumber;
    string packer1Txt;
    int tags1;
    int lines1;
    string packer2;
    int tags2;
    int lines2;
    string packer3;
    int tags3;
    int lines3;
    string packer4;
    int tags4;
    int lines4;
    string packer5;
    int tags5;
    int lines5;
    string packer6;
    int tags6;
    int lines6;
    string lastOrderNumber="";
    string lastTruckcompany = "";
    string lastMonth = "";
    string lastDay = "";
    string lastYear = "";
    string lastTrackingNumber = "";
    string lastFOB = "";


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /*
    public static void BindList(ListControl list, IEnumerable datasource, string valueName, string textName)
    {
        list.Items.Clear();
        list.Items.Add("","");
        list.AppendDataBoundItems = true;
        list.DataValueField = valueName;
        list.DataTextField = textName;
        list.DataSource = datasource;
        list.DataBind();
    }
    */

    public void clearForm()
    {
        Order_Number.Text = null;
        Truck_CO.Text = null;
        ShipMonth.Text = null;
        ShipDay.Text = null;
        ShipYear.Text = null;
        Packer1.Text = null;
        linesPacker1.Text = null;
        tagsPacker1.Text = null;
        Packer2.Text = null;
        linesPacker2.Text = null;
        tagsPacker2.Text = null;
        Packer3.Text = null;
        linesPacker3.Text = null;
        tagsPacker3.Text = null;
        Packer4.Text = null;
        linesPacker4.Text = null;
        tagsPacker4.Text = null;
        Packer5.Text = null;
        linesPacker5.Text = null;
        tagsPacker5.Text = null;
        Packer6.Text = null;
        linesPacker6.Text = null;
        tagsPacker6.Text = null;
        Tracking_number.Text = null;
        truck_FOB.Text = null;
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

    public void updateFOB(string orderNumber, decimal truckFob)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
            String query = "Update TruckEntry set ML_UDF_SOH_OUTBOUND_SHIP =@truckFOB  where SALESORDERNumber =@SalesOrderNumber";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@truckFOB", truckFob);
            cmd.Parameters.AddWithValue("@SalesOrderNumber", orderNumber);
            cmd.ExecuteNonQuery();
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();

        }
    }

    public void insertFOB(string orderNumber, decimal truckFob)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
			String query = "Insert into TruckEntry (SalesOrderNumber,ML_UDF_SOH_OUTBOUND_SHIP) values(@SalesOrderNumber,@truckFOB)";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@truckFOB", truckFob);
            cmd.Parameters.AddWithValue("@SalesOrderNumber",  orderNumber);
            cmd.ExecuteNonQuery();
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();

        }
    }

    public void checkFOB(string orderNumber, decimal truckFob)
    {
        String check = "";
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
        {

            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
			String query = "SELECT SALESORDERNumber,ML_UDF_SOH_OUTBOUND_SHIP From TruckEntry where SALESORDERNumber =@SalesOrderNumber";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@SalesOrderNumber",  orderNumber);
            Console.Write("Executing Reader");
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["SALESORDERNumber"].ToString() != null)
                {
                    check = reader["SALESORDERNumber"].ToString();
                }
            }
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();

        }
        if (check!=""){
            updateFOB(orderNumber, truckFob);
        }
        else{
            insertFOB(orderNumber, truckFob);
        }
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

    protected void Enter_Click(object sender, EventArgs e)
    {
        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_UPS"].ConnectionString))
        {

            String enteredOrderNumber = Order_Number.Text;
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
            String query = "SELECT * From Truck where Order_Number =@Order_Number";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@Order_Number", enteredOrderNumber);
            Console.Write("Executing Reader");
            reader = cmd.ExecuteReader();

            int loopCounter = 1;
            while (reader.Read())
            {
                orderNumber = (string)reader["Order_Number"];
                Console.Write("{0,-25}", orderNumber);
                truckcompany = (string)reader["Truck_CO"];
                month = (string)reader["ShipMonth"];
                day = (string)reader["ShipDay"];
                year = (string)reader["ShipYear"];
                if (!reader["Tracking_number"].Equals(System.DBNull.Value))
                {
                    trackingNumber = (string)reader["Tracking_number"];
                }
            }
            reader.Close();
            cmd.Parameters.Clear();
            if (orderNumber == enteredOrderNumber)
            {
                messageLabel1.Visible = true;
                messageLabel2.Visible = true;
                messageLabel2.Style.Add("color", "Red");
                messageLabel1.Text = orderNumber;
                messageLabel2.Text = " is already in the system.";
                editOrderButton.Visible = true;
                clearForm();
            }
            else//if there is nothing in datbase with this Order Number
            {
                query = "INSERT INTO Truck VALUES(@Order_Number,@Truck_CO,@ShipMonth,@ShipDay,@ShipYear,@Packer,@Lines,@Tags,@Tracking_number)";

                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                String packer1string = Packer1.SelectedValue;

                if (Packer1.SelectedValue != "-- ")
                {
                    cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text.Trim());
                    cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Packer", Packer1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lines", linesPacker1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tags", tagsPacker1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text.Trim());
                    cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Packer", Packer1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lines", linesPacker1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tags", tagsPacker1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();               
                }

                if (Packer2.SelectedValue != "-- ")
                {
                    cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text.Trim());
                    cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Packer", Packer2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lines", linesPacker2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tags", tagsPacker2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                if (Packer3.SelectedValue != "-- ")
                {
                    cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text.Trim());
                    cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Packer", Packer3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lines", linesPacker3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tags", tagsPacker3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                if (Packer4.SelectedValue != "-- ")
                {
                    cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text.Trim());
                    cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Packer", Packer4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lines", linesPacker4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tags", tagsPacker4.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                if (Packer5.SelectedValue != "-- ")
                {
                    cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text.Trim());
                    cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Packer", Packer5.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lines", linesPacker5.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tags", tagsPacker5.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                if (Packer6.SelectedValue != "-- ")
                {
                    cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text.Trim());
                    cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@Packer", Packer6.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lines", linesPacker6.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tags", tagsPacker6.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text.Trim());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                messageLabel1.Visible = true;
                messageLabel2.Visible = true;
                messageLabel2.Style.Add("color", "Green");
                messageLabel1.Text = orderNumber;
                messageLabel2.Text = " successfully added.";
                editOrderButton.Visible = false;

            }
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();
        }
        //update Truck FOB
        if (truck_FOB.Text != "")
        {
            checkFOB(Order_Number.Text, Decimal.Parse(truck_FOB.Text));
        }
        clearForm();
    }

    protected void editOrderButton_Click(object sender, EventArgs e)
    {
        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_UPS"].ConnectionString))
        {
            int loopCounter = 0;
            string checkString;
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
            String query = "SELECT * From Truck where Order_Number =@Order_Number";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@Order_Number", messageLabel1.Text);
            Console.Write("Executing Reader");
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                loopCounter++;
                Order_Number.Text = (string)reader["Order_Number"];
                checkString = (reader["Truck_CO"].ToString());
                if (checkString != "")
                {
                    int s3 = Truck_CO.Items.IndexOf(Truck_CO.Items.FindByValue(checkString));
                    Truck_CO.SelectedIndex = Truck_CO.Items.IndexOf(Truck_CO.Items.FindByValue(checkString));
                    //= Truck_CO.Items.IndexOf(Truck_CO.Items.FindByValue((reader["Truck_CO"].ToString())));
                }
                ShipMonth.Text = (string)reader["ShipMonth"];
                ShipDay.Text = "" + (string)reader["ShipDay"];
                ShipYear.Text = "" + (string)reader["ShipYear"];
                Tracking_number.Text = "" + (string)reader["Tracking_number"];
                int check;
                if (loopCounter == 1)
                {
                    checkString = reader["Packer"].ToString();
                    Packer1.SelectedIndex = Packer1.Items.IndexOf(Packer1.Items.FindByValue((reader["Packer"].ToString())));
                    linesPacker1.Text = reader["Lines"].ToString();
                    tagsPacker1.Text = reader["Tags"].ToString();
                }
                else if (loopCounter == 2)
                {
                    checkString = reader["Packer"].ToString();
                    Packer2.SelectedIndex = Packer2.Items.IndexOf(Packer2.Items.FindByValue((reader["Packer"].ToString())));
                    linesPacker2.Text = reader["Lines"].ToString();
                    tagsPacker2.Text = reader["Tags"].ToString();
                }
                else if (loopCounter == 3)
                {
                    checkString = reader["Packer"].ToString();
                    Packer3.SelectedIndex = Packer3.Items.IndexOf(Packer3.Items.FindByValue((reader["Packer"].ToString())));
                    linesPacker3.Text = reader["Lines"].ToString();
                    tagsPacker3.Text = reader["Tags"].ToString();
                }
                else if (loopCounter == 4)
                {
                    checkString = reader["Packer"].ToString();
                    Packer4.SelectedIndex = Packer4.Items.IndexOf(Packer4.Items.FindByValue((reader["Packer"].ToString())));
                    linesPacker4.Text = reader["Lines"].ToString();
                    tagsPacker4.Text = reader["Tags"].ToString();
                }
                else if (loopCounter == 5)
                {
                    checkString = reader["Packer"].ToString();
                    Packer5.SelectedIndex = Packer5.Items.IndexOf(Packer5.Items.FindByValue((reader["Packer"].ToString())));
                    linesPacker5.Text = reader["Lines"].ToString();
                    tagsPacker5.Text = reader["Tags"].ToString();
                }
                else if (loopCounter == 6)
                {
                    checkString = reader["Packer"].ToString();
                    Packer6.SelectedIndex = Packer6.Items.IndexOf(Packer6.Items.FindByValue((reader["Packer"].ToString())));
                    linesPacker6.Text = reader["Lines"].ToString();
                    tagsPacker6.Text = reader["Tags"].ToString();
                }



            }
            reader.Close();
            Order_Number.Enabled = false;
            cmd.Parameters.Clear();
            messageLabel1.Visible = true;
            messageLabel2.Visible = true;
            messageLabel2.Style.Add("color", "Green");
            messageLabel1.Text = Order_Number.Text;
            messageLabel2.Text = " being edited.";
            Update.Visible = true;
            Enter.Visible = false;
            editOrderButton.Visible = false;
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();
        }
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_INSIDER"].ConnectionString))
        {

            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
			String query = "SELECT ML_UDF_SOH_OUTBOUND_SHIP From TruckEntry where SALESORDERNumber =@SalesOrderNumber";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@SalesOrderNumber",  Order_Number.Text);
            Console.Write("Executing Reader");
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["ML_UDF_SOH_OUTBOUND_SHIP"].ToString() != null)
                {
                    truck_FOB.Text = reader["ML_UDF_SOH_OUTBOUND_SHIP"].ToString();
                }
            }
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();

        }
    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        messageLabel1.Visible = false;
        messageLabel2.Visible = false;
        editOrderButton.Visible = false;
        Update.Visible = false;
        Enter.Visible = true;
        editOrderButton.Visible = false;
        Order_Number.Enabled = true;
        clearForm();
    }

    protected void Update_Click(object sender, EventArgs e)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_UPS"].ConnectionString))
        {
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
            String query = "Delete From Truck where Order_Number =@Order_Number";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();


            query = "INSERT INTO Truck VALUES(@Order_Number,@Truck_CO,@ShipMonth,@ShipDay,@ShipYear,@Packer,@Lines,@Tags,@Tracking_number)";

            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            if (Packer1.Text != "")
            {
                cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text);
                cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text);
                cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text);
                cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text);
                cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text);
                cmd.Parameters.AddWithValue("@Packer", Packer1.Text);
                cmd.Parameters.AddWithValue("@Lines", linesPacker1.Text);
                cmd.Parameters.AddWithValue("@Tags", tagsPacker1.Text);
                cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            if (Packer2.Text != "")
            {
                cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text);
                cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text);
                cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text);
                cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text);
                cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text);
                cmd.Parameters.AddWithValue("@Packer", Packer2.Text);
                cmd.Parameters.AddWithValue("@Lines", linesPacker2.Text);
                cmd.Parameters.AddWithValue("@Tags", tagsPacker2.Text);
                cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            if (Packer3.Text != "")
            {
                cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text);
                cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text);
                cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text);
                cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text);
                cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text);
                cmd.Parameters.AddWithValue("@Packer", Packer3.Text);
                cmd.Parameters.AddWithValue("@Lines", linesPacker3.Text);
                cmd.Parameters.AddWithValue("@Tags", tagsPacker3.Text);
                cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            if (Packer4.Text != "")
            {
                cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text);
                cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text);
                cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text);
                cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text);
                cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text);
                cmd.Parameters.AddWithValue("@Packer", Packer4.Text);
                cmd.Parameters.AddWithValue("@Lines", linesPacker4.Text);
                cmd.Parameters.AddWithValue("@Tags", tagsPacker4.Text);
                cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            if (Packer5.Text != "")
            {
                cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text);
                cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text);
                cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text);
                cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text);
                cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text);
                cmd.Parameters.AddWithValue("@Packer", Packer5.Text);
                cmd.Parameters.AddWithValue("@Lines", linesPacker5.Text);
                cmd.Parameters.AddWithValue("@Tags", tagsPacker5.Text);
                cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            if (Packer6.Text != "")
            {
                cmd.Parameters.AddWithValue("@Order_Number", Order_Number.Text);
                cmd.Parameters.AddWithValue("@Truck_CO", Truck_CO.Text);
                cmd.Parameters.AddWithValue("@ShipMonth", ShipMonth.Text);
                cmd.Parameters.AddWithValue("@ShipDay", ShipDay.Text);
                cmd.Parameters.AddWithValue("@ShipYear", ShipYear.Text);
                cmd.Parameters.AddWithValue("@Packer", Packer6.Text);
                cmd.Parameters.AddWithValue("@Lines", linesPacker6.Text);
                cmd.Parameters.AddWithValue("@Tags", tagsPacker6.Text);
                cmd.Parameters.AddWithValue("@Tracking_number", Tracking_number.Text);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            messageLabel1.Visible = true;
            messageLabel2.Visible = true;
            messageLabel2.Style.Add("color", "Green");
            messageLabel1.Text = orderNumber;
            messageLabel2.Text = " successfully updated.";
            editOrderButton.Visible = false;
            Order_Number.Enabled = true;
            Update.Visible = false;
            Enter.Visible = true;

            cmd.Cancel();
            cmd.Dispose();
            conn.Close();
        }
        //update Truck FOB
        if (truck_FOB.Text != null || truck_FOB.Text != "")
        {
            checkFOB(Order_Number.Text, Decimal.Parse(truck_FOB.Text));
        }
        clearForm();

    }
}
