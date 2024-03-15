using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApi : ControllerBase
    {
        private readonly AuthService _authService;
        public TestApi()
        {
            _authService = new AuthService();
        }
        [HttpGet]
        public async Task<ActionResult<string>>Get(string access_token)
        {
            var r = await _authService.VerificationTokenAsync(access_token);
            //f4a46f0a-19d2-44b5-b33c-671b9d69f895 client

            return Ok(r);
        }
    }
}
