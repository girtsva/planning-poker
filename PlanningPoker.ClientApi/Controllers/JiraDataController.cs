using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Services.Interfaces;

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
    public async Task<IActionResult> Projects2()
    {
        var result = await _jiraClientService.GetProjects2();
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> IssuesByProject7(string projectName)
    {
        var result = await _jiraClientService.GetIssuesByProject7(projectName);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public async Task<IActionResult> Issue7(string issueKey)
    {
        var result = await _jiraClientService.GetIssue7(issueKey);
        
        return Ok(result);
    }
}