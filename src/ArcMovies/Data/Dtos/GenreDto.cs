using Newtonsoft.Json;

namespace ArcMovies.Data.Dtos
{
    class GenreDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
