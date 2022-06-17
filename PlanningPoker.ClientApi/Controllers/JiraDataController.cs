using Microsoft.AspNetCore.Mvc;
using PlanningPoker.ApiModels.Response;
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
    
    /// <summary>
    ///     Gets the list of JIRA projects available
    /// </summary>
    /// <response code="200">Success: Returns the list of available JIRA projects</response>
    [ProducesResponseType(typeof(List<JiraProjectResponse>), 200)]
    [HttpGet]
    public async Task<IActionResult> Projects()
    {
        var result = await _jiraClientService.GetProjects();
        
        return Ok(result);
    }
    
    /// <summary>
    ///     Gets the list of JIRA issues by project key specified
    /// </summary>
    /// <param name="projectKey">The key of JIRA project for which to show the issues</param>
    /// <response code="200">Success: Returns the list of available JIRA issues</response>
    /// <response code="400">Bad Request: if project key is not valid
    /// or the project with the specified key does not exist,
    /// or there is no sufficient permission to see it</response>
    [ProducesResponseType(typeof(List<JiraIssueResponse>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> IssuesByProject(
        [JiraProjectKeyValidation]
        string projectKey)
    {
        var result = await _jiraClientService.GetIssuesByProject(projectKey);
        
        return Ok(result);
    }
    
    /// <summary>
    ///     Gets the information on JIRA issue by issue key specified
    /// </summary>
    /// <param name="issueKey">The key of JIRA issue for which to show the details</param>
    /// <response code="200">Success: Returns the details of selected JIRA issue</response>
    /// <response code="400">Bad Request: if issue key is not valid
    /// or the issue with the specified key does not exist,
    /// or there is no sufficient permission to see it</response>
    [ProducesResponseType(typeof(JiraIssueResponse), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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