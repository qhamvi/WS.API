using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.API.DTO.Response;
using WS.API.DTO.Topic;
using WS.API.Extensions;
using WS.API.Models;
using WS.API.Service;

namespace WS.API.Controllers
{
    [ApiController]
    [Route("topics")]
    public class TopicsController : Controller
    {
        private readonly ITopicService _service;

        public TopicsController(ITopicService service)
        {
            _service = service;
        }

        //GET /topics
        //[HttpGet]
        //public async Task<ActionResult<GetListTopicResponse>> GetTopics(GetListTopicRequest request)
        //{
        //    var topics = (await _service.GetTopicsAsync())
        //                .Select(topic => topic.AsTopicDto());
        //    return topics;
        //}

        //GET /topics/{id}
        [HttpGet("{idTopic}")]
        public async Task<ActionResult<TopicResponse>> GetTopicAsync(Guid idTopic)
        {
            var topic = await _service.GetTopicAsync(idTopic);
            if (topic is null)
            {
                return NotFound();
            }
            return topic.AsTopicDto();
        }


        //POST /topics

        [HttpPost]
        public async Task<ActionResult> CreateTopicAsync(TopicRequest request)
        {
            
            var existTopic = await _service.GetTopicExist(request);
            if(existTopic != null)
            {
                return Conflict(new ReponseMessage()
                {
                    message = "Topic existed"
                });
            }    

            await _service.CreateTopicAsync(request);
            return Ok("Success");


        }
        //PUT /topics
        [HttpPut("{idTopic}")]
        public async Task<ActionResult> UpdateTopicAsync(Guid idTopic, TopicRequest request)
        {
            var existingTopic = await _service.GetTopicAsync(idTopic);
            if (existingTopic is null)
            {
                return NotFound();
            }
            Topic updateTopic = existingTopic with
            {
                NameTopic = request.NameTopic
            };
            await _service.UpdateTopicAsync(updateTopic);
            return Content("Updated suceess");
        }

        //DELETE /topics/{idTopic}
        [HttpDelete("{idTopic}")]
        public async Task<ActionResult> DeleteTopicAsync(Guid idTopic)
        {
            var existingTopic = await _service.GetTopicAsync(idTopic);
            if (existingTopic is null)
            {
                return NotFound();
            }
            await _service.DeleteTopicAsync(idTopic);
            return Content("Deleted Success");
        }
    }
}
