using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.DevOps.Web.Models;

namespace SSNZ.DevOps.Web.Controllers
{
    public class DevOpsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Intro()
        {
            return View();
        }

        public ActionResult CI()
        {
            return View();
        }

        public ActionResult CICurrentBuildServers()
        {
            return View();
        }

        public ActionResult CISetupBuild()
        {
            return View();
        }

        public ActionResult CINewBuildServer()
        {
            return View();
        }

        public ActionResult CINewTestServer()
        {
            return View();
        }

        public ActionResult CICodeCoverage()
        {
            return View();
        }

        public ActionResult CD()
        {
            return View();
        }

        public ActionResult CDSetupNewRelease()
        {
            return View();
        }

        public ActionResult CDSetupDeploymentServer()
        {
            return View();
        }

        public ActionResult CDSetupDeploymentToIIS()
        {
            return View();
        }

        public ActionResult CDSetupDeploymentToSQL()
        {
            return View();
        }

        public ActionResult CDSetupDeploymentOfWindowsService()
        {
            return View();
        }

        public ActionResult CDSetupQAandProductionReleases()
        {
            return View();
        }

        public ActionResult CDPlanning()
        {
            return View();
        }

        public ActionResult TFS2015Parameters()
        {
            return View();
        }

        public ActionResult Code()
        {
            return View();
        }

        public ActionResult CodeBranching()
        {
            return View();
        }

        public ActionResult CodeBranchingPolicies()
        {
            return View();
        }

        public ActionResult CodeBranchingConflicts()
        {
            return View();
        }

        public ActionResult CodePullRequests()
        {
            return View();
        }

        public ActionResult CodeLinkingWorkItems()
        {
            return View();
        }

        public ActionResult CodeReviews()
        {
            return View();
        }
        public ActionResult Monitor()
        {
            return View();
        }

        public ActionResult MonitorAppInsightsSetup()
        {
            return View();
        }

        public ActionResult MonitorDashboards()
        {
            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }
    }
}
