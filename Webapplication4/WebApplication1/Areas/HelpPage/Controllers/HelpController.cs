using System;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Areas.HelpPage.ModelDescriptions;
using WebApplication1.Areas.HelpPage.Models;
using System.Net.Http;
using System.Threading.Tasks;




namespace WebApplication1.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

      
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        
        public ActionResult ApiList()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }
       

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }






        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }


        public async Task<ActionResult> CouponApi()

        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://get-promo-codes.p.rapidapi.com/data/get-coupons/?page=1&sort=update_time_desc"),
                Headers =
        {
            { "X-RapidAPI-Key", "f45a75ea1cmsh0dd1751d526138fp1f1ac1jsn1b2f727d5f7a" },
            { "X-RapidAPI-Host", "get-promo-codes.p.rapidapi.com" },
        },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    ViewBag.CouponData = body; // Assuming the response is in a format that your view can handle
                    return View("CouponApiView"); // Replace with the name of your Razor view
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (log the error, return an error view, etc.)
                return View(ErrorViewName); // Or a view that displays the error
            }
        }










    }
}