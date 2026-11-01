namespace FinalTask.Items
{
    public readonly struct Dice
    {
        private const int MinAllowedNumber = 1;
        private readonly int _min;
        private readonly int _max;
        public Dice(int min, int max)
        {
            if (min < MinAllowedNumber)
            {
                throw new WrongDiceNumberException(min, MinAllowedNumber, int.MaxValue);
            }
            if (max < MinAllowedNumber)
            {
                throw new WrongDiceNumberException(max, MinAllowedNumber, int.MaxValue);
            }
            if (max < min)
            {
                throw new WrongDiceNumberException(max, min, int.MaxValue);
            }
            _min = min;
            _max = max;
        }
     
        public int Number => (int)Random.Shared.NextInt64(_min, (long)_max + 1);
    }
}