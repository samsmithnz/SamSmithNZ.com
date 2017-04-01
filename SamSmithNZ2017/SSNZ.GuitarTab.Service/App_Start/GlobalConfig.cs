using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SSNZ.GuitarTab.Service
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GlobalConfig
    {
        public static void CustomizeConfig(HttpConfiguration config)
        {
            //config.Filters.Add(new UnhandledExceptionFilter());

            // Remove Xml formatters. This means when we visit an endpoint from a browser,
            // Instead of returning Xml, it will return Json.
            // More information from Dave Ward: http://jpapa.me/P4vdx6
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}