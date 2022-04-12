using Microsoft.AspNetCore.Mvc;
using MainLABAPI.Data;
using MainLABAPI.Data.Models.DB;
using MainLABAPI.Data.Models.DTO;
using MainLABAPI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using MainLABAPI.Services.User_Data;
using MainLABAPI.Enum;

namespace MainLABAPI.Controllers.User_Data
{
    [ApiController]
    [Route("api")]
    public class UserDataController: ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IUserDataService _userDataService;

        public UserDataController(ApplicationContext context, IUserDataService userDataService)
        {
            _context = context;
            _userDataService = userDataService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users")]

        public IActionResult UsersGet()
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }

            try
            {
                return Ok(_userDataService.GetAllUser());
            }catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong in method _MethodName.");
            }
        }
    }
}
