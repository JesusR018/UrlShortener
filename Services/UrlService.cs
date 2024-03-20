using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using URL_Shortener.Data;

namespace URL_Shortener.Services
{
    public class UrlService
    {
        public readonly ApplicationDbContext _Db;
        public readonly HttpContext _httpContext;

        public UrlService(ApplicationDbContext db, HttpContext httpContext)
        {
            _Db = db;
            _httpContext = httpContext;
        }

        public string CreateShortUrl()
        {
            string shortUrl;
            string pathBase = _httpContext.Request.PathBase;

            do
            {
                shortUrl = pathBase + "/" + CreateCode();
            } while (!ShortUrlExists(shortUrl));

            return shortUrl;
        }

        public bool OriginalUrlExists(string originalUrl)
        {
            return _Db.Urls.Any(u => u.OriginalUrl == originalUrl);
        }

        public bool ShortUrlExists(string shortUrl)
        {
            return _Db.Urls.Any(u => u.ShortUrl == shortUrl);
        }

        private string CreateCode()
        {
            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Za-z0-9]{5}" });
            return randomizerTextRegex.Generate();
        }
    }
}
