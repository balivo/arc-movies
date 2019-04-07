using ArcMovies.Data.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArcMovies.Clients
{
    sealed class TmdbGenresResult
    {
        [JsonProperty("genres")]
        public List<GenreDto> Genres { get; set; }
    }
}
