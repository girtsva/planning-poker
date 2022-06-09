using PlanningPoker.ApiModels.Response;

namespace PlanningPoker.Services.Interfaces;

public interface IJiraClientService
{
    Task<ICollection<JiraProjectResponse>> GetProjects2();
    Task<object> GetIssuesByProject7(string projectName);
    Task<object> GetIssue7(string issueKey);
}