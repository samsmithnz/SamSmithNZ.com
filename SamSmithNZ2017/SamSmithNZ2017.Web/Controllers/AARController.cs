using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamSmithNZ2017.Controllers
{
    public class AARController : Controller
    {
        //
        // GET: /AAR/

        private bool _chapter1Enabled = true;
        private bool _chapter2Enabled = false;
        private bool _chapter3Enabled = false;
        private bool _chapter4Enabled = false;
        private bool _chapter5Enabled = false;
        private bool _chapter6Enabled = false;
        private bool _chapter7Enabled = false;
        private bool _chapter8Enabled = false;
        private bool _chapter9Enabled = false;

        public ActionResult Index()
        {
            return Redirect("http://samsmithnz.com/AAR/Xcom");
            //return RedirectToAction("XCOM", "AAR");
        }

        public ActionResult XCOMNoChapter()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMNoChapter");
            //return View();
        }

        public ActionResult XCOM()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOM");
            //
            //return View(new SamSmithNZ2015.Models.AAR.AARViewModel(this._chapter1Enabled,
            //                                                    this._chapter2Enabled,
            //                                                    this._chapter3Enabled,
            //                                                    this._chapter4Enabled,
            //                                                    this._chapter5Enabled,
            //                                                    this._chapter6Enabled,
            //                                                    this._chapter7Enabled,
            //                                                    this._chapter8Enabled,
            //                                                    this._chapter9Enabled));
        }

        public ActionResult XCOMCH1()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter1Enabled == true)
            //{
            //    return View(this._chapter2Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH2()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter2Enabled == true)
            //{
            //    return View(this._chapter3Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH3()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter3Enabled == true)
            //{
            //    return View(this._chapter4Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH4()
        {

            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter4Enabled == true)
            //{
            //    return View(this._chapter5Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH5()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter5Enabled == true)
            //{
            //    return View(this._chapter6Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH6()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter6Enabled == true)
            //{
            //    return View(this._chapter7Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH7()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter7Enabled == true)
            //{
            //    return View(this._chapter8Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH8()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter8Enabled == true)
            //{
            //    return View(this._chapter9Enabled);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

        public ActionResult XCOMCH9()
        {
            return Redirect("http://samsmithnz.com/AAR/XCOMCH1");
            //if (this._chapter9Enabled == true)
            //{
            //    return View(false);
            //}
            //else
            //{
            //    return RedirectToAction("XCOMNoChapter", "AAR");
            //}
        }

    }
}
