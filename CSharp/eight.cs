using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CSharp
{
    class eight
    {
        public eight()
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8

            // readonly members
            var what = new ReadonlyMembers();

            // default interface methods
            var aCoolCar = new MyCar();
            // cannot do, not exposed
            //var output = aCoolCar.MakeNoise();

            // pattern improvements
            var patterns = new MorePatterns();

            // using declarations
            var useDeclarations = new UsingDeclarations();

            // static local functions
            var staticLocal = new StaticLocal();

            // disposable ref structs
            // don't care

            // nullable reference types
            var argh = new NullableRefs();

            // asynchronous streams
            // don't care, but probably will one day

            // indices and ranges
            var indiciesAndRanges = new IndiciesAndRanges();

            // null coalescing assignment
            var pantsChecking = new NullCoalescingAssignment();

            // unmanaged constructed types
            // why?

            // stackalloc in nested expressions
            // what


        }
    }

    // readonly members
    // you can make members/methods readonly
    struct ReadonlyMembers
    {
        // who even uses structs
        public int ActiveWheelCount { get; set; }
        public int SpareWheelCount { get; set; }

        // you can make this gut readonly ... 
        public readonly int HowManyWheelsIGot => ActiveWheelCount + SpareWheelCount;

        // can't make me readonly
        //public readonly void AddSpareWheel()
        //{
        //    SpareWheelCount += 1;
        //}
    }

    // default interface methods
    // can put a member on an interface rather than just signatures, but a default implementation isn't exposed when using the class that implements the interface ...
    // only really used for adding new methods to existing interfaces and not forcing all implementors of that interface to support?
    // I guess you could use this so that stuff that implements the interface didn't have to impelment a method, like ... it's optional
    interface IVehicle
    {
        void Start();
        void Stop(bool crashing);

        // look at this thing
        string MakeNoise()
        {
            return "vrroooooooom";
        }
    }
    class Bus : IVehicle
    {
        string status = "stopped";
        public void Start() => status = "started";

        public void Stop(bool crashing) => status = crashing ? "crashed" : "stopped";

        // I can overload it if I want
        string MakeNoise()
        {
            return "VRRRROOOOM";
        }
    }
    class MyCar : IVehicle
    {
        string status = "idle";
        public void Start() => status = "going";

        public void Stop(bool crashing) => status = crashing ? "crashed" : "hanging";

        // I don't have to do a MakeNoise member
    }

    // pattern improvements
    class MorePatterns
    {
        // the variable comes before the switch keyword
        // => instead of case :
        // _ instead of default
        public string SwitchChanges(int wheelCount) =>
            wheelCount switch
            {
                1 => "unicycle",
                2 => "bike",
                _ => "impossible!"
            };

        public void SwitchNope()
        {
            // I can't do this?
            int wheelCount = 3;
            string otherThing = "";

            //wheelCount switch
            //{
            //    1 => otherThing = "one",
            //};
        }

        // also for properties, tuples and positions
    }
    
    // using declarations
    // doin't need to put brackets after usings, less code but less readable
    class UsingDeclarations
    {
        public void DoSomething()
        {
            // new way
            // this has no brakets, so it gets disposed at the end of the scope / end of the method
            using var file1 = new System.IO.StreamWriter("WriteLines2.txt");
            file1.WriteLine("hello");

            // old way
            using (var file2 = new System.IO.StreamWriter("WriteLines2.txt"))
            {
                file2.WriteLine("hello");
            }

        }
    }

    // static local functions
    class StaticLocal
    {
        void Something()
        {
            var test = 0;

            // a regular local function
            void LocalFunction1()
            {

            }
            void LocalFunction2() => test = 9;
            // can't do this because test isn't static
            //static void LocalFunction3() => test = 11;
            static int LocalFunction4(int value1, int value2) => value1 + value2;
        }
    }

    // nullable reference types
    // so this is really just making it so you can make references types non-nullable? At just a global level?
    class NullableRefs
    {

        // enable: The nullable annotation context is enabled.The nullable warning context is enabled.
        //      Variables of a reference type, string for example, are non-nullable.All nullability warnings are enabled.
        // warnings: The nullable annotation context is disabled.The nullable warning context is enabled.
        //      Variables of a reference type are oblivious.All nullability warnings are enabled.
        // annotations: The nullable annotation context is enabled.The nullable warning context is disabled.
        //      Variables of a reference type, string for example, are non-nullable.All nullability warnings are disabled.
        // disable: The nullable annotation context is disabled.The nullable warning context is disabled.
        //      Variables of a reference type are oblivious, just like earlier versions of C#. All nullability warnings are disabled.

        // Any variable where the ? is not appended to the type name is a non-nullable reference type
        public NullableRefs()
        {
            // this is normal
            Goat goat1 = null;
            string test1 = null;

            // #nullable enable: Sets the nullable annotation context and nullable warning context to enabled
#nullable enable
            // we've made nullable enable which means we can't have nullable references; hello, slightly backwards???
            // so this lets me still assign null, but puts a squiggly under the null as a warning
            Goat goat2 = null;
            string test2 = null;

            // #nullable disable: Sets the nullable annotation context and nullable warning context to disabled
#nullable disable
            // now we're back to normal. We disabled nullable which lets us set null. Great terminology there team, top work.
            Goat goat3 = null;
            string test3 = null;

            // #nullable restore: Restores the nullable annotation context and nullable warning context to the project settings.
#nullable restore
            // we're back to the default for the project, which is allowing nullable ref types
            var myDuck = new Duck();
            // we can do this because we're in normal mode
            myDuck.BeakCount = null;

            // #nullable enable annotations: Set the nullable annotation context to enabled.
#nullable enable annotations
            // this doesn't do a warning even tho we have the annotation active
            myDuck.BeakCount = null;
#nullable restore

            // #nullable enable warnings: Set the nullable warning context to enabled
#nullable disable annotations
#nullable enable warnings
            // this does a warning even tho we don't have annotations enabled. I DON'T KNOW WHY
            // has the enable warnings turned on nullable and so made it so that ref types can't be null, if so why does the warning mention the annotation??
            myDuck.BeakCount = null;
#nullable restore

        }
    }
    class Goat
    {
        public int LegCount = 4;
    }
    class Duck
    {
        [DisallowNull]
        public int? BeakCount = null;
    }
    // indices and ranges
    // we now have system.index -> an index in a sequence with ^[index] being from the end
    // we also have system.range -> a range of a sequence
    class IndiciesAndRanges
    {
        public IndiciesAndRanges()
        {
            var pokemons = new List<string>
            {
                "Psyduck",
                "Pikamon",
                "Cloudy",
                "Dog",
                "Blastoise",
                "Other turtle thing",
                "Chargrilled"
            };
            var pokemomenents = pokemons.ToArray();

            // indexes
            string item;
            string item2;
            //item = pokemons[pokemons.Count]; // exception
            //item = pokemons[^0]; // exception, same as above
            item = pokemons[^1]; // last item
            item = pokemons[^pokemons.Count]; // first one i guess?

            // item == item2
            item = pokemons[pokemons.Count - 3];
            item2 = pokemons[^3];

            // ranges
            // the start is inclusive, end is exclusive. what? What are you doing Bill Gates!
            // you can't do a range on a list :(
            //var range1 = pokemons[1..3];
            var subset1 = pokemomenents[1..3]; // this should be just the second and third items
            subset1 = pokemomenents[^3..^1]; // this should be blastoise and Other turtle thing  ? 
            // this throws an exception, guess you can only go forwards
            //subset1 = pokemomenents[^2..^3]; // this should be Other turtle thing and blastoise? so they're backwards. I hope.
            subset1 = pokemomenents[..]; // everything? what is the point of this?
            subset1 = pokemomenents[..4]; // everything up to (and excluding) blastoise
            subset1 = pokemomenents[2..]; // everything after (and including) cloudy

            // nope, can't go backwards
            //string canIReverseThis = "Can I reverse this string using ranges?";
            //string reversed = canIReverseThis[^1..0];
            //reversed = canIReverseThis[^1..^canIReverseThis.Length]; // this is the same right?

        }
    }

    // null coalescing assignment
    // this is a shortcut for checking so see if something is null and if yes the give it a value
    class NullCoalescingAssignment
    {
        public NullCoalescingAssignment()
        {
            List<string> Pants;

            // can't do this because pants ain't null yet
            //Pants ??= new List<string>();
            Pants = null;

            // the old way
            if(Pants==null)
            {
                Pants = new List<string>();
            }

            // the new way
            Pants = null;
            Pants ??= new List<string>();


            int? specialNumber = null;
            var myResult = (specialNumber ??= 4) * 10; // uses 4 in the calc AND assigns it into the specialNumber variable

        }
    }
}
