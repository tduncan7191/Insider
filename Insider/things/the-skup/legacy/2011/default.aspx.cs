using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;

public partial class skup : System.Web.UI.Page
{
    #region Variables
    String[,] queryArray;
    int rows = 0;
    int beginingBalance = 0;
    //year Start dates Need to move to maintance page or area
    int firstTransactionEntryYear = 2025;
    DateTime todaysDate = DateTime.Now;
    DateTime firstDateSold = GetEndOfCurrentWeek();
    DateTime firstDay = GetStartOfCalculationWeek(0);
    DateTime lastDay = GetEndOfCalculationWeek(0);
    DateTime Start = GetStartOfCalculationWeek(1);
    DateTime End = GetEndOfCalculationWeek(1);
    DateTime[,] startEndDates = new DateTime[53, 6];
    DateTime begin2012 = new DateTime(2012, 1, 1, 0, 0, 0);
    DateTime begin2011 = new DateTime(2011, 1, 2, 0, 0, 0);
    DateTime begin2010 = new DateTime(2010, 1, 3, 0, 0, 0);
    DateTime begin2009 = new DateTime(2009, 1, 4, 0, 0, 0);
    DateTime begin2008 = new DateTime(2008, 1, 6, 0, 0, 0);
    int[, ,] unitsSold = new int[(DateTime.Now.Year - 2008) + 1, 53, 35];
    
    
                
    
    List<SqlParameter> insertParameters = new List<SqlParameter>();

    int arrivalsLoopCounter = 0;
    int totalcount = 0;
    String enteredSku = "0";

    /**START Varible Postions**/
    String[] InfoList = new String[] { "Dates", "BI", "BR", "BZ", "IA", "IP", "IT", "IZ", "PZ", "AFMBW", "AMBW", "FMBW", "AFSLSU", "ASLSU", "FSLSU", "HSLSU", "FPSLSU", "AFMOV", "AMOV", "FMOV", "AFBW", "ABW", "FBW", "AFBOP", "ABOP", "FBOP", "BOPWS", "AFSO%", "AFOS", "OS", "FOS", "AFFPO", "APO", "FutPO", "FPO" };

    //A prefix = Actual
    //F prefix = Forecast
    //AF prefix = Actual or Forecast
    //imports
    int datePos = 0;
    int BI = 1;
    int BR = 2;
    int BZ = 3;
    int IA = 4;
    int IP = 5;
    int IT = 6;
    int IZ = 7;
    int PZ = 8;

    //MBW
    int AFMBW = 9;
    int AMBW = 10;
    int FMBW = 11;

    //SLS
    int AFSLSU = 12;
    int ASLSU = 13; //import
    int FSLSU = 14;
    int HSLSU = 15;
    int FPSLSU = 16;

    //imports total changes
    int AFMOV = 17;
    int AMOV = 18;
    int FMOV = 19;

    //BW
    int AFBW = 20;
    int ABW = 21;
    int FBW = 22;

    //BOP
    int BOP = 23;
    int ABOP = 24;
    int FBOP = 25;
    int BOPWS = 26;
    int AFSOpercent = 27;

    //OUT of Stock
    int AFOS = 28;
    int AOS = 29;
    int FOS = 30;

    //Purchase orders
    int AFFPO = 31;
    int APO = 32;
    int FutPO = 33;
    int FPO = 34;
    /**END Varible Postions**/
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        Image1.ImageUrl = "http://www.redemptionplus.com/_FileLibrary/FileImage/redemption_news.jpg";
        Image1.Attributes.Remove("Width");
        Image1.Attributes.Remove("Height");
        Image1.Attributes.Add("Width", "132");
        Image1.Attributes.Add("Height", "84");

