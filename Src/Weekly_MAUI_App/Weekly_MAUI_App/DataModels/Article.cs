using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly_MAUI_App.DataModels
{
    public partial class Article : BindableBase
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string EditionId { get; set; }

        public string Author { get; set; }

        public string Id { get; set; }

        public string Category { get; set; }

        private bool _isBookmarked;
        public bool IsBookmarked
        {
            get => _isBookmarked;
            set => SetProperty(ref _isBookmarked, value);
        }

        public override string ToString()
        {
            return $"{Title} {Description} {Author} {Category}".ToLower();
        }
    }
}
