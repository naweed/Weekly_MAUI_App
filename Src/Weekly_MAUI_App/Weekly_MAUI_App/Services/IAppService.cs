namespace Weekly_MAUI_App.Services
{
    public interface IAppService
    {
        Task<List<Edition>> GetEditions();
        Task<Edition> GetEditionDetails(string editionId);
        IAsyncEnumerable<Article> SearchArticles(string searchTerm);
    }
}
