using AutoMapper;
using JetBrains.Annotations;
using MoreLinq;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models.JiraClient;

namespace PlanningPoker.Services.Mapping;

[UsedImplicitly]
public class JiraIssueResponseConverter : ITypeConverter<JqlSearchResult, List<JiraIssueResponse>>
{
    public List<JiraIssueResponse> Convert(JqlSearchResult shortIssues, List<JiraIssueResponse> _, ResolutionContext context)
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
}