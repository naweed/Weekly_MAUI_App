
namespace Weekly_MAUI_App.ViewModels
{
    public class BookmarksPageViewModel : ViewModelBase
    {
        private IArticleHelperService _articleHelperService;

        public Command<Article> OpenArticleCommand { get; set; }
        public Command<Article> ShareArticleCommand { get; set; }
        public Command<Article> AddBookmarkCommand { get; set; }
        public Command<Article> RemoveBookmarkCommand { get; set; }

        private List<Article> _articles;
        public List<Article> Articles
        {
            get => _articles;
            set => SetProperty(ref _articles, value);
        }


        public BookmarksPageViewModel() : base()
        {
            this.Title = "My Bookmarks";

            _articleHelperService = ServiceContainer.GetService<IArticleHelperService>();

            OpenArticleCommand = new Command<Article>(async (article) => await _articleHelperService.OpenArticleURL(article));
            ShareArticleCommand = new Command<Article>(async (article) => await _articleHelperService.ShareArticle(article));
            AddBookmarkCommand = new Command<Article>(async (article) => await _articleHelperService.AddBookmark(article));
            RemoveBookmarkCommand = new Command<Article>(async (article) => await _articleHelperService.RemoveBookmark(article));
        }

        public override async void OnNavigatedTo(object parameters = null)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            Articles = _articleHelperService.Bookmarks.SavedArticles;

            this.DataLoaded = true;
        }
    }
}
