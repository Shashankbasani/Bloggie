using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repository.Interface
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetTagAsync(Guid id);
        Task<Tag> AddTagAsync(Tag tag);
        Task<Tag?> UpdateTagAsync(Tag tag);
        Task<Tag?> DeleteTagAsync(Guid id);


    }
}
