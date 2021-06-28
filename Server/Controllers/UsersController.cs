using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using DataStorage.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController
    {
        private readonly ApplicationContext _db;

        public UsersController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _db.Users.OrderBy(u => u.Id).ToListAsync();
        }

        [HttpPost]
        public async Task<IEnumerable<User>> PostAsync([FromBody] List<User> users)
        {
            _db.UpdateRange(users);
            await _db.SaveChangesAsync();
            return users;
        }
    }
}