using System.Text.RegularExpressions;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PlanningPoker.ApiModels.Response;

namespace PlanningPoker.Services.ReadFile.Mapping;

public sealed class JiraIssueResponseMapper : ClassMap<JiraIssueResponse>
{
    public JiraIssueResponseMapper()
    {
        Map(i => i.Id).Name("Issue id")
            .Validate(key => Regex.IsMatch(key.Field, "[0-9]+", RegexOptions.None));
        Map(i => i.Key).Name("Issue key")
            .Validate(key => Regex.IsMatch(key.Field, "[a-zA-Z]+-[0-9]+", RegexOptions.CultureInvariant));
        Map(i => i.Summary).Name("Summary");
        Map(i => i.Description).Name("Description").TypeConverter<DescriptionConverter>();
    }
}