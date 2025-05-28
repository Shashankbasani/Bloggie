using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];

            // mapping AddTagRequest to domain model
            var tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            bloggieDbContext.Tags.Add(tag);

            bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var tags = bloggieDbContext.Tags.ToList();

            return View(tags);
        }
    }
}
