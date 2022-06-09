using Atlassian.Jira;
using Newtonsoft.Json.Linq;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models.JiraClient;

namespace PlanningPoker.Services.Interfaces;

public interface IJiraClientService
{
    Task<IEnumerable<Project>> GetProjects();
    Task<ICollection<JiraProjectResponse>> GetProjects2();
    Issue[] GetIssuesByProject();
    Task<IPagedQueryResult<Issue>> GetIssuesByProject2(string projectName);
    Task<object> GetIssuesByProject3(string projectName);
    Task<object> GetIssuesByProject4(string projectName);
    Task<object> GetIssuesByProject6(string projectName);
    Task<object> GetIssuesByProject7(string projectName);
    Task<Issue> GetIssue(string issueKey);
    Task<JiraIssue> GetIssue2(string issueKey);
    Task<Root> GetIssue3(string issueKey);
    List<string> Description(string issueKey);
    object GetIssue4(string issueKey);
    object GetIssue5(string issueKey);
    object GetIssue6(string issueKey);
    Task<object> GetIssue7(string issueKey);
}