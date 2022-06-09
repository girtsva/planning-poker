using PlanningPoker.ApiModels.Response;

namespace PlanningPoker.Services.Interfaces;

public interface IJiraClientService
{
    Task<ICollection<JiraProjectResponse>> GetProjects();
    Task<object> GetIssuesByProject(string projectKey);
    Task<object> GetIssue(string issueKey);
}