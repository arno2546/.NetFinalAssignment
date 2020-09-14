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
            List<Post> HRefedPosts = PostHyperRef(pRep.GetAll());
            return Ok(HRefedPosts);
        }
        [Route("{id}",Name="GetById")]
        public IHttpActionResult Get(int id)
        {
            Post p = pRep.GetById(id);
            if (p == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + p.PostId, HttpMethod = "GET", Relation = "Self" });
            p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts", HttpMethod = "Post", Relation = "Create a new Post" });
            p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + p.PostId, HttpMethod = "PUT", Relation = "Edit self" });
            p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + p.PostId, HttpMethod = "DELETE", Relation = "DELETE self" });
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
            List<Comment> HRefedComments = CommentHyperRef(pRep.GetPostsWithComments(id));
            return Ok(HRefedComments);
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
            c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + c.PostId+"/comments/"+c.CommentId, HttpMethod = "GET", Relation = "Self" });
            c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/"+c.PostId+"/comments", HttpMethod = "Post", Relation = "Create a new Post" });
            c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" +c.PostId+"/comments/"+c.CommentId, HttpMethod = "PUT", Relation = "Edit self" });
            c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + c.PostId + "/comments/" + c.CommentId, HttpMethod = "DELETE", Relation = "DELETE self" });
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

        private List<Post> PostHyperRef(List<Post> posts)
        {
            List<Post> HRefedPosts = new List<Post>();
            foreach(Post p in posts)
            {
                p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + p.PostId, HttpMethod = "GET", Relation = "Self" });
                p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts", HttpMethod = "Post", Relation = "Create a new Post" });
                p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + p.PostId, HttpMethod = "PUT", Relation = "Edit self" });
                p.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + p.PostId, HttpMethod = "DELETE", Relation = "DELETE self" });
                HRefedPosts.Add(p);
            }
            return HRefedPosts;
        }

        private List<Comment> CommentHyperRef(List<Comment> Comments)
        {
            List<Comment> HRefedComments = new List<Comment>();
            foreach(Comment c in Comments)
            {
                c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + c.PostId + "/comments/" + c.CommentId, HttpMethod = "GET", Relation = "Self" });
                c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + c.PostId + "/comments", HttpMethod = "Post", Relation = "Create a new Post" });
                c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + c.PostId + "/comments/" + c.CommentId, HttpMethod = "PUT", Relation = "Edit self" });
                c.HyperLinks.Add(new HyperLink() { HRef = "https://localhost::44313/api/posts/" + c.PostId + "/comments/" + c.CommentId, HttpMethod = "DELETE", Relation = "DELETE self" });
                HRefedComments.Add(c);
            }
            return HRefedComments;
        }
    }
}
