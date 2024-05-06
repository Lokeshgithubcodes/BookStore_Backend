using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;

namespace BusinessLayer.Services
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepository _userRepo;

        public UserBusiness(IUserRepository userRepo)
        {
           this._userRepo = userRepo;
        }

        public UserModel RegisterUser(UserModel model)
        {
            return _userRepo.RegisterUser(model);
        }

        public object GetData()
        {
            return _userRepo.GetData();
        }

        public object UpdateUser(int id, UpdateUser model)
        {
            return _userRepo.UpdateUser(id, model);
        }

        public object LoginUser(LoginModel model)
        {
            return _userRepo.LoginUser(model);

        }

        public ForgotPasswordModel ForgotPassword(string email)
        {
            return _userRepo.ForgotPassword(email);
        }

        public bool ResetPassword(string email, string password)
        {
            return _userRepo.ResetPassword(email, password);
        }

        public bool UserInsertOrUpdate(User user)
        {
            return _userRepo.UserInsertOrUpdate(user);
        }
    }
}
