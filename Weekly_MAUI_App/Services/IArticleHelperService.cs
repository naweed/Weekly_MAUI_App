namespace Weekly_MAUI_App.Services
{
    public interface IArticleHelperService
    {
        Bookmarks Bookmarks { get; }

        Task SetBookmarkFlag(List<Article> articles);
        Task SetBookmarkFlag(Article article);
        Task AddBookmark(Article article);
        Task RemoveBookmark(Article article);
        Task OpenArticleURL(Article article);

        Task ShareArticle(Article article);

    }
}
