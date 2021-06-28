using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStorage.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController
    {
        private readonly ApplicationContext _db;

        public MetricsController(ApplicationContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Returns Rolling Retention X day 
        /// </summary>
        [HttpGet("rolling/{x}")]
        public async Task<double> GetRollingAsync(uint x)
        {
            var fromDate = DateTime.Today - TimeSpan.FromDays(x);

            var allBeforeDate = await _db.Users.CountAsync(u => u.DateRegistration <= fromDate);
            var activeAfterDate = await _db.Users.CountAsync(u =>
                u.DateLastActivity >= fromDate && u.DateRegistration <= fromDate);

            if (allBeforeDate == 0)
                return 0;
            
            return 100.0 * activeAfterDate / allBeforeDate;
        }

        public record LifeDuration
        {
            public int? UserId { get; set; }
            public string Duration { get; set; }
        }

        [HttpGet("lifedurations")]
        public async Task<IEnumerable<LifeDuration>> GetLifeDurationsAsync()
        {
            var users = await _db.Users.ToListAsync();
            return users.Select(u => new LifeDuration()
            {
                UserId = u.Id,
                Duration = (u.DateLastActivity - u.DateRegistration).ToString()
            });
        }
        
    }
}