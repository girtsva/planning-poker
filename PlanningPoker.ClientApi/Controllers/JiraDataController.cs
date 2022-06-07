using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> IssuesByProject4(string projectName)
    {
        var result = await _jiraClientService.GetIssuesByProject4(projectName);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> IssuesByProject6(string projectName)
    {
        var result = await _jiraClientService.GetIssuesByProject6(projectName);
        
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
    public IActionResult Issue(string issueKey)
    {
        return Ok(_jiraClientService.GetIssue(issueKey).Result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public async Task <IActionResult> Issue2(string issueKey)
    {
        var result = await _jiraClientService.GetIssue2(issueKey);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public async Task <IActionResult> Issue3(string issueKey)
    {
        var result = await _jiraClientService.GetIssue3(issueKey);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public IActionResult IssueDescription(string issueKey)
    {
        var result = _jiraClientService.Description(issueKey);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public IActionResult Issue4(string issueKey)
    {
        var result = _jiraClientService.GetIssue4(issueKey);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public IActionResult Issue5(string issueKey)
    {
        var result = _jiraClientService.GetIssue5(issueKey);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public IActionResult Issue6(string issueKey)
    {
        var result = _jiraClientService.GetIssue6(issueKey);
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{issueKey}")]
    public async Task<IActionResult> Issue7(string issueKey)
    {
        var result = await _jiraClientService.GetIssue7(issueKey);
        
        return Ok(JsonConvert.SerializeObject(result));
    }
}