using Azure.Core;
using EInvoice.Document.Application.Common.Interfaces;
using EInvoice.Document.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EInvoice.Document.API.Services
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        public CurrentUser(IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;

        }
        public string? UserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        

        public Task<string> AccessToken
        {
            get
            {
               return this.GetAccessToken();
            }
        }
  
        private async Task<string> GetAccessToken()
        {
            if(string.IsNullOrEmpty(UserId))
            {
                return "";
            }           
            return await _accountService.GetUserAccessTokenAsync(UserId);
        }


    }
}
