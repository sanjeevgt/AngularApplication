using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular.NetCoreAPI.Common;
using API.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Angular.NetCoreAPI.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {

        private ApiService<User> apiService;

        public AuthController()
        {
            apiService = new ApiService<User>();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]User user)
        {
            var result = await apiService.PostRecordAndReturnAsString(user, "https://localhost:44343/api/UserLogin/login");
            return Ok(result);
        }
    }
}