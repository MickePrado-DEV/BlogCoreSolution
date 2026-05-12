using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository
{


    public interface IUserRepository
    {
        ApplicationUser getUser(string userId);
        IEnumerable<ApplicationUser> GetAll(string currentUser);
        void BlockUser(string userId);
        void UnBlockUser(string userId);


    }
}
