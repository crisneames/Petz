using PetzApi.Models;

namespace PetzApi.Repositories
{
    public interface IPostsRepository
    {
        List<Posts> GetPostsByUser(int Id);
    }
}