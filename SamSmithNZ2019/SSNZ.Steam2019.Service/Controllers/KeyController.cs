using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SSNZ.Steam2019.Service.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class KeyController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public KeyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string GetData()
        {
            string result = "Not Found";
            //AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

            //try
            //{
            //    var keyVaultClient = new KeyVaultClient(
            //        new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            //    var secret = await keyVaultClient.GetSecretAsync("https://keyvaultname.vault.azure.net/secrets/secret")
            //        .ConfigureAwait(false);

            //    ViewBag.Secret = $"Secret: { secret.Value}";

            //}
            //catch (Exception exp)
            //{
            //    ViewBag.Error = $"Something went wrong: {exp.Message}";
            //}
            result = _configuration["CacheConnection"];

            return result;
        }
    }
}