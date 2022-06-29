using PlanningPoker.ApiModels.Response;

namespace PlanningPoker.Services.Interfaces;

public interface IParseFileService
{
    int TestTxtFileRead();
    List<JiraIssueResponse> ParseIssuesLocalFile();
    List<JiraIssueResponse> ParseIssues(MemoryStream stream);
}