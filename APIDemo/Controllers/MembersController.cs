using APIDemo.Data;
using APIDemo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Controllers
{
    [Authorize]
    public class MembersController(AppDbContext dbContext) : BaseApiController
    {
        [HttpGet]// localhsot:5001/api/members
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await dbContext.Users.ToListAsync();
            return members;
        }

        // [AllowAnonymous]
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
