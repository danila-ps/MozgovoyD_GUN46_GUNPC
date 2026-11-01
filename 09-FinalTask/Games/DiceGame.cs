using FinalTask.Items;
namespace FinalTask.Games
{
    public class DiceGame : CasinoGameBase
    {
        private const int MinDiceCount = 1;
        private readonly int _diceCount;
        private readonly int _min;
        private readonly int _max;
        private List<Dice> _dice;
        public DiceGame(int diceCount, int min, int max)
        {
            if (diceCount < MinDiceCount)
            {
                throw new ArgumentOutOfRangeException(
                nameof(diceCount), $"Dice count must be at least {MinDiceCount}");
            }
            _diceCount = diceCount;
            _min = min;
            _max = max;
            FactoryMethod();
        }
        public override void PlayGame()
        {
            var playerScore = RollDice("You");
            var computerScore = RollDice("Computer");
            if (playerScore > computerScore)
            {
                OnWinInvoke();
            }
            else if (playerScore < computerScore)
            {
                OnLooseInvoke();
            }
            else
            {
                OnDrawInvoke();
            }
        }
        protected override void FactoryMethod()
        {
            _dice = new List<Dice>(_diceCount);
            for (int i = 0; i < _diceCount; i++)
            {
                _dice.Add(new Dice(_min, _max));
            }
        }
        private int RollDice(string playerName)
        {
            var numbers = new List<int>();
            foreach (var dice in _dice)
            {
                numbers.Add(dice.Number);
            }
            var sum = numbers.Sum();
            var numbersText = string.Join(", ", numbers);
            Console.WriteLine($"{playerName} rolled: {numbersText} (sum: {sum})");
            return sum;
        }
    }
}
