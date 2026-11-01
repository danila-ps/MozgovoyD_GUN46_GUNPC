namespace FinalTask.Items
{
    public readonly struct Card
    {
        public Card(Suit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }
        public Suit Suit { get; }
        public CardValue Value { get; }
        public override string ToString() => $"{Value} of {Suit}";
    }
}