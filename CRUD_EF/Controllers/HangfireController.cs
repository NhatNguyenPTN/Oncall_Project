using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD_EF.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        [Route("queue")]
        [HttpGet]
        public async Task<ActionResult> QueueAsync()
        {
            Console.WriteLine("Queue");
            string result = "";
            var ping = new Ping();
            var pingreply = ping.Send("127.0.0.1");

            if (pingreply.Status == IPStatus.Success)
            {
                using var httpClient = new HttpClient();
                var httpMessageRequest = new HttpRequestMessage();

                httpMessageRequest.Method = HttpMethod.Get;
                httpMessageRequest.RequestUri = new Uri("http://localhost:5008/api/queue");

                var httpResponseMessage = await httpClient.SendAsync(httpMessageRequest);

                var resultGet = httpResponseMessage.StatusCode.ToString();
                result = resultGet;
            }
            return Ok(result);
        }

        [Route("delayed")]
        [HttpGet]
        public async Task<ActionResult> Delayed()
        {
            Console.WriteLine("delayed");
            Console.WriteLine("Queue");
            string result = "";
            var ping = new Ping();
            var pingreply = ping.Send("127.0.0.1");

            if (pingreply.Status == IPStatus.Success)
            {
                using var httpClient = new HttpClient();
                var httpMessageRequest = new HttpRequestMessage();

                httpMessageRequest.Method = HttpMethod.Get;
                httpMessageRequest.RequestUri = new Uri("http://localhost:5008/api/delayed");

                var httpResponseMessage = await httpClient.SendAsync(httpMessageRequest);

                var resultGet = httpResponseMessage.StatusCode.ToString();
                result = resultGet;
            }
            return Ok(result);
        }

        [Route("recurring")]
        [HttpGet]
        public async Task<ActionResult> Recurring()
        {
            Console.WriteLine("recurring");
            string result = "";
            var ping = new Ping();
            var pingreply = ping.Send("127.0.0.1");

            if (pingreply.Status == IPStatus.Success)
            {
                using var httpClient = new HttpClient();
                var httpMessageRequest = new HttpRequestMessage();

                httpMessageRequest.Method = HttpMethod.Get;
                httpMessageRequest.RequestUri = new Uri("http://localhost:5008/api/recurring");

                var httpResponseMessage = await httpClient.SendAsync(httpMessageRequest);

                var resultGet = httpResponseMessage.StatusCode.ToString();
                result = resultGet;
            }
            return Ok(result);
        }

    }
}
