using Nest;
using PersonSearchApi.Models;

namespace PersonSearchApi.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticClient _client;
        private const string IndexName = "persons";

        public ElasticsearchService(string elasticsearchUrl)
        {
            var settings = new ConnectionSettings(new Uri(elasticsearchUrl)).DefaultIndex(IndexName);
            _client = new ElasticClient(settings);
        }

        public async Task IndexPersonAsync(Person person)
        {
            await _client.IndexDocumentAsync(person);
        }

        public async Task<List<Person>> SearchPersonsByQualityAsync(string quality)
        {
            var response = await _client.SearchAsync<Person>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Qualities)
                        .Query(quality)
                    )
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<Person>> SearchPersonsByProjectAsync(string project)
        {
            var response = await _client.SearchAsync<Person>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Project)
                        .Query(project)
                    )
                )
            );
            return response.Documents.ToList();
        }

        public async Task<List<Person>> SearchPersonsByNameAsync(string name)
        {
            var response = await _client.SearchAsync<Person>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query(name)
                    )
                )
            );
            return response.Documents.ToList();
        }
    }
}
