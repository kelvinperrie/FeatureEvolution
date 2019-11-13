using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp
{
    class seven_one
    {
        public seven_one()
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-1

            // Async main
            // yawn

            // default literal expressions
            var defaultLiteralExp = new DefaultLiteralExp();

            // inferred tuple element names
            var inferredTupleElementNames = new InferredTupleElementNames();

            // pattern matching on generic type parameters

        }
    }

    // default literal expressions
    class DefaultLiteralExp
    {
        public DefaultLiteralExp()
        {
            // old busted
            Func<string, bool> whereClause1 = default(Func<string, bool>);

            // new
            Func<string, bool> whereClause2 = default;

            string what = default;
            if(what == default)
            {

            }

        }
    }

    // inferred tuple element names
    // easier creation of your friend and mine, mr tuples
    class InferredTupleElementNames
    {
        public InferredTupleElementNames()
        {
            var test = "";

            string make = "Toyota";
            string model = "Corolla";

            // old
            var car1 = (make: make, model: model);
            test = car1.make;

            // new
            var car2 = (make, model);
            test = car2.make;

        }
    }
}
