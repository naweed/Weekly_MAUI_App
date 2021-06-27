namespace Weekly_MAUI_App.Services
{
    public class ArticleHelperService : IArticleHelperService
    {
        private Bookmarks _bookmarks;
        public Bookmarks Bookmarks
        {
            get => _bookmarks;
        }


        public ArticleHelperService()
        {
            //Get Bookmarks from Cache if possible
            if (CacheService.Exists(AppConstants.BookmarksKey))
            {
                _bookmarks = CacheService.Get<Bookmarks>(AppConstants.BookmarksKey);
            }
            else
                _bookmarks = new Bookmarks();
        }


        //Set Bookmarks Flag for all articles
        public async Task SetBookmarkFlag(List<Article> articles)
        {
            articles.ForEach(article => { SetBookmarkFlag(article); });
            await Task.CompletedTask;
        }

        //Set Bookmarks Flag for individual article
        public async Task SetBookmarkFlag(Article article)
        {
            if (_bookmarks.SavedArticles.Any(bookmark => bookmark.Id == article.Id))
                article.IsBookmarked = true;

            await Task.CompletedTask;
        }

        //Save Bookmarked Articles
        private async void SaveBookmarks(Bookmarks bookmarks)
        {
            CacheService.Add(AppConstants.BookmarksKey, bookmarks, TimeSpan.FromDays(5 * 365)); //Save for 5 years

            await Task.CompletedTask;
        }

        //Add Bookmark
        public async Task AddBookmark(Article article)
        {
            article.IsBookmarked = true;

            _bookmarks.Add(article);

            SaveBookmarks(_bookmarks);

            ////TODO
            ////Show Toast Message
            //Xamarin.Forms.DependencyService.Get<IToastMessage>().ShortAlert("The article has been saved in Bookmarks!");

            await Task.CompletedTask;
        }

        //Remove Bookmark
        public async Task RemoveBookmark(Article article)
        {
            article.IsBookmarked = false;

            _bookmarks.Remove(article);

            SaveBookmarks(_bookmarks);

            ////TODO
            ////Show Toast Message
            //Xamarin.Forms.DependencyService.Get<IToastMessage>().ShortAlert("Bookmark has been removed!");

            await Task.CompletedTask;
        }

        public async Task OpenArticleURL(Article article)
        {
            await Browser.OpenAsync(article.Url, new BrowserLaunchOptions { LaunchMode = BrowserLaunchMode.SystemPreferred, TitleMode = BrowserTitleMode.Show });
        }

        public async Task ShareArticle(Article article)
        {
            await Share.RequestAsync(new ShareTextRequest { Uri = article.Url, Title = article.Title, Subject = article.Title, Text = "Hi, I found this wonderful article on Xamarin. Thought you might like it. Enjoy!" });
        }
    }
}
