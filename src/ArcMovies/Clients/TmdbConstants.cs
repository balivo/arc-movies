namespace ArcMovies.Clients
{
    class TmdbConstants
    {
        public const string BaseUrl = "https://api.themoviedb.org";

        const string ApiVersion = "/3";

        const string Language = "pt-BR";

        const string Movie = "/movie";

        const string ApiKey = "?api_key=1f54bd990f1cdfb230adb312546d765d";

        const string Upcoming = "/upcoming";

        const string Search = "/search";

        public const string MoviePosterPrefix = "https://image.tmdb.org/t/p/original";

        const string Genre = "/genre";

        public const string MovieUpcomingUrl = ApiVersion + Movie + Upcoming + ApiKey + "&language=" + Language + "&page={0}";

        public const string GenreUrl = ApiVersion + Genre + Movie + "/list" + ApiKey + "&language=" + Language;

        public const string SearchUrl = ApiVersion + Search + Movie + ApiKey + "&language=" + Language + "&query={0}&page={1}";
    }
}
