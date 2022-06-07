using Atlassian.Jira;
using Microsoft.Extensions.Options;
using MoreLinq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlanningPoker.Common.Models;
using PlanningPoker.Common.Models.JiraClient;
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
    
    public Atlassian.Jira.Issue[] GetIssuesByProject()
    {
        //return await _jira.Issues.GetIssuesAsync("NET-5");
        return _jira.Issues.Queryable.Where(x => x.Project == "NET").ToArray();
    }

    public async Task<IPagedQueryResult<Atlassian.Jira.Issue>> GetIssuesByProject2(string projectName)
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
    
    public async Task<object> GetIssuesByProject3(string projectName)
    {
        var jqlString = "project=" + projectName + "+and+status!=done&fields=summary,description";

        var fullIssues = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");

        var shortIssues = JsonConvert.DeserializeObject<Root>(fullIssues.ToString());

        return shortIssues;
    }
    
    public async Task<object> GetIssuesByProject4(string projectName)
    {
        var jqlString = "project=" + projectName + "+and+status!=done&fields=summary,description";

        var fullIssues = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");
        
        JObject obj = JObject.Parse(fullIssues.ToString());

        //var issuesCount = (int)obj.SelectToken("total");

        var issues = new List<object>();

        var issueKeys = obj.SelectTokens("issues[*].key").Values<string>().ToList();

        foreach (var issueKey in issueKeys)
        {
            var issue = GetIssue4(issueKey);
            
            issues.Add(issue);
        }

        // for (int i = 0; i < issuesCount; i++)
        // {
        //     var issueKey = (string)obj.SelectToken("issues[i].key");
        //
        //     Console.WriteLine(issueKey);
        //
        //     var issue = GetIssue4(issueKey);
        //     
        //     issues.Add(issue);
        // }

        return issues;
    }
    
    public async Task<object> GetIssuesByProject6(string projectName)
    {
        var jqlString = "project=" + projectName + "+and+status!=done&fields=summary,description";

        var fullIssues = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");
        
        var shortIssues = JsonConvert.DeserializeObject<Root>(fullIssues.ToString());

        var issueKeys = new List<object>();
        
        foreach (var issueKey in shortIssues.issues.Select(f => f.id))
        {
            issueKeys.Add(GetIssue6(issueKey));
        }

        return issueKeys;
    }
    
    public async Task<object> GetIssuesByProject7(string projectName)
    {
        var jqlString = "project=" + projectName + "+and+status!=done&fields=summary,description";

        var fullIssues = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");
        
        var shortIssues = JsonConvert.DeserializeObject<Root>(fullIssues.ToString());

        return TransformIssues(shortIssues);
    }

    private List<(string id, string key, string summary, List<string> textValues)> TransformIssues(Root shortIssues)
    {
        var query =
            from issue in shortIssues!.issues
            let issueContents = issue.fields?.description?.content.ToList()
            from content in issueContents
            let issueAllContents =
                MoreEnumerable
                    .TraverseDepthFirst(content, topLevelContent => topLevelContent?.content ?? new List<Content>())
                    .Where(c => c.text is not null)
            group issueAllContents by new { issue.id, issue.key, issue.fields.summary }
            into contentsGroupedByIssue
            let flattenedContents = contentsGroupedByIssue.SelectMany(c => c).ToList()
            let textValues = flattenedContents.Select(c => c.text).ToList()
            select 
            (
                contentsGroupedByIssue.Key.id,
                contentsGroupedByIssue.Key.key,
                contentsGroupedByIssue.Key.summary,
                textValues //string.Join(Environment.NewLine, textValues)
            );

        return query.ToList();
    }

    public async Task<Atlassian.Jira.Issue> GetIssue(string issueKey)
    {
        return await _jira.Issues.GetIssueAsync(issueKey);
    }
    
    public async Task<JiraIssue> GetIssue2(string issueKey)
    {
        //return await _jira.Issues.GetIssuesAsync("NET-5");
        var fullIssue = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/issue/{issueKey}");
        //.Result;

        var shortIssue = JsonConvert.DeserializeObject<JiraIssue>(fullIssue.ToString());

        return shortIssue;
    }
    
    public async Task<Root> GetIssue3(string issueKey)
    {
        var jqlString = "issue=" + issueKey + "&fields=summary,description";
        
        var fullIssue = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/search?jql={jqlString}");
        //.Result;

        var shortIssue = JsonConvert.DeserializeObject<Root>(fullIssue.ToString());

        return shortIssue;
    }


    public List<string>  Description(string issueKey)
    {
        var fullIssue = FullIssue(issueKey).Result;

        JObject obj=JObject.Parse(fullIssue.ToString());
        
        IEnumerable<JToken> texts = obj.SelectTokens("$..content[*].text");  //"$..content[?(@.text != null)].text"
        
        return texts.Values<string>().ToList();
        
        // var shortIssue = GetIssue2(issueKey);
        //
        // foreach (var field in shortIssue.Result.fields.ToString())
        // {
        //     Console.WriteLine(field);
        // }
        //
        // var tmp = shortIssue.Result.fields.description.content.Select(x => x.text).ToList();
        //
        // foreach (var desc in tmp)
        // {
        //     Console.WriteLine(desc);
        // }
        //
        // return tmp;
    }

    public async Task<JToken> FullIssue(string issueKey)
    {
        return await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
            $"{_jiraPath}/issue/{issueKey}?fields=summary,description");
    }
    
    public object GetIssue4(string issueKey)
    {
        var fullIssue = FullIssue(issueKey).GetAwaiter().GetResult();

        JObject obj=JObject.Parse(fullIssue.ToString());
        
        var result = new
        {
            Id = (string)obj.SelectToken("id"),
            Key = (string)obj.SelectToken("key"),
            Summary = (string)obj.SelectToken("fields.summary"),
            Description = obj.SelectTokens("$..content[*].text").Values<string>().ToList()
        };

        return result;

        // string text = (string)obj.SelectToken("fields.description.content[0].content[0].text"); //"fields[0].description[0].content[0].content[0].text"  "fields.summary"
        //
        // IEnumerable<JToken> texts = obj.SelectTokens("$..content[*].text");  //"$..content[?(@.text != null)].text"
        //
        // foreach (JToken item in texts)
        // {
        //     Console.WriteLine(item);
        // }
        //
        // var list = new List<string>();
        //
        // list.Add(text);

        //var token = (JArray)obj.SelectToken("result");

        // var token = (JArray)fullIssue;
        //
        // var list=new List<string>();
        //
        // foreach (var item in token)
        // {
        //     string json = JsonConvert.SerializeObject(item.SelectToken("text"));
        //     list.Add(JsonConvert.DeserializeObject<string>(json));
        // }
        //
        //return texts.Values<string>().ToList();
    }

    public IEnumerable<string> Descr (Root shortIssue)
    {
        //yield return this;
        foreach (var item in shortIssue.issues.SelectMany(f => f.fields.description.content.SelectMany(c => c.content)))
        {
            yield return item.text;
        }
        
        // var desc = shortIssue.issues.Select(f => f.fields.description.content);
        //
        // if(d)
    }
    
    public object GetIssue5(string issueKey)
    {
        // var jqlString = "issue=" + issueKey + "&fields=summary,description";
        //
        // var fullIssue = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
        //     $"{_jiraPath}/search?jql={jqlString}");
        // //.Result;

        var shortIssue = GetIssue3(issueKey).GetAwaiter().GetResult();
        
        //var desc = 
        
        var result = new
        {
            Id = shortIssue.issues.Select(f => f.id).Single(),
            Key = shortIssue.issues.Select(f => f.key).Single(),
            Summary = shortIssue.issues.Select(f => f.fields.summary).SingleOrDefault(),
            Description = shortIssue.issues.SelectMany(f => f.fields.description.content.SelectMany(c => c.content.Select(t => t.text))),
            Description2 = Descr(shortIssue)
            // Description = obj.SelectTokens("$..content[*].text").Values<string>().ToList()
        };

        return result;
    }

    public object GetIssue6(string issueKey)
    {
        var shortIssue = GetIssue3(issueKey).GetAwaiter().GetResult();
        
        var query = 
            from issue in shortIssue!.issues
            let issueContents = issue.fields?.description?.content.ToList()
            from content in issueContents
            let issueAllContents =
                MoreEnumerable
                    .TraverseDepthFirst(content,topLevelContent => topLevelContent?.content ?? new List<Content>())
                    .Where(c => c.text is not null)
            group issueAllContents by new { issue.id, issue.key, issue.fields.summary }
            into contentsGroupedByIssue
            let flattenedContents = contentsGroupedByIssue.SelectMany(c => c).ToList()
            let textValues = flattenedContents.Select(c => c.text).ToList()
            select new
            {
                id = contentsGroupedByIssue.Key.id,
                key = contentsGroupedByIssue.Key.key,
                summary = contentsGroupedByIssue.Key.summary,
                description = textValues //string.Join(Environment.NewLine, textValues)
            };

        return query.First();
    }

    public async Task<object> GetIssue7(string issueKey)
    {
        // var jqlString = "issue=" + issueKey + "&fields=summary,description";
        //
        // var fullIssue = await _jira.RestClient.ExecuteRequestAsync(RestSharp.Method.GET,
        //     $"{_jiraPath}/search?jql={jqlString}");

        var shortIssue = await GetIssue3(issueKey);

        return TransformIssues(shortIssue).First(t =>
            string.Equals(t.key, issueKey, StringComparison.InvariantCultureIgnoreCase));
    }
    
    // IOrderedQueryable<Issue> issues = from i in jira.Issues.Queryable
    //     where i.Assignee == "admin" && i.Priority == "Major"
    //     orderby i.Created
    //     select i;
    
    //var connectionString = IConfigurationRoot
}