using DogGo.Models;

namespace DogGo.Repositories
{
    public interface IOwnerRepository
    {
        List<Owner> getAllOwners();
        Owner getOwnerById(int id);
    }
}
