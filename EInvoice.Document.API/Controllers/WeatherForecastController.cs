using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Duende.IdentityServer.Extensions;
using System.Security.Claims;
using EInvoice.Document.Application.Common.Interfaces;
using EInvoice.Document.Application.Interfaces;

namespace EInvoice.Document.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEInvoiceDocumentClientService _einvoiceDocuemtnClientService;
        private readonly IAccountService _accountService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEInvoiceDocumentClientService einvoiceDocuemtnClientService, IAccountService accountService)
        {
            _logger = logger;
            _einvoiceDocuemtnClientService = einvoiceDocuemtnClientService;
            _accountService = accountService;
           
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [AllowAnonymous]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("GetDocmentTestWithGoogle")]
        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetDocmentTestWithGoogle()
        {
            var result = await _einvoiceDocuemtnClientService.GetGoogleMe();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        //Authentication

        [HttpGet]
        [AllowAnonymous]
        [Route("external-login-redirect")]
        public async Task<IActionResult> ExternalLoginRedirect()
        {
            await _accountService.SignOutAsync();
            string redirectUrl = Url.Action(nameof(this.ExternalLogin), "WeatherForecast");
            Console.WriteLine("==== REDIRECT URL: " + redirectUrl);

            AuthenticationProperties properties = _accountService.ConfigureExternalLoginPropertiesForRedirect(GoogleDefaults.DisplayName, redirectUrl);
            properties.AllowRefresh = true;

            ChallengeResult challengeResult = new ChallengeResult(GoogleDefaults.AuthenticationScheme, properties);

            Console.WriteLine("\n" + challengeResult.ToString());
            Console.WriteLine("- Challenge Result Properties");
            foreach (var s in challengeResult.Properties.Items)
            {
                Console.WriteLine(" + Key: " + s.Key + " -- Value: " + s.Value);
            }

            return challengeResult;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("signin-google-retrieve")]
        public async Task<IActionResult> ExternalLogin()
        {
            //Redirect to google authorization
            await _accountService.ExternalLoginRetrieveInfo();

            return Ok("Success");
        }

        [HttpGet]
        [Route("log-out")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();

            return Ok("Success");
        }
    }


}
