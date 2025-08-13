namespace PersonSearchApi.Models
{
    public class PersonInputDto
    {
        public string? Name { get; set; }
        public List<string>? Qualities { get; set; }
        public string? Project { get; set; }
    }
}
