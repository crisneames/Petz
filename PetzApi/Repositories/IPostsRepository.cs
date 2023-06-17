using PetzApi.Models;

namespace PetzApi.Repositories
{
    public interface IPostsRepository
    {
        void Add(Posts post);
        void DeletePost(int id);
        List<Posts> GetAll();
        Posts GetPostWithPets(int postId);
        Posts GetById(int id);
        List<Posts> GetPostsByUser(int Id);
        void Update(Posts post);
        void AddPetPosts(PetPosts petPosts);
        List<Posts> GetAllPostsWithPets();
        void DeletePostWithPets(int id);
    }
}