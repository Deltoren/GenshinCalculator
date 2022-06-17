namespace GenshinCalculator.Models.ViewModels
{
    public class CharacterCreateModel
    {
        public string Name { get; set; }
        public string? Fullname { get; set; }
        public string? AvatarPath { get; set; }
        public int Rarity { get; set; }
        public string? Description { get; set; }
        public DateTime DayOfBirth { get; set; }
        public Vision Vision { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}
