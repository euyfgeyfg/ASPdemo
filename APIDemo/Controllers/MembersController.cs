using System.Threading.Tasks;
using APIDemo.Data;
using APIDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext dbContext) : ControllerBase
    {
        [HttpGet]// localhsot:5001/api/members
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await dbContext.Users.ToListAsync();
            return members;
        }


        [HttpGet("{id}")]// localhsot:5001/api/members/bob-id
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await dbContext.Users.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }
            else
            {
                return member;
            }
        }

    }
}
