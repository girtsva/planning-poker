using Newtonsoft.Json;

namespace PlanningPoker.Common.Models.JiraClient;

public record Content(
   string Type,
   [JsonProperty("content")]
   IReadOnlyList<Content> Contents,
   string Text
);

public record Creator(
   string AccountId,
   string DisplayName
);

public record Description(
   IReadOnlyList<Content> Content
);

public record Fields(
   Status Status,
   Description Description,
   string Summary,
   Creator Creator
);

public record JiraIssue(
   string Id,
   string Key,
   Fields Fields
);

public record JqlSearchResult(
   int Total,
   IReadOnlyList<JiraIssue> Issues
);

public record Status(
   string Name,
   string Id
);