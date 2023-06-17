using PetzApi.Models;

namespace PetzApi.Repositories
{
    public interface IPetsRepository
    {
        void Add(Pets pet);
        void Delete(int id);
        List<Pets> GetAllPets();
        Pets GetById(int id);
        void Update(Pets pet);
        List<Pets> GetPetsByUser(int UserId);
    }
}