using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using URL_Shortener.Data;
using URL_Shortener.Models;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers
{
    public class UrlController : Controller
    {
        public readonly ApplicationDbContext _Db;

        public UrlController(ApplicationDbContext db)
        {
           _Db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Index(string id)
        {
            string thisUrl = HttpContext.Request.Scheme + HttpContext.Request.Host + HttpContext.Request.Path;

            var urlToRedirect = _Db.Urls.FirstOrDefault(u => u.ShortUrl.EndsWith(id));
            if (urlToRedirect == null)
            {
                ModelState.AddModelError(string.Empty, "No URL has been found");
                return View("Error");
            }
            urlToRedirect.ClickCounter++;
            _Db.SaveChanges();
            return Redirect(urlToRedirect.OriginalUrl);
        }

        // Get
        public IActionResult AllUrls()
        {
            List<UrlModel> urls = _Db.Urls.ToList();

            return View(urls);
        }

        // Create 
        [HttpPost]
        public IActionResult Shorten(string originalUrl)
        {
            UrlService _urlService = new UrlService(_Db, HttpContext);

            UrlModel newUrl = null;
            string shortUrl = _urlService.CreateShortUrl();
            
            if(_urlService.OriginalUrlExists(originalUrl))
            {
                newUrl = _Db.Urls.FirstOrDefault(u => u.OriginalUrl == originalUrl);
            } else
            {

                try
                {
                    newUrl = new UrlModel(originalUrl, shortUrl);
                    ValidationContext validationContext = new ValidationContext(newUrl);
                    Validator.ValidateObject(newUrl, validationContext, true);
                    _Db.Urls.Add(newUrl);
                    _Db.SaveChanges();
                } catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Invalid URL");
                }
            }

            if(newUrl != null && ModelState.IsValid)
            {
                return View("Views/Url/Index.cshtml", newUrl);

			}

            return View("Views/Url/Index.cshtml");
        }
    }
}
