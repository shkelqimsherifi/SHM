using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace HotelProject.Logic
{
    public class ReportLogic
    {

        private static ReportLogic instance = null;

        public ReportLogic()
        {

        }

        public static ReportLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportLogic();
                }

                return instance;
            }
        }

        public DataTable Product_Report(string state_text, string startDate, string endDate) {

            DataTable dt = new DataTable();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SET DATEFORMAT dmy;");
                    sb.AppendLine("select ProductName, Details, Price, Amount, iif(State_text = 1, 'Active', 'No Active')[State_text],CONVERT(char(10),CreationDate,103)[CreationDate] from PRODUCT");
                    sb.AppendLine("where State_text = iif(@state_text = 2, State_text, @state_text) and convert(date,CreationDate) between @startDate and @endDate");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConnection);
                    cmd.Parameters.AddWithValue("@state_text", state_text);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                }
                catch (Exception ex)
                {
                    dt = new DataTable();
                }

            }
            return dt;

        }

        public DataTable Reception_Report(string roomID, string startDate, string endDate)
        {

            DataTable dt = new DataTable();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SET DATEFORMAT dmy;");
                    sb.AppendLine("select h.Number[Number_room],h.Details[Details_room] ,c.CategoryId[CategoryId_room],");
                    sb.AppendLine("(p.PersonName + ' ' + p.LastName)[Client],p.Mail[Mail Client],");
                    sb.AppendLine("CONVERT(char(10), r.EntryDate, 103)[Entry Date],CONVERT(char(10), r.DepartureDate, 103)[Departure Date],CONVERT(char(10), r.DateDepartureConfirmation, 103)[Date Departure Confirmation],");
                    sb.AppendLine("r.StartingPrice[Starting Price],r.Advancement,r.PenaltyCost[Penalty Cost],r.TotalPaid[Total Paid]");
                    sb.AppendLine("from RECEPTION r");
                    sb.AppendLine("inner join ROOM h on h.RoomId = r.RoomId");
                    sb.AppendLine("inner join HOTELCATEGORY c on c.CategoryId = h.CategoryId");
                    sb.AppendLine("inner join PERSON p on p.PersonId = r.ClientId");
                    sb.AppendLine("where convert(date, r.EntryDate) between @startDate and @endDate");
                    sb.AppendLine("and h.RoomId = iif(@roomID = 0, h.RoomId,@roomID)");
                    
                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConnection);
                    cmd.Parameters.AddWithValue("@roomID", roomID);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                }
                catch (Exception ex)
                {
                    dt = new DataTable();
                }

            }
            return dt;

        }

    }
}