using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
     
        public  ActionResult Index()
        {
            return  View();
        }

        [HttpPost]
        public async Task<ActionResult> ArticlesList(VerifyMainSearchInputViewModel searchingString)
        {
            if (ModelState.IsValid)
            {

                var table = new VerifyMainSearchInputViewModel();
                string url =   "https://api.nytimes.com/svc/search/v2/articlesearch.json?q=" + searchingString.inputView + "&api-key=iV0re7Z4zTAccdORNq8JEANgkqntbE5A";
                      using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));

                    System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        table = JsonConvert.DeserializeObject<VerifyMainSearchInputViewModel>(data);
                    }
                }

                return PartialView("_Articles", table);
            }

            return View("Error.cshml");
        }
    }
}