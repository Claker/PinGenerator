using System;
using System.Collections.Generic;
using System.Linq;

namespace PinGenerator
{
    /// <summary>
    /// Generates n(first constructor parameter) number of pins with the following criterias:
    /// - all n pins must be unique within the generated result
    /// - cannot have incremental digits, e.g. 1234, or 1248
    /// - cannot have repeated digits, e.g. 888, 88631 or 102109
    /// - it can start with 0 e.g. 024, 09268 or 0246531
    /// - must be m(second constructor parameter) digits
    /// </summary>
    public class PinGenerator : IPinGenerator
    {
        private static readonly Random random = new Random();

        private int pinsStartingPoint = 0, pinsEndingPoint;
        private int nrOfPinsToGenerate, nrOfDigits;
        private int[] generatedPins;

        public PinGenerator(int nrOfPinsToGenerate, int nrOfDigits)
        {
            ValidateInput(nrOfDigits, nrOfPinsToGenerate);

            this.nrOfPinsToGenerate = nrOfPinsToGenerate;
            this.nrOfDigits = nrOfDigits;

            int vectorSize = CalculateVectorSize(nrOfDigits);
            generatedPins = new int[vectorSize];
            pinsEndingPoint = vectorSize-1;
        }

        public IList<string> GeneratePins()
        {
            List<string> generatedPinsList = new List<string>();

            while(nrOfPinsToGenerate-- > 0)
            {
                int pin = GeneratePin();
                generatedPins[pin] = 1;
            }

            // get generated pins and save then into a string list
            for (int i = pinsStartingPoint; i <= pinsEndingPoint; i++)
            {
                if (generatedPins[i] == 1)
                    generatedPinsList.Add(i.ToString("D4"));
            }

            return generatedPinsList;
        }

        public int GeneratePin()
        {
            int result;

            while (true)
            {
                int pin = random.Next(pinsStartingPoint, pinsEndingPoint);
                IList<int> digits = new List<int>(nrOfDigits);
                int digitPosition = 0;
                bool validPin = true;

                for (int i = (int)Math.Pow(10, nrOfDigits-1); i>0; i/=10)
                {
                    int digit = pin / i; // get first digit in the left

                    if (digitPosition != 0 && isPinInvalid(digits, digitPosition, digit))
                    {
                        validPin = false;
                        digitPosition++;
                        break;
                    }

                    digits.Add(digit);
                    pin -= digit * i; // remove valid digit from pin
                    digitPosition++;
                }

                pin = ConvertIntListToInt(digits);

                if (!validPin || generatedPins[pin] == 1)
                    continue;

                result = pin;
                break;
            }

            return result;
        }

        #region Helper methods
        private void ValidateInput(int nrOfDigits, int nrOfPinsToGenerate){

            if (IsInputOutOfRange(nrOfDigits, nrOfPinsToGenerate))
                throw new ArgumentOutOfRangeException(Constants.outOfRange);

            if (IsInputInvalid(nrOfDigits, nrOfPinsToGenerate))
                throw new InvalidOperationException(Constants.invalidRange);
        }

        private Func<int, int> CalculateVectorSize = (int nrOfDigits) 
            => (int)Math.Pow(10, nrOfDigits);

        private Func<IList<int>, int,int, bool> isPinInvalid = (digits, digitPosition, digit)
            => digits[digitPosition - 1] == digit - 1 || digits.Any(d => d == digit);

        private Func<IList<int>,int> ConvertIntListToInt = (digits)
            => int.Parse(string.Join(string.Empty, digits));

        private Func<int, int, bool> IsInputOutOfRange = (nrOfDigits, nrOfPinsToGenerate)
            => nrOfDigits < Constants.minNrOfDigits || nrOfDigits > Constants.maxNrOfDigits
                || nrOfPinsToGenerate > Constants.maxNrOfPinsToGen;

        private Func<int, int, bool> IsInputInvalid = (nrOfDigits, nrOfPinsToGenerate)
            => (int)Math.Ceiling(Math.Log10(nrOfPinsToGenerate)) >= nrOfDigits;
        #endregion
    }
}
