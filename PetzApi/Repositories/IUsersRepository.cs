using PetzApi.Models;

namespace PetzApi.Repositories
{
    public interface IUsersRepository
    {
        void Add(Users user);
        void Delete(int id);
        List<Users> GetAllUsers();
        Users GetById(int id);
        void Update(Users user);
    }
}