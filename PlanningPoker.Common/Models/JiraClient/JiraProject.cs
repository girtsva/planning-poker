namespace PlanningPoker.Common.Models.JiraClient;

public record JiraProject(
    string Id,
    string Key,
    Lead Lead,
    string Name
);

public record Lead(
    string DisplayName
);

