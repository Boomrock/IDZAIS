using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AuthDBContext _authDBContext;

        public TokenController(AuthDBContext authDBContext)
        {
            _authDBContext = authDBContext;
        }
        [HttpGet("{access_token}")]
        public ActionResult<bool> CheckAccessToken(string access_token)
        {
            if (Guid.TryParse(access_token, out var token))
            {
                var dbToken = _authDBContext.AccessTokens.Find(token);
                if (dbToken == null)
                {
                    return false;
                }
                return true;
            }

            return false;
        }
    }
}
