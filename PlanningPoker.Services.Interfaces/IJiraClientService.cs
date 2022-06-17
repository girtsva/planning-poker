using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models.JiraClient;

namespace PlanningPoker.Services.Interfaces;

public interface IJiraClientService
{
    /// <summary>
    ///     Gets the list of JIRA projects available
    /// </summary>
    /// <returns>List of JIRA project response objects if any; otherwise empty list</returns>
    Task<ICollection<JiraProjectResponse>> GetProjects();
    
    /// <summary>
    ///     Gets the list of JIRA issues by project key specified
    /// </summary>
    /// <param name="projectKey">The key of JIRA project for which to show the issues</param>
    /// <returns>List of available JIRA issues</returns>
    Task<List<JiraIssueResponse>> GetIssuesByProject(string projectKey);

    /// <summary>
    ///     Gets the information on JIRA issue by issue key specified
    /// </summary>
    /// <param name="issueKey">The key of JIRA issue for which to show the details</param>
    /// <returns>Details of selected JIRA issue</returns>
    Task<JiraIssueResponse> GetIssue(string issueKey);
    
    /// <summary>
    ///     Invokes <see cref="GetProjects">GetProjects</see> method and checks if provided project key exists
    /// </summary>
    /// <param name="projectKey">Specified key of JIRA project to be checked for existence</param>
    /// <returns><c>true</c> if project key exists; otherwise <c>false</c></returns>
    bool JiraProjectKeyExists(string projectKey);
    
    /// <summary>
    ///     Invokes <see cref="GetIssue">GetIssue</see> method and checks if "does not exist"
    ///     exception is thrown
    /// </summary>
    /// <param name="issueKey">Specified key of JIRA issue to be checked for existence</param>
    /// <returns><c>true</c> if exception is not thrown and issue key exists; otherwise <c>false</c></returns>
    bool JiraIssueKeyExists(string issueKey);
}