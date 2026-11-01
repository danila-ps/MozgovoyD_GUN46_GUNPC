namespace FinalTask.SaveLoad
{
    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private const string FileExtension = ".txt";
        private readonly string _path;
        public FileSystemSaveLoadService(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path can not be empty", nameof(path));
            }
            _path = path;
        }
        public void SaveData(string data, string id)
        {
            try
            {
                CreateDirectoryIfNotExists();
                using (var writer = new StreamWriter(GetFilePath(id)))
                {
                    writer.Write(data);
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Save error: {exception.Message}");
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine($"No access to save file: {exception.Message}");
            }
        }
        public string LoadData(string id)
        {
            try
            {
                CreateDirectoryIfNotExists();
                var filePath = GetFilePath(id);
                if (!File.Exists(filePath))
                {
                    return string.Empty;
                }
                using (var reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Load error: {exception.Message}");
                return string.Empty;
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine($"No access to save file: {exception.Message}");
                return string.Empty;
            }
        }
        private void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
        private string GetFilePath(string id) => Path.Combine(_path, id + FileExtension);
    }
}