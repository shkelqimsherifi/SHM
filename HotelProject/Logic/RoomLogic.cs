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
    public class RoomLogic
    {
        private static RoomLogic instance = null;

        public RoomLogic()
        {

        }

        public static RoomLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomLogic();
                }

                return instance;
            }
        }

        public bool Register_Room(Room oRoom)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterRoom", oConnection);
                    cmd.Parameters.AddWithValue("Number", oRoom.Number);
                    cmd.Parameters.AddWithValue("Details", oRoom.Details);
                    cmd.Parameters.AddWithValue("Price", Convert.ToDecimal(oRoom.PriceText,new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("FloorId", oRoom.oHotelFloor.FloorId);
                    cmd.Parameters.AddWithValue("CategoryId", oRoom.oCategory.CategoryId);
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

        public bool Modify_Room(Room oRoom)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModifyRoom", oConnection);
                    cmd.Parameters.AddWithValue("RoomId", oRoom.RoomId);
                    cmd.Parameters.AddWithValue("Number", oRoom.Number);
                    cmd.Parameters.AddWithValue("Details", oRoom.Details);
                    cmd.Parameters.AddWithValue("Price", Convert.ToDecimal(oRoom.PriceText, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("FloorId", oRoom.oHotelFloor.FloorId);
                    cmd.Parameters.AddWithValue("CategoryId", oRoom.oCategory.CategoryId);
                    cmd.Parameters.AddWithValue("State_text", oRoom.State_text);
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


        public List<Room> List_Room()
        {
            List<Room> List = new List<Room>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select h.RoomId,h.Number,h.Details,h.Price,p.FloorId,p.Description_text[Description_floor],c.CategoryId,c.Description_text[Description_category],h.State_text,");
                    query.AppendLine("eh.IdStateRoom,eh.Description_text[Description_StateRoom]");
                    query.AppendLine("from ROOM h");
                    query.AppendLine("inner join HOTEL_FLOOR p on p.FloorId = h.FloorId");
                    query.AppendLine("inner join HOTELCATEGORY c on c.CategoryId = h.CategoryId");
                    query.AppendLine("inner join ROOM_STATE eh on eh.IdStateRoom = h.IdStateRoom");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new Room()
                            {
                                RoomId = Convert.ToInt32(dr["RoomId"]),
                                Number = dr["Number"].ToString(),
                                Details = dr["Details"].ToString(),
                                Price = Convert.ToDecimal(dr["Price"].ToString()),
                                oHotelFloor = new HotelFloor() { FloorId = Convert.ToInt32(dr["FloorId"]), Description_text = dr["Description_floor"].ToString() },
                                oCategory = new Category() { CategoryId = Convert.ToInt32(dr["CategoryId"]), Description_text = dr["Description_category"].ToString() },
                                oRoomState = new RoomState() { IdStateRoom = Convert.ToInt32(dr["IdStateRoom"]) , Description_text = dr["Description_StateRoom"].ToString() },
                                State_text = Convert.ToBoolean(dr["State_text"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<Room>();
                }
            }
            return List;
        }

        public bool Remove_Room(int id)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from ROOM where RoomId = @id", oConnection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    response = true;

                }
                catch (Exception ex)
                {
                    response = false;
                }

            }

            return response;

        }

        public bool UpdateStatus_Room(int roomId, int idstateroom_text)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update ROOM set IdStateRoom = @idstateroom where RoomId = @roomId ", oConnection);
                    cmd.Parameters.AddWithValue("@roomId", roomId);
                    cmd.Parameters.AddWithValue("@idstateroom", idstateroom_text);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();

                    cmd.ExecuteNonQuery();

                    response = true;

                }
                catch (Exception ex)
                {
                    response = false;
                }

            }

            return response;

        }
    }
}