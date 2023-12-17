using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelProject.Logic
{
    public class CategoryLogic
    {
        private static CategoryLogic instance = null;

        public CategoryLogic()
        {

        }

        public static CategoryLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryLogic();
                }

                return instance;
            }
        }

        public bool Register_Category(Category oCategory)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterCategory", oConnection);
                    cmd.Parameters.AddWithValue("Description_text", oCategory.Description_text);
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

        public bool Modify_Category(Category oCategory)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModifyCategory", oConnection);
                    cmd.Parameters.AddWithValue("CategoryId", oCategory.CategoryId);
                    cmd.Parameters.AddWithValue("Description_text", oCategory.Description_text);
                    cmd.Parameters.AddWithValue("State_text", oCategory.State_text);
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


        public List<Category> List_Category()
        {
            List<Category> List = new List<Category>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select CategoryId,Description_text,State_text from HOTELCATEGORY", oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new Category()
                            {
                                CategoryId = Convert.ToInt32(dr["CategoryId"]),
								Description_text = dr["Description_text"].ToString(),
                                State_text = Convert.ToBoolean(dr["State_text"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<Category>();
                }
            }
            return List;
        }

        public bool Remove_Category(int id)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from HOTELCATEGORY where CategoryId = @id", oConnection);
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