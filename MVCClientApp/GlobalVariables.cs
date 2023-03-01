using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MVCClientApp
{
    public class GlobalVariables
    {
    
        public static HttpClient WebApi = new HttpClient();

        // A constructor that executes everytime when the application is run. This is to get the data we want for the client app.
        static GlobalVariables() 
        {
            // API URI
            WebApi.BaseAddress = new Uri("http://localhost:52575/Api/");
            WebApi.DefaultRequestHeaders.Clear(); // clearing default headers for every request
            WebApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // stating what type of data this application expects from the API in the response
        }
    }
}