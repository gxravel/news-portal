using Microsoft.EntityFrameworkCore;
using NewsPortal3.Data;
using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Domain.Services.Interfaces;
using NewsPortal3.Models.Auxiliary;
using NewsPortal3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal3.Domain.Services
{
    public class NewsService : INewsService
    {
        private readonly DataContext _context;

        public NewsService(DataContext context)
        {
            _context = context;
        }

        public async Task<AnswerModel> AddNews(NewsViewModel news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return new AnswerModel(news.Id);
        }

        public async Task<AnswerModel> DeleteNews(Guid id)
        {
            var answer = await GetNews(id);
            if (answer.ProblemDetails != null)
            {
                return answer;
            }
            _context.News.Remove(answer.Data as NewsViewModel);

            await _context.SaveChangesAsync();
            return new AnswerModel(Constants.Answers.Success);
        }

        public async Task<AnswerModel> EditNews(NewsViewModel news)
        {
            //var answer = await GetNews(news.Id);
            //if (answer.ProblemDetails != null)
            //{
            //    return answer;
            //}
            //var _news = answer.Data as NewsViewModel;
            //_news.AssignNews(news);
            _context.News.Update(news);
            await _context.SaveChangesAsync();
            
            return new AnswerModel(Constants.Answers.Success);
        }

        public async Task<AnswerModel> GetNews(Guid id)
        {
            var news = await _context.News
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (news == null)
            {
                var problemDetail = new ProblemDetails
                {
                    Title = Constants.Errors.NotFound,
                    Status = 404,
                    Detail = $"No news with id = {news.Id}"
                };
                return new AnswerModel(problemDetail);
            }
            return new AnswerModel(news);
        }

        public async Task<AnswerModel> GetNewsList(int? page, int? pageSize)
        {
            int _page = page ?? 1;
            int _pageSize = pageSize ?? Constants.PageSize;
            var newsList = await _context.News
                .Skip((_page - 1) * _pageSize)
                .Take(_pageSize)
                .ToArrayAsync();
            return new AnswerModel(newsList);
        }
    }
}
