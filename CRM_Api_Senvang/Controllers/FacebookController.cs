using CRM_Api_Senvang.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {



        [HttpGet("/api/facebook/pages")]
        public async Task<IActionResult> GetPageData(string access_token)
        {
            FacebookApi facebook = new(access_token);
            var pageData = await facebook.GetPageData();
            Dictionary<String, dynamic> data = new Dictionary<String, dynamic>();
            List<dynamic> list = new List<dynamic>();
            foreach (var item in pageData.data)
            {
                data.Add("name", item.name);
                data.Add("id", item.id);
                data.Add("pageAccessToken", item.access_token);
            }
            list.Add(data);

            return Ok(list);
        }

        #region Bài viết trên fanpage

        [HttpGet("/api/facebook/pages/posts")]
        public async Task<IActionResult> GetListPostFromPage(string pageAcessToken, string pageId)
        {
            FacebookApi facebook = new(pageAcessToken);
            var postData = await facebook.GetPagePosts(pageId);
            Dictionary<String, dynamic> data = new Dictionary<String, dynamic>();
            return Ok(postData?.data);
        }


        [HttpPost("/api/facebook/pages/posts")]
        public async Task<IActionResult> CreatePostPage(CreatePostPageFBDTO createPostPage)
        {
            FacebookApi facebook = new(createPostPage.pageAcessToken);
            var res = await facebook.CreatePostPage(createPostPage.message, createPostPage.pageId);
            return Ok(res?.id);
        }

        #endregion

        #region Commment của bài viết trên fanpage

        [HttpGet("/api/facebook/pages/posts/comment")]
        public async Task<IActionResult> GetCommentPostPage(string pageAcessToken, string page_postId, string filter = "toplevel")
        {
            FacebookApi facebook = new(pageAcessToken);
            var res = await facebook.GetCommentPostPage(page_postId, filter);
            return Ok(res);
        }


        [HttpPost("/api/facebook/pages/posts/comment")]
        public async Task<IActionResult> CreateCommentPostPage(CreateCommentPostPageFBDTO createCommentPost)
        {
            FacebookApi facebook = new(createCommentPost.pageAcessToken);
            var res = await facebook.CreateCommentPostPage(createCommentPost.message, createCommentPost.page_postId);
            return Ok(res);
        }


        #endregion

    }
}
