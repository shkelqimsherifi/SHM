using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelProject.Logic
{
    public class HotelFloorLogic
    {
        private static HotelFloorLogic instance = null;

        public HotelFloorLogic()
        {

        }

        public static HotelFloorLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HotelFloorLogic();
                }

                return instance;
            }
        }

        public bool Register_HotelFloor(HotelFloor oHotelFloor)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterFloor", oConnection);
                    cmd.Parameters.AddWithValue("Description_text", oHotelFloor.Description_text);
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

        public bool Modify_HotelFloor(HotelFloor oHotelFloor)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModifyFloor", oConnection);
                    cmd.Parameters.AddWithValue("FloorId", oHotelFloor.FloorId);
                    cmd.Parameters.AddWithValue("Description_text", oHotelFloor.Description_text);
                    cmd.Parameters.AddWithValue("State_text", oHotelFloor.State_text);
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


        public List<HotelFloor> List_HotelFloor()
        {
            List<HotelFloor> List = new List<HotelFloor>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select FloorId,Description_text,State_text from HOTEL_FLOOR", oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new HotelFloor()
                            {
                                FloorId = Convert.ToInt32(dr["FloorId"]),
                                Description_text = dr["Description_text"].ToString(),
                                State_text = Convert.ToBoolean(dr["State_text"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<HotelFloor>();
                }
            }
            return List;
        }

        public bool Remove_HotelFloor(int id)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from HOTEL_FLOOR where FloorId = @id", oConnection);
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
    }
}