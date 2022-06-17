namespace PlanningPoker.ApiModels.Response;

public class JiraIssueResponse
{
    public string? Id { get; set; }
    public string? Key { get; set; }
    public string? Summary { get; set; }
    public List<string> Description { get; set; } = new List<string>();
}