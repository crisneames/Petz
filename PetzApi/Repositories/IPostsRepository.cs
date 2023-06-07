using PetzApi.Models;

namespace PetzApi.Repositories
{
    public interface IPostsRepository
    {
        void Add(Posts post);
        void DeletePost(int id);
        List<Posts> GetAll();
        List<Posts> GetAllWithPets();
        Posts GetById(int id);
        List<Posts> GetPostsByUser(int Id);
        void Update(Posts post);
        void AddPetPosts(PetPosts petPosts);
    }
}