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
    public class PersonLogic
    {

        private static PersonLogic instance = null;

        public PersonLogic()
        {

        }

        public static PersonLogic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonLogic();
                }

                return instance;
            }
        }

        public List<PersonType> List_PersonType()
        {
            List<PersonType> List = new List<PersonType>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select PersonTypeId,Description_text from PERSON_TYPE", oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new PersonType()
                            {
                                PersonTypeId = Convert.ToInt32(dr["PersonTypeId"]),
                                Description_text = dr["Description_text"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<PersonType>();
                }
            }
            return List;
        }

        public bool Register_Person(Person obj)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterPerson", oConnection);
                    cmd.Parameters.AddWithValue("DocumentType", obj.DocumentType);
                    cmd.Parameters.AddWithValue("Document", obj.Document);
                    cmd.Parameters.AddWithValue("PersonName", obj.PersonName);
                    cmd.Parameters.AddWithValue("LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("Mail", obj.Mail);
                    cmd.Parameters.AddWithValue("Code", obj.Code);
                    cmd.Parameters.AddWithValue("PersonTypeId", obj.oPersonType.PersonTypeId);
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

        public bool Modify_Person(Person obj)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModifyPerson", oConnection);
                    cmd.Parameters.AddWithValue("PersonId", obj.PersonId);
                    cmd.Parameters.AddWithValue("DocumentType", obj.DocumentType);
                    cmd.Parameters.AddWithValue("Document", obj.Document);
                    cmd.Parameters.AddWithValue("PersonName", obj.PersonName);
                    cmd.Parameters.AddWithValue("LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("Mail", obj.Mail);
                    cmd.Parameters.AddWithValue("Code", obj.Code);
                    cmd.Parameters.AddWithValue("PersonTypeId", obj.oPersonType.PersonTypeId);
                    cmd.Parameters.AddWithValue("State_text", obj.State_text);
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


        public List<Person> List_Person()
        {
            List<Person> List = new List<Person>();
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.PersonId, p.DocumentType, p.Document, p.PersonName, p.LastName, p.Mail, p.Code, tp.PersonTypeId, tp.Description_text, p.State_text from PERSON p");
                    sb.AppendLine("inner join PERSON_TYPE tp on tp.PersonTypeId = p.PersonTypeId");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConnection);
                    cmd.CommandType = CommandType.Text;

                    oConnection.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            List.Add(new Person()
                            {
                                PersonId = Convert.ToInt32(dr["PersonId"]),
                                DocumentType = dr["DocumentType"].ToString(),
                                Document = dr["Document"].ToString(),
                                PersonName = dr["PersonName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Mail = dr["Mail"].ToString(),
                                Code = dr["Code"].ToString(),
                                oPersonType = new PersonType() { PersonTypeId = Convert.ToInt32(dr["PersonTypeId"]), Description_text = dr["Description_text"].ToString() },
                                State_text = Convert.ToBoolean(dr["State_text"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    List = new List<Person>();
                }
            }
            return List;
        }

        public bool Remove_Person(int id)
        {
            bool response = true;
            using (SqlConnection oConnection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from PERSON where PersonId = @id", oConnection);
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