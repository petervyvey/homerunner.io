
using System;
using static System.Console;

namespace HomeRunner.Foundation.NextGen.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Test");

            var o = new Organisation();
            var p = default(Person);
            o.Add(Guid.NewGuid(), p);
        }
    }
}
