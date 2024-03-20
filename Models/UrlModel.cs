using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.Models
{
    [Index(nameof(OriginalUrl), IsUnique = true)]
    public class UrlModel
    {
        [Key]
        public int Id { get; set; }

        [Url]
        public string OriginalUrl { get; set; }

        [Url]
        public string ShortUrl { get; set; }
        public int ClickCounter { get; set; }

        public UrlModel() 
        {
        }

        public UrlModel(string originalUrl, string shortUrl)
        {
            OriginalUrl = originalUrl;
            ShortUrl = shortUrl;
            ClickCounter = 0;
        }
    }
}
