using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

var fileContent = await File.ReadAllTextAsync("data.json");
var cars = JsonSerializer.Deserialize<CarData[]>(fileContent);

// Print all cars with at least 4 doors
var carsWithAtLeastFourDoors = cars.Where(car => car.NumberOfDoors >= 4);
foreach(var car in carsWithAtLeastFourDoors)
{
    // Console.WriteLine($"{car.Model} {car.NumberOfDoors}");
}

// Print all Mazda cars with at least 4 doors
var mazdasWithAtLeastFourDoors = cars
    .Where(car => car.Make.Equals("Mazda"))
    .Where(car => car.NumberOfDoors >= 4);
foreach(var car in mazdasWithAtLeastFourDoors)
{
    // Console.WriteLine($"{car.Model} {car.NumberOfDoors}");
}

// Print Make + Model for all Makes that start with "M"

// cars.Where(car => car.Make.StartsWith("M"))
//    .Select(car => $"{car.Make} {car.Model}")
//    .ToList()
//    .ForEach(car => Console.WriteLine(car));

// Display a list of the 10 most powerful cars (hp)
// cars.OrderByDescending(car => car.HP)
//     .Take(10)
//     .Select(car => $"{car.Make} {car.HP}")
//     .ToList()
//     .ForEach(car => Console.WriteLine(car));
    

// Display the number of models per make that appear after 2008
// Makes should be display with number of 0 if there are no models after 2008
// cars.GroupBy(car => car.Make)
//     .Select(c => new 
//     { 
//         c.Key, 
//         NumberOfModels = c.Count(car => car.Year >= 2008)
//     })
//     .ToList()
//     .ForEach(item => Console.WriteLine($"{item.Key} {item.NumberOfModels}"));


// Display a list of makes that have at least 2 models with >= 400hp
// cars.Where(car => car.HP >= 400)
//     .GroupBy(car => car.Make)
//     .Select(car => new
//     {
//         Make = car.Key,
//         NumberOfPowerfilCars = car.Count()
//     })
//     .Where(car => car.NumberOfPowerfilCars >= 2)
//     .ToList()
//     .ForEach(car => Console.WriteLine($"{car.Make} {car.NumberOfPowerfilCars}"));


// Display the average horse power per make
// cars.GroupBy(car => car.Make)
//     .Select(car => new
//     {
//         Make = car.Key,
//         AvgHP = car.Average(c => c.HP)
//     })
//     .OrderByDescending(c => c.AvgHP)
//     .ToList()
//     .ForEach(item => Console.WriteLine($"{item.Make} {item.AvgHP}"));


// How many makes build cars with HP between 0..100, 101..200, 201..300, 301..400, 401..500
cars.GroupBy(car => car.HP switch
    {
        <= 100 => "0..100",
        <= 200 => "101.200",
        <= 300 => "201..300",
        <= 400 => "301..400",
        _ => "401..500"
    })
    .Select(HPCat => new
    {
        HPCategory = HPCat.Key,
        NumberOfMakes = HPCat
            .Select(c => c.Make)
            .Distinct()
            .Count()
    })
    .ToList()
    .ForEach(HPCat => Console.WriteLine($"{HPCat.HPCategory} {HPCat.NumberOfMakes}"));

cars.ski
class CarData
{
    [JsonPropertyName("id")]
    public int ID { get; set; }
    
    [JsonPropertyName("car_make")]
    public string Make { get; set; }
    
    [JsonPropertyName("car_model")]
    public string Model { get; set; }
    
    [JsonPropertyName("car_year")]
    public int Year { get; set; }
    
    [JsonPropertyName("number_of_doors")]
    public int NumberOfDoors { get; set; }
    
    [JsonPropertyName("hp")]
    public int HP { get; set; }
}

// Defer execution
// var even = true;
//
// var result = GenerateNumbers(10);
//
// if(even) result = result.Where(n => n % 2 == 0);
// result = result.Select(n => n * 3);
// result = result.OrderByDescending(n => n);
//
// Console.WriteLine(result.Count());
//
// IEnumerable<int> GenerateNumbers(int maxValue)
// {
//     for (var i = 0; i <= maxValue; i++)
//     {
//         yield return i;
//     }
// }