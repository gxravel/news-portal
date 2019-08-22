using Microsoft.AspNetCore.Mvc;
using NewsPortal3.Domain.ErrorHandler;
using NewsPortal3.Domain.Services.Interfaces;
using NewsPortal3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal3.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/news")]
    public class NewsController : Controller
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("getAll")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> GetAll(int? page, int? pageSize)
        {
            return await _service.GetNewsList(page, pageSize);
        }

        /// <summary>
        /// Get news
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Get(Guid id)
        {
            return await _service.GetNews(id);
        }

        /// <summary>
        /// Edit news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Edit(NewsViewModel news)
        {
            return await _service.EditNews(news);
        }

        /// <summary>
        /// Add news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Add(NewsViewModel news)
        {
            return await _service.AddNews(news);
        }

        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        [ProducesResponseType(200)]
        public async Task<AnswerModel> Delete(Guid id)
        {
            return await _service.DeleteNews(id);
        }
    }
}
