using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AddressRepository:IAddressRepository
    {
        string connectionstring = "Data Source=LOKESH\\SQLEXPRESS;Initial Catalog=BookStoreDB;Integrated Security=True;";


        public AddressModel AddAddress(AddressModel model)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddAddress_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }


        public List<AddressModel> GetAddresses(int UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    List<AddressModel> addresses = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("GetAddressByUserId_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        AddressModel model = new AddressModel();
                        model.UserId = Convert.ToInt32(dataReader["UserId"]);
                        model.FullAddress = dataReader["FullAddress"].ToString();
                        model.City = dataReader["City"].ToString();
                        model.State = dataReader["State"].ToString();
                        model.Type = dataReader["Type"].ToString();
                        addresses.Add(model);

                    }
                    return addresses;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        public AddressUpdateModel UpdateAddress(AddressUpdateModel model)
        {

            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("UpdateAddressByUserId_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@AId", model.AId);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }




    }
}
