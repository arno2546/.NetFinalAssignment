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
    public class PostController : ApiController
    {
        PostRepository pRep = new PostRepository();
        [Route("api/posts")]
        public IHttpActionResult Get()
        {
            return Ok(pRep.GetAll());
        }
        [Route("api/posts/{id}",Name="GetById")]
        public IHttpActionResult Get(int id)
        {
            Post p = pRep.GetById(id);
            if (p == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(p);
        }
        [Route("api/posts")]
        public IHttpActionResult Post(Post p)
        {
            pRep.Insert(p);
            string url = Url.Link("GetById", new { id = p.PostId });
            return Created(url, p);
        }
        [Route("api/posts/{id}")]
        public IHttpActionResult Put([FromBody]Post p, [FromUri]int id)
        {
            p.PostId = id;
            pRep.Edit(p);
            return Ok(p);
        }
        [Route("api/posts/{id}")]
        public IHttpActionResult Delete(int id)
        {
            pRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
