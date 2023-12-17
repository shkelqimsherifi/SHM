using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace HotelProject.Logic
{
    public class ReceptionLogic
    {
        private static ReceptionLogic instance = null;

        public ReceptionLogic()
        {

        }

        public static ReceptionLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReceptionLogic();
                }

                return instance;
            }
        }


        public List<Reception> List_Reception()
        {
            List<Reception> List = new List<Reception>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select r.ReceptionId,p.PersonName,p.LastName,p.Document,p.Mail,h.RoomId,h.Number,h.Details,ps.Description_text[Description_floor],c.Description_text[Description_category],");
                    query.AppendLine("convert(char(10), r.EntryDate, 103)[EntryDate], convert(char(10), r.DepartureDate, 103)[DepartureDate],r.StartingPrice,r.Advancement,r.RemainingPrice,r.Observation,r.State_text");
                    query.AppendLine("from RECEPTION r");
                    query.AppendLine("inner join PERSON p on p.PersonId = r.ClientId");
                    query.AppendLine("inner join ROOM h on h.RoomId = r.RoomId");
                    query.AppendLine("inner join HOTEL_FLOOR ps on ps.FloorId = h.FloorId");
                    query.AppendLine("inner join HOTELCATEGORY c on c.CategoryId = h.CategoryId");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new Reception()
                            {
                                ReceptionId = Convert.ToInt32(dr["ReceptionId"]),
                                oPerson = new Person() {
                                    PersonName = dr["PersonName"].ToString(),
                                    LastName = dr["LastName"].ToString(),
                                    Document = dr["Document"].ToString(),
                                    Mail = dr["Mail"].ToString(),
                                },
                                oRoom = new Room() {
                                    RoomId = Convert.ToInt32(dr["RoomId"]),
                                    Number = dr["Number"].ToString(),
                                    Details = dr["Details"].ToString(),
                                    oHotelFloor = new HotelFloor() { Description_text = dr["Description_floor"].ToString() },
                                    oCategory = new Category() { Description_text = dr["Description_category"].ToString() }
                                },
                                EntryDateText = dr["EntryDate"].ToString(),
                                DepartureDateText = dr["DepartureDate"].ToString(),
                                StartingPrice = Convert.ToDecimal(dr["StartingPrice"].ToString(),new CultureInfo("en-US")),
                                Advancement = Convert.ToDecimal(dr["Advancement"].ToString(), new CultureInfo("en-US")),
                                RemainingPrice = Convert.ToDecimal(dr["RemainingPrice"].ToString(), new CultureInfo("en-US")),
                                Observation = dr["Observation"].ToString(),
                                State_text = Convert.ToBoolean(dr["State_text"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<Reception>();
                }
            }
            return List;
        }

        public bool Register_Reception(Reception obj)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterReception", oConnection);
                    cmd.Parameters.AddWithValue("ClientId", obj.oPerson.PersonId);
                    cmd.Parameters.AddWithValue("DocumentType", obj.oPerson.DocumentType);
                    cmd.Parameters.AddWithValue("Document", obj.oPerson.Document);
                    cmd.Parameters.AddWithValue("PersonName", obj.oPerson.PersonName);
                    cmd.Parameters.AddWithValue("LastName", obj.oPerson.LastName);
                    cmd.Parameters.AddWithValue("Mail", obj.oPerson.Mail);
                    cmd.Parameters.AddWithValue("RoomId", obj.oRoom.RoomId);
					cmd.Parameters.AddWithValue("DepartureDate", DateTime.ParseExact(obj.DepartureDateText, "dd/MM/yyyy", CultureInfo.InvariantCulture));
					//cmd.Parameters.AddWithValue("DepartureDate", Convert.ToDateTime(obj.DepartureDateText, new CultureInfo("en-US")));
                    cmd.Parameters.AddWithValue("StartingPrice", Convert.ToDecimal(obj.StartingPriceText, new CultureInfo("en-US")));
                    cmd.Parameters.AddWithValue("Advancement", Convert.ToDecimal(obj.AdvancementText, new CultureInfo("en-US")));
                    cmd.Parameters.AddWithValue("RemainingPrice", Convert.ToDecimal(obj.RemainingPriceText, new CultureInfo("en-US")));
                    cmd.Parameters.AddWithValue("Observation", obj.Observation);
                    cmd.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    response = Convert.ToBoolean(cmd.Parameters["Result"].Value);

                }
                catch (Exception ex)
                {
                    response = false;
                }
            }
            return response;
        }

        public bool Close_Reception(Reception obj)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {

                oConnection.Open();
                SqlTransaction objTransacion = oConnection.BeginTransaction();

                try
                {
                    
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update RECEPTION set DateDepartureConfirmation = GETDATE() , TotalPaid = @totalPaid , PenaltyCost = @penaltyCost, State_text = 0");
                    query.AppendLine("where ReceptionId = @receptionId");
                    query.AppendLine("");
                    query.AppendLine("update ROOM set IdStateRoom = 3");
                    query.AppendLine("where RoomId = @roomId");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConnection);
                    cmd.Parameters.AddWithValue("@totalPaid", Convert.ToDecimal(obj.TotalPaidText,new CultureInfo("en-US")));
                    cmd.Parameters.AddWithValue("@penaltyCost", Convert.ToDecimal(obj.PenaltyCostText, new CultureInfo("en-US")));
                    cmd.Parameters.AddWithValue("@receptionId", obj.ReceptionId);
                    cmd.Parameters.AddWithValue("@roomId", obj.oRoom.RoomId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = objTransacion;

                    cmd.ExecuteNonQuery();

                    response = true;
                    objTransacion.Commit();

                }
                catch (Exception ex)
                {
                    objTransacion.Rollback();
                    response = false;
                }

            }

            return response;

        }


    }
}