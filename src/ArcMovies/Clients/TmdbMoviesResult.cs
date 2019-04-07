using ArcMovies.Data.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArcMovies.Clients
{
    sealed class TmdbMoviesResult
    {
        [JsonProperty("page")]
        public int Page { get; set; } = 0;

        [JsonProperty("total_pages")]
        public int Pages { get; set; }

        [JsonProperty("results")]
        public List<MovieDto> Movies { get; set; }
    }
}
