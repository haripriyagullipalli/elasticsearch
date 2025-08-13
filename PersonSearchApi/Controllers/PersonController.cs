using Microsoft.AspNetCore.Mvc;
using PersonSearchApi.Models;
using PersonSearchApi.Services;

namespace PersonSearchApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ElasticsearchService _elasticsearchService;

        public PersonController()
        {
            // Use localhost:9200 for Elasticsearch
            _elasticsearchService = new ElasticsearchService("http://localhost:9200");
        }

        [HttpPost]
        public async Task<IActionResult> IndexPerson([FromBody] PersonInputDto input)
        {
            var person = new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Qualities = input.Qualities,
                Project = input.Project
            };
            await _elasticsearchService.IndexPersonAsync(person);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPersons([FromQuery] string quality)
        {
            var results = await _elasticsearchService.SearchPersonsByQualityAsync(quality);
            return Ok(results.Select(p => p.Name));
        }

        [HttpGet("byproject")]
        public async Task<IActionResult> SearchPersonsByProject([FromQuery] string project)
        {
            var results = await _elasticsearchService.SearchPersonsByProjectAsync(project);
            return Ok(results.Select(p => p.Name));
        }

        [HttpGet("byname")]
        public async Task<IActionResult> SearchPersonsByName([FromQuery] string name)
        {
            var results = await _elasticsearchService.SearchPersonsByNameAsync(name);
            return Ok(results);
        }
    }
}
