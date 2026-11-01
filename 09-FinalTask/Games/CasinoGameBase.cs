namespace FinalTask.Games
{
    public abstract class CasinoGameBase
    {
        public event Action OnWin;
        public event Action OnLoose;
        public event Action OnDraw;
        public abstract void PlayGame();
        protected void OnWinInvoke()
        {
            Console.WriteLine("Result: you win!");
            OnWin?.Invoke();
        }
        protected void OnLooseInvoke()
        {
            Console.WriteLine("Result: you lose!");
            OnLoose?.Invoke();
        }
        protected void OnDrawInvoke()
        {
            Console.WriteLine("Result: draw!");
            OnDraw?.Invoke();
        }
       
        protected abstract void FactoryMethod();
    }
}
