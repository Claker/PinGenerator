using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace PinGenerator.Tests
{
    [TestClass]
    public class FourDigits1000PinsUnitTests
    {
        [TestMethod]
        public void GeneratePinTest()
        {
            // arrange
            int nrOfPinsToGenerate = 1000;
            int nrOfDigits = 4;

            // act
            IPinGenerator pinGen = new PinGenerator(nrOfPinsToGenerate, nrOfDigits);

            // assert
            for (int p = 0; p < 99999; p++)
            {
                var pin = pinGen.GeneratePin();

                int i = pin / 1000;
                int j = (pin - 1000 * i) / 100;
                int k = (pin - 1000 * i - 100 * j) / 10;
                int l = pin - 1000 * i - 100 * j - k * 10;

                Assert.IsTrue(i != j && i != k && i != l && j != k && j != l && k != l);
                Assert.IsTrue(i != j - 1 && j != k - 1 && k != l - 1);
                Assert.IsTrue((int)Math.Ceiling(Math.Log10(pin)) <= 4);
            }
        }

        [TestMethod]
        public void GeneratePinsTest()
        {
            // arrange
            int nrOfPinsToGenerate = 1000;
            int nrOfDigits = 4;

            // act
            IPinGenerator pinGen = new PinGenerator(nrOfPinsToGenerate, nrOfDigits);
            var pins = pinGen.GeneratePins();

            // assert
            Assert.IsTrue(!pins.Any(p => p.Length != 4));

            foreach (int pin in pins.Select(s => int.Parse(s)))
            {
                int i = pin / 1000;
                int j = (pin - 1000 * i) / 100;
                int k = (pin - 1000 * i - 100 * j) / 10;
                int l = pin - 1000 * i - 100 * j - k * 10;

                Assert.IsTrue(i != j && i != k && i != l && j != k && j != l && k != l);
                Assert.IsTrue(i != j - 1 && j != k - 1 && k != l - 1);
                Assert.IsTrue((int)Math.Ceiling(Math.Log10(pin)) <= 4);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OutOfRangeTest()
        {
            // arrange
            int nrOfPinsToGenerate = 10001;
            int nrOfDigits = 4;

            // act
            IPinGenerator pinGen = new PinGenerator(nrOfPinsToGenerate, nrOfDigits);
            // assert is handled by the ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidRangeTest()
        {
            // arrange
            int nrOfPinsToGenerate = 1001;
            int nrOfDigits = 4;

            // act
            IPinGenerator pinGen = new PinGenerator(nrOfPinsToGenerate, nrOfDigits);
            // assert is handled by the ExpectedException
        }
    }
}
