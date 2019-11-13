using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    class seven
    {
        public seven()
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7

            // out variables
            var outVariable = new OutVariables();

            // tuples
            var tuples = new Tuples();

            // discards
            var discared = new Discards();

            // pattern matching
            var patternMatching = new PatternMatching();

            // ref locals and returns
            // double ugh

            // local functions
            var localFunction = new LocalFunctions();

            // more expression-bodied members
            var expressionBodiedMembers = new ExpressionBodiedMembers();

            // using throw as an expression
            var throwExpression = new ThrowExpression();

            // numeric literal syntax improvements

        }
    }

    // out variables
    // when passing an out var to a method can declare it inline
    class OutVariables
    {
        public OutVariables()
        {
            string maybeAnInt = "55";

            // old
            int result;
            if(!int.TryParse(maybeAnInt, out result))
            {
                // error etc
            }

            // new
            if (!int.TryParse(maybeAnInt, out int otherResult))
            {
                // error etc
            }

            // new with implicit type
            if (!int.TryParse(maybeAnInt, out var yetAnotherResult))
            {
                // error etc
            }
        }

    }

    // tuples
    // flasher than they were ...
    class Tuples
    {
        public Tuples()
        {
            (int X, int Y) coordinate = (100, 45);
            var output = $"Located at {coordinate.X} and {coordinate.Y}";

            (int X, int Y) anotherCoordinate = (15, 150);
            output = $"The anotherCoordinate at {anotherCoordinate.X} and {anotherCoordinate.Y} does not interfere with the X and Y in the first coordinate";
            
            (int X, int Y, int Z) yetAnotherCoordinate = (15, 150, 45);
            output = $"I always thought that tubples could only have two entries, shows what I know; at {yetAnotherCoordinate.X}, {yetAnotherCoordinate.Y} and {yetAnotherCoordinate.Z}";

            // another way of intializing
            var moreCoordinates = (X : 46, Y : 78);
            output = $"Now we've got {moreCoordinates.X} and {moreCoordinates.Y}";

            // how to pull out the values / deconstruct (e.g. if returned from a method)
            (int theX, int theY) = coordinate;
            output = $"it is {theX} and {theY}";

        }
    }

    // discards
    // for write only variables that are never used, e.g. out params or deconstructing tuples
    class Discards
    {
        public Discards()
        {
            string maybeAnInt = "60";
            if(int.TryParse(maybeAnInt, out _)){
                // do something
            }


            var myDumbTuple = (Name: "Rupert", Age: 43);
            (string Name, _) = myDumbTuple;


            (_, int Age) = MakeMeATuplePlease();

        }

        private (string, int ) MakeMeATuplePlease()
        {
            return ("Sarah", 33);
        }
    }

    // pattern matching
    // for expanding switch statements; can check on types - good for dealing with objects when you don't know the type???
    class PatternMatching
    {
        public PatternMatching()
        {
            int sum = 0;
            object i = 9;
            switch (i)
            {
                case 0:
                    break;
                case IEnumerable<int> childSequence:
                    // is i an IEnumberable<int>? if yes then assign it to the variable childSequence
                    {
                        foreach (var item in childSequence)
                            sum += (item > 0) ? item : 0;
                        break;
                    }
                case int n when n > 0:
                    // if i is an int then assign it to the integer n; only do if greater than 0
                    sum += n;
                    break;
                case int n:
                    // must be an int but less than zero
                    break;
                case null:
                    throw new NullReferenceException("Null found");
                default:
                    throw new InvalidOperationException("Unrecognized type");
            }

            int j = 9;
            switch(j)
            {
                case 1:
                    break;
                case int n when n < 5:
                    break;
                case int n when n == 15:
                    break;
                case int n:
                    break;
                //case string n:
                //    break;
            }
        }
    }

    // local functions
    // put a method in a method. Why not. Just do whatever you want.
    class LocalFunctions
    {
        public void DoStuff()
        {

            int Calc2()
            {
                // cannot reference variables in scope above that have not been declaired yet ...
                //return a + b;
                return 0;
            }

            int a = 4;
            int b = 9;


            // can be accessed by expressions above or below it
            int c = Calc1(a, b);

            int Calc1(int v1, int v2)
            {
                return v1 + v2;
            }

            int k = Calc1(a, b);


            int Calc3()
            {
                // can reference variables in scope above
                return a + b;
            }

        }
    }

    // more expression-bodied members
    // now can use these on constructors or property getter/setters
    class ExpressionBodiedMembers
    {
        // example on constructor
        public ExpressionBodiedMembers() => DoSomeStuff();

        // example on property get and set
        private string colour;
        public string Colour
        {
            get => colour.ToLower();
            set => colour = value + "!";
        }

        public void DoSomeStuff() { }
    }

    // throw expression
    // I guess it could condense the code a bit
    class ThrowExpression
    {
        public void DoThatThing(string value)
        {
            // old way
            if(value == null)
            {
                throw new ArgumentNullException("what!");
            }
            var myThing1 = value;

            // a new way
            var myThing2 = value ?? throw new ArgumentNullException("what!");
        }
    }

    // numeric literal syntax improvements
    // used for defining numeric constants in a more readable way
    class NumericLiterals
    {
        // can write as binary using 0b prefix
        public const int Eight = 0b0000_1000;
        public const int Sixteen = 0b0001_0000;
        public const int ThirtyTwo = 0b0010_0000;

        // use underscore as thousands separator
        public const long Heaps = 100_000_000;
        public const int MoreHeaps = 65_342_333;

    }
}
