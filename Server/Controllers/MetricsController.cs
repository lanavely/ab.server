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
            var allMatch = await _db.Users.CountAsync(u => u.DateLastActivity - u.DateRegistration >= TimeSpan.FromDays(x));
            var all = await _db.Users.CountAsync();

            if (all == 0)
                return 0;
            
            return 100.0 * allMatch / all;
        }

        public record LifeDuration
        {
            public int Value { get; set; }
            public int Count { get; set; }
        }

        [HttpGet("lifedurations")]
        public async Task<IEnumerable<LifeDuration>> GetLifeDurationsAsync()
        {
            return (await _db.Users
                .GroupBy(p => (p.DateLastActivity - p.DateRegistration).Days)
                .Select(g => new {Value = g.Key, Count = g.Count()} )
                .ToListAsync()).Select(i => new LifeDuration{Value = i.Value, Count = i.Count});
        }
        
    }
}