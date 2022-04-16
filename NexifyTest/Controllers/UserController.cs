using Microsoft.AspNetCore.Mvc;

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
            return await _baseContext.UserInfos.ToListAsync();
        }

        [HttpGet("Add")]
        public async Task<List<UserInfos>> AddUserAsync()
        {
            var userInfos = await _baseContext.UserInfos.ToListAsync();

            userInfos.Add(new UserInfos
            {
                Id = Guid.NewGuid(),
                Name = String.Empty,
                DateOfBirth = DateTime.Now.ToString("yyyy-MM-dd"),
                Salary = 0,
                Address = String.Empty,
            });

            return userInfos;
        }
    }
}
