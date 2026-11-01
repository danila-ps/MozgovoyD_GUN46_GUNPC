namespace FinalTask.Items
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int number, int min, int max)
        : base($"Wrong dice number: {number}. Allowed range: {min} - {max}")
        {
        }
    }
}