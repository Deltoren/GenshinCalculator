namespace GenshinCalculator.Models
{
    public class CharacterRegion
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
