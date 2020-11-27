using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Web.Models.MandMCounter;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class MandMCounterController : Controller
    {
        private readonly IMandMCounterServiceAPIClient _ServiceApiClient;
        private readonly IConfiguration _configuration;

        public MandMCounterController(IMandMCounterServiceAPIClient ServiceApiClient, IConfiguration configuration)
        {
            _ServiceApiClient = ServiceApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string volumeUnit = null, float? quantity = null, float? mandMResult = null, float? peanutMandMResult = null, float? skittlesResult = null)
        {
            List<string> unitsForVolume = await _ServiceApiClient.GetUnitsForVolume();
            List<string> unitsForContainer = await _ServiceApiClient.GetUnitsForContainer();
            IndexViewModel model = new IndexViewModel(unitsForVolume, unitsForContainer);
            if (volumeUnit != null)
            {
                model.VolumeUnit = volumeUnit;
            }
            if (quantity != null)
            {
                model.Quantity = (float)quantity;
            }
            if (mandMResult != null)
            {
                model.MandMResult = (float)mandMResult;
            }
            if (mandMResult != null)
            {
                model.PeanutMandMResult = (float)peanutMandMResult;
            }
            if (mandMResult != null)
            {
                model.SkittlesResult = (float)skittlesResult;
            }

            return View(model);
        }

        public async Task<IActionResult> RunVolumeCalculation(string VolumeUnit, string txtQuantity)
        {
            float mandMResult = 0f;
            float peanutMandMResult = 0f;
            float skittlesResult = 0f;
            float quantity;
            if (float.TryParse(txtQuantity, out quantity) == true)
            {
                mandMResult = await _ServiceApiClient.GetMandMDataForUnit(VolumeUnit, quantity);
                peanutMandMResult = await _ServiceApiClient.GetPeanutMandMDataForUnit(VolumeUnit, quantity);
                skittlesResult = await _ServiceApiClient.GetSkittlesDataForUnit(VolumeUnit, quantity);
            }

            return RedirectToAction("Index", new
            {
                volumeUnit = VolumeUnit,
                quantity = txtQuantity,
                mandMResult = mandMResult.ToString("0.0"),
                peanutMandMResult = peanutMandMResult.ToString("0.0"),
                skittlesResult = skittlesResult.ToString("0.0")
            });

        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }
    }
}