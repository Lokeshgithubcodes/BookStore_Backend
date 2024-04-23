using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System.Data.SqlClient;
using System.Data;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepository:IUserRepository
    {
        string connectionstring = "Data Source=LOKESH\\SQLEXPRESS;Initial Catalog=BookStoreDB;Integrated Security=True;";

        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
           this._config = config;
        }


        public UserModel RegisterUser(UserModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("BookStoreUserAdd_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return model;

        }

        public object GetData()
        {
            List<User> users = new List<User>();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BookStoreGetAll_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(dataReader["Id"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.EmailId = dataReader["EmailId"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.Mobile = Convert.ToInt64(dataReader["Mobile"]);
                        users.Add(user);
                    }
                    return users;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return null;
            }

        }

        public object UpdateUser(int id, UpdateUser model)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("BookStoreUserUpdate_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return null;
                    }
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

        public object LoginUser(LoginModel model)
        {
            User user = new User();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("BookStoreLogin_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    conn.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user.UserId = Convert.ToInt32(dataReader["Id"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.EmailId = dataReader["EmailId"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.Mobile = Convert.ToInt64(dataReader["Mobile"]);

                    }
                    if (user.EmailId == model.EmailId && user.Password == model.Password)
                    {
                        LoginTokenModel login = new LoginTokenModel();
                        var token = GenerateToken(user.UserId, user.EmailId);
                        login.Id = user.UserId;
                        login.FullName = user.FullName;
                        login.EmailId = user.EmailId;
                        login.Token = token;
                        login.Password = user.Password;
                        login.Mobile = user.Mobile;


                        return login;
                    }

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


        private string GenerateToken(long UserId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",userEmail),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public ForgotPasswordModel ForgotPassword(string email)
        {
            User user = new User();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {


                    SqlCommand cmd = new SqlCommand("BookStoreForget_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", email);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user.UserId = Convert.ToInt32(dataReader["Id"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.EmailId = dataReader["EmailId"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.Mobile = Convert.ToInt64(dataReader["Mobile"]);

                    }
                    if (email == user.EmailId)
                    {
                        ForgotPasswordModel model = new ForgotPasswordModel();

                        model.EmailId = user.EmailId;
                        model.UserId = user.UserId;
                        model.token = GenerateToken(user.UserId, user.EmailId);
                        return model;
                    }

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

        public bool ResetPassword(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("BookStoreReset_sp", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }

    }
}
