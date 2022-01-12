namespace Models
{
    public class CrossingCirclesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string HistoryCollectionName { get; set; } = null!;
    }
}