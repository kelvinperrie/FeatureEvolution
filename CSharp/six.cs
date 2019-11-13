using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static System.Math;

namespace CSharp
{
    public class six
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-6

        public six()
        {
            // readonly auto properties
            var myCar = new Car("Toyota", "Rocket", 1919);

            // initialize backing fields for auto properties
            var myPants = new Pants("Homer Blue", 50);

            // expression-bodied function members
            var myWrestler = new Wrestler("Bob", "Roberts", "Big BobRob");

            // using static
            var myCalc = new DumbCalcs();

            // null-conditional operators
            var vroom = new Plane();

            // string interpolation
            var logger = new Logger();

            // exception filters
            var webhandler = new Webhandler();

            // the nameof expression
            var fieldChecker = new FieldChecker();

            // Await in Catch and Finally blocks
            // ugh

            // initialize associative collections using indexers
        }
    }

    // read-only auto properties
    // the intent is to make it so the props can only be updated by the constructor
    class Car {

        // this has no setter but can still be updated by the constructor, but cannot be updated elsewhere
        public string Make { get; }
        // this can be updated by more than the constructor
        public string Model { get; private set; }
        // this is a field only setable by the constuctor
        public readonly int Year;

        public Car(string make, string model, int year )
        {
            Make = make;
            Model = model;
            Year = year;
        }

        public void PaintCar(string colour)
        {
            // Make = "accidentally changing this"; // this will generate a compile error
            Model = "accidentally changing this";   // this can be set
            // Year = 1967; // this will generate a compile error as per normal
        }


    }

    // initialize backing fields for auto properties
    // new syntax for setting a properties initial value
    class Pants
    {
        // not sure there is much point on setting the initial value of a readonly auto prop string/int etc as 
        // presumably it will be set by the constructor anyway (as that is the only place it can be set)
        public string Colour { get; } = "blue";
        public int LegCount { get; } = 2;
        public int Size { get; }
        // but being able to initialize an object/collection could be useful, rather than having to do it in the constructor?
        public List<int> PocketCount { get; } = new List<int>();

        public Pants(string colour, int size)
        {
            Colour = colour;
            Size = size;

            //PocketCount = new List<int>(); // don't have to do this if it's done above
        }
    }

    // expression-bodied function members
    // this just means that single line functions can now be written in an inline type syntax
    class Wrestler
    {
        public string Firstname;
        public string Surname;
        public string Tagname;

        public Wrestler(string first, string second, string tag)
        {
            Firstname = first;
            Surname = second;
            Tagname = tag;
        }

        // this is a method of a single line that can be made into an expression-bodied member
        public string FullNameOld()
        {
            return $"{Firstname} {Surname} aka {Tagname}";
        }

        // shortened syntax of the above method
        public string FullNameNew => $"{Firstname} {Surname} aka {Tagname}";
    }

    // using static
    // easier access to static members of another class. I guess.
    class DumbCalcs
    {
        // have added using static System.Math; to the top of the file
        public decimal RandomCalc1(decimal input)
        {
            // without the using static System.Math we would have to do
            //return Math.Abs(input);
            // with the using static System.Math we can do away with the 'Math.' bit
            return Abs(input);
        }
    }

    // null-conditional operators
    // makes for easier null checks
    class Plane
    {
        public PlaneEngine Engine;

        public string GetDetails()
        {
            // would fail because engine is null
            var test = Engine.Name;
            // standard null check & setting default if null
            if(Engine != null)
            {
                test = Engine.Name;
            }
            if(test == null)
            {
                test = "Unknown";
            }
            // using the null-conditional ops & the null coalescing op
            // should achieve the same as the above
            test = Engine?.Name ?? "Unknown";

            // if engine is null then test2 will = null, else it will equal the value of Name
            var test2 = Engine?.Name;

            return test;
        }
    }
    class PlaneEngine
    {
        public string Name;
    }

    // string interpolation
    // this allows for the easier contactenating/joining of strings
    class Logger
    {
        public void Log(string message, string level, string user)
        {
            string output = "";

            // previously we did one of
            output = string.Format("Here's a {0} log item - {1} - from {2}", level, message, user);
            output = "Here's a " + level + " log item - " + message + " - from " + user;

            // now we do
            output = $"Here's a {level} log item - {message} - from {user}";
        }
    }

    // exception filters
    // allows for a catch block to specify details of what it can handle
    class Webhandler
    {
        public static string CheckSite(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var stringTask = client.GetStringAsync(url);
                try
                {
                    return "it's ok";
                }
                catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
                {
                    return "Site Moved";
                }
                catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("500"))
                {
                    return "I've got some bad news";
                }
                catch (Exception e)
                {
                    return "Something bad happened";
                }
            }
        }
    }

    // the nameof expression
    // returns the name of the variable e.g. nameof(myVar) would return 'myVar'
    class FieldChecker
    {
        public List<string> BadFields = new List<string>();

        public FieldChecker()
        {
            var Test1 = "WOW!";
            var Test2 = "MOM";
            if (!string.Equals(Test1, "WOW"))
            {
                BadFields.Add(nameof(Test1));
            }
            if (!string.Equals(Test2, "WOW"))
            {
                BadFields.Add(nameof(Test2));
            }
        }

    }

    // initialize associative collections using indexers
    class BreadInfo
    {
        public Dictionary<string, string> Breads;

        public BreadInfo()
        {
            // old and busted
            Breads = new Dictionary<string, string>
            {
                { "Donut","Circular. Has a bit missing" },
                { "Baguette","Tastes french" },
                { "Donut 2","This one has cream in it" }
            };

            // new and fancy
            Breads = new Dictionary<string, string>
            {
                ["Donut"] ="Circular. Has a bit missing" ,
                ["Baguette"] = "Tastes french" ,
                ["Donut 2"] = "This one has cream in it" 
            };
        }
    }
}
