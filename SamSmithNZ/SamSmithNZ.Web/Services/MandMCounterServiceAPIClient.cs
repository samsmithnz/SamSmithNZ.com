using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.FooFighters;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class MandMCounterServiceAPIClient : BaseServiceAPIClient, IMandMCounterServiceAPIClient
    {
        private readonly IConfiguration _configuration;

        public MandMCounterServiceAPIClient(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(_configuration["AppSettings:MandMWebServiceURL"])
            };
            base.SetupClient(client);
        }

        public async Task<float> GetMandMDataForUnit(string unit, float quantity)
        {
            Uri url = new Uri($"api/MandMCounter/GetDataForUnit?unit=" + unit + "&quantity=" + quantity, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetMandMDataForRectangle(string unit, float height, float width, float length)
        {
            Uri url = new Uri($"api/MandMCounter/GetDataForRectangle?unit=" + unit + "&height=" + height + "&width=" + width + "&length=" + length, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetMandMDataForCylinder(string unit, float height, float radius)
        {
            Uri url = new Uri($"api/MandMCounter/GetDataForCylinder?unit=" + unit + "&height=" + height + "&radius=" + radius, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }


        public async Task<float> GetPeanutMandMDataForUnit(string unit, float quantity)
        {
            Uri url = new Uri($"api/PeanutMandMCounter/GetDataForUnit?unit=" + unit + "&quantity=" + quantity, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetPeanutMandMDataForRectangle(string unit, float height, float width, float length)
        {
            Uri url = new Uri($"api/PeanutMandMCounter/GetDataForRectangle?unit=" + unit + "&height=" + height + "&width=" + width + "&length=" + length, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetPeanutMandMDataForCylinder(string unit, float height, float radius)
        {
            Uri url = new Uri($"api/PeanutMandMCounter/GetDataForCylinder?unit=" + unit + "&height=" + height + "&radius=" + radius, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }


        public async Task<float> GetSkittlesDataForUnit(string unit, float quantity)
        {
            Uri url = new Uri($"api/SkittleCounter/GetDataForUnit?unit=" + unit + "&quantity=" + quantity, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetSkittlesDataForRectangle(string unit, float height, float width, float length)
        {
            Uri url = new Uri($"api/SkittleCounter/GetDataForRectangle?unit=" + unit + "&height=" + height + "&width=" + width + "&length=" + length, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetSkittlesDataForCylinder(string unit, float height, float radius)
        {
            Uri url = new Uri($"api/SkittleCounter/GetDataForCylinder?unit=" + unit + "&height=" + height + "&radius=" + radius, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<List<string>> GetUnitsForVolume()
        {
            Uri url = new Uri($"api/Units/GetUnitsForVolume", UriKind.Relative);
            List<string> results = await base.ReadMessageList<string>(url);
            if (results == null)
            {
                return new List<string>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<string>> GetUnitsForContainer()
        {
            Uri url = new Uri($"api/Units/GetUnitsForContainer", UriKind.Relative);
            List<string> results = await base.ReadMessageList<string>(url);
            if (results == null)
            {
                return new List<string>();
            }
            else
            {
                return results;
            }
        }

    }
}
