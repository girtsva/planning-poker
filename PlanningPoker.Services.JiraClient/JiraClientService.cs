using System.Web;
using Atlassian.Jira;
using AutoMapper;
using Microsoft.Extensions.Options;
using MoreLinq;
using Newtonsoft.Json;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models.JiraClient;
using PlanningPoker.Common.Options;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services.JiraClient;

public class JiraClientService : IJiraClientService
{
    private readonly Jira _jira;
    private readonly string _jiraPath = "rest/api/3";
    private readonly IMapper _mapper;

    public JiraClientService(IOptions<JiraConnection> jiraOptions, IMapper mapper)
    {
        _jira = Jira.CreateRestClient(
            url: jiraOptions.Value.Url,
            username: jiraOptions.Value.Username,
            password: jiraOptions.Value.Token);
        _mapper = mapper;
    }
    
    public async Task<ICollection<JiraProjectResponse>> GetProjects()
    {
        // ?expand options = description,lead,issueTypes,url,projectKeys,permissions,insight
        var fullProjects = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/project?expand=lead");

        var shortProjects = JsonConvert.DeserializeObject<List<JiraProject>>(fullProjects.ToString());
        
        var response = _mapper.Map<ICollection<JiraProjectResponse>>(shortProjects);

        return response;
    }
    
    public async Task<List<JiraIssueResponse>> GetIssuesByProject(string projectKey)
    {
        var jqlString = $"project={HttpUtility.UrlEncode(projectKey)}+and+status!=done&fields=summary,description";

        var fullIssues = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");
        
        var shortIssues = JsonConvert.DeserializeObject<JqlSearch>(fullIssues.ToString());

        return MapIssues(shortIssues);
    }

    private List<JiraIssueResponse> MapIssues(JqlSearch shortIssues) // mapping issue fields and flattening issues
    {
        var query =
            from issue in shortIssues!.Issues
            let issueContents = issue.Fields?.Description?.Content.ToList() ?? new List<Content>()
            let textValues = (
                from content in issueContents
                let issueAllContents =
                    MoreEnumerable
                        .TraverseDepthFirst(content, topLevelContent => topLevelContent?.Contents ?? new List<Content>())
                        .Where(c => c.Text is not null)
                select issueAllContents.Select(c => c.Text))
                .SelectMany(v => v).ToList()
            select new JiraIssueResponse()
                {
                Id = issue.Id,
                Key = issue.Key,
                Summary = issue.Fields.Summary,
                Description = textValues //string.Join(Environment.NewLine, textValues)
                };

        return query.ToList();
    }

    public async Task<JiraIssueResponse> GetIssue(string issueKey)
    {
        var jqlString = $"issue={HttpUtility.UrlEncode(issueKey)}&fields=summary,description";
        
        var fullIssue = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");

        var shortIssue = JsonConvert.DeserializeObject<JqlSearch>(fullIssue.ToString());

        return MapIssues(shortIssue).First();
    }
    
    public bool JiraProjectKeyExists(string projectKey)
    {
        var projects = GetProjects().GetAwaiter().GetResult();
        
        return projects.Any(project => 
            string.Equals(project.Key, projectKey, StringComparison.InvariantCultureIgnoreCase));
    }
    
    public bool JiraIssueKeyExists(string issueKey)
    {
        try
        {
            var issue = GetIssue(issueKey).GetAwaiter().GetResult();
            return issue.Key is not null;
        }
        catch (InvalidOperationException e) when (e.Message.ToLowerInvariant().Contains("does not exist"))
        {
           return false;
        }
    }
}