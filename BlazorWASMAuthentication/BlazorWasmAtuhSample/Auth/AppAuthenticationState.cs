using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorWasmAtuhSample.Auth
{
    public class AppAuthenticationState : AuthenticationStateProvider
    {
        private AppUser _user;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //AuthenticationState สร้างจาก ClaimsPrincipal

            if (_user == null)
            {
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

            }
            else
            {

                //สร้าง identity Claims ถ้าใช้ ค่า Token จาก Rest Service ก็ inject service เข้ามาใน constructor ได้ ตัวอย่างนี้เป็นเพียงจาก Hard coding เพื่อเป็นแนวทางเท่านั้น!
                var identity = new ClaimsIdentity();

                var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
                                new Claim(ClaimTypes.Name, _user.UserName),
                                new Claim(ClaimTypes.GivenName, _user.UserName)
                            };

                identity = new ClaimsIdentity(claims, "Authentication ja");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }

        public void UpdateAuthenticationState(AppUser loginUser)
        {
            _user = loginUser;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
