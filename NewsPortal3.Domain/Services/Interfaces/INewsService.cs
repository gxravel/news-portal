using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal3.Domain.Services.Interfaces
{
    public interface INewsService
    {
        Task<AnswerModel> GetNewsList(int? page, int? pageSize);
        Task<AnswerModel> GetNews(Guid id);
        Task<AnswerModel> AddNews(NewsViewModel news);
        Task<AnswerModel> EditNews(NewsViewModel news);
        Task<AnswerModel> DeleteNews(Guid id);
    }
}
