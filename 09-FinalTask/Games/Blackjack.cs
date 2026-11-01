using FinalTask.Items;
namespace FinalTask.Games
{
    public class Blackjack : CasinoGameBase
    {
        private const int MinCardsCount = 4;
        private const int StartCardsCount = 2;
        private const int BlackjackScore = 21;
        private const int AceScore = 11;
        private const int FaceCardScore = 10;
        private const int AceScoreReduction = 10;
        private readonly int _cardsCount;
        private List<Card> _cards;
        private Queue<Card> _deck;
        public Blackjack(int cardsCount)
        {
            if (cardsCount < MinCardsCount)
            {
                throw new ArgumentOutOfRangeException(
                nameof(cardsCount), $"Cards count must be at least {MinCardsCount}");
            }
            _cardsCount = cardsCount;
            FactoryMethod();
        }
        public override void PlayGame()
        {
            Shuffle();
            var playerCards = new List<Card>();
            var computerCards = new List<Card>();
            TakeCards(playerCards, StartCardsCount);
            TakeCards(computerCards, StartCardsCount);
            var playerScore = GetScore(playerCards);
            var computerScore = GetScore(computerCards);
            while (playerScore == computerScore && playerScore < BlackjackScore)
            {
                TakeCards(playerCards, 1);
                TakeCards(computerCards, 1);
                playerScore = GetScore(playerCards);
                computerScore = GetScore(computerCards);
            }
            ShowCards("Your cards", playerCards, playerScore);
            ShowCards("Computer cards", computerCards, computerScore);
            FinishGame(playerScore, computerScore);
        }
        protected override void FactoryMethod()
        {
            _cards = new List<Card>(_cardsCount);
            var suits = Enum.GetValues<Suit>();
            var values = Enum.GetValues<CardValue>();
            for (int i = 0; i < _cardsCount; i++)
            {
                var suit = suits[i % suits.Length];
                var value = values[i / suits.Length % values.Length];
                _cards.Add(new Card(suit, value));
            }
        }
        private void Shuffle()
        {
            var shuffledCards = _cards.OrderBy(card => Random.Shared.Next());
            _deck = new Queue<Card>(shuffledCards);
        }
        private void TakeCards(List<Card> hand, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (_deck.Count == 0)
                {
                    Shuffle();
                }
                hand.Add(_deck.Dequeue());
            }
        }
        private static int GetScore(List<Card> cards)
        {
            var score = 0;
            var acesCount = 0;
            foreach (var card in cards)
            {
                score += GetCardScore(card.Value);
                if (card.Value == CardValue.Ace)
                {
                    acesCount++;
                }
            }
            while (score > BlackjackScore && acesCount > 0)
            {
                score -= AceScoreReduction;
                acesCount--;
            }
            return score;
        }
        private static int GetCardScore(CardValue value)
        {
            if (value == CardValue.Ace)
            {
                return AceScore;
            }
            if (value >= CardValue.Jack)
            {
                return FaceCardScore;
            }
            return (int)value;
        }
        private static void ShowCards(string title, List<Card> cards, int score)
        {
            Console.WriteLine($"{title}: {string.Join(", ", cards)} (score: {score})");
        }
        private void FinishGame(int playerScore, int computerScore)
        {
            if (playerScore <= BlackjackScore
            && (computerScore > BlackjackScore || computerScore < playerScore))
            {
                OnWinInvoke();
            }
            else if (computerScore <= BlackjackScore
            && (playerScore > BlackjackScore || playerScore < computerScore))
            {
                OnLooseInvoke();
            }
            else
            {
                OnDrawInvoke();
            }
        }
    }
}