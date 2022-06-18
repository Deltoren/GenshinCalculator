namespace GenshinCalculator.Models.ViewModels
{
    public class WeaponEditModel
    {
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string? ImagePath { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}
