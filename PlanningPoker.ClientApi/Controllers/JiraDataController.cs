using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Services.Interfaces;
using PlanningPoker.Validation;

namespace PlanningPoker.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class JiraDataController : ControllerBase
{
    private readonly IJiraClientService _jiraClientService;

    public JiraDataController(IJiraClientService jiraClientService)
    {
        _jiraClientService = jiraClientService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Projects()
    {
        var result = await _jiraClientService.GetProjects();
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> IssuesByProject(
        [JiraProjectKeyValidation]
        string projectKey)
    {
        var result = await _jiraClientService.GetIssuesByProject(projectKey);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public async Task<IActionResult> Issue(
        [JiraIssueKeyValidation]
        string issueKey)
    {
        var result = await _jiraClientService.GetIssue(issueKey);
        
        return Ok(result);
    }
}