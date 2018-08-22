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
	/*
	JMW 28Dec2011: Updated global variable naming conventions to prep for dynamic content logic.
	JMW 27Dec2011: Updated grid2008 name to grdSKUP to avoid naming confusion with dates.
	*/

    String[,] queryArray;
    int beginingBalance = 0;

    //year Start dates Need to move to maintance page or area
    int firstTransactionEntryYear = 2025;
    DateTime firstDateSold = GetEndOfCurrentWeek();
    DateTime firstDay = GetStartOfCalculationWeek(0);
    DateTime lastDay = GetEndOfCalculationWeek(0);
    DateTime Start = GetStartOfCalculationWeek(1);
    DateTime End = GetEndOfCalculationWeek(1);
	
    List<SqlParameter> insertParameters = new List<SqlParameter>();

    String enteredSku = "0";

    /**START Varible Postions**/
	//JMW 29Dec2011: Deprecated all "MBW" values from list
    //String[] InfoList = new String[] { "Dates", "BI", "BR", "BZ", "IA", "IP", "IT", "IZ", "PZ", "AF_MBW", "A_MBW", "F_MBW", "AF_SU", "A_SU", "F_SU", "H_SU", "FP_SU", "AF_MOV", "A_MOV", "F_MOV", "AF_BW", "AB_W", "F_BW", "AF_BP", "A_BP", "F_BP", "BP_WS", "AFSO%", "AF_OS", "OS", "F_OS", "AF_PO", "A_PO", "F_PO", "FUT_PO" };
	String[] InfoList = new String[] { "Dates", "BI", "BR", "BZ", "IA", "IP", "IT", "IZ", "PZ", "AF_SU", "A_SU", "F_SU", "H_SU", "FP_SU", "AF_MOV", "A_MOV", "F_MOV", "AF_BW", "AB_W", "F_BW", "AF_BP", "A_BP", "F_BP", "BP_WS", "AFSO%", "AF_OS", "OS", "F_OS", "AF_PO", "A_PO", "F_PO", "FUT_PO" };

	/*
	 * Prefixes:
	 * A = Actual
	 * F = Future
	 * AF = Actual or Forecasted
	 * FP = Forecast Projected (from SKUP data entry form)
	 * FU = Future Usage (from SKUP data entry form)
	*/

	#region Grid Positions
	int iGridRows = 32; //Number of rows in the grid, should be max count from the following variables plus one

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

    //SU: Sales Units
	int AF_SU = 9; // ***MIGRATE***
    int A_SU = 10; //Actual
    int F_SU = 11; //Future Sales Units
    int H_SU = 12; //Historical Sales Units
    int FP_SU = 13; //Forecast Projected Sales Units

    //MOV: Movement by Week
    int AF_MOV = 14;
    int A_MOV = 15;
    int F_MOV = 16;

    //BW: Beginning of Week
    int AF_BW = 17;
    int AB_W = 18;
    int F_BW = 19;

    //BP: Beginning of Period
    int BP = 20;
    int A_BP = 21;
    int F_BP = 22;
    int BP_WS = 23; //Week Supply ***MIGRATE***
    int AF_SOpercent = 24;

    //OS: Out of Stock
    int AF_OS = 25;
    int A_OS = 26;
    int F_OS = 27;

    //PO: Purchase orders
	int AF_PO = 28; // ***MIGRATE***
    int A_PO = 29;
    int F_PO = 30;
    int FUT_PO = 31;

	//MBW: Unknown Movement value...what is this???
	/* JMW: Not used...apparently.
    int AF_MBW = 9;
    int A_MBW = 10;
    int F_MBW = 11;
	*/
	#endregion

	#region Page Init
	//See Page_Load() for more value declarations.
	//Init global variables used to store data and date ranges
	int iYearsToCalc = 1; //JMW: Defaults to one year, updated to 2 by Page_Load() if more than half way through current year.
	int[, ,] iArr_SKUP; //Holds data as it comes back from data source as well as calculated values.
	DateTime[,] dtDateRange = new DateTime[53, 6]; //Holds first transaction and last transaction dates to get max date ranges.
	
	DateTime dtToday = DateTime.Now;
	DateTime dtPrevYear = DateTime.Now.AddYears(-1);
	DateTime dtThisYear = DateTime.Now;
	DateTime dtNextYear = DateTime.Now.AddYears(1);
	/*
	JMW: Set dates manually to test against historical data.
	DateTime dtToday = Convert.ToDateTime("01/01/2011");
	DateTime dtPrevYear = Convert.ToDateTime("01/01/2010");
	DateTime dtThisYear = Convert.ToDateTime("01/01/2011");
	DateTime dtNextYear = Convert.ToDateTime("01/01/2012");
	*/
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Image1.ImageUrl = "http://www.redemptionplus.com/_FileLibrary/FileImage/redemption_news.jpg";
        Image1.Attributes.Remove("Width");
        Image1.Attributes.Remove("Height");
        Image1.Attributes.Add("Width", "132");
        Image1.Attributes.Add("Height", "84");
		
		//Set start dates for each year.
		fnDates_GetFirstWeekByYear();

		//If we're more than halfway through the year, include next year.
        if (DateTime.Now.Month > 6) iYearsToCalc = 2;
		iArr_SKUP = new int[(iYearsToCalc + 1), 53, iGridRows];
    }
	
	//Sets the start date for the first full week (first Sunday) of each year.
	private void fnDates_GetFirstWeekByYear()
	{
		string sDate = "";
		for (int iYr = dtPrevYear.Year; iYr <= dtNextYear.Year; iYr++)
		{
			for (int i = 1; i <= 7; i++)
			{
				sDate = "01/0" + i.ToString() + "/" + (iYr).ToString();
				if (Convert.ToDateTime(sDate).DayOfWeek.ToString() == "Sunday")
				{
					if (dtPrevYear.Year == iYr) dtPrevYear = new DateTime(iYr, 1, i, 0, 0, 0);
					if (dtThisYear.Year == iYr) dtThisYear = new DateTime(iYr, 1, i, 0, 0, 0);
					if (dtNextYear.Year == iYr) dtNextYear = new DateTime(iYr, 1, i, 0, 0, 0);
				}
			}
		}
	}
    #endregion

    #region Servercalls
    public Boolean fillItemsWOrders(String EnteredSku)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
        {
            int weekNumber = 0;
            int RecordYear = -1;
            int bBalAdj = 0;
            System.Data.SqlClient.SqlDataReader reader = null;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
			
            String query = "SELECT ItemNumber,TransactionCode, TransactionDate, sum(qty) as qty"
							+ " FROM ("
								+ " SELECT SKU as ItemNumber,'FutPO' as TransactionCode, DATEADD(wk,DATEDIFF(wk,0,RequiredDate),-1) as TransactionDate, QTYOrdered as qty"
									+ " FROM [ROReport_ProductArrival]"
									+ " WHERE sku= @Entered_Sku"
								+ " UNION ALL SELECT SKU as ItemNumber,'FutPO' as TransactionCode, DATEADD(wk,DATEDIFF(wk,0,RequiredDate),-1) as TransactionDate, QTYOrdered as qty"
									+ " FROM [ROReport_ProductArrival_STAPLE]"
									+ " WHERE sku= @Entered_Sku"
								+ " UNION ALL SELECT IM5.ItemCode ItemNumber, TransactionCode,DATEADD(wk,DATEDIFF(wk,0,TransactionDate),-1) as TransactionDate, sum(TransactionQty) as qty"
									+ " FROM IM_ITEMTRANSACTIONHISTORY IM5"
									+ " WHERE YEAR(IM5.TransactionDate) >= " + dtPrevYear.Year.ToString()
									+ " AND CAST(IM5.TransactionDate As datetime) <= '" + dtToday.Year.ToString() + "-" + dtToday.Month.ToString() + "-" + dtToday.Day.ToString() + "'"
									+ " AND (IM5.warehouseCode ='000') and IM5.ItemCode = @Entered_Sku"
									+ " GROUP BY IM5.ItemCode, TransactionCode,TransactionDate"
								+ " UNION ALL SELECT ItemNumber, 'AOS' as TransactionCode, DATEADD(wk, DATEDIFF(wk, 0, Date_Out), - 1) AS TransactionDate, SUM(convert(int,Count_Day)) AS qty"
									+ " FROM SALES.dbo.PRODUCT_OUT_STOCK_REORDER PRODUCT_OUT_STOCK_REORDER"
									+ " WHERE (ItemNumber = @Entered_Sku)"
									+ " AND DATEADD(wk, DATEDIFF(wk, 0, Date_Out), - 1) BETWEEN '" + dtPrevYear.Year.ToString() + "-01-01 00:00:00.000' AND '" + dtNextYear.Year.ToString() + "-01-01 00:00:00.000'"
									+ " GROUP BY ItemNumber,DATEADD(wk, DATEDIFF(wk, 0, Date_Out), - 1)"
								+ " UNION ALL SELECT ItemCode as ItemNumber,Type as TransactionCode,DATEADD(wk,DATEDIFF(wk,0,TransactionDate), - 1) as TransactionDate,value as qty"
									+ " FROM SALES.dbo.SkupFutureEntrys"
									+ " WHERE itemcode= @Entered_Sku"
								+ ") Base"
								+ " GROUP BY ItemNumber, TransactionCode, TransactionDate"
								+ " ORDER BY transactionDate";
			cmd.CommandTimeout = 300;
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

                if ((TransactionDate < dtDateRange[0, 0]))
                {
                    bBalAdj += (int)((decimal)reader["qty"]);
                }

                if (TransactionDate != null)
                {

                    /********** Need add findOldestEntry(); *************/
					if ((firstTransactionEntryYear > TransactionDate.Year) && (TransactionDate.Year >= dtPrevYear.Year))
                    {
                        firstTransactionEntryYear = TransactionDate.Year;
                    }
                    weekNumber = FindDateRange(TransactionDate);
                    if (weekNumber != -1) dateRangeFound = true;

					RecordYear = TransactionDate.Year - dtPrevYear.Year;
                }
                else
                {
                    RecordYear = -1;
                    weekNumber = -1;
                    dateRangeFound = false;
                }

                /*************standardized make method**************/
                if ((dateRangeFound))
                {
                    if (((String)reader["TransactionCode"]) == "BI")
                    {
                        iArr_SKUP[RecordYear, weekNumber, BI] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "BR")
                    {
                        iArr_SKUP[RecordYear, weekNumber, BR] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "BZ")
                    {
                        iArr_SKUP[RecordYear, weekNumber, BZ] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IA")
                    {
                        iArr_SKUP[RecordYear, weekNumber, IA] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IP")
                    {
                        iArr_SKUP[RecordYear, weekNumber, IP] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IT")
                    {
                        iArr_SKUP[RecordYear, weekNumber, IT] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "IZ")
                    {
                        iArr_SKUP[RecordYear, weekNumber, IZ] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "PO")
                    {
                        iArr_SKUP[RecordYear, weekNumber, A_PO] += (int)((decimal)reader["qty"]);
                        iArr_SKUP[RecordYear, weekNumber, AF_PO] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "PZ")
                    {
                        iArr_SKUP[RecordYear, weekNumber, PZ] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "SO")
                    {
                        iArr_SKUP[RecordYear, weekNumber, A_SU] += (int)((decimal)reader["qty"]);
                        iArr_SKUP[RecordYear, weekNumber, AF_SU] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "AOS")
                    {
                        iArr_SKUP[RecordYear, weekNumber, A_OS] += (int)((decimal)reader["qty"]);
                        iArr_SKUP[RecordYear, weekNumber, AF_OS] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "FutPO")
                    {
                        iArr_SKUP[RecordYear, weekNumber, FUT_PO] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "FPO")
                    {
                        iArr_SKUP[RecordYear, weekNumber, F_PO] += (int)((decimal)reader["qty"]);
                    }
                    if (((String)reader["TransactionCode"]) == "FSU")
                    {
                        iArr_SKUP[RecordYear, weekNumber, F_SU] += (int)((decimal)reader["qty"]);
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

    public int getOldSystemADU(String EnteredSku,int year, int month)
    {
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_SALES"].ConnectionString))
        {
            int formerADU = 0;
            System.Data.SqlClient.SqlDataReader reader2 = null;
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

            String query = "SELECT ADU from SkupHistoricADUs"
							+ " WHERE ItemCode=@Entered_Sku"
							+ " AND year(entryDate)=@year"
							+ " AND month(entryDate)=@month";
			cmd2.CommandTimeout = 300;
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

	public int beginingBalanceFinder(String SkuEntered)
	{
		int beginingbalance = 0;
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
		{
			System.Data.SqlClient.SqlDataReader reader = null;
			System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
			int fds = firstDateSold.Year;
			if (fds < dtPrevYear.Year) fds = dtPrevYear.Year;

			String query = "SELECT [BEGINNINGBALQTY]"
							+ " FROM [MAS_RDP].[dbo].[IM_PERIODPOSTINGHISTORY]"
							+ " WHERE itemcode = @Entered_Sku"
							+ " AND Warehousecode = '000'"
							+ " AND FISCALCALYEAR = @FISCALCALYEAR AND FISCALCALPERIOD = 01";
			cmd.CommandTimeout = 300;
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


	//Gets forecasted Sales Units from Forecasts table.
	//The projected amount is then weighted against Actual Daily Usage for the current week to get the "Forecast Projected Sales Units" (FP_SU).
	public int getFP_SU(int currentADU, String category, DateTime weekDate)
	{
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_SALES"].ConnectionString))
		{
			decimal forecastPercent = 0;
			System.Data.SqlClient.SqlDataReader reader2 = null;
			System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

			String query = "SELECT forecastYear,forecastCategory,Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec"
							+ " FROM SkupForecasts"
							+ " WHERE forecastCategory=@forecastCategory"
							+ " AND forecastYear=@forecastYear";
			cmd2.CommandTimeout = 300;
			cmd2.CommandText = query;
			cmd2.CommandType = System.Data.CommandType.Text;
			cmd2.Connection = conn;
			cmd2.Parameters.AddWithValue("@forecastCategory", category);
			cmd2.Parameters.AddWithValue("@forecastYear", weekDate.Year);
			conn.Open();
			reader2 = cmd2.ExecuteReader();

			while (reader2.Read())
			{
				if (!((reader2["Jan"]).Equals(System.DBNull.Value)) && (weekDate.Month == 1))
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
					forecastPercent = (decimal)(reader2["Jun"]);
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
			int adjustedADU = Convert.ToInt32((Math.Round((currentADU / ((decimal).927)) * forecastPercent) * 100) / 100);
			return adjustedADU;
		}
	}

	//Get the basic Item Setup information from the database.
	public Boolean fillItemsInfo(String EnteredSku)
	{
		using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CN_MAS_RDP"].ConnectionString))
		{
			//int loopCounter = 0;
			System.Data.SqlClient.SqlDataReader reader2 = null;
			System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand();

			String query = "SELECT * FROM vw_Item_Setup where ItemNumber=@Entered_Sku";
			cmd2.CommandTimeout = 300;
			cmd2.CommandText = query;
			cmd2.CommandType = System.Data.CommandType.Text;
			cmd2.Connection = conn;
			cmd2.Parameters.AddWithValue("@Entered_Sku", EnteredSku);
			conn.Open();
			reader2 = cmd2.ExecuteReader();

			while (reader2.Read())
			{
				if (reader2["First_DATE_SOLD"] != DBNull.Value)
				{
					firstDateSold = Convert.ToDateTime(reader2["First_DATE_SOLD"]);
				}
				else firstDateSold = Convert.ToDateTime("01/01/1900");

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
    #endregion

    #region Calcs
	public void CalcValues()
	{
		#region fillActuals
		int start = FinddtTodayRange();
		int fAdU = 0;
		int currentWeekNumber = FindDateRange(GetStartOfCurrentWeek());

		if (ADUOverride.Text != "") fAdU = System.Convert.ToInt32(ADUOverride.Text, 10);
		else fAdU = System.Convert.ToInt32(adu.Text, 10);

		firstTransactionEntryYear = firstDateSold.Year;
		if (firstTransactionEntryYear < dtPrevYear.Year) { firstTransactionEntryYear = dtPrevYear.Year; }
		
		if (firstDateSold < GetEndOfCurrentWeek())
		{
			// Place holder for when years have 53 instead of 52 weeks
			int previousYearWks = 0;

			for (int y = (firstTransactionEntryYear - dtPrevYear.Year); y <= iYearsToCalc; y++)
			{
				//set weeks in year 3
				/*** Needs logic for 53 Week Years*/
				int weeks = 52;
				int F_SUADU = 0;

				for (int w = 0; w < weeks; w++)
				{
					//zero out week total
					int weekTotal = 0;
					int fweekTotal = 0;
					int FuturePO = 0;

					//total of all weeks transactions
					weekTotal += iArr_SKUP[y, w, BI];
					weekTotal += iArr_SKUP[y, w, BR];
					weekTotal += iArr_SKUP[y, w, BZ];
					weekTotal += iArr_SKUP[y, w, IA];
					weekTotal += iArr_SKUP[y, w, IP];
					weekTotal += iArr_SKUP[y, w, IT];
					weekTotal += iArr_SKUP[y, w, IZ];
					weekTotal += iArr_SKUP[y, w, PZ];
					weekTotal += iArr_SKUP[y, w, A_SU];
					weekTotal += iArr_SKUP[y, w, A_PO];

					if (iArr_SKUP[y, w, F_SU] != 0)
					{
						F_SUADU = iArr_SKUP[y, w, F_SU];
					}

					if (currentWeekNumber > w && y > 0)
					{ //Get FP_SU if 
						iArr_SKUP[y, w, FP_SU] = -(Math.Abs(Convert.ToInt32(getOldSystemADU(skuNumber.Text, (dtPrevYear.Year + y), dtDateRange[w, y].Month))) * (7));
					}
					else if (currentWeekNumber <= w && y > 0)
					{
						if (currentWeekNumber == w)
						{
							iArr_SKUP[y, w, FP_SU] = -(Math.Abs(fAdU) * ((int)GetEndOfCurrentWeek().DayOfWeek - (int)DateTime.Now.DayOfWeek));
							iArr_SKUP[y, w, F_SU] = -((Math.Abs(F_SUADU)/7) * ((int)GetEndOfCurrentWeek().DayOfWeek - (int)DateTime.Now.DayOfWeek));
						}
						else {
							int test56 = getFP_SU(fAdU, ClassBox.Text, dtDateRange[w, y]);//adu.Text
							iArr_SKUP[y, w, FP_SU] = -(Math.Abs(Convert.ToInt32(test56)) * (7));
							iArr_SKUP[y, w, F_SU] = -(Math.Abs(F_SUADU));
						}

						if (F_SUADU > 0)
						{
							fweekTotal += iArr_SKUP[y, w, F_SU];
							iArr_SKUP[y, w, AF_SU] += iArr_SKUP[y, w, F_SU] + iArr_SKUP[y, w, A_SU];
						}
						else
						{
							fweekTotal += iArr_SKUP[y, w, FP_SU];
							iArr_SKUP[y, w, AF_SU] += iArr_SKUP[y, w, FP_SU] + iArr_SKUP[y, w, A_SU];
						}

						if (iArr_SKUP[y, w, FUT_PO] == 0) {
							fweekTotal += iArr_SKUP[y, w, F_PO];
							FuturePO = iArr_SKUP[y, w, F_PO];
						}
						else {
							fweekTotal += iArr_SKUP[y, w, FUT_PO];
							FuturePO = iArr_SKUP[y, w, FUT_PO];
						}
					}
					
					iArr_SKUP[y, w, AF_PO] = FuturePO + iArr_SKUP[y, w, A_PO];

					//add total to weeks movement
					iArr_SKUP[y, w, F_MOV] = fweekTotal;
					iArr_SKUP[y, w, A_MOV] = weekTotal;
					iArr_SKUP[y, w, AF_MOV] = (iArr_SKUP[y, w, A_MOV] + iArr_SKUP[y, w, F_MOV]);

					/* Calcs by Week and Year*/
					if (w == 0 && y == 0)
					{ //First week of the first year to be displayed.
						iArr_SKUP[y, w, BP] = iArr_SKUP[y, w, A_BP] = beginingBalance;
						iArr_SKUP[y, w, AF_BW] = (Math.Abs(iArr_SKUP[y, w, A_SU]) - Math.Abs(iArr_SKUP[y, w, FP_SU]));
					}
					else if (w > 0 && y == 0)
					{//Each week of the first year.
						iArr_SKUP[y, w, BP] = iArr_SKUP[y, w, A_BP] = iArr_SKUP[y, (w - 1), AF_MOV] + iArr_SKUP[y, (w - 1), A_BP];
						iArr_SKUP[y, w, AF_BW] = (Math.Abs(iArr_SKUP[y, w, A_SU]) - Math.Abs(iArr_SKUP[y, w, FP_SU]));
					}
					else if (w == 0 && y > 0 && previousYearWks > 0)
					{ //First week of each year after first year to be displayed.
						iArr_SKUP[y, w, A_BP] = iArr_SKUP[y - 1, previousYearWks - 1, AF_MOV] + iArr_SKUP[y - 1, previousYearWks - 1, A_BP];
						iArr_SKUP[y, w, BP] = iArr_SKUP[y, w, A_BP];
						iArr_SKUP[y, w, AF_BW] = (Math.Abs(iArr_SKUP[y, w, A_SU]) - Math.Abs(iArr_SKUP[y, w, FP_SU]));
					}
					else if (y >= 0)
					{
						if (w < currentWeekNumber && w > 0)
						{
							iArr_SKUP[y, w, BP] = iArr_SKUP[y, w, A_BP] = iArr_SKUP[y, (w - 1), AF_MOV] + iArr_SKUP[y, (w - 1), A_BP];
							iArr_SKUP[y, w, AF_BW] = (Math.Abs(iArr_SKUP[y, w, A_SU]) - Math.Abs(iArr_SKUP[y, w, FP_SU]));
						}
						else if (w == currentWeekNumber && w > 0)
						{
							iArr_SKUP[y, w, BP] = iArr_SKUP[y, w, A_BP] = iArr_SKUP[y, w, F_BP] = iArr_SKUP[y, (w - 1), AF_MOV] + iArr_SKUP[y, (w - 1), A_BP];
						}
						else if (w >= currentWeekNumber && w > 0)
						{
							iArr_SKUP[y, w, BP] = iArr_SKUP[y, w, F_BP] = iArr_SKUP[y, (w - 1), AF_MOV] + iArr_SKUP[y, (w - 1), F_BP];
						}
					}

					if (iArr_SKUP[y, w, BP] < 0) {
						iArr_SKUP[y, w, BP] = iArr_SKUP[y, w, F_BP] = 0;
						iArr_SKUP[y, w, AF_BW] = (Math.Abs(iArr_SKUP[y, w, A_SU]) - Math.Abs(iArr_SKUP[y, w, FP_SU]));                  
					}

					/**** Calc Historic Weekly Usage ****/
					getHistoricADU(y, w, (firstTransactionEntryYear - dtPrevYear.Year));
				}
				previousYearWks = weeks;
			}
		}
		#endregion

		#region afterActuals
		/***** Begin Calcs after Actuals***/
        if (firstDateSold < GetEndOfCurrentWeek())
        {
            //int yearsOfUsage = DateTime.Now.Year - firstTransactionEntryYear;
			for (int y = 0; y <= iYearsToCalc; y++)
            {
                int weeks = 52;

                for (int w = 0; w < weeks; w++)
                {
                    /**** Calc BP WS ****/
                    if (iArr_SKUP[y, w, BP] > 0)
					{
                        int week = 0;
                        int turns = 0;
                        int yearAdjust = 0;
                        int units = iArr_SKUP[y, w, BP];
                        int startYear = y;
                        int startWeek = w;

                        do
                        {
							if ((startWeek + week <= 53) && (startYear + yearAdjust) <= iYearsToCalc)
                            {
                                if ((iArr_SKUP[startYear + yearAdjust, startWeek + week, A_OS] == 7) ||(iArr_SKUP[startYear + yearAdjust, startWeek + week, BP]<=0))
                                {
                                    units = 0;
                                }
                                else
                                {
                                    units = units + iArr_SKUP[startYear + yearAdjust, startWeek + week, AF_SU];
                                    turns++;
                                }
                            }
							else if (((startYear + yearAdjust) <= iYearsToCalc))
                            {
                                startWeek = 0;
                                week = 0;
                                yearAdjust++;
								if ((startYear + yearAdjust) <= iYearsToCalc)
                                {
                                    if ((iArr_SKUP[startYear + yearAdjust, startWeek + week, A_OS] == 7) || (iArr_SKUP[startYear + yearAdjust, startWeek + week, BP] <= 0))
                                    {
                                        units = 0;
                                    }
                                    else
                                    {
                                        units = units + iArr_SKUP[startYear + yearAdjust, startWeek + week, AF_SU];
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
                        } while (units > 0);
                        
						iArr_SKUP[y, w, BP_WS] = turns;
                        turns = 0;
                    }
                }
            }
		}
		#endregion
	}

    void getHistoricADU(int Year, int week, int startYr)
    {
        int RunningTotal = 0;
        int OS = 0;
        for (int y = startYr; y <= Year; y++)
        {
            RunningTotal += iArr_SKUP[y, week, A_SU];
            OS += iArr_SKUP[y, week, A_OS];
        }
        if ((((Year - startYr + 1) * 7) - OS) > 0)
        {
            iArr_SKUP[Year, week, H_SU] = RunningTotal / (((Year - startYr + 1) * 7) - OS);
        }
        else
        {
            iArr_SKUP[Year, week, H_SU] = 0;
        }
    }

    public int FindDateRange(DateTime datesent)
    {
		int Year = datesent.Year - dtPrevYear.Year;
		int cYear = dtToday.Year - dtPrevYear.Year;
        int weekValueFound = -1;
        int weeks = 52;

        for (int i = 0; i < weeks; i++)
        {
            if (Year == -1)
            {
                weekValueFound = -1;
                i = weeks + 1;
            }
            else if (datesent == dtDateRange[i, Year])
            {
                weekValueFound = i;
                i = weeks + 1;
            }
        }
        return weekValueFound;


    }

    public int FinddtTodayRange()
    {
		int Year = dtToday.Year - dtPrevYear.Year;
        int weekValueFound = -1;
        for (int i = 0; i < 52; i++)
        {
            if ((dtToday >= dtDateRange[i, Year]) && (dtToday < dtDateRange[i + 1, Year]))
            {
                weekValueFound = i;
                i = 52;
            }
        }
        return weekValueFound;
    }

    public int determineWeek(DateTime date)
    {
        int testval = -1;
        for (int i = 0; i > 52; i++)
        {
            DateTime startDate = dtDateRange[i, 0];
            DateTime endDate = dtDateRange[i + 1, 0];

            if (i == 51)
            {
                testval = 51;
                break;
            }
            else if ((date > dtDateRange[i, 0]) && (date < dtDateRange[i + 1, 0]))
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
			cmd.CommandTimeout = 300;
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
		if (skuNumber.Text.Length == 0)
		{
			EnterSku.Style.Add(HtmlTextWriterStyle.Color, "red");
		}
		else
		{
			DateTime dtBegin = System.DateTime.Now;
			
			EnterSku.Style.Add(HtmlTextWriterStyle.Color, "");
			enteredSku = skuNumber.Text;
			Image1.ImageUrl = "http://www.redemptionplus.com/_Sku/" + enteredSku + ".jpg";
			Image1.Attributes.Remove("Width");
			Image1.Attributes.Remove("Height");
			Image1.Attributes.Add("Width", "140");
			Image1.Attributes.Add("Height", "140");

			for (int i = 0; i < 52; i++)
			{
				for (int i2 = 0; i2 <= iYearsToCalc; i2++)
				{
					if (i2 == 0) dtDateRange[i, i2] = GetStartOfCalculationWeek(i, dtPrevYear);
					else if (i2 == 1) dtDateRange[i, i2] = GetStartOfCalculationWeek(i, dtThisYear);
					else if (i2 == 2) dtDateRange[i, i2] = GetStartOfCalculationWeek(i, dtNextYear);
				}

				if (i < 53)
				{
					grdSKUP.Columns[(i + 1)].HeaderText = "Week" + i;
				}
			}

			queryArray = new String[1, 55];
			queryArray.Initialize();
			fillItemsInfo(enteredSku);
			fillItemsWOrders(enteredSku);
			FutureUsageHyperLink.Visible = true;
			Session["ItemCode"] = skuNumber.Text;
			CalcValues();
			displayInfo();

			lblPageLoadTime.Text = "Data loaded in " + System.DateTime.Now.Subtract(dtBegin).TotalSeconds.ToString() + " seconds.";
		}
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

        System.Data.DataTable dtblSKUP = new System.Data.DataTable();
        dtblSKUP.Columns.Add("code", Type.GetType("System.String"));
        for (int w = 1; w < 54; w++)//weeks
        {
            dtblSKUP.Columns.Add("Date" + w, Type.GetType("System.String"));
        }
        int yearsloop = iArr_SKUP.GetLength(0);
        for (int y = 0; y < iArr_SKUP.GetLength(0); y++)
        {
            for (int i = 0; i < InfoList.Length; i++)//rows
            {
                dtblSKUP.Rows.Add();
                dtblSKUP.Rows[dtblSKUP.Rows.Count - 1]["code"] = "'" + System.Convert.ToString((dtPrevYear.Year + y)).Substring(2) + " " + InfoList[i]/* + " " + System.Convert.ToString(i)*/;
                for (int w = 0; w < 52; w++)//weeks
                {
                    if (i == 0)
                    {
                        dtblSKUP.Rows[dtblSKUP.Rows.Count - 1]["Date" + (w + 1)] = String.Format("{0:MM/dd}", dtDateRange[w, y]);
                    }
                    else
                    {
                        dtblSKUP.Rows[dtblSKUP.Rows.Count - 1]["Date" + (w + 1)] = iArr_SKUP[y, w, i];
                    }
                }

            }
        }

        grdSKUP.DataSource = dtblSKUP;
        grdSKUP.DataBind();
        grdSKUP.Visible = true;
        Session.Add("dt",dtblSKUP);
        Exportbtn.Visible = true;

		for (int y = 0; y <= iYearsToCalc; y++)
        {
			/**** Default visibility ****/
			grdSKUP.Rows[datePos + (y * iGridRows)].Visible = true;
			grdSKUP.Rows[AF_BW + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[AF_SOpercent + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[AF_SU + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[A_SU + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[F_SU + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[H_SU + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[AF_MOV + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[A_MOV + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[F_MOV + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[AB_W + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[F_BW + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[A_BP + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[F_BP + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[BP + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[AF_OS + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[F_OS + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[A_MOV + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[F_MOV + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[AF_PO + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[F_PO + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[FUT_PO + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[BI + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[BR + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[BZ + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[IA + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[IP + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[IT + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[IZ + (y * iGridRows)].Visible = false;
			grdSKUP.Rows[PZ + (y * iGridRows)].Visible = false;

            /**** Color ****/
            grdSKUP.Rows[datePos + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#ffff99");
			grdSKUP.Rows[A_SU + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[F_SU + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[H_SU + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[AF_SU + y * iGridRows].BackColor = System.Drawing.Color.FromName("#cc99ff");
			grdSKUP.Rows[FP_SU + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[AF_MOV + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[AF_BW + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#cc99ff");
			grdSKUP.Rows[AB_W + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[F_BW + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#ccffcc");
			grdSKUP.Rows[BP + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#cc99ff");
			grdSKUP.Rows[A_BP + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[F_BP + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#ccffcc");
			grdSKUP.Rows[BP_WS + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#cc99ff");
			grdSKUP.Rows[AF_SOpercent + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#cc99ff");
			grdSKUP.Rows[AF_OS + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[A_OS + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[F_OS + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[AF_PO + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#cc99ff");
			grdSKUP.Rows[A_PO + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[F_PO + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#c0c0c0");
			grdSKUP.Rows[FUT_PO + (y * iGridRows)].BackColor = System.Drawing.Color.FromName("#ccffcc");

			/**** Visiblity and Color by Year ****/
			if (y == 0)
			{ //Previous Year Display
				grdSKUP.Rows[A_SU + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[AF_BW + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[A_BP + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[BP_WS + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[AF_SOpercent + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[A_OS + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[A_PO + (y * iGridRows)].Visible = true;
			}
			else if (y == 1)
			{ //Current Year Display
				grdSKUP.Rows[AF_SU + (y * iGridRows)].Visible = true;
				//grdSKUP.Rows[FP_SU + (y * iGridRows)].Visible = true;
				//grdSKUP.Rows[AF_BW + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[BP + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[BP_WS + (y * iGridRows)].Visible = true;
				//grdSKUP.Rows[AF_SOpercent + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[A_OS + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[AF_PO + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[A_PO + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[FUT_PO + (y * iGridRows)].Visible = true;
				grdSKUP.Rows[F_PO + (y * iGridRows)].Visible = true;
			}
			else if (y == 2)
			{ //Next Year Display
			}
        }
    }
    #endregion
}