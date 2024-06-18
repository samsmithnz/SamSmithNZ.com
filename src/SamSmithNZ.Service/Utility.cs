namespace SamSmithNZ.Service
{
    public class Utility
    {
        private readonly static System.Random rnd = new();
        public static double GenerateRandomNumber(int lowerBound, int upperBound)
        {
            double result = rnd.Next(lowerBound, upperBound + 1); // +1 because the upperbound is never used
            return result;
        }
    }
}
