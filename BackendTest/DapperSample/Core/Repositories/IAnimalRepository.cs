using STGenetics.Core.Entities;
using System.Threading.Tasks;

namespace STGenetics.Core.Repositories
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Task<Animal> GetById(int id);
        Task<Animal> GetByFilter(string filter, string value);
        Task<AnimalSexByQuantity> AnimalSexByQuantity();
        Task<Animal> GetByOlderThanTwoYearsAndFemale();
        Task<int> Create(Animal animal);
        Task<int> Update(Animal animal);
        Task<int> Delete(int id);
    }
}
