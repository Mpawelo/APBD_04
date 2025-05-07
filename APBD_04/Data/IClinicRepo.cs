using APBD_04.Models;

namespace APBD_04.Data;

public interface IClinicRepository
{
    IEnumerable<Animal> GetAllAnimals();
    Animal? GetAnimalById(int id);
    IEnumerable<Animal> FindAnimalsByName(string name);
    int AddAnimal(AnimalDTO animalDto);
    bool UpdateAnimal(int id, AnimalDTO animalDto);
    bool DeleteAnimal(int id);
    IEnumerable<Visit> GetAnimalVisits(int animalId);
    int AddVisit(int animalId, VisitDTO visitDto);
}
