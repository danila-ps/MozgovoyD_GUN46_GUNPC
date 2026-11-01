using FinalTask.Core;
using FinalTask.SaveLoad;
namespace FinalTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var savePath = Path.Combine(AppContext.BaseDirectory, "Saves");
            var saveLoadService = new FileSystemSaveLoadService(savePath);
            IGame casino = new Casino(saveLoadService);
            casino.StartGame();
        }
    }
}
