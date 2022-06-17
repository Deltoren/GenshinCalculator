namespace GenshinCalculator.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string? ImagePath { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}
