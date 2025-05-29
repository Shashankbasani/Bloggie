using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModel;
using Bloggie.web.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repository
{
    public class TagRepositiry : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepositiry(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddTagAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);

            await bloggieDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteTagAsync(Guid id)
        {
            var tag = await bloggieDbContext.Tags.FindAsync(id);
            if (tag != null)
            {

                bloggieDbContext.Tags.Remove(tag);
                await bloggieDbContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var listOfTags = await bloggieDbContext.Tags.ToListAsync();
            return listOfTags;
        }

        public async Task<Tag> GetTagAsync(Guid id)
        {
            return await bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
