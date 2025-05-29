using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModel;
using Bloggie.web.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository repo;

        public AdminTagController(ITagRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];

            // mapping AddTagRequest to domain model
            var tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

              await repo.AddTagAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await repo.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await repo.GetTagAsync(id);
            if (tag != null) {

                var editTagRequest = new EditTagRequest()
                {
                    Id = id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag()
            {
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
                Id = editTagRequest.Id
            };

            var existingTag = await repo.UpdateTagAsync(tag);

            if (existingTag != null)
            {
                return RedirectToAction("List");
            }

            return View(editTagRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {
            var tag = await repo.DeleteTagAsync(editTagRequest.Id);
            if (tag != null) { 

                //show success notification
                return RedirectToAction("List");
            }

            //show error notification
            return RedirectToAction("Edit", editTagRequest);
        }
    }
}
