using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;








namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Shorts()
        {
            return View();
        }
        public ActionResult Genre()
        {
            return View();
        }

        public ActionResult Directors()
        {
            return View();
        }

        public ActionResult Review()
        {
            return View();
        }

        /*

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

          */

        public async Task<ActionResult> Coupons()

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
                    return View("Coupons");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to load coupons. Please try again later.";
                return View("Error"); // An error view that shows the message


            }
        }




        public async Task<ActionResult> Rating(string id)
        {


            var movieId = string.IsNullOrEmpty(id) ? "Chicago" : id;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://movies-ratings2.p.rapidapi.com/ratings?id={movieId}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "f45a75ea1cmsh0dd1751d526138fp1f1ac1jsn1b2f727d5f7a" },
                    { "X-RapidAPI-Host", "movies-ratings2.p.rapidapi.com" },


                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    ViewBag.RatingData = body;
                    return View("Rating");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log the error, return an error view, etc.)
                ViewBag.ErrorMessage = "Failed to load rating data. Please try again later.";
                return View("Error");
            }
        }

        public async Task<ActionResult> Actors(string nconst = "nm0000151")
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb-com.p.rapidapi.com/actors/videos?nconst={nconst}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "f45a75ea1cmsh0dd1751d526138fp1f1ac1jsn1b2f727d5f7a" },
                    { "X-RapidAPI-Host", "imdb-com.p.rapidapi.com" },
                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    ViewBag.ActorData = body; // Assuming the response is in a format that your view can handle
                    return View("Actors"); // Make sure you have a corresponding View for ActorVideos
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to load actor videos. Please try again later.";
                return View("Error"); // Ensure you have an Error view to display this message
            }
        }

        public async Task<ActionResult> Top100()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
            {
                { "X-RapidAPI-Key", "f45a75ea1cmsh0dd1751d526138fp1f1ac1jsn1b2f727d5f7a" },
                { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
            },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    ViewBag.Top100Data = body; // Pass the data to the view
                    return View("Top100"); // Ensure you have a view named 'Top100'
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to load the top 100 movies. Please try again later.";
                return View("Error"); // Ensure you have an Error view
            }
        }


        public async Task<ActionResult> LookUp(string kw = "Chicago")
        {
            var moviekw = string.IsNullOrEmpty(kw) ? "Chicago" : kw;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://utelly-tv-shows-and-movies-availability-v1.p.rapidapi.com/lookup?term={moviekw}"),
                Headers =
        {
            { "X-RapidAPI-Key", "f45a75ea1cmsh0dd1751d526138fp1f1ac1jsn1b2f727d5f7a" },
            { "X-RapidAPI-Host", "utelly-tv-shows-and-movies-availability-v1.p.rapidapi.com" },
        },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    ViewBag.LookUpData = body;
                    return View("LookUp");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log the error, return an error view, etc.)
                ViewBag.ErrorMessage = $"Failed to load LookUp data. Error: {ex.Message}";
                return View("Error");
            }
        }

        public ActionResult ChatWithGPT()
        {
            return View();
        }



        private static readonly HttpClient _httpClient = new HttpClient();

      

        [HttpPost]
        public async Task<ActionResult> SendMessageToGPT4(string message)
        {
            var jsonPayload = JsonConvert.SerializeObject(new
            {
                messages = new[] { new { role = "user", content = message } },
                tone = "Balanced"
            });

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://chatgpt-42.p.rapidapi.com/gpt4")
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json"),
                Headers = {
                { "X-Rapidapi-Key", "f45a75ea1cmsh0dd1751d526138fp1f1ac1jsn1b2f727d5f7a" },
                { "X-Rapidapi-Host", "chatgpt-42.p.rapidapi.com" }
            }
            };

            try
            {
                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return Content(responseBody, "application/json");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(new { error = $"Failed to send message. Error: {ex.Message}" });
            }
        }





































    }
}