        if (DateTime.Now.Month > 6) {
            unitsSold = new int[((DateTime.Now.Year+1) - 2008) + 1, 53, 35];
        }
    }
    #endregion

    #region Servercalls

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

    public Boolean fillItemsWOrders(String EnteredSku)
    {
        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
        {
            int weekNumber = 0;
            int RecordYear = -1;
            int bBalAdj = 0;
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            String query = "select ItemNumber,TransactionCode, TransactionDate, sum(qty) as qty from ("
								+ " SELECT SKU as ItemNumber,'FutPO' as TransactionCode, DATEADD(wk,DATEDIFF(wk,0,RequiredDate),-1) as TransactionDate, QTYOrdered as qty"
									+ " FROM [ROReport_ProductArrival]"
									+ " where sku= @Entered_Sku"
								+ " Union All select IM5.ItemCode ItemNumber, TransactionCode,DATEADD(wk,DATEDIFF(wk,0,TransactionDate),-1) as TransactionDate, sum(TransactionQty) as qty"
									+ " from IM_ITEMTRANSACTIONHISTORY IM5"
									+ " where IM5.TransactionDate>= '2008-01-01 00:00:00.000' and IM5.TransactionDate<Getdate() and IM5.warehouseCode ='000' and IM5.ItemCode = @Entered_Sku"
									+ " group by IM5.ItemCode, TransactionCode,TransactionDate"
								+ " union all SELECT ItemNumber, 'AOS' as TransactionCode, DATEADD(wk, DATEDIFF(wk, 0, Date_Out), - 1) AS TransactionDate, SUM(convert(int,Count_Day)) AS qty"
									+ " FROM SALES.dbo.PRODUCT_OUT_STOCK_REORDER PRODUCT_OUT_STOCK_REORDER"
									+ " WHERE (ItemNumber = @Entered_Sku)"
									+ " GROUP BY ItemNumber,DATEADD(wk, DATEDIFF(wk, 0, Date_Out), - 1)"
								+ " union all SELECT ItemCode as ItemNumber,Type as TransactionCode,DATEADD(wk,DATEDIFF(wk,0,TransactionDate), - 1) as TransactionDate,value as qty"
									+ " FROM SALES.dbo.SkupFutureEntrys"
									+ " where itemcode= @Entered_Sku"
								+ " )Base"
								+ " group by ItemNumber,TransactionCode,TransactionDate order by transactionDate";
            // 300162 - String query = "select ItemNumber,TransactionCode, TransactionDate, sum(qty) as qty from (SELECT SKU as ItemNumber,'PO' as TransactionCode, DATEADD(wk,DATEDIFF(wk,0,RequiredDate),-1) as TransactionDate, QTYOrdered as qty FROM [ROReport_ProductArrival] where sku= 300162 Union All select IM5.ItemNumber, TransactionCode,DATEADD(wk,DATEDIFF(wk,0,TransactionDate),-1) as TransactionDate, sum(TransactionQty) as qty from IM5_TransactionDetail IM5 inner Join IM_90_UDF_IM_Masterfile IM90 on IM90.ItemNumber=IM5.ItemNumber where IM5.TransactionDate>= '2008-01-01 00:00:00.000' and IM5.TransactionDate<'2011-01-04 00:00:00.000' and IM5.warehouseCode ='000' and IM5.ItemNumber = 300162 group by IM5.ItemNumber, TransactionCode,TransactionDate union all SELECT ItemNumber, 'OS' as TransactionCode, DATEADD(wk, DATEDIFF(wk, 0, Date_Out), - 1) AS TransactionDate, SUM(convert(int,Count_Day)) AS qty FROM SALES.dbo.PRODUCT_OUT_STOCK_REORDER PRODUCT_OUT_STOCK_REORDER WHERE (ItemNumber = '300162') GROUP BY ItemNumber,DATEADD(wk, DATEDIFF(wk, 0, Date_Out), - 1))Base group by ItemNumber,TransactionCode,TransactionDate order by transactionDate";
            /* String query = "select IM5.ItemNumber, TransactionCode,TransactionDate, sum(TransactionQty) as qty from IM5_TransactionDetail IM5 inner Join IM_90_UDF_IM_Masterfile IM90 on IM90.ItemNumber=IM5.ItemNumber where IM5.TransactionDate>= '2008-01-01 00:00:00.000' and IM5.TransactionDate<'2011-01-04 00:00:00.000' and IM5.warehouseCode ='000' and IM5.ItemNumber = @Entered_Sku group by IM5.ItemNumber	, TransactionCode	,TransactionDate order by IM5.ItemNumber,TransactionDate,TransactionCode";
             SELECT ROReport_NoParm_IS.SKU, ROReport_NoParm_IS.ItemDescription, ROReport_NoParm_IS.Class, ROReport_NoParm_IS.DAILYUSAGE AS ADU, ((ROReport_NoParm_IS.QOH)-(ROReport_NoParm_IS.QOSC)-(ROReport_NoParm_IS.QOSO)-(ROReport_NoParm_IS.QOBO)-(ROReport_NoParm_IS.QOSC)-(ROReport_NoParm_IS.QOASC)) AS BB FROM ROReport_NoParm_IS ROReport_NoParm_IS WHERE ROReport_NoParm_IS.SKU like '3%' AND ROReport_NoParm_IS.WhseCode = '000' AND ROReport_NoParm_IS.IM9WC = '000' AND (ROReport_NoParm_IS.Class = 'IS' OR ROReport_NoParm_IS.Class = 'IPG')and  ROReport_NoParm_IS.CB_UDF_IMH_REORDER ='Y'*/
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@Entered_Sku", EnteredSku);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                weekNumber = 0;
                String ItemNumber = (String)(reader["ItemNumber"]);
                String TransactionCode = (String)(reader["ItemNumber"]);
                DateTime TransactionDate = GetStartOfDateWeek(Convert.ToDateTime(reader["TransactionDate"]));
                decimal qty = (decimal)reader["qty"];
                bool dateRangeFound = false;
                String testTransCode = ((String)reader["TransactionCode"]);

                if ((TransactionDate < startEndDates[0, 0]))
                {
                    bBalAdj += (int)((decimal)reader["qty"]);
                }

                if (TransactionDate != null)
                {

                    /********** Need add findOldestEntry(); *************/
                    if ((firstTransactionEntryYear > TransactionDate.Year) && (TransactionDate.Year != 2007))
                    {
                        firstTransactionEntryYear = TransactionDate.Year;
                    }
                    weekNumber = FindDateRange(TransactionDate);
                    if (weekNumber != -1)
                    {
                        dateRangeFound = true;
                    }
                    RecordYear = TransactionDate.Year - 2008;
                }
                else
                {
                    RecordYear = -1;
                    weekNumber = -1;
                    dateRangeFound = false;
                }//out of range escape


                /*************standardized make method**************/
                if ((dateRangeFound))
                {
                    if (((String)reader["TransactionCode"]) == "BI")
                    {
                        unitsSold[RecordYear, weekNumber, BI] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "BR")
                    {
                        unitsSold[RecordYear, weekNumber, BR] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "BZ")
                    {
                        unitsSold[RecordYear, weekNumber, BZ] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IA")
                    {
                        unitsSold[RecordYear, weekNumber, IA] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IP")
                    {
                        unitsSold[RecordYear, weekNumber, IP] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IT")
                    {
                        unitsSold[RecordYear, weekNumber, IT] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IZ")
                    {
                        unitsSold[RecordYear, weekNumber, IZ] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "PO")
                    {
                        unitsSold[RecordYear, weekNumber, APO] += (int)((decimal)reader["qty"]);
                        unitsSold[RecordYear, weekNumber, AFFPO] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "PZ")
                    {
                        unitsSold[RecordYear, weekNumber, PZ] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "SO")
                    {
                        unitsSold[RecordYear, weekNumber, ASLSU] += (int)((decimal)reader["qty"]);
                        unitsSold[RecordYear, weekNumber, AFSLSU] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "AOS")
                    {
                        unitsSold[RecordYear, weekNumber, AOS] += (int)((decimal)reader["qty"]);
                        unitsSold[RecordYear, weekNumber, AFOS] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "FPO")
                    {
                        unitsSold[RecordYear, weekNumber, FPO] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "FutPO")
                    {
                        unitsSold[RecordYear, weekNumber, FutPO] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "FSLSU")
                    {
                        unitsSold[RecordYear, weekNumber, FSLSU] += (int)((decimal)reader["qty"]);
                    }
                }
            }//end while
            reader.Close();
            cmd.Parameters.Clear();
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();
            beginingBalance = beginingBalanceFinder(enteredSku) + bBalAdj;
            return true;
        }
    }

    public Boolean fillItemsInfo(String EnteredSku)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
        {
            //int loopCounter = 0;
            System.Data.SqlClient.SqlDataReader reader2 = null;
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

            String query = "SELECT * FROM vw_Item_Setup where ItemNumber=@Entered_Sku";
            cmd2.CommandText = query;
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = conn;
            cmd2.Parameters.AddWithValue("@Entered_Sku", EnteredSku);
            conn.Open();
            reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                firstDateSold = Convert.ToDateTime(reader2["First_DATE_SOLD"]);
                VendorNumberBox.Text = (String)(reader2["VendorNumber"]);
                DescriptionBox.Text = (String)(reader2["ItemDescription"]);
                ActiveTextBox.Text = (String)(reader2["Active"]);
                ReorderTextBox.Text = (String)(reader2["Reorder"]);
                if (!((reader2["MainCategory"]).Equals(System.DBNull.Value)))
                {
                    MainCategoryBox.Text = (String)(reader2["MainCategory"]);
                }
                AvgCostBox.Text = System.Convert.ToString(Math.Round((decimal)(reader2["AveCost"]) * 1000) / 1000);
                SB1Box.Text = System.Convert.ToString(Math.Round((decimal)(reader2["SBP1"]) * 1000) / 1000);
                LeadTimeBox.Text = System.Convert.ToString((int)(decimal)(reader2["LastLeadTime"]));
                StandardLeadTimeBox.Text = System.Convert.ToString((int)(decimal)(reader2["StandardLeadTime"]));
                ClassBox.Text = (String)(reader2["Class"]);
                MOQBox.Text = System.Convert.ToString((int)(decimal)(reader2["MinimumOrderQty"]));
                adu.Text = System.Convert.ToString(Math.Round((decimal)(reader2["DAILYUSAGE"])));
            }
            reader2.Close();
            cmd2.Parameters.Clear();
            cmd2.Cancel();
            cmd2.Dispose();
            conn.Close();
            return true;
        }
    }

    public int getOldSystemADU(String EnteredSku,int year, int month)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_SALES"].ConnectionString))
        {
            int formerADU = 0;
            System.Data.SqlClient.SqlDataReader reader2 = null;
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

            String query = "SELECT ADU from SkupHistoricADUs where ItemCode=@Entered_Sku and year(entryDate)=@year and month(entryDate)=@month";
            cmd2.CommandText = query;
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = conn;
            cmd2.Parameters.AddWithValue("@Entered_Sku", EnteredSku);
            cmd2.Parameters.AddWithValue("@year", year);
            cmd2.Parameters.AddWithValue("@month", month);
            conn.Open();
            reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                if (!((reader2["ADU"]).Equals(System.DBNull.Value)))
                {
                    formerADU = (int)(Math.Round((decimal)(reader2["ADU"]) * 100) / 100);
                }
            }
            reader2.Close();
            cmd2.Parameters.Clear();
            cmd2.Cancel();
            cmd2.Dispose();
            conn.Close();
            return formerADU;
        }
    }

    #endregion

    #region Calcs
    public void CalcValues()
    {
        #region fillActuals
        int start = FindTodaysDateRange();
        int Year = todaysDate.Year - 2008;
        int fAdU = System.Convert.ToInt32(adu.Text, 10);
        int Years = unitsSold.GetLength(0);
        int currentWeekNumber = FindDateRange(GetStartOfCurrentWeek());

        firstTransactionEntryYear = firstDateSold.Year;
        if (firstTransactionEntryYear < 2008) { firstTransactionEntryYear = 2008; }
        if (firstDateSold < GetEndOfCurrentWeek())
        {
            // Place holder for when years have 53 instead of 52 weeks
            int previousYearWks = 0;
            for (int y = (firstTransactionEntryYear - 2008); y < Years; y++)
            {
                //set weeks in year 3
                /*** Needs logic for 53 Week Years*/
                int yearsOfUsage = DateTime.Now.Year - 2008;
                int weeks = 52;
                int FSLSUADU = 0;
                for (int w = 0; w < weeks; w++)
                {
                    //zero out week total
                    int weekTotal = 0;
                    int fweekTotal = 0;
                    int FuturePO = 0;
                    //total of all weeks transactions
                    weekTotal += unitsSold[y, w, BI];
                    weekTotal += unitsSold[y, w, BR];
                    weekTotal += unitsSold[y, w, BZ];
                    weekTotal += unitsSold[y, w, IA];
                    weekTotal += unitsSold[y, w, IP];
                    weekTotal += unitsSold[y, w, IT];
                    weekTotal += unitsSold[y, w, IZ];
                    weekTotal += unitsSold[y, w, PZ];
                    weekTotal += unitsSold[y, w, ASLSU];
                    weekTotal += unitsSold[y, w, APO];

                    if (unitsSold[y, w, FSLSU] != 0)
                    {
                        FSLSUADU = unitsSold[y, w, FSLSU];
                    }

                    if (currentWeekNumber > w && yearsOfUsage == y)
                    {
                        unitsSold[y, w, FPSLSU] = -(Math.Abs(Convert.ToInt32(getOldSystemADU(skuNumber.Text, (2008 + y), startEndDates[w, y].Month))) * (7));
                    }
                    else if (currentWeekNumber <= w && yearsOfUsage == y)
                    {
                        int fadu = 0;

                        if (currentWeekNumber == w)
                        {
                            unitsSold[y, w, FPSLSU] = -(Math.Abs(Convert.ToInt32(adu.Text)) * ((int)GetEndOfCurrentWeek().DayOfWeek - (int)DateTime.Now.DayOfWeek));
                            unitsSold[y, w, FSLSU] = -((Math.Abs(FSLSUADU)/7) * ((int)GetEndOfCurrentWeek().DayOfWeek - (int)DateTime.Now.DayOfWeek));
                        }
                        else {
                            int test56=getFPSLSU(int.Parse(adu.Text), ClassBox.Text, startEndDates[w, y]);//adu.Text
                            unitsSold[y, w, FPSLSU] = -(Math.Abs(Convert.ToInt32(test56)) * (7));
                            unitsSold[y, w, FSLSU] = -(Math.Abs(FSLSUADU));
                        }

                        if (FSLSUADU > 0)
                        {
                            fweekTotal += unitsSold[y, w, FSLSU];
                            unitsSold[y, w, AFSLSU] += unitsSold[y, w, FSLSU] + unitsSold[y, w, ASLSU];
                        }
                        else
                        {
                            fweekTotal += unitsSold[y, w, FPSLSU];
                            unitsSold[y, w, AFSLSU] += unitsSold[y, w, FPSLSU] + unitsSold[y, w, ASLSU];
                        }

                        if (unitsSold[y, w, FPO] == 0) {
                            fweekTotal += unitsSold[y, w, FutPO];
                            FuturePO = unitsSold[y, w, FutPO];
                        }
                        else {
                        fweekTotal += unitsSold[y, w, FPO];
                        FuturePO = unitsSold[y, w, FPO];
                        }
                    }


                    unitsSold[y, w, AFFPO] = FuturePO + unitsSold[y, w, APO];

                    //add total to weeks movement
                    unitsSold[y, w, FMOV] = fweekTotal;
                    unitsSold[y, w, AMOV] = weekTotal;
                    unitsSold[y, w, AFMOV] = (unitsSold[y, w, AMOV] + unitsSold[y, w, FMOV]);

                    /* Start Calc Week Begin */
                    if (w == 0 && y == (firstTransactionEntryYear - 2008))
                    {//calc for first year mainly 2008
                        unitsSold[y, w, BOP] = unitsSold[y, w, ABOP] = beginingBalance;
                        unitsSold[y, w, AFBW] = (Math.Abs(unitsSold[y, w, ASLSU]) - Math.Abs(unitsSold[y, w, FPSLSU]));
                    }
                    else if (w == 0 && y != (firstTransactionEntryYear - 2008))//calc first week -- after first year
                    {
                        unitsSold[y, w, BOP] = unitsSold[y, w, ABOP] = unitsSold[y - 1, previousYearWks - 1, AFMOV] + unitsSold[y - 1, previousYearWks - 1, ABOP];
                        unitsSold[y, w, AFBW] = (Math.Abs(unitsSold[y, w, ASLSU]) - Math.Abs(unitsSold[y, w, FPSLSU]));
                       // int test1=unitsSold[y - 1, previousYearWks - 1, ASLSU];
                       // int test2 = unitsSold[y, w, BOP];
                      //  unitsSold[y, w, AFSOpercent] = -(unitsSold[y, w, ASLSU]) / (unitsSold[y, w, BOP]);
                      //  int test5 = unitsSold[y, w, AFSOpercent];
                    }
                    else if (y != Years - 1 && w > 0)
                    {
                        unitsSold[y, w, BOP] = unitsSold[y, w, ABOP] = unitsSold[y, (w - 1), AFMOV] + unitsSold[y, (w - 1), ABOP];
                        unitsSold[y, w, AFBW] = (Math.Abs(unitsSold[y, w, ASLSU]) - Math.Abs(unitsSold[y, w, FPSLSU]));
                       // int test3 = unitsSold[y, (w - 1), ASLSU];
                       // int test4 = unitsSold[y, w, BOP];
                       // unitsSold[y, w, AFSOpercent] = -(unitsSold[y, w, ASLSU]) / (unitsSold[y, w, BOP]);
                      //  int test7 = unitsSold[y, w, AFSOpercent];
                    }
                    else if (y == Years - 1 && w < currentWeekNumber && w > 0)
                    {
                        unitsSold[y, w, BOP] = unitsSold[y, w, ABOP] = unitsSold[y, (w - 1), AFMOV] + unitsSold[y, (w - 1), ABOP];
                        unitsSold[y, w, AFBW] = (Math.Abs(unitsSold[y, w, ASLSU]) - Math.Abs(unitsSold[y, w, FPSLSU]));
                       // unitsSold[y, w, AFSOpercent] = -(unitsSold[y, w, ASLSU]) / (unitsSold[y, w, BOP]);
                        //int test8 = unitsSold[y, w, AFSOpercent];
                    }
                  else if (y == Years - 1 && w == currentWeekNumber && w > 0)
                  { //hererrerere
                      unitsSold[y, w, BOP] = unitsSold[y, w, ABOP] = unitsSold[y, w, FBOP] = unitsSold[y, (w - 1), AFMOV] + unitsSold[y, (w - 1), ABOP];
                      
                  }
                    else if (y == Years - 1 && w >= currentWeekNumber && w > 0)
                    { //hererrerere
                        unitsSold[y, w, BOP] = unitsSold[y, w, FBOP] = unitsSold[y, (w - 1), AFMOV] + unitsSold[y, (w - 1), FBOP];
                       
                    }

                    if (unitsSold[y, w, BOP] < 0) {
                        unitsSold[y, w, BOP] = unitsSold[y, w, FBOP] = 0;
                        unitsSold[y, w, AFBW] = (Math.Abs(unitsSold[y, w, ASLSU]) - Math.Abs(unitsSold[y, w, FPSLSU]));                  
                    }
                    /**** Calc Historic Weekly Usage ****/
                   getHistoricADU(y, w, (firstTransactionEntryYear - 2008));
                    //unitsSold[y, w, ASLSU] + unitsSold[y - 1, w, HSLSU];
                }
                previousYearWks = weeks;
            }
        }
        #endregion

        /***** Begin Calcs after Actuals***/
        if (firstDateSold < GetEndOfCurrentWeek())
        {
            //int yearsOfUsage = DateTime.Now.Year - firstTransactionEntryYear;
            for (int y = (firstTransactionEntryYear - 2008); y < Years; y++)
            {
                int yearsOfUsage = DateTime.Now.Year - 2008;
                if (DateTime.Now.Month > 6)
                {
                    yearsOfUsage++;
                }
                int weeks = 52;
                for (int w = 0; w < weeks; w++)
                {
                    /**** Calc BOP WS ****/
                    if (unitsSold[y, w, BOP] > 0) {

                        int week = 0;
                        int turns = 0;
                        int yearAdjust = 0;
                        int units = unitsSold[y, w, BOP];
                        int startYear = y;
                        int startWeek = w;
                        do
                        {
                            if (w==8){
                                int testline = w;
                            }
                                if ((startWeek + week <= 53) && (startYear + yearAdjust) <= yearsOfUsage)
                                {
                                    if ((unitsSold[startYear + yearAdjust, startWeek + week, AOS] == 7) ||(unitsSold[startYear + yearAdjust, startWeek + week, BOP]<=0))
                                    {
                                        units = 0;
                                    }
                                    else
                                    {
                                        int testUnitsold = unitsSold[startYear + yearAdjust, startWeek + week, AFSLSU];
                                        int testBOP = unitsSold[y, w, BOP];
                                        units = units + unitsSold[startYear + yearAdjust, startWeek + week, AFSLSU];
                                        turns++;
                                    }
                                }
                                else if (((startYear + yearAdjust) <= yearsOfUsage))
                                {
                                    startWeek = 0;
                                    week = 0;
                                    yearAdjust++;
                                    if ((startYear + yearAdjust) <= yearsOfUsage)
                                    {
                                        if ((unitsSold[startYear + yearAdjust, startWeek + week, AOS] == 7) || (unitsSold[startYear + yearAdjust, startWeek + week, BOP] <= 0))
                                        {
                                            units = 0;
                                        }
                                        else
                                        {
                                            int testUnitsold2=unitsSold[startYear + yearAdjust, startWeek + week, AFSLSU];
                                            units = units + unitsSold[startYear + yearAdjust, startWeek + week, AFSLSU];
                                            turns++;
                                        }

                                    }
                                }
                                else
                                {
                                    //add switch over to fsls
                                    units = 0;
                                }                            
                            week++;
                        } while (units > 0); //while (units > 0 && (unitsSold[startYear + yearAdjust, startWeek + week, AOS] < 7));
                        unitsSold[y, w, BOPWS] = turns;
                        turns = 0;
                    }

                }
            }
        }

    }

    public int getFPSLSU(int currentADU,  String category, DateTime weekDate)
    {
        int adjustedADU=0;


		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_SALES"].ConnectionString))
        {
            decimal forecastPercent = 0;
            System.Data.SqlClient.SqlDataReader reader2 = null;
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

            String query = "SELECT forecastYear,forecastCategory,Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec from SkupForecasts where forecastCategory=@forecastCategory and forecastYear=@forecastYear";
            cmd2.CommandText = query;
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = conn;
            cmd2.Parameters.AddWithValue("@forecastCategory", category);
            cmd2.Parameters.AddWithValue("@forecastYear", weekDate.Year);
            conn.Open();
            reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                if ( !((reader2["Jan"]).Equals(System.DBNull.Value)) && (weekDate.Month==1) )
                {
                    forecastPercent = (decimal)(reader2["Jan"]);
                }
                if (!((reader2["Feb"]).Equals(System.DBNull.Value)) && (weekDate.Month == 2))
                {
                    forecastPercent = (decimal)(reader2["Feb"]);
                }
                if (!((reader2["Mar"]).Equals(System.DBNull.Value)) && (weekDate.Month == 3))
                {
                    forecastPercent = (decimal)(reader2["Mar"]);
                }
                if (!((reader2["Apr"]).Equals(System.DBNull.Value)) && (weekDate.Month == 4))
                {
                    forecastPercent = (decimal)(reader2["Apr"]);
                }
                if (!((reader2["May"]).Equals(System.DBNull.Value)) && (weekDate.Month == 5))
                {
                    forecastPercent = (decimal)(reader2["May"]);
                }
                if (!((reader2["Jun"]).Equals(System.DBNull.Value)) && (weekDate.Month == 6))
                {
                    forecastPercent = (decimal)(reader2["Jun"]) ;
                }
                if (!((reader2["Jul"]).Equals(System.DBNull.Value)) && (weekDate.Month == 7))
                {
                    forecastPercent = (decimal)(reader2["Jul"]);
                }
                if (!((reader2["Aug"]).Equals(System.DBNull.Value)) && (weekDate.Month == 8))
                {
                    forecastPercent = (decimal)(reader2["Aug"]);
                }
                if (!((reader2["Sep"]).Equals(System.DBNull.Value)) && (weekDate.Month == 9))
                {
                    forecastPercent = (decimal)(reader2["Sep"]);
                }
                if (!((reader2["Oct"]).Equals(System.DBNull.Value)) && (weekDate.Month == 10))
                {
                    forecastPercent = (decimal)(reader2["Oct"]);
                }
                if (!((reader2["Nov"]).Equals(System.DBNull.Value)) && (weekDate.Month == 11))
                {
                    forecastPercent = (decimal)(reader2["Nov"]);
                }
                if (!((reader2["Dec"]).Equals(System.DBNull.Value)) && (weekDate.Month == 12))
                {
                    forecastPercent = (decimal)(reader2["Dec"]);
                }
            }
            reader2.Close();
            cmd2.Parameters.Clear();
            cmd2.Cancel();
            cmd2.Dispose();
            conn.Close();
            adjustedADU = Convert.ToInt32((Math.Round((currentADU / ((decimal).927)) * forecastPercent)*100)/100);
            return adjustedADU;
        }        
    }
    void getHistoricADU(int Year, int week, int startYr)
    {
        int RunningTotal = 0;
        int OS = 0;
        for (int y = startYr; y <= Year; y++)
        {
            RunningTotal += unitsSold[y, week, ASLSU];
            OS += unitsSold[y, week, AOS];
        }
        if ((((Year - startYr + 1) * 7) - OS) > 0)
        {
            unitsSold[Year, week, HSLSU] = RunningTotal / (((Year - startYr + 1) * 7) - OS);
        }
        else
        {
            unitsSold[Year, week, HSLSU] = 0;
        }
    }
    public int FindDateRange(DateTime datesent)
    {
        int Year = datesent.Year - 2008;
        int cYear = todaysDate.Year - 2008;
        int weekValueFound = -1;
        int weeks = 52;
        if (todaysDate.Year == 2012)
        {
            weeks = 53;
        }

        for (int i = 0; i < weeks; i++)
        {
            if (Year == -1)
            {
                weekValueFound = -1;
                i = weeks + 1;
            }
            else if (datesent == startEndDates[i, Year])
            {
                weekValueFound = i;
                i = weeks + 1;
            }

            if (Year == 2)
            {
                int yearTest = 2;
            }

        }
        return weekValueFound;


    }
    public int FindTodaysDateRange()
    {//record year 2010=2 
        int Year = todaysDate.Year - 2008;
        int weekValueFound = -1;
        for (int i = 0; i < 52; i++)
        {
            if ((todaysDate >= startEndDates[i, Year]) && (todaysDate < startEndDates[i + 1, Year]))
            {
                weekValueFound = i;
                i = 52;
            }
        }
        return weekValueFound;
    }
    public int beginingBalanceFinder(String SkuEntered)
    {
        int beginingbalance = 0;
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
        {
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            int fds = firstDateSold.Year;
            if (fds<2008){fds=2008;}

            String query = "SELECT [BEGINNINGBALQTY]  FROM [MAS_RDP].[dbo].[IM_PERIODPOSTINGHISTORY] where itemcode = @Entered_Sku and FISCALCALYEAR=@FISCALCALYEAR and FISCALCALPERIOD=01 and Warehousecode='000'";
            /*SELECT ROReport_NoParm_IS.SKU, ROReport_NoParm_IS.ItemDescription, ROReport_NoParm_IS.Class, ROReport_NoParm_IS.DAILYUSAGE AS ADU, ((ROReport_NoParm_IS.QOH)-(ROReport_NoParm_IS.QOSC)-(ROReport_NoParm_IS.QOSO)-(ROReport_NoParm_IS.QOBO)-(ROReport_NoParm_IS.QOSC)-(ROReport_NoParm_IS.QOASC)) AS BB FROM ROReport_NoParm_IS ROReport_NoParm_IS WHERE ROReport_NoParm_IS.SKU like '3%' AND ROReport_NoParm_IS.WhseCode = '000' AND ROReport_NoParm_IS.IM9WC = '000' AND (ROReport_NoParm_IS.Class = 'IS' OR ROReport_NoParm_IS.Class = 'IPG')and  ROReport_NoParm_IS.CB_UDF_IMH_REORDER ='Y'*/
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@Entered_Sku", SkuEntered);
            cmd.Parameters.AddWithValue("@FISCALCALYEAR", fds);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (!reader["BEGINNINGBALQTY"].Equals(System.DBNull.Value))
                {
                    beginingbalance = (int)((decimal)reader["BEGINNINGBALQTY"]);
                }
            }//end while
            reader.Close();
            cmd.Parameters.Clear();
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();

        }

        return beginingbalance;
    }
    public int determineWeek(DateTime date)
    {
        int testval = -1;
        for (int i = 0; i > 52; i++)
        {
            DateTime startDate = startEndDates[i, 0];
            DateTime endDate = startEndDates[i + 1, 0];

            if (i == 51)
            {
                testval = 51;
                break;
            }
            else if ((date > startEndDates[i, 0]) && (date < startEndDates[i + 1, 0]))
            {
                testval = i;
                break;
            }
        }

        return testval;


    }
    public int inString(String s, int r)
    {
        int present = -1;
        for (int i = 0; i < r; i++)
        {
            if (queryArray[i, 0] == s)
            {
                present = i;
            }
            if (present > -1)
            {
                i = r;
            }
        }
        return present;
    }

    #endregion

    #region Weeks
    public static DateTime GetStartOfLastWeek()
    {
        int DaysToSubtract = (int)DateTime.Now.DayOfWeek + 7;
        DateTime dt = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
    }

    public static DateTime GetEndOfLastWeek()
    {
        DateTime dt = GetStartOfLastWeek().AddDays(6);
        return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
    }
    public static DateTime GetMonths(int i)
    {
        DateTime dt = DateTime.Now.AddMonths(i);
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
    }
    public static DateTime GetStartOfCurrentWeek()
    {
        int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
        DateTime dt = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
    }
    public static DateTime GetStartOfDateWeek(DateTime current)
    {
        DateTime dt = current.Subtract(TimeSpan.FromDays((int)current.DayOfWeek));
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
    }
    public static DateTime GetEndOfCurrentWeek()
    {
        DateTime dt = GetStartOfCurrentWeek().AddDays(6);
        return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
    }
    public static DateTime GetStartOfCalculationWeek(int multiplyer)
    {
        return GetStartOfCurrentWeek().Add(new System.TimeSpan((multiplyer * 7), 0, 0, 0));
    }
    public static DateTime GetStartOfCalculationWeek(int multiplyer, DateTime startdate)
    {
        return startdate.Add(new System.TimeSpan((multiplyer * 7), 0, 0, 0));
    }
    public static DateTime GetEndOfCalculationWeek(int multiplyer)
    {
        return GetEndOfCurrentWeek().Add(new System.TimeSpan((multiplyer * 7), 0, 0, 0));

    }
    #endregion

    #region SelectableRows
    protected void Grid2008_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;

        // Get the selected index and the command name

        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;

        switch (_commandName)
        {
            case ("SingleClick"):
                _gridView.SelectedIndex = _selectedIndex;
                this.Debug.Text += "Single clicked GridView row at index "
               + _selectedIndex.ToString() + "<br />";
                Debug.Visible = true;
                break;
            case ("DoubleClick"):
                this.Debug.Text += "Double clicked GridView row at index "
                + _selectedIndex.ToString() + "<br />";
                break;
        }
    }
    #endregion

    #region ClickActions

    protected void editOrderButton_Click(object sender, EventArgs e)
    {
        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_UPS"].ConnectionString))
        {
            int loopCounter = 0;
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            //check for duplicates
            String query = "SELECT * FROM [ROReport_ProductArrival]";
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            //cmd.Parameters.AddWithValue("@Order_Number", messageLabel1.Text);
            Console.Write("Executing Reader");
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                loopCounter++;
            }
            reader.Close();
            cmd.Parameters.Clear();
            cmd.Cancel();
            cmd.Dispose();
            conn.Close();
        }
    }

    protected void EnterSkuButton_Click(object sender, EventArgs e)
    {
        enteredSku = skuNumber.Text;
        Image1.ImageUrl = "http://www.redemptionplus.com/_Sku/" + enteredSku + ".jpg";
        Image1.Attributes.Remove("Width");
        Image1.Attributes.Remove("Height");
        Image1.Attributes.Add("Width", "140");
        Image1.Attributes.Add("Height", "140");

        for (int i = 0; i < 52; i++)
        {
            //2012
            startEndDates[i, 4] = GetStartOfCalculationWeek(i, begin2012);
            //2011
            startEndDates[i, 3] = GetStartOfCalculationWeek(i, begin2011);
            //2010
            startEndDates[i, 2] = GetStartOfCalculationWeek(i, begin2010);
            //2009
            startEndDates[i, 1] = GetStartOfCalculationWeek(i, begin2009);
            //2008
            startEndDates[i, 0] = GetStartOfCalculationWeek(i, begin2008);

            if (i < 53)
            {
                Grid2008.Columns[(i + 1)].HeaderText = "Week" + i;
            }
        }
        //rows = getRows();//number of totalrows

        queryArray = new String[1, 55];
        queryArray.Initialize();
        fillItemsInfo(enteredSku);
        fillItemsWOrders(enteredSku);
        FutureUsageHyperLink.Visible = true;
        Session["ItemCode"] = skuNumber.Text;
        CalcValues();
        displayInfo();
    }
    #endregion

    #region export
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        DataGrid dg = new DataGrid();
        dg.DataSource = (System.Data.DataTable)Session["dt"];
        dg.DataBind();
        ExportToExcel("Report.xls", dg);
        dg = null;
        dg.Dispose();
    }

    private void ExportToExcel(string strFileName, DataGrid dg)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=" + strFileName);
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    #endregion

    #region Display
    public void displayInfo()
    {
        First_DATE_SOLDBox.Text = Convert.ToString(firstDateSold);

        if (2008 == 2008)
        {
            //String.Format("{0:MM/dd}", startEndDates[i, 0])

            System.Data.DataTable dt08 = new System.Data.DataTable();
            dt08.Columns.Add("code", Type.GetType("System.String"));
            for (int w = 1; w < 54; w++)//weeks
            {
                dt08.Columns.Add("Date" + w, Type.GetType("System.String"));
            }
            int yearsloop = unitsSold.GetLength(0);
            //for (int y = (firstTransactionEntryYear-2008); y < unitsSold.GetLength(0); y++)//years
            for (int y = (firstTransactionEntryYear - 2008); y < unitsSold.GetLength(0); y++)
            {
                for (int i = 0; i < InfoList.Length; i++)//rows
                {
                    dt08.Rows.Add();
                    //if (i == 0)
                    // {
                    //    dt08.Rows[dt08.Rows.Count - 1]["code"] = System.Convert.ToString((2008+y));
                    // }

                    //else
                    //{
                    dt08.Rows[dt08.Rows.Count - 1]["code"] = "'" + System.Convert.ToString((2008 + y)).Substring(2) + " " + InfoList[i]/* + " " + System.Convert.ToString(i)*/;
                    //}
                    for (int w = 0; w < 52; w++)//weeks
                    {
                        if (i == 0)
                        {
                            dt08.Rows[dt08.Rows.Count - 1]["Date" + (w + 1)] = String.Format("{0:MM/dd}", startEndDates[w, y]);
                        }
                        else
                        {
                            dt08.Rows[dt08.Rows.Count - 1]["Date" + (w + 1)] = unitsSold[y, w, i];
                        }
                    }

                }
            }

            Grid2008.DataSource = dt08;
            Grid2008.DataBind();
            Grid2008.Visible = true;
            Session.Add("dt",dt08);
            Exportbtn.Visible = true;

            //(firstTransactionEntryYear - 2008)2011-2008=3
            //currentyr-firstTransactionEntryYear=3
            int yearsToFormat = DateTime.Now.Year - firstTransactionEntryYear;
            if (DateTime.Now.Month > 6) {
                yearsToFormat++;
            }

            for (int y = 0; y <= (yearsToFormat); y++)
            {   /*** all Visibilty *******/
				Grid2008.Rows[AOS + y * 35].Visible = true;
				Grid2008.Rows[FPSLSU + y * 35].Visible = true;
				Grid2008.Rows[AFBW + y * 35].Visible = true;
				Grid2008.Rows[AFSOpercent + y * 35].Visible = true;
				Grid2008.Rows[datePos + y * 35].Visible = true;

                Grid2008.Rows[AFMBW + y * 35].Visible = false;
                Grid2008.Rows[AMBW + y * 35].Visible = false;
                Grid2008.Rows[FMBW + y * 35].Visible = false;
                Grid2008.Rows[ASLSU + y * 35].Visible = false;
                Grid2008.Rows[FSLSU + y * 35].Visible = false;
                Grid2008.Rows[HSLSU + y * 35].Visible = false;
                Grid2008.Rows[AFMOV + y * 35].Visible = false;
                Grid2008.Rows[AMOV + y * 35].Visible = false;
                Grid2008.Rows[FMOV + y * 35].Visible = false;
                Grid2008.Rows[ABW + y * 35].Visible = false;
                Grid2008.Rows[FBW + y * 35].Visible = false;
                Grid2008.Rows[ABOP + y * 35].Visible = false;
                Grid2008.Rows[FBOP + y * 35].Visible = false;
                Grid2008.Rows[AFOS + y * 35].Visible = false;

                Grid2008.Rows[AFOS + y * 35].Visible = false;
                Grid2008.Rows[FOS + y * 35].Visible = false;
                Grid2008.Rows[AMOV + y * 35].Visible = false;
                Grid2008.Rows[FMOV + y * 35].Visible = false;
                Grid2008.Rows[BI + y * 35].Visible = false;
                Grid2008.Rows[BR + y * 35].Visible = false;
                Grid2008.Rows[BZ + y * 35].Visible = false;
                Grid2008.Rows[BZ + y * 35].Visible = false;
                Grid2008.Rows[IA + y * 35].Visible = false;
                Grid2008.Rows[IP + y * 35].Visible = false;
                Grid2008.Rows[IT + y * 35].Visible = false;
                Grid2008.Rows[IZ + y * 35].Visible = false;
                Grid2008.Rows[PZ + y * 35].Visible = false;
                /******** Color ***********/
                Grid2008.Rows[datePos + y * 35].BackColor = System.Drawing.Color.FromName("#ffff99");
                Grid2008.Rows[BI + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[BR + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[BZ + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[IA + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[IP + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[IT + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[IZ + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[PZ + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[AFMBW + y * 35].BackColor = System.Drawing.Color.FromName("#cc99ff");
                Grid2008.Rows[AMBW + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FMBW + y * 35].BackColor = System.Drawing.Color.FromName("#ccffcc");
                Grid2008.Rows[AFSLSU + y * 35].BackColor = System.Drawing.Color.FromName("#cc99ff");
                Grid2008.Rows[ASLSU + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FSLSU + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[HSLSU + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FPSLSU + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[AFMOV + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[AFBW + y * 35].BackColor = System.Drawing.Color.FromName("#cc99ff");
                Grid2008.Rows[ABW + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FBW + y * 35].BackColor = System.Drawing.Color.FromName("#ccffcc");
                Grid2008.Rows[BOP + y * 35].BackColor = System.Drawing.Color.FromName("#cc99ff");
                Grid2008.Rows[ABOP + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FBOP + y * 35].BackColor = System.Drawing.Color.FromName("#ccffcc");
                Grid2008.Rows[BOPWS + y * 35].BackColor = System.Drawing.Color.FromName("#cc99ff");
                Grid2008.Rows[AFSOpercent + y * 35].BackColor = System.Drawing.Color.FromName("#cc99ff");
                Grid2008.Rows[AFOS + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[AOS + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FOS + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[AFFPO + y * 35].BackColor = System.Drawing.Color.FromName("#cc99ff");
                Grid2008.Rows[APO + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FutPO + y * 35].BackColor = System.Drawing.Color.FromName("#c0c0c0");
                Grid2008.Rows[FPO + y * 35].BackColor = System.Drawing.Color.FromName("#ccffcc");
                /**** Past Visiblity  and Color **********/
                if (y != (DateTime.Now.Year - firstTransactionEntryYear))
                {
					Grid2008.Rows[ASLSU + y * 35].Visible = true;
					Grid2008.Rows[AFBW + y * 35].Visible = true;
					Grid2008.Rows[ABOP + y * 35].Visible = true;
					Grid2008.Rows[BOPWS + y * 35].Visible = true;
					Grid2008.Rows[AFSOpercent + y * 35].Visible = true;
					Grid2008.Rows[AOS + y * 35].Visible = true;
					Grid2008.Rows[APO + y * 35].Visible = true;

                    Grid2008.Rows[AFMBW + y * 35].Visible = false;
                    Grid2008.Rows[AMBW + y * 35].Visible = false;
                    Grid2008.Rows[FMBW + y * 35].Visible = false;
                    Grid2008.Rows[AFSLSU + y * 35].Visible = false;
                    Grid2008.Rows[FSLSU + y * 35].Visible = false;
                    Grid2008.Rows[HSLSU + y * 35].Visible = false;
                    Grid2008.Rows[FPSLSU + y * 35].Visible = false;
                    Grid2008.Rows[AFMOV + y * 35].Visible = false;
                    Grid2008.Rows[AMOV + y * 35].Visible = false;
                    Grid2008.Rows[FMOV + y * 35].Visible = false;
                    Grid2008.Rows[ABW + y * 35].Visible = false;
                    Grid2008.Rows[FBW + y * 35].Visible = false;
                    Grid2008.Rows[BOP + y * 35].Visible = false;
                    Grid2008.Rows[FBOP + y * 35].Visible = false;
                    Grid2008.Rows[AFOS + y * 35].Visible = false;
                    Grid2008.Rows[FOS + y * 35].Visible = false;
                    Grid2008.Rows[AFFPO + y * 35].Visible = false;
                    Grid2008.Rows[FutPO + y * 35].Visible = false;
                    Grid2008.Rows[FPO + y * 35].Visible = false;
                }

            }
        }
    }

    protected void DropDownDisplay_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SeeAll_Click(object sender, EventArgs e)
    {
    }

    protected void HideInfo_Click(object sender, EventArgs e)
    {
        for (int y = firstTransactionEntryYear; y < DateTime.Now.Year; y++)
        {
            Grid2008.Rows[AFMBW + y * 35].Visible = false;
            Grid2008.Rows[AMBW + y * 35].Visible = false;
            Grid2008.Rows[FMBW + y * 35].Visible = false;
            Grid2008.Rows[AFSLSU + y * 35].Visible = false;
            Grid2008.Rows[ASLSU + y * 35].Visible = true;
            Grid2008.Rows[FSLSU + y * 35].Visible = false;
            Grid2008.Rows[HSLSU + y * 35].Visible = false;
            Grid2008.Rows[FPSLSU + y * 35].Visible = false;
            Grid2008.Rows[AFMOV + y * 35].Visible = true;
            Grid2008.Rows[AMOV + y * 35].Visible = false;
            Grid2008.Rows[FMOV + y * 35].Visible = false;
            Grid2008.Rows[AFBW + y * 35].Visible = false;
            Grid2008.Rows[ABW + y * 35].Visible = false;
            Grid2008.Rows[FBW + y * 35].Visible = false;
            Grid2008.Rows[BOP + y * 35].Visible = true;
            Grid2008.Rows[ABOP + y * 35].Visible = true;
            Grid2008.Rows[FBOP + y * 35].Visible = false;
            Grid2008.Rows[BOPWS + y * 35].Visible = true;
            Grid2008.Rows[AFSOpercent + y * 35].Visible = false;
            Grid2008.Rows[AFOS + y * 35].Visible = false;
            Grid2008.Rows[AOS + y * 35].Visible = true;
            Grid2008.Rows[FOS + y * 35].Visible = false;
            Grid2008.Rows[AFFPO + y * 35].Visible = false;
            Grid2008.Rows[APO + y * 35].Visible = true;
            Grid2008.Rows[FutPO + y * 35].Visible = false;
            Grid2008.Rows[FPO + y * 35].Visible = false;
        }

        Image1.ImageUrl = "http://www.redemptionplus.com/_Sku/" + skuNumber.Text + ".jpg";
        Image1.Attributes.Remove("Width");
        Image1.Attributes.Remove("Height");
        Image1.Attributes.Add("Width", "140");
        Image1.Attributes.Add("Height", "140");

        HideInfo.Visible = false;
        SeeAll.Visible = true;
    }

    #endregion
}