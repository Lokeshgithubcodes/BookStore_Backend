﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        public UserModel RegisterUser(UserModel model);
        public object GetData();

        public object UpdateUser(int id, UpdateUser model);

        public object LoginUser(LoginModel model);

        public ForgotPasswordModel ForgotPassword(string email);

        public bool ResetPassword(string email, string password);
    }
}
