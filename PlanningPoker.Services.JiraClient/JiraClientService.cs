using System.Web;
using Atlassian.Jira;
using AutoMapper;
using Microsoft.Extensions.Logging;
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
    private const string JiraPath = "rest/api/3";
    private readonly IMapper _mapper;
    private readonly ILogger<JiraClientService> _logger;

    public JiraClientService(IOptions<JiraConnection> jiraOptions, IMapper mapper, ILogger<JiraClientService> logger)
    {
        _jira = Jira.CreateRestClient(
            url: jiraOptions.Value.Url,
            username: jiraOptions.Value.Username,
            password: jiraOptions.Value.Token);
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<ICollection<JiraProjectResponse>> GetProjects()
    {
        // ?expand options = description,lead,issueTypes,url,projectKeys,permissions,insight
        var fullProjects = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{JiraPath}/project?expand=lead");
        _logger.LogDebug("Receiving information on JIRA projects");

        var shortProjects = JsonConvert.DeserializeObject<List<JiraProject>>(fullProjects.ToString());
        _logger.LogDebug("Deserializing into JiraProject, projects [{@Projects}]", shortProjects);
        
        var response = _mapper.Map<ICollection<JiraProjectResponse>>(shortProjects);
        _logger.LogDebug("Receiving list of transformed projects, project response objects [{@Projects}]", response);

        return response;
    }
    
    public async Task<List<JiraIssueResponse>> GetIssuesByProject(string projectKey)
    {
        var jqlString = $"project={HttpUtility.UrlEncode(projectKey)}+and+status!=done&fields=summary,description";

        var fullIssues = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{JiraPath}/search?jql={jqlString}");
        _logger.LogDebug("Receiving information on JIRA issues by specified project key [{Key}]", projectKey);
        
        var shortIssues = JsonConvert.DeserializeObject<JqlSearchResult>(fullIssues.ToString());
        _logger.LogDebug("Deserializing into JqlSearch, issues [{@Issues}]", shortIssues);


        var jiraIssueResponses = _mapper.Map<List<JiraIssueResponse>>(shortIssues);
        _logger.LogDebug("Mapping and flattening issues into JiraIssueResponse objects [{@Issues}]", jiraIssueResponses);
        
        return jiraIssueResponses;
    }

    public async Task<JiraIssueResponse> GetIssue(string issueKey)
    {
        var jqlString = $"issue={HttpUtility.UrlEncode(issueKey)}&fields=summary,description";
        
        var fullIssue = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{JiraPath}/search?jql={jqlString}");
        _logger.LogDebug("Receiving information on JIRA issue by specified issue key [{Key}]", issueKey);

        var shortIssue = JsonConvert.DeserializeObject<JqlSearchResult>(fullIssue.ToString());
        _logger.LogDebug("Deserializing into JqlSearch, issue [{@Issue}]", shortIssue);

        var jiraIssueResponse = _mapper.Map<List<JiraIssueResponse>>(shortIssue).First();
        _logger.LogDebug("Mapping and flattening issues into JiraIssueResponse objects [{@Issues}]", jiraIssueResponse);
        
        return jiraIssueResponse;
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