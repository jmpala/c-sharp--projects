using System;
using System.Threading.Channels;

var heroes = new List<Hero> {
    new("Dade", "Dide", "ChocolateMan", false),
    new("zee", string.Empty, "Homelander", true),
    new("Bruce", "Wayne", "Batman", false),
    new("pimp", string.Empty, "pups", true)
};

var result = Filter(heroes, hero => string.IsNullOrEmpty(hero.LastName));
var heroesWhoCanFly = string.Join(", ", result);
Console.WriteLine(heroesWhoCanFly);

// Linq: where
IEnumerable<T> Filter<T>(IEnumerable<T> items, Func<T, bool> f)
{
    foreach (var item in items)
        if (f(item))
            yield return item;
}


delegate bool Filter<T>(T element); // we do not need it, C# offers Func<T,TResult>

record Hero(string FirstName, string LastName, string HeroName, bool CanFly);