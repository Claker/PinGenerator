using System.Collections.Generic;

namespace PinGenerator
{
    public interface IPinGenerator
    {
        /// <summary>
        /// Generates random n pins containing m digits.
        /// n and m are parameters passed while creating the object.
        /// </summary>
        IList<string> GeneratePins();
        /// <summary>
        /// Helper method to generate a random pin.
        /// If the method is called twice, this method can generate the same number as before.
        /// Only GeneratePins generates unique numbers.
        /// </summary>
        /// <returns></returns>
        int GeneratePin();
    }
}
