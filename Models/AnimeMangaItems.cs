namespace AniMangaVault2.Models
{
    public class AnimeMangaItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Title} (ID: {Id})";
        }
    }
}
