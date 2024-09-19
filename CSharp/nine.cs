using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Text;

namespace CSharp
{
    internal class nine
    {
        public nine() { 
            // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-9

            // Records - don't really need them, but useful for passing (mostly immutable) data around
            Records();

            // Init only setters
            InitSetters();

            // Top-level statements
            // for console apps you can skip the 'class program {'  and 'static void Main(string[] args)'
            // doesn't seem to serve a purpose other than making things look like a script and hiding stuff

            // Pattern matching enhancements: relational patterns and logical patterns
            // things you can already do, but now you can do it in a different way??
            // might want this one day, but the likelyhood of remembering it exists is low
            RelationalPatterns();
            LogicalPatterns();

            // Performance and interop
            //      Native sized integers
            //          who cares. Integers sized to the platform
            //      Function pointers
            //          don't care
            //      Suppress emitting localsinit flag
            //      Module initializers
            //      New features for partial methods
            // Fit and finish features
            //      Target-typed new expressions
            TargetTypedNewExpressions();
            //      static anonymous functions
            //          dunno
            //      Target-typed conditional expressions
            //          dunno
            //      Covariant return types
            //          probably don't care
            //      Extension GetEnumerator support for foreach loops
            //          // I can't see this ever being useful
            //      Lambda discard parameters
            //      Attributes on local functions
        }

        record Pokemon(string Name, string Type);
        record Pokemon2(string Name)
        {
            public string Type {  get; set; }
        }

        private void Records()
        {
            // what is the point of records?
            // a record is by default a reference type (you can make a record struct for some reason)

            // delcare a record like this - if outside a method you would put an access modifier on it? e.g. public
            // the compiler will magically make a Name and Type member on the Pokemon record
            //record Pokemon(string Name, string Type);

            // which is the same as doing this, because the members have to be set on initialization
            //public record Pokemon2
            //{
            //    public required string Name { get; init; }
            //    public required int Age { get; init; }
            //}

            // if you're not obsessed with being immutable like most of these nerds you can make your properties mutable
            //public record Pokemon2
            //{
            //    public required string Name { get; set; }
            //    public required int Age { get; set; }
            //}

            // to create a new instance of a record you don't have to name the params, but why would you, it's like a constructor, hell if I know
            Pokemon poke1 = new("Chunky", "Earth");
            var poke2 = new Pokemon("Bubs", "Air");

            // you can make copies of records using the with keyword to change some properties or fields
            var poke3 = poke1 with { Type = "Water" };

            // which I suppose is the same as doing
            Pokemon2 poke4 = new("Wow") { Type = "Air" };
            var poke5 = poke4;
            poke5.Type = "Water";

            // apparently a record can inherit from another record, but not from a class, ditto class can't inherit a record

            // what is the difference between a record and class?
            // * I guess a record can't have a method, so records are really just for storing data
            // * supposidly 'records are immutable and classes are not' - but that is garbage
            // *    they are maybe designed to be primarily immutable
        }


        class TurtleInitSetterExample
        {
            public TurtleInitSetterExample()
            {
                Name = "Donetallo";
            }
            public string Name { get; init; } // this can only be set on initialization / by the constructor
        }
        class RabbitInitSetterExample
        {
            public required string Name { get; init; }
        }

        private void InitSetters()
        {
            // init setters just mean the field can only be set on object initialization
            var ninjaTurtle = new TurtleInitSetterExample { Name = "Leonardo" };
            // can't do this
            //ninjaTurtle.Name = "asdf";
            // the constructor can do it though
            ninjaTurtle = new TurtleInitSetterExample();
            // if you want to force the init setter to be set then you can make it 'required'
            // this won't work because the name is required
            //var rabbit = new RabbitInitSetterExample();
            // have to set the name
            var rabbit = new RabbitInitSetterExample { Name = "Bobby"};

        }


        static string ClassifyR(double measurement) => measurement switch
        {
            < -4.0 => "Too low",
            > 10.0 => "Too high",
            double.NaN => "Unknown",
            _ => "Acceptable",
        };
        static string ClassifyL(double measurement) => measurement switch
        {
            < -40.0 => "Too low",
            >= -40.0 and < 0 => "Low",
            >= 0 and < 10.0 => "Acceptable",
            >= 10.0 and < 20.0 => "High",
            >= 20.0 => "Too high",
            double.NaN => "Unknown",
        };

        private void RelationalPatterns()
        {
            // relational patterns are used to compare an expression to a constant
            var result = ClassifyR(4.5);

        }
        private void LogicalPatterns()
        {
            // logical patterns are like relational but you use an 'and', 'or', or 'is not'. So exciting.
            var result = ClassifyL(4.5);
        }

        record BadInteger(int Integer, string Reason);
        private void TargetTypedNewExpressions()
        {
            // if the compiler knows the type then you don't need to specify it during object creating using the new keyword
            List<BadInteger> BadIntegers = new();

            // useful for adding heaps of things to a list?
            List<BadInteger> BadIntegers2 = new List<BadInteger> { new(4, "Just a garbage number"), new(4, "Pure filth") };

            // or use it twice!!!!!
            List<BadInteger> BadIntegers3 = new() { new(4, "Just a garbage number"), new(4, "Pure filth") };

            // you can also do this bs to make this pretend tuple thing???
            (int a, int b) t = new(1, 2);
            t.a = 3;

            // don't know what this is? on-the-fly types
            (string name, int age) person;
            person.name = "Tex";
            person.age = 4;
            var type = person.GetType();

            // it creates an instance of an object (not a type) on the fly, you have to initialize it like this
            (string favouriteWorm, DateTime favouriteDate, int favouriteNumber, string coolestAnimal) myFavs = new("Gary", DateTime.Now, 3, "Moose");
            
        }

    }

}
