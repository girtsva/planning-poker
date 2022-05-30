using Atlassian.Jira;
using Newtonsoft.Json.Linq;

namespace PlanningPoker.Services.Interfaces;

public interface IJiraClientService
{
    Task<IEnumerable<Project>> GetProjects();
    Task<JToken> GetProjects2();
    Issue[] GetIssuesByProject();
    Task<IPagedQueryResult<Issue>> GetIssuesByProject2(string projectName);
    Task<JToken> GetIssuesByProject3(string projectName);
    Task<Issue> GetIssue(string issueKey);
    Task<JToken> GetIssue2(string issueKey);
}