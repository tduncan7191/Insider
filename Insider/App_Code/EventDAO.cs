using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
public class EventDAO
{
    //change the connection string as per your database connection.
    //private static string connectionString = "Data Source=MASSERV01;Initial Catalog=INSIDER;Persist Security Info=True;User ID=rplus-insider;Password=1ns1d3r-r+";
	private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cnSQL_MAS_INSIDER"].ConnectionString;

    //this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {   
        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end FROM SALES.dbo.event where event_start>=@start AND event_end<=@end", con);
        SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end, event_type FROM INSIDER.dbo.event where (event_start>=@start AND event_end<=@end) and IsNull(event_type,0)<1 union all SELECT event_id, description, title, 'event_start' = CASE WHEN Year(@start) < Year(@end) and DATEPART(MONTH,event_start)<=12 and DATEPART(MONTH,event_start)>=6 then DATEADD(yyyy,datediff(yyyy,event_start,getdate())-1,event_start) ELSE DATEADD(yyyy,datediff(yyyy,event_start,getdate()),event_start) END, 'event_end' = CASE WHEN Year(@start) < Year(@end) and DATEPART(MONTH,event_end)<=12 and DATEPART(MONTH,event_end)>=6 then DATEADD(yyyy,datediff(yyyy,event_end,getdate())-1,event_end) ELSE DATEADD(yyyy,datediff(yyyy,event_end,getdate()),event_end) END,event_type FROM INSIDER.dbo.event where (DATEADD(YEAR, DATEDIFF(YEAR,  event_start, @start), event_start) BETWEEN @start AND @end OR DATEADD(YEAR, DATEDIFF(YEAR,  event_start, @end), event_start) BETWEEN @start AND @end) and event_type>0", con);
        cmd.Parameters.AddWithValue("@start", start);
        cmd.Parameters.AddWithValue("@end", end);

        using (con)
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CalendarEvent cevent = new CalendarEvent();
                cevent.id = (int)reader["event_id"];
                cevent.title = (string)reader["title"];
                cevent.description = (string)reader["description"];
                cevent.start = (DateTime)reader["event_start"];
                cevent.end = (DateTime)reader["event_end"];
                cevent.type = (int)reader["event_type"];
                events.Add(cevent);
            }
        }
        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains a extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM SALES.dbo.event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }

    //this method updates the event title and description
    public static void updateEvent(int id, String title, String description,int type)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE INSIDER.dbo.event SET title=@title, description=@description WHERE event_id=@event_id", con);
        cmd.Parameters.AddWithValue("@title", title);
        cmd.Parameters.AddWithValue("@description", description);
        cmd.Parameters.AddWithValue("@event_id", id);
        cmd.Parameters.AddWithValue("@event_type", type);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }


    }

    //this method updates the event start and end time
    public static void updateEventTime(int id, DateTime start, DateTime end)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("UPDATE INSIDER.dbo.event SET event_start=@event_start, event_end=@event_end WHERE event_id=@event_id", con);
        cmd.Parameters.AddWithValue("@event_start", start);
        cmd.Parameters.AddWithValue("@event_end", end);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    //this mehtod deletes event with the id passed in.
    public static void deleteEvent(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("DELETE FROM INSIDER.dbo.event WHERE (event_id = @event_id)", con);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    //this method adds events to the database
    public static int addEvent(CalendarEvent cevent)
    {
        //add event to the database and return the primary key of the added event row

        //insert
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO INSIDER.dbo.event (title, description, event_start, event_end,contact_id,event_type) VALUES(@title, @description, @event_start, @event_end,0,0)", con);
        cmd.Parameters.AddWithValue("@title", cevent.title);
        cmd.Parameters.AddWithValue("@description", cevent.description);
        cmd.Parameters.AddWithValue("@event_start", cevent.start);
        cmd.Parameters.AddWithValue("@event_end", cevent.end);
        //cmd.Parameters.AddWithValue("@contact_id", 0);
        //cmd.Parameters.AddWithValue("@event_type", cevent.type);
        int key = 0;
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();

            //get primary key of inserted row
            cmd = new SqlCommand("SELECT max(event_id) FROM INSIDER.dbo.event where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end AND event_type=@event_type", con);
            cmd.Parameters.AddWithValue("@title", cevent.title);
            cmd.Parameters.AddWithValue("@description", cevent.description);
            cmd.Parameters.AddWithValue("@event_start", cevent.start);
            cmd.Parameters.AddWithValue("@event_end", cevent.end);
            cmd.Parameters.AddWithValue("@event_type", cevent.type);

            key = (int)cmd.ExecuteScalar();
        }

        return key;

    }




}
