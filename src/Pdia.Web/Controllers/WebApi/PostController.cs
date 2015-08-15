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
    [RoutePrefix("api/post")]
    public class PostController : ApiController
    {
        IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Get Post Information by Id
        /// </summary>
        /// <param name="postO=Id"></param>
        /// <returns></returns>
        [Route("{postId}")]
        [HttpGet]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetById(Guid postId)
        {
            return Ok(await _postService.FindAsync(postId));
        }

        /// <summary>
        /// Create Post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(Post post)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _postService.InsertAsync(post);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Post Info
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PutPost(Post post)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (post.Id == Guid.Empty)
                    return BadRequest();

                var result = await _postService.UpdateAsync(post);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Flag Post as deleted
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [Route("{postId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid postId)
        {
            if (postId == Guid.Empty)
                return NotFound();

            Post post = await _postService.FindAsync(postId);
            if (post == null)
                return NotFound();

            await _postService.DeleteAsync(post);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
