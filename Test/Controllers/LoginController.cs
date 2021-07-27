using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Models.VM;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<string> Login()
        {
            var url = "http://test-demo.aemenersol.com/api/Account/Login";

            using var client = new HttpClient();

            var _login = new Models.Login()
            {
                username = "user@aemenersol.com",
                password = "Test@123",
            };
            var json = JsonConvert.SerializeObject(_login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);

            var result = response.StatusCode;
            var result1 = response.Content.ReadAsStringAsync().Result;
            return result1.Trim('"');
        }
    }
}
