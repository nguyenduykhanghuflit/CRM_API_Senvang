using CRM_Api_Senvang.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Headers;
using System.Text;

namespace CRM_Api_Senvang.Helpers
{
    public class FacebookApi
    {
        private static readonly string fbUrl = "https://graph.facebook.com";

        private readonly string AccessToken;

        public FacebookApi(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }


        public async Task<FacebookProfile?> GetProfile()
        {

            try
            {


                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync(fbUrl + "/me?access_token=" + AccessToken);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<FacebookProfile>(jsonResult);
                        return result;

                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        throw new Exception(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Facebook API -->/me ", ex);
            }
        }


        public async Task<PageData?> GetPageData()
        {
            try
            {
                FacebookProfile? facebookProfile = await GetProfile();
                var client = new RestClient(fbUrl);
                var request = new RestRequest(facebookProfile?.id + "/accounts").AddParameter("access_token", AccessToken);

                var response = await client.GetAsync<PageData>(request);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Facebook API -->/account ", ex);
            }
        }


        public async Task<PostPage?> GetPagePosts(string pageId)
        {
            try
            {

                var client = new RestClient(fbUrl);
                var request = new RestRequest(pageId + "/feed?fields=message,created_time,full_picture,id,story").AddParameter("access_token", AccessToken);

                var response = await client.GetAsync<PostPage>(request);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Facebook API -->/pageId/feed ", ex);
            }
        }


        public async Task<CreateResponse?> CreatePostPage(string message, string pageId)
        {
            try
            {

                var client = new RestClient(fbUrl);
                var request = new RestRequest(pageId + "/feed", Method.Post)
                    .AddParameter("access_token", AccessToken)
                    .AddParameter("message", message);

                var response = await client.PostAsync<CreateResponse>(request);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Facebook API -->create post ", ex);
            }
        }


        public async Task<CommentData?> GetCommentPostPage(string page_postId, string filter = "toplevel")
        {
            try
            {

                var client = new RestClient(fbUrl);
                var request = new RestRequest(page_postId + "/comments").AddParameter("access_token", AccessToken).AddParameter("filter", filter).AddParameter("order", "reverse_chronological");

                var response = await client.GetAsync<CommentData>(request);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling Facebook API -->/pageId/feed ", ex);
            }
        }


        //create comment or reply commnent
        public async Task<CreateResponse?> CreateCommentPostPage(string message, string page_postId)
        {
            try
            {

                var client = new RestClient(fbUrl);
                var request = new RestRequest(page_postId + "/comments", Method.Post);
                request.AddQueryParameter("access_token", AccessToken);
                request.AddJsonBody(new
                {
                    message = message
                });
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                var response = await client.PostAsync<CreateResponse>(request);
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception("Error calling Facebook API -->create comment ", ex);
            }
        }


    }

    #region Các model

    public class Comment
    {
        public string message { get; set; }
        public Comment(string message) { this.message = message; }
    }

    public class CreateResponse
    {
        public string? id { get; set; }
    }

    public class FacebookProfile
    {
        public string? name { get; set; }
        public string? id { get; set; }

    }

    public class PageData
    {
        public List<Datum>? data { get; set; }
        public Paging? paging { get; set; }
    }

    public class CategoryList
    {
        public string? id { get; set; }
        public string? name { get; set; }
    }

    public class Cursors
    {
        public string? before { get; set; }
        public string? after { get; set; }
    }

    public class Datum
    {
        public string? access_token { get; set; }
        public string? category { get; set; }
        public List<CategoryList>? category_list { get; set; }
        public string? name { get; set; }
        public string? id { get; set; }
        public List<string>? tasks { get; set; }
    }

    public class Paging
    {
        public Cursors? cursors { get; set; }
    }

    public class PostItem
    {
        public string? created_time { get; set; }
        public string? message { get; set; }
        public string? id { get; set; }
        public string? story { get; set; }
        public string? full_picture { get; set; }
    }

    public class PostPage
    {
        public List<PostItem>? data { get; set; }
        public Paging? paging { get; set; }
    }

    public class CreatePostPageFBDTO
    {
        public string pageAcessToken { get; set; }
        public string pageId { get; set; }
        public string message { get; set; }
        public CreatePostPageFBDTO()
        {
            pageAcessToken = string.Empty;
            pageId = string.Empty;
            message = string.Empty;

        }
    }

    public class CreateCommentPostPageFBDTO
    {
        public string pageAcessToken { get; set; }
        public string page_postId { get; set; }
        public string message { get; set; }
        public CreateCommentPostPageFBDTO()
        {
            pageAcessToken = string.Empty;
            page_postId = string.Empty;
            message = string.Empty;

        }
    }

    public class CommentItem
    {
        public string created_time { get; set; }
        public From from { get; set; }
        public string message { get; set; }
        public string id { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class CommentData
    {
        public List<CommentItem> data { get; set; }
        public Paging paging { get; set; }
        public Summary summary { get; set; }
    }

    public class Summary
    {
        public string order { get; set; }
        public int total_count { get; set; }
        public bool can_comment { get; set; }
    }

    #endregion



}


/*
 * Các bước: 
 * ====require: access_token
 * ====/me -> id user
 * ====/id_user/accounts -> list page -> id page, name page, accesstoken_page
 * ==== get list post page
 * ==== create post page
 * ==== get comment post page
 * ==== create comment page
 * ==== reply comment page
 * 
 
 
 */