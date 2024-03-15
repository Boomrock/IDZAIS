using AuthApi.Models;
using AuthApi.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace AuthApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly double TokenExpirationDate = 10;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly AuthDBContext _authDBContext;

        public AuthController(AuthDBContext authDBContext)
        {
            _authDBContext = authDBContext;
        }

        public ActionResult Index(string client_id, string redirect_uri)
        {
            if(client_id == null)
            {
                return NotFound();
            }

            var clientGuid = Guid.Parse(client_id);
            var client = _authDBContext.Clients.Find(clientGuid);
            if(client == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(UserVM userVM, string client_id, string redirect_uri)
        {
            if (ModelState.IsValid)
            {
                var clientGuid = Guid.Parse(client_id);
                var client = _authDBContext.Clients.Find(clientGuid);
                if (client == null)
                {
                    return NotFound();
                }
                var user = new User();

                user.LoginHash = SHA1.HashData(_encoding.GetBytes(userVM.Login)).ToString();
                user.PasswordHash = SHA1.HashData(_encoding.GetBytes(userVM.Password)).ToString();

                user = _authDBContext.Users.FirstOrDefault(u => u.LoginHash == user.LoginHash && u.PasswordHash == user.PasswordHash);


                if (user == null)
                {
                    return View();
                }

                var accessToken = new AccessToken();
                accessToken.Id = Guid.NewGuid();
                accessToken.TokenExpirationDate = DateTime.Now.AddMinutes(TokenExpirationDate);
                _authDBContext.AccessTokens.Add(accessToken);
                _authDBContext.SaveChanges();

                var uriBuilder = new StringBuilder();
                if (redirect_uri.Contains("?"))
                {
                    uriBuilder.Append(redirect_uri);
                    uriBuilder.Append("&");
                    uriBuilder.Append("access_token=");
                    uriBuilder.Append(accessToken.Id.ToString());
                }
                else
                {
                    uriBuilder.Append(redirect_uri);
                    uriBuilder.Append("?");
                    uriBuilder.Append("access_token=");
                    uriBuilder.Append(accessToken.Id.ToString());
                }

                return Redirect(uriBuilder.ToString());

            }

            return View();
        }

        public string RegisterClient()
        {
            var clientGUID = Guid.NewGuid();
            var client = new Client();
            client.Guid = clientGUID;
            client.TokenExpirationDate = DateTime.Now.AddHours(1);
            _authDBContext.Clients.Add(client);
            _authDBContext.SaveChanges();
            return clientGUID.ToString();
        }

        public ActionResult RegisterUser(string login, string password)
        {
            if(login == null || password == null)
            {
                return NotFound();
            }
            var user = new User();

            user.LoginHash = SHA1.HashData(_encoding.GetBytes(login)).ToString();
            user.PasswordHash = SHA1.HashData(_encoding.GetBytes(password)).ToString();

            var dbUser = _authDBContext.Users.FirstOrDefault(u => u.LoginHash == user.LoginHash && u.PasswordHash == user.PasswordHash);
            if (dbUser != null)
            {
                return NotFound();
            }

            user.Id = Guid.NewGuid();

            _authDBContext.Add(user);
            _authDBContext.SaveChanges();

            return new JsonResult(new { login, password });
        }



    }
}
