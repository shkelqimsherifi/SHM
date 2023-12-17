using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HotelProject.Logic
{
    public class ProductLogic
    {
        private static ProductLogic instance = null;

        public ProductLogic()
        {

        }

        public static ProductLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductLogic();
                }

                return instance;
            }
        }

        public bool Register_Product(Product oProduct)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("sp_RegisterProduct", oConnection);
                    cmd.Parameters.AddWithValue("ProductName", oProduct.ProductName);
                    cmd.Parameters.AddWithValue("Details", oProduct.Details);
                    cmd.Parameters.AddWithValue("Price", Convert.ToDecimal(oProduct.PriceText,new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("Amount", oProduct.Amount);
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

        public bool Modify_Product(Product oProduct)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModifyProduct", oConnection);
                    cmd.Parameters.AddWithValue("ProductID", oProduct.ProductID);
                    cmd.Parameters.AddWithValue("ProductName", oProduct.ProductName);
                    cmd.Parameters.AddWithValue("Details", oProduct.Details);
                    cmd.Parameters.AddWithValue("Price", Convert.ToDecimal(oProduct.PriceText, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("Amount", oProduct.Amount);
                    cmd.Parameters.AddWithValue("State_text", oProduct.State_text);
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


        public List<Product> List_Product()
        {
            List<Product> List = new List<Product>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select ProductID,ProductName,Details,Price,Amount,State_text from PRODUCT", oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new Product()
                            {
                                ProductID = Convert.ToInt32(dr["ProductID"]),
                                ProductName = dr["ProductName"].ToString(),
                                Details = dr["Details"].ToString(),
                                Price = Convert.ToDecimal(dr["Price"].ToString()),
                                Amount = Convert.ToInt32(dr["Amount"].ToString()),
                                State_text = Convert.ToBoolean(dr["State_text"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<Product>();
                }
            }
            return List;
        }

        public bool Remove_Product(int id)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from PRODUCT where ProductID = @id", oConnection);
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