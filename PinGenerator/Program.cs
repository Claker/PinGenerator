using System;
using System.Collections.Generic;

namespace PinGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int nrOfPinsToGenerate = 1000;
            int nrOfDigits = 4;

            IPinGenerator pinGen = new PinGenerator(nrOfPinsToGenerate, nrOfDigits);
            IList<string> generatedPins = pinGen.GeneratePins();

            Console.WriteLine(String.Join("\n ", generatedPins));
            Console.WriteLine($"{generatedPins.Count} pins generated.");

            Console.ReadKey();
        }
    }
}
