using CouchPotato.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CouchPotato.Helper
{
    public class OmdbApiHelper
    {
        readonly string baseUri = "http://www.omdbapi.com";

        //Gets movie details from Omdb Api by MovieName
        public async Task<OmdbApi> GetMovieDetailsFromApi(string moviename)
        {
            OmdbApi omdbApi = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("?apikey=f2625f26&t=" + moviename);
                if (response.IsSuccessStatusCode)
                {
                    omdbApi = await response.Content.ReadAsAsync<OmdbApi>();
                }
            }
            return omdbApi;
        }
    }

}