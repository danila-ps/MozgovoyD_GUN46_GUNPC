using System.Text.Json;
using FinalTask.Games;
using FinalTask.Profiles;
using FinalTask.SaveLoad;
namespace FinalTask.Core
{
    public class Casino : IGame
    {
        private const string ProfileId = "profile";
        private const int StartBank = 1000;
        private const int MaxBank = 5000;
        private const int BlackjackCardsCount = 36;
        private const int DiceCount = 2;
        private const int DiceMin = 1;
        private const int DiceMax = 6;
        private readonly ISaveLoadService<string> _saveLoadService;
        private readonly CasinoGameBase _blackjack;
        private readonly CasinoGameBase _diceGame;
        private PlayerProfile _profile;
        private int _bet;
        public Casino(ISaveLoadService<string> saveLoadService)
        {
            _saveLoadService = saveLoadService
            ?? throw new ArgumentNullException(nameof(saveLoadService));
            _blackjack = new Blackjack(BlackjackCardsCount);
            _diceGame = new DiceGame(DiceCount, DiceMin, DiceMax);
            Subscribe(_blackjack);
            Subscribe(_diceGame);
        }
        public void StartGame()
        {
            Console.WriteLine("Welcome to the casino!");
            LoadProfile();
            var game = ChooseGame();
            if (_profile.Bank <= 0)
            {
                Console.WriteLine("No money? Kicked!");
            }
            else
            {
                _bet = AskBet();
                Console.WriteLine($"Your bet: {_bet}. Casino bet: {_bet}");
                game.PlayGame();
            }
            Console.WriteLine($"Goodbye, {_profile.Name}! Your bank: {_profile.Bank}");
            SaveProfile();
        }
        private void Subscribe(CasinoGameBase game)
        {
            game.OnWin += HandleWin;
            game.OnLoose += HandleLoose;
            game.OnDraw += HandleDraw;
        }
        private void LoadProfile()
        {
            var data = _saveLoadService.LoadData(ProfileId);
            _profile = ParseProfile(data);
            if (_profile == null)
            {
                Console.WriteLine("Profile not found. Enter your name:");
                _profile = new PlayerProfile(ReadName(), StartBank);
                Console.WriteLine($"Profile created. Your bank: {_profile.Bank}");
                return;
            }
            Console.WriteLine($"Welcome back, {_profile.Name}! Your bank: {_profile.Bank}");
            if (_profile.Bank >= MaxBank)
            {
                _profile.Bank /= 2;
                Console.WriteLine("You wasted half of your bank money in casino's bar");
                Console.WriteLine($"Your bank: {_profile.Bank}");
            }
        }
        private static PlayerProfile ParseProfile(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }
            try
            {
                var profile = JsonSerializer.Deserialize<PlayerProfile>(data);
                if (profile == null || string.IsNullOrWhiteSpace(profile.Name))
                {
                    return null;
                }
                return profile;
            }
            catch (JsonException)
            {
                Console.WriteLine("Profile file is damaged, a new profile will be created");
                return null;
            }
        }
        private static string ReadName()
        {
            while (true)
            {
                var name = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    return name;
                }
                Console.WriteLine("Name can not be empty. Enter your name:");
            }
        }
        private CasinoGameBase ChooseGame()
        {
            Console.WriteLine("Choose game: 1 - Blackjack, 2 - Dice");
            while (true)
            {
                var input = Console.ReadLine()?.Trim();
                if (input == "1")
                {
                    return _blackjack;
                }
                if (input == "2")
                {
                    return _diceGame;
                }
                Console.WriteLine("Wrong input! Type 1 or 2");
            }
        }
        private int AskBet()
        {
            Console.WriteLine($"Make your bet (from 1 to {_profile.Bank}):");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var bet)
                && bet > 0 && bet <= _profile.Bank)
                {
                    return bet;
                }
                Console.WriteLine($"Wrong bet! Type a number from 1 to {_profile.Bank}");
            }
        }
        private void HandleWin()
        {
            var newBank = _profile.Bank + _bet;
            if (newBank > MaxBank)
            {
                var rest = newBank - MaxBank;
                _profile.Bank = MaxBank;
                Console.WriteLine($"You broke the casino! Rest of your money: {rest}");
                Console.WriteLine("A new casino will be built in its place");
            }
            else
            {
                _profile.Bank = newBank;
            }
            Console.WriteLine($"You won {_bet}. Your bank: {_profile.Bank}");
        }
        private void HandleLoose()
        {
            _profile.Bank -= _bet;
            Console.WriteLine($"You lost {_bet}. Your bank: {_profile.Bank}");
        }
        private void HandleDraw()
        {
            Console.WriteLine($"Your bet is returned. Your bank: {_profile.Bank}");
        }
        private void SaveProfile()
        {
            var data = JsonSerializer.Serialize(_profile);
            _saveLoadService.SaveData(data, ProfileId);
            Console.WriteLine("Profile saved");
        }
    }
}
