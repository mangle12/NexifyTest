using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NexifyTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BaseContext _baseContext;

        public UserController(BaseContext baseContext)
        {
            _baseContext = baseContext;
        }

        [HttpGet("UserList")]
        public async Task<List<UserInfos>> GetUserListAsync()
        {
            return await _baseContext.UserInfos.AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        [HttpGet("Add")]
        public async Task<List<UserInfos>> AddUserAsync()
        {
            var userInfos = await _baseContext.UserInfos.AsNoTracking()
                .ToListAsync();

            var newUser = new UserInfos()
            {
                Id = 0,
                Name = String.Empty,
                DateOfBirth = DateTime.Now.ToString("yyyy-MM-dd"),
                Salary = 0,
                Address = String.Empty,
            };

            userInfos.Insert(0, newUser);

            return userInfos;
        }

        [HttpPost("Save")]
        public async Task SaveUserAsync(List<UserInfos> req)
        {            
            var userInfos = await _baseContext.UserInfos.AsNoTracking().ToListAsync();

            _baseContext.UserInfos.RemoveRange(userInfos);
            await _baseContext.UserInfos.AddRangeAsync(req);

            await _baseContext.SaveChangesAsync();
        }
    }
}
