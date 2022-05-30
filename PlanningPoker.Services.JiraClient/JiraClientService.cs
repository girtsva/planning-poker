using Atlassian.Jira;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlanningPoker.Common.Options;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services.JiraClient;

public class JiraClientService : IJiraClientService
{
    //private readonly IOptions<Jira> _options;
    //private readonly IConfigurationRoot
    
    private readonly Jira _jira;
    private readonly string _jiraPath = "rest/api/3";

    public JiraClientService(IOptions<JiraConnection> jiraOptions)
    {
        //_options = options;
        _jira = Jira.CreateRestClient(
            url: jiraOptions.Value.Url,
            username: jiraOptions.Value.Username,
            password: jiraOptions.Value.Token);
    }

    // static Jira jira = Jira.CreateRestClient("https://28stone.atlassian.net/rest/api/3",
    //     "girts.varna@28stone.com", "60g2TUVKthy0lW4MCyEgA57C");

    public async Task<IEnumerable<Project>> GetProjects()
    {
        return await _jira.Projects.GetProjectsAsync();
    }
    
    public async Task<JToken> GetProjects2()
    {
        return await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/project?expand=description,lead,issueTypes,url,projectKeys,permissions,insight");
    }
    
    public Issue[] GetIssuesByProject()
    {
        //return await _jira.Issues.GetIssuesAsync("NET-5");
        return _jira.Issues.Queryable.Where(x => x.Project == "NET").ToArray();
    }

    public async Task<IPagedQueryResult<Issue>> GetIssuesByProject2(string projectName)
    {
        var jqlString = "project = " + projectName;
        // var options = new IssueSearchOptions(jqlString)
        // {
        //     StartAt = 0,
        //     MaxIssuesPerRequest = -1
        // };
        //
        // List<Issue> issuesList = new List<Issue>();
        return await _jira.Issues.GetIssuesFromJqlAsync(jqlString);
        // issuesList.AddRange(req);
        // return issuesList;
    }
    
    public async Task<JToken> GetIssuesByProject3(string projectName)
    {
        var jqlString = "project = " + projectName;

        return await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");
    }
    
    public async Task<Issue> GetIssue(string issueKey)
    {
        return await _jira.Issues.GetIssueAsync(issueKey);
    }
    
    public async Task<JToken> GetIssue2(string issueKey)
    {
        //return await _jira.Issues.GetIssuesAsync("NET-5");
        return _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
                $"{_jiraPath}/issue/{issueKey}")
            .Result;
    }
    
    // IOrderedQueryable<Issue> issues = from i in jira.Issues.Queryable
    //     where i.Assignee == "admin" && i.Priority == "Major"
    //     orderby i.Created
    //     select i;
    
    //var connectionString = IConfigurationRoot
}