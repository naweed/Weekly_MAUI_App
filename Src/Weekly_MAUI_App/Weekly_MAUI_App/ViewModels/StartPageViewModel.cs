
namespace Weekly_MAUI_App.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        private IAppService _appDataService;
        private IArticleHelperService _articleHelperService;

        public event EventHandler DataDownloadCompleted;

        private Edition _latestEdition;
        public Edition LatestEdition
        {
            get => _latestEdition;
            set => SetProperty(ref _latestEdition, value);
        }

        public Command<Article> OpenArticleCommand { get; set; }
        public Command<Article> ShareArticleCommand { get; set; }
        public Command<Article> AddBookmarkCommand { get; set; }
        public Command<Article> RemoveBookmarkCommand { get; set; }
        public Command NavigateToBookmarksPageCommand { get; set; }

        public StartPageViewModel() : base()
        {
            this.Title = "Latest Articles";

            _appDataService = ServiceContainer.GetService<IAppService>();
            _articleHelperService = ServiceContainer.GetService<IArticleHelperService>();

            OpenArticleCommand = new Command<Article>(async (article) => await _articleHelperService.OpenArticleURL(article));
            ShareArticleCommand = new Command<Article>(async (article) => await _articleHelperService.ShareArticle(article));
            AddBookmarkCommand = new Command<Article>(async (article) => await _articleHelperService.AddBookmark(article));
            RemoveBookmarkCommand = new Command<Article>(async (article) => await _articleHelperService.RemoveBookmark(article));
            NavigateToBookmarksPageCommand = new Command(async () => await (App.Current.MainPage as NavigationPage).PushAsync(new BookmarksPage()));
        }

        public override async void OnNavigatedTo(object parameters = null)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            this.LoadingText = "Preparing latest MAUI content...";

            SetDataLodingIndicators(true);

            try
            {
                //Get Latest Edition
                var editions = await _appDataService.GetEditions();

                //Get Articles for Latest Edition
                var edition = await _appDataService.GetEditionDetails(editions.OrderByDescending(e => e.Id).FirstOrDefault().Id);


                //Set Bookmark Flag
                await _articleHelperService.SetBookmarkFlag(edition.Articles);

                LatestEdition = edition;

                //TODO: To be removed: Intentionally inducing 2 secs delay to check for Loading Indicator (for testing purposes only)
                await Task.Delay(2000);

                this.DataLoaded = true;

                //Raise the Event to notify the page of download completion
                DataDownloadCompleted?.Invoke(this, new EventArgs());

                //Preload Editions
                foreach (var preloadEdition in editions.Where(e => e.Id != edition.Id))
                {
                    _ = _appDataService.GetEditionDetails(preloadEdition.Id);
                }
            }
            catch (InternetConnectionException iex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Slow or no internet connection." + Environment.NewLine + "Please check you internet connection and try again.";
                ErrorImage = "nointernet.png";
            }
            catch (Exception ex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Something went wrong. If the problem persists, plz contact support at Apps@xgeno.com with the error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
                ErrorImage = "error.png";
            }
            finally
            {
                SetDataLodingIndicators(false);
            }
        }
    }
}
