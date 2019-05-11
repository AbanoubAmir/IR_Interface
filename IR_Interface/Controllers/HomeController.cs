using IR_Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IR_Interface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Results results = new Results();
            return View(results);
        }
        [HttpPost]
        public ActionResult Index(string Query, bool soundex, bool spelling)
        {
            Results results = new Results();
            results.Query = Query;            
            Module3 module3 = new Module3();
            char[] delimiters = new char[] { '\r', '\n', ' ', ',' };
            if(soundex)
            {
                results.Search = true;
                string[] words = Query.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 1).ToArray();
                results.SoundexSearch = module3.SoundexSearch(words);
            }
            if(spelling)
            {
                results.Spelling = true;
                string[] words = Query.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 1).ToArray();
                results.SpellCheck = module3.SpellCheck(words);
            }
            return View(results);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}