using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray.HelperClasses
{

    [ExcludeFromCodeCoverage]
    public class Person
    {

        public int Id { get; set; }

        public string Name { get; set; }
        
    }

    [ExcludeFromCodeCoverage]
    public static class People
    {

        public static IEnumerable<Person> TestData => new List<Person>()
        {
            new Person() { Id = 1, Name = "Cave Johnson" },
            new Person() { Id = 2, Name = "Fjord" },
            new Person() { Id = 3, Name = "Vax" },
            new Person() { Id = 4, Name = "Vex" },
            new Person() { Id = 5, Name = "Billy" },
            new Person() { Id = 6, Name = "Joe" },
            new Person() { Id = 7, Name = "Soul" },
            new Person() { Id = 8, Name = "Black Star" }
        };

    }

}
