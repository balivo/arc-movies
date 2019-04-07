using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArcMovies.Clients
{
    class TmdbHttpClient
    {
        static Lazy<TmdbHttpClient> _Lazy = new Lazy<TmdbHttpClient>(() => new TmdbHttpClient());

        public static TmdbHttpClient Current { get => _Lazy.Value; }

        readonly HttpClient _HttpClient;

        TmdbHttpClient()
        {
            _HttpClient = new HttpClient
            {
                BaseAddress = new Uri(TmdbConstants.BaseUrl)
            };

            _HttpClient.DefaultRequestHeaders.ConnectionClose = false;
        }

        public async Task<TmdbGenresResult> GetGenres()
        {
            using (var response = await _HttpClient.GetAsync(TmdbConstants.GenreUrl).ConfigureAwait(false))
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TmdbGenresResult>(content);
            }
        }

        public async Task<TmdbMoviesResult> GetMovies(string query = null, int page = 1)
        {
            string url = null;

            if (string.IsNullOrWhiteSpace(query))
                url = string.Format(TmdbConstants.MovieUpcomingUrl, page);
            else
                url = string.Format(TmdbConstants.SearchUrl, query, page);

            using (var response = await _HttpClient.GetAsync(url).ConfigureAwait(false))
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TmdbMoviesResult>(content);
            }
        }
    }
}