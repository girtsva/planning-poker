using PlanningPoker.ApiModels.Response;

namespace PlanningPoker.Services.Interfaces;

public interface IJiraClientService
{
    Task<ICollection<JiraProjectResponse>> GetProjects();
    Task<List<JiraIssueResponse>> GetIssuesByProject(string projectKey);
    Task<JiraIssueResponse> GetIssue(string issueKey);
    bool JiraProjectKeyExists(string projectKey);
    bool JiraIssueKeyExists(string issueKey);
}