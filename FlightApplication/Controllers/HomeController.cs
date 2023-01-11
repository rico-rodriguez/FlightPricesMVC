using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using FlightApplication.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace FlightApplication.Controllers;

public class HomeController : Controller
{
[System.Web.Mvc.HttpGet]
    public async Task<ActionResult> Index(FlightSearchModel searchModel)
    {

        if (HttpContext.Request.Method == "POST")
        {
            DateTime departureDate = DateTime.ParseExact(searchModel.DepartureDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime returnDate = DateTime.ParseExact(searchModel.ReturnDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            string formattedDepartureDate = departureDate.ToString("yyyy-MM-dd");
            string formattedReturnDate = returnDate.ToString("yyyy-MM-dd");

            string apiUrl = "https://api.tequila.kiwi.com/v2/search?fly_from=" + 
                            searchModel.Origin + "&fly_to=" + searchModel.Destination + 
                            "&dateFrom=" + formattedDepartureDate + "&dateTo=" + formattedReturnDate;
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.tequila.kiwi.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("apikey", "8JhlCuLlFr8NoPTC6YuEeaYUJbfOtMGw");

            // Send GET request to API
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsondata = await response.Content.ReadAsStringAsync();
                try
                {
                    // Deserialize JSON data into a list of Flight objects
                    var flightsResponse = JsonConvert.DeserializeObject<FlightsResponse>(jsondata);
                    if (flightsResponse != null) ViewBag.Flights = flightsResponse;
                    Debug.WriteLine("Api finished");
                    return PartialView("_ResultsTable");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                // Set flights as a property of the ViewBag

            }

        }
        return View(searchModel);
    }
    public IActionResult Privacy()
    {
        throw new NotImplementedException();
    }
}
