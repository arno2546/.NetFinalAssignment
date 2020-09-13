using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using testFinalAssignment.Repository;

namespace FinalAssignment.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostController : ApiController
    {
        PostRepository pRep = new PostRepository();
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(pRep.GetAll());
        }
        [Route("{id}",Name="GetById")]
        public IHttpActionResult Get(int id)
        {
            Post p = pRep.GetById(id);
            if (p == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(p);
        }
        [Route("")]
        public IHttpActionResult Post(Post p)
        {
            pRep.Insert(p);
            string url = Url.Link("GetById", new { id = p.PostId });
            return Created(url, p);
        }
        [Route("{id}")]
        public IHttpActionResult Put([FromBody]Post p, [FromUri]int id)
        {
            p.PostId = id;
            pRep.Edit(p);
            return Ok(p);
        }
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            pRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("{id}/comments")]
        public IHttpActionResult GetCommentWithPost(int id)
        {
            return Ok(pRep.GetPostsWithComments(id));
        }
        [Route("{id}/comments")]
        public IHttpActionResult PostCommentWithPost([FromBody]Comment c,[FromUri]int id)
        {
            CommentRepository cr = new CommentRepository();
            c.PostId = id;
            cr.Insert(c);
            return Ok(c);
        }
        [Route("{id}/comments/{cid}")]
        public IHttpActionResult GetCommentWithId(int cid, int id)
        {
            CommentRepository cr = new CommentRepository();
            Comment c = cr.GetById(cid);
            if (c == null || c.PostId!=id)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(c);
        }
        [Route("{id}/comments/{cid}")]
        public IHttpActionResult PutCommentWithId([FromBody]Comment c,[FromUri] int cid,[FromUri] int id)
        {
            CommentRepository cr = new CommentRepository();
            c.PostId = id;
            c.CommentId = cid;
            cr.Edit(c);
            return Ok(c);
        }

        [Route("{id}/comments/{cid}")]
        public IHttpActionResult DeleteCommentWithId(int cid, int id)
        {
            CommentRepository cr = new CommentRepository();
            Comment c = cr.GetById(cid);
            if (c.PostId == id)
            {
                cr.Delete(cid);
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.NotFound);
        }
    }
}
