using Pdia.Entities;
using Pdia.Infrastructure;
using Pdia.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Pdia.Web.Controllers.WebApi
{
    [RequireToken]
    [RoutePrefix("api/page")]
    public class PageController : ApiController
    {
        IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        /// <summary>
        /// Get Page Information by Id
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [Route("{pageId}")]
        [HttpGet]
        [ResponseType(typeof(Page))]
        public async Task<IHttpActionResult> GetById(Guid pageId)
        {
            return Ok(await _pageService.FindAsync(pageId));
        }

        /// <summary>
        /// Create Page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Page))]
        public async Task<IHttpActionResult> PostPage(Page page)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _pageService.InsertAsync(page);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Page))]
        public async Task<IHttpActionResult> PutPage(Page page)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (page.Id == Guid.Empty)
                    return BadRequest();

                var result = await _pageService.UpdateAsync(page);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Flag Page as deleted
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        [Route("{pageId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid pageId)
        {
            if (pageId == Guid.Empty)
                return NotFound();

            Page page = await _pageService.FindAsync(pageId);
            if (page == null)
                return NotFound();

            await _pageService.DeleteAsync(page);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
