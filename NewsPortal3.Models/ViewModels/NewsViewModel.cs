using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsPortal3.Models.ViewModels
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            DateAdded = DateTimeOffset.Now;
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        public string Photo { get; set; }
        public string Header { get; set; }
        public string Review { get; set; }
        public string Text { get; set; }
        public DateTimeOffset? DateAdded { get; }

        public void AssignNews(NewsViewModel news)
        {
            Id = news.Id;
            Photo = news.Photo;
            Header = news.Header;
            Review = news.Review;
            Text = news.Text;
        }
    }
}
