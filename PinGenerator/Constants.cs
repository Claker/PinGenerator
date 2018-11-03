namespace PinGenerator
{
    internal class Constants
    {
        internal const int minNrOfDigits = 4;
        internal const int maxNrOfDigits = 9;
        internal const int maxNrOfPinsToGen = 10000;
        internal const string outOfRange = "Nr of digits must be between 1 and 10 and max pins to generate must be 10000";
        internal const string invalidRange = "You cannot generate this many unique pins with the number of digits you specified";
    }
}
