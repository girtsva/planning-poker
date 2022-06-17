using AutoMapper;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models.JiraClient;

namespace PlanningPoker.Services.Mapping;

public class JiraProjectResponseMappingProfile : Profile
{
    public JiraProjectResponseMappingProfile()
    {
        CreateMap<JqlSearchResult, List<JiraIssueResponse>>()
            .ConvertUsing<JiraIssueResponseConverter>();
    }
}