using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlanningPoker.Common.Options;
using PlanningPoker.Services.Interfaces;
using PlanningPoker.Services.JiraClient;

namespace PlanningPoker.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class JiraDataController : ControllerBase
{
    private readonly IJiraClientService _jiraClientService;
    //private readonly Jira _jiraOptions;

    public JiraDataController(IJiraClientService jiraClientService)
    {
        _jiraClientService = jiraClientService;
        //_jiraOptions = jiraOptions.Value;
    }
    
    [HttpGet]
    public IActionResult Projects()
    {
        return Ok(_jiraClientService.GetProjects().Result);
    }
    
    [HttpGet]
    public IActionResult Projects2()
    {
        return Ok(_jiraClientService.GetProjects2().Result.ToString());
    }
    
    [HttpGet]
    public IActionResult IssuesByProject()
    {
        return Ok(_jiraClientService.GetIssuesByProject());
    }

    [HttpGet]
    public IActionResult IssuesByProject2(string projectName)
    {
        return Ok(_jiraClientService.GetIssuesByProject2(projectName).Result);
    }
    
    [HttpGet]
    public async Task<IActionResult> IssuesByProject3(string projectName)
    {
        var result = await _jiraClientService.GetIssuesByProject3(projectName);
        
        return Ok(result.ToString());
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public IActionResult Issue(string issueKey)
    {
        return Ok(_jiraClientService.GetIssue(issueKey).Result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public IActionResult Issue2(string issueKey)
    {
        return Ok(_jiraClientService.GetIssue2(issueKey).Result.ToString());
    }
}