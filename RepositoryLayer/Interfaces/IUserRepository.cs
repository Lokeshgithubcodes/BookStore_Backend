using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public UserModel RegisterUser(UserModel model);
        public object GetData();

        public object UpdateUser(int id, UpdateUser model);

        public object LoginUser(LoginModel model);

        public ForgotPasswordModel ForgotPassword(string email);

        public bool ResetPassword(string email, string password);

        public bool UserInsertOrUpdate(User user);
    }
}
