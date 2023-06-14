using CRM_Api_Senvang.Helpers;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace CRM_Api_Senvang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {

        [HttpPost("/api/chatgpt")]
        public async Task<IActionResult> ChatGPTAsync(Message msg)
        {
            string apiKey = "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";
            var openAIRequest = new OpenAIRequest(apiKey);
            var response = await openAIRequest.SendRequest("gpt-3.5-turbo", msg.content);
            return Ok(response);
        }



    }
    public class Message
    {
        public string content { get; set; }
    }
}
