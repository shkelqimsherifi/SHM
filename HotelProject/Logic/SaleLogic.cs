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
    public class SaleLogic
    {
        private static SaleLogic instance = null;

        public SaleLogic()
        {

        }

        public static SaleLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SaleLogic();
                }

                return instance;
            }
        }

        public List<Sale> List_Sale()
        {
            List<Sale> List = new List<Sale>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select SalesID,ReceptionId,Total,State_text from SALE", oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new Sale()
                            {
                                SalesID = Convert.ToInt32(dr["SalesID"]),
                                oReception = new Reception() { ReceptionId = Convert.ToInt32(dr["ReceptionId"]) },
                                Total = Convert.ToDecimal(dr["Total"].ToString()),
                                State_text = dr["State_text"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<Sale>();
                }
            }
            return List;
        }

        public bool Register_Sale(Sale obj)
        {
            bool response = true;
            
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    oConnection.Open();

                    SqlTransaction objTransacion = oConnection.BeginTransaction();


                    sb.AppendLine("declare @idsale int = 0");
                    sb.AppendLine(string.Format("insert into SALE(ReceptionId,Total,State_text) values ({0},{1},'{2}')", obj.oReception.ReceptionId,obj.Total,obj.State_text));
                    sb.AppendLine("set @idsale = SCOPE_IDENTITY()");
                    foreach (SaleDetail dv in obj.oSaleDetail)
                    {
                        sb.AppendLine(string.Format("insert into SALE_DETAIL(SalesID,ProductID,Amount,SubTotal) values({0},{1},{2},{3})", "@idsale", dv.oProduct.ProductID, dv.Amount, dv.SubTotal));

                        sb.AppendLine(string.Format("update PRODUCT set Amount = Amount - {0} where ProductID = {1}", dv.Amount, dv.oProduct.ProductID));
                    }
                    sb.AppendLine("select @idsale");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConnection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = objTransacion;
                    try
                    {
                        int idsale = 0;
                        int.TryParse(cmd.ExecuteScalar().ToString(), out idsale);

                        if (idsale != 0)
                        {
                            objTransacion.Commit();
                            response = true;
                        }
                        else
                        {
                            objTransacion.Rollback();
                            response = false;
                        }

                    }
                    catch (Exception e)
                    {
                        objTransacion.Rollback();
                        response = false;
                    }

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