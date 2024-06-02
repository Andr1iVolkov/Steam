namespace Steam.Models.News
{
    public class NewsItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string Image {  get; set; }
        public string VideoURL { get; set; }
        public int GameId { get; set; }
     // public string GameName { get; set; }
    }
}
