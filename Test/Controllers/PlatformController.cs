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
using Test.Controllers;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly TestAssessmentContext _context;
        public PlatformController(TestAssessmentContext context)
        {
            _context = context;
        }

        [HttpGet("GetPlatform")]
        public async Task<IActionResult> GetPlatform(int id)
        {
            try
            {
                var platforms = new List<Platform>();
                var platformVM = new List<Platform>();

                if (id > 0)
                {
                    platforms = await _context.Platforms.Where(p=>p.Id == id).ToListAsync();
                }
                else
                {
                    platforms = await _context.Platforms.Include(a => a.well).ToListAsync();
                }

                foreach(var p in platforms)
                {
                    var plat = new Platform();
                    plat.Id = p.Id;
                    plat.UniqueName = p.UniqueName;
                    plat.Latitude = p.Latitude;
                    plat.Longitude = p.Longitude;
                    plat.CreatedAt = p.CreatedAt;
                    plat.UpdatedAt = p.UpdatedAt;
                    var wellList = await _context.Wells.Where(w => w.PlatformId == p.Id).ToListAsync();
                    var wellVM = new List<Well>();
                    foreach(var w in wellList)
                    {
                        var well = new Well();
                        well.Id = w.Id;
                        well.UniqueName = w.UniqueName;
                        well.PlatformId = w.PlatformId;
                        well.Latitude = w.Latitude;
                        well.Longitude = w.Longitude;
                        well.CreatedAt = w.CreatedAt;
                        well.UpdatedAt = w.UpdatedAt;
                        wellVM.Add(well);
                    }
                    plat.well = wellVM;
                    platformVM.Add(plat);
                }
                return Ok(platformVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPlatformWellDummy")]
        public async Task<IActionResult> GetPlatformWellDummy(string token)
        {
            var client = new HttpClient();
            var url = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellDummy";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token );
            var res = await client.GetAsync(url);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                return Content(res.ToString());
            }

            var result = await res.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<List<Platform>>(result);
            var message = string.Empty;
            foreach (var j in json)
            {
                var existingPlatform = await _context.Platforms.Where(p => p.Id == j.Id).FirstOrDefaultAsync();
                if(existingPlatform == null)
                {
                    var plat = new Platform();
                    plat.Id = j.Id;
                    plat.UniqueName = j.UniqueName;
                    plat.Latitude = j.Latitude;
                    plat.Longitude = j.Longitude;
                    plat.CreatedAt = DateTime.Now;
                    _context.Platforms.Add(plat);
                    foreach (var f in j.well)
                    {
                        var well = new Well();
                        well.Id = f.Id;
                        well.UniqueName = f.UniqueName;
                        well.PlatformId = f.PlatformId;
                        well.Latitude = f.Latitude;
                        well.Longitude = f.Longitude;
                        well.CreatedAt = DateTime.Now;
                        _context.Wells.Add(well);
                        
                    }
                    await _context.SaveChangesAsync();
                    message="Successfully Created";
                }
                else
                {
                    existingPlatform.UniqueName = j.UniqueName;
                    existingPlatform.Latitude = j.Latitude;
                    existingPlatform.Longitude = j.Longitude;
                    existingPlatform.UpdatedAt = DateTime.Now;
                    _context.Platforms.Update(existingPlatform);
                    foreach (var f in j.well)
                    {
                        var well = new Well();
                        well.Id = f.Id;
                        well.UniqueName = f.UniqueName;
                        well.PlatformId = f.PlatformId;
                        well.Latitude = f.Latitude;
                        well.Longitude = f.Longitude;
                        well.CreatedAt = DateTime.Now;
                        well.UpdatedAt = DateTime.Now;
                        _context.Wells.Update(well);

                    }
                    await _context.SaveChangesAsync();
                    message = "Successfully Updated";
                }
            }
            return Ok(message);
        }

        [HttpGet("GetPlatformWellActual")]
        public async Task<IActionResult> GetPlatformWellActual(string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                throw new Exception("No Token is included.");
            }
            var client = new HttpClient();
            var url = "http://test-demo.aemenersol.com/api/PlatformWell/GetPlatformWellActual";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await client.GetAsync(url);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                return Content(res.ToString());
            }

            var result = await res.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<List<Platform>>(result);
            var message = string.Empty;
            foreach (var j in json)
            {
                var existingPlatform = await _context.Platforms.Where(p => p.Id == j.Id).FirstOrDefaultAsync();
                if (existingPlatform == null)
                {
                    var plat = new Platform();
                    plat.Id = j.Id;
                    plat.UniqueName = j.UniqueName;
                    plat.Latitude = j.Latitude;
                    plat.Longitude = j.Longitude;
                    plat.CreatedAt = j.CreatedAt;
                    plat.UpdatedAt = j.UpdatedAt;
                    _context.Platforms.Add(plat);
                    foreach (var f in j.well)
                    {
                        var well = new Well();
                        well.Id = f.Id;
                        well.UniqueName = f.UniqueName;
                        well.PlatformId = f.PlatformId;
                        well.Latitude = f.Latitude;
                        well.Longitude = f.Longitude;
                        well.CreatedAt = f.CreatedAt;
                        well.UpdatedAt = f.UpdatedAt;
                        _context.Wells.Add(well);

                    }
                    await _context.SaveChangesAsync();
                    message = "Successfully Created";
                }
                else
                {
                    existingPlatform.UniqueName = j.UniqueName;
                    existingPlatform.Latitude = j.Latitude;
                    existingPlatform.Longitude = j.Longitude;
                    existingPlatform.UpdatedAt = j.UpdatedAt;
                    existingPlatform.CreatedAt = j.CreatedAt;
                    _context.Platforms.Update(existingPlatform);
                    foreach (var f in j.well)
                    {
                        var well = new Well();
                        well.Id = f.Id;
                        well.UniqueName = f.UniqueName;
                        well.PlatformId = f.PlatformId;
                        well.Latitude = f.Latitude;
                        well.Longitude = f.Longitude;
                        well.CreatedAt = f.CreatedAt;
                        well.UpdatedAt = f.UpdatedAt;
                        _context.Wells.Update(well);

                    }
                    await _context.SaveChangesAsync();
                    message = "Successfully Updated";
                }
            }
            return Ok(message);
        }
    }
}
