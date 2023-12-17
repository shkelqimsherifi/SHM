using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace HotelProject.Logic
{
    public class SaleDetailLogic
    {
        private static SaleDetailLogic instance = null;

        public SaleDetailLogic()
        {

        }

        public static SaleDetailLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaleDetailLogic();
                }

                return instance;
            }
        }

        public List<SaleDetail> List_SaleDetail()
        {
            List<SaleDetail> List = new List<SaleDetail>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select dv.SalesDetailId,dv.SalesID,p.ProductID,p.ProductName,p.Price,dv.Amount,dv.SubTotal from SALE_DETAIL dv");
                    query.AppendLine("inner join PRODUCT p on p.ProductID = dv.ProductID");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new SaleDetail()
                            {
								SalesDetailId = Convert.ToInt32(dr["SalesDetailId"]),
								SalesID = Convert.ToInt32(dr["SalesID"]),
								oProduct = new Product() {
                                    ProductID = Convert.ToInt32(dr["ProductID"]),
                                    ProductName = dr["ProductName"].ToString(),
                                    Price = Convert.ToDecimal(dr["Price"].ToString()),
                                },
								Amount = Convert.ToInt32(dr["Amount"].ToString()),
                                SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString())
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<SaleDetail>();
                }
            }
            return List;
        }
    }
}