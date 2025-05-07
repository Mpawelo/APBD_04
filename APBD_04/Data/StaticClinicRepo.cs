using APBD_04.Models;

namespace APBD_04.Data;

public class StaticClinicRepository : IClinicRepository
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal
        {
            Id = 1,
            Name = "Rex",
            Category = "Pies",
            Weight = 25.5,
            FurColor = "Brązowy"
        },
        new Animal
        {
            Id = 2,
            Name = "Mruczek",
            Category = "Kot",
            Weight = 4.2,
            FurColor = "Czarny"
        }
    };

    private static readonly List<Visit> _visits = new()
    {
        new Visit
        {
            Id = 1,
            AnimalId = 1,
            VisitDate = DateTime.Now.AddDays(-10),
            Description = "Szczepienie przeciw wściekliźnie",
            Price = 150.00m
        },
        new Visit
        {
            Id = 2,
            AnimalId = 2,
            VisitDate = DateTime.Now.AddDays(-5),
            Description = "Kontrola zdrowia",
            Price = 100.00m
        }
    };

    private static int _nextAnimalId = 3;
    private static int _nextVisitId = 3;

    public IEnumerable<Animal> GetAllAnimals()
    {
        return _animals;
    }

    public Animal? GetAnimalById(int id)
    {
        return _animals.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Animal> FindAnimalsByName(string name)
    {
        return _animals.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public int AddAnimal(AnimalDTO animalDto)
    {
        var animal = new Animal
        {
            Id = _nextAnimalId++,
            Name = animalDto.Name,
            Category = animalDto.Category,
            Weight = animalDto.Weight,
            FurColor = animalDto.FurColor
        };

        _animals.Add(animal);
        return animal.Id;
    }

    public bool UpdateAnimal(int id, AnimalDTO animalDto)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
            return false;

        animal.Name = animalDto.Name;
        animal.Category = animalDto.Category;
        animal.Weight = animalDto.Weight;
        animal.FurColor = animalDto.FurColor;
        
        return true;
    }

    public bool DeleteAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
            return false;

        _animals.Remove(animal);
        return true;
    }

    public IEnumerable<Visit> GetAnimalVisits(int animalId)
    {
        return _visits.Where(v => v.AnimalId == animalId);
    }

    public int AddVisit(int animalId, VisitDTO visitDto)
    {
        if (!_animals.Any(a => a.Id == animalId))
            return -1;

        var visit = new Visit
        {
            Id = _nextVisitId++,
            AnimalId = animalId,
            VisitDate = visitDto.VisitDate,
            Description = visitDto.Description,
            Price = visitDto.Price
        };

        _visits.Add(visit);
        return visit.Id;
    }
}
