using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using MandMCounter.Core;

namespace SamSmithNZ.Service.Controllers.MandMCounter
{
    [Produces("application/json")]
    [Route("api/MandMCounter")]
    public class MandMCounterController : Controller
    {
        [HttpGet("GetDataForUnit")]
        public float GetDataForUnit(string unit, float quantity)
        {
            //Calculator calc = new Calculator();
            //return calc.CountMandMs(unit, quantity);
            return 0f;
        }

        [HttpGet("GetDataForRectangle")]
        public float GetDataForRectangle(string unit, float height, float width, float length)
        {
            //Calculator calc = new Calculator();
            //return calc.CountMandMs(unit, height, width, length);
            return 0f;
        }

        [HttpGet("GetDataForCylinder")]
        public float GetDataForCylinder(string unit, float height, float radius)
        {
            //Calculator calc = new Calculator();
            //return calc.CountMandMs(unit, height, radius);
            return 0f;
        }
    }
}
