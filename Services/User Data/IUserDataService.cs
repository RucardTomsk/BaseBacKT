using Microsoft.AspNetCore.Mvc;
using MainLABAPI.Data;
using MainLABAPI.Data.Models.DB;
using MainLABAPI.Data.Models.DTO;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using MainLABAPI.Enum;
using System.Numerics;


namespace MainLABAPI.Services.User_Data
{
    public interface IUserDataService
    {
        public List<ModelForUserDataInIndexMethodModel> GetAllUser();
    }

    public class UserDataService : IUserDataService
    {
        private readonly ApplicationContext _context;

        public UserDataService(ApplicationContext context)
        {
            _context = context;
        }

        public List<ModelForUserDataInIndexMethodModel> GetAllUser()
        {
            List<ModelForUserDataInIndexMethodModel> newList = new List<ModelForUserDataInIndexMethodModel>();
            
            var users = _context.Users.ToList();

            foreach (var user in users)
            {
                newList.Add(new ModelForUserDataInIndexMethodModel
                {
                    Username = user.UserName,
                    UserId = user.Id,
                    RoleId = user.RoleId
                });
            }

            return newList;
        }
    }
}
