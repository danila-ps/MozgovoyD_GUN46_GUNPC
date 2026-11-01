namespace FinalTask.Profiles
{
    public class PlayerProfile
    {
        public PlayerProfile()
        {
        }
        public PlayerProfile(string name, int bank)
        {
            Name = name;
            Bank = bank;
        }
        public string Name { get; set; }
        public int Bank { get; set; }
    }
}
