using Microsoft.AspNetCore.Mvc;
using MainLABAPI.Data;
using MainLABAPI.Data.Models.DB;
using MainLABAPI.Data.Models.DTO;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using MainLABAPI.Enum;
using System.Numerics;

namespace MainLABAPI.Services.Auth
{
    public interface IAuthService
    {
        System.Threading.Tasks.Task AddUser(RegistrationDataModel model);
        ClaimsIdentity GetIdentity(string username, string password);
        User UserVerification(string username, string password);
    }

    public class AuthService: IAuthService
    {
        private readonly ApplicationContext _context;

        public AuthService(ApplicationContext context)
        {
            _context = context;
        }
        private string Hash(string password)
        {
            byte[] data = Encoding.Default.GetBytes(password);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(data);
            password = Convert.ToBase64String(result);
            return password;
        }

        public User UserVerification(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == username && x.PasswordHash == Hash(password));
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async System.Threading.Tasks.Task AddUser(RegistrationDataModel model)
        {
            await _context.Users.AddAsync(new User
            {
                Name = model.Name,
                SurName = model.Surname,
                UserName = model.Username,
                PasswordHash = Hash(model.Password),
                RoleId = 0
            });
            await _context.SaveChangesAsync();
        }
        
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = UserVerification(username, password);
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, ((EnumRoles)user.RoleId).ToString())
        };

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        
    }
}
