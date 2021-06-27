namespace Weekly_MAUI_App.DataModels
{
    public class Edition
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime UpdatedTimeStamp { get; set; }

        public string Introduction { get; set; }

        public string Summary { get; set; }

        public DateTime PublishDate { get; set; }

        public string Curators { get; set; }

        public List<Article> Articles { get; set; }

        public string EditionNo
        {
            get => $"Edition {Id}";
        }

        public string SummaryDisplay
        {
            get => (Summary == "No Edition Summary" ? "The latest edition of Weekly MAUI is out, sharing the latest goodness of MAUI." : Summary);
        }

    }
}
