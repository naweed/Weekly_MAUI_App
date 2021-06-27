using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly_MAUI_App.DataModels
{
    public class Bookmarks
    {
        public List<Article> SavedArticles { get; set; } = new List<Article>();

        //Add a Bookmark
        public void Add(Article atricle)
        {
            if (!SavedArticles.Any(_artcile => _artcile.Id == atricle.Id))
            {
                SavedArticles.Add(atricle);
            }
        }

        //Remove a bookmark
        public void Remove(Article atricle)
        {
            if (SavedArticles.Any(_artcile => _artcile.Id == atricle.Id))
            {
                SavedArticles.Remove(SavedArticles.Where(s => s.Id == atricle.Id).First());
            }
        }
    }
}
