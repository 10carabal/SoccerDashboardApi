namespace FootballStats.Domain.Entities
{
    public class PlayerTopScorer
    {
        public string Name { get; set; } = string.Empty;
        public string Team { get; set; } = string.Empty;
        public int Goals { get; set; }
        public int Assists { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
