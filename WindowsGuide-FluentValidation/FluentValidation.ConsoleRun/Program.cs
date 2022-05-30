using FluentValidation.Demo.Model;
using FluentValidation.Demo.Validations;

Console.WriteLine("Start");

var clientModel = new ClientModel()
{
    Id = 1,
    FirstName = "Sam",
    LastName = "Zim"
};
var clientValidator = new ClientValidator();

var result = clientValidator.Validate(clientModel);

foreach (var error in result.Errors)
{
    Console.WriteLine(error);
}

Console.WriteLine("Finish");