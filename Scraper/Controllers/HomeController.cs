using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scraper.Data;

namespace Scraper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            page = page ?? 1;
            SData data = new SData();
            return View(data.GetAll($"http://www.thelakewoodscoop.com/news/page/{page}"));
        }

        
    }
}