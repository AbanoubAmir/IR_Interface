using IR_Interface.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Query = Query.ToLower();
            results.Query = Query;            
            Module3 module3 = new Module3();
            char[] delimiters = new char[] { '\r', '\n', ' ', ',' };
            if(Query.StartsWith("\"") && Query.EndsWith("\""))
            {
                results.Search = "Exact";
                results.ExactSearch = module3.ExactSearch(Query.Substring(1,Query.Length-1));
            }
            else if(soundex==false && Query!="")
            {
                results.Search = "Multiple";
                results.MultipleSearch = module3.multipleWordSearch(Query);
            }
            if(soundex)
            {
                results.Search = "Soundex";
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
        public ActionResult About(string url="")
        {
            string connectionString = "Data Source=ABANOUB\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            string command = "select [Content] from Documents where URL=@parm1";
            SqlCommand cmd = new SqlCommand(command, connection);
            connection.Open();
            SqlParameter par1 = new SqlParameter("@parm1", url);
            cmd.Parameters.Add(par1);
            string Content="";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Content = reader.GetString(0);
                }
            }
            connection.Close();
            ViewBag.Content = Content;
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}