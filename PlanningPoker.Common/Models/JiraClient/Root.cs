// namespace PlanningPoker.Services.JiraClient.Models;
//
//     public record Aggregateprogress(
//  int progress,
//  int total
//     );
//
//     public record Assignee(
//  string self,
//  string accountId,
//  AvatarUrls avatarUrls,
//  string displayName,
//  bool active,
//  string timeZone,
//  string accountType
//     );
//
//     public record AvatarUrls(
//  string _48x48,
//  string _24x24,
//  string _16x16,
//  string _32x32
//     );
//

namespace PlanningPoker.Common.Models.JiraClient;

public record Content(
   string type,
   IReadOnlyList<Content> content,
   string text
   // IReadOnlyList<Mark> marks
);

public record Creator(
   // string self,
   string accountId,
   // AvatarUrls avatarUrls,
   string displayName
   // bool active,
   // string timeZone,
   // string accountType
);
//
//     public record Customfield10300(
//  bool hasEpicLinkFieldDependency,
//  bool showField,
//  NonEditableReason nonEditableReason
//     );
//
public record Description(
   // int version,
   // string type,
   IReadOnlyList<Content> content
);

public record Fields(
   // DateTime statuscategorychangedate,
   // Issuetype issuetype,
   // Parent parent,
   // object timespent,
   // Project project,
   // IReadOnlyList<object> fixVersions,
   // object aggregatetimespent,
   // Resolution resolution,
   // object customfield_10795,
   // object customfield_10796,
   // object customfield_10500,
   // object customfield_10700,
   // object customfield_10701,
   // object customfield_10702,
   // object customfield_10703,
   // DateTime? resolutiondate,
   // int workratio,
   // DateTime lastViewed,
   // Watches watches,
   // DateTime created,
   // object customfield_10780,
   // object customfield_10781,
   // Priority priority,
   // object customfield_10782,
   // object customfield_10783,
   // object customfield_10784,
   // object customfield_10102,
   // Customfield10300 customfield_10300,
   // IReadOnlyList<object> labels,
   // object aggregatetimeoriginalestimate,
   // object timeestimate,
   // object customfield_10813,
   // IReadOnlyList<object> versions,
   // object customfield_10814,
   // IReadOnlyList<object> issuelinks,
   // Assignee assignee,
   // DateTime updated,
   Status status,
   // IReadOnlyList<object> components,
   // object timeoriginalestimate,
   Description description,
   // object customfield_10770,
   // string customfield_10012,
   // object customfield_10771,
   // object customfield_10772,
   // object customfield_10720,
   // object customfield_10721,
   // object customfield_10765,
   // object customfield_10600,
   // object security,
   // object customfield_10007,
   // string customfield_10722,
   // object customfield_10766,
   // object customfield_10008,
   // object customfield_10801,
   // object aggregatetimeestimate,
   string summary,
   Creator creator
   // IReadOnlyList<object> subtasks,
   // Reporter reporter,
   // DateTime? customfield_10000,
   // Aggregateprogress aggregateprogress,
   // string customfield_10001,
   // object customfield_10200,
   // object customfield_10201,
   // object customfield_10003,
   // object customfield_10762,
   // object customfield_10004,
   // string customfield_10400,
   // object customfield_10797,
   // object customfield_10798,
   // object customfield_10799,
   // object environment,
   // object customfield_10714,
   // object customfield_10715,
   // object duedate,
   // IReadOnlyList<object> customfield_10716,
   // object customfield_10717,
   // Progress progress,
   // Votes votes,
   // object customfield_10719
);

public record JiraIssue(
   // string expand,
   string id,
   // string self,
   string key,
   Fields fields
);
//
//     public record Issuetype(
//  string self,
//  string id,
//  string description,
//  string iconUrl,
//  string name,
//  bool subtask,
//  int avatarId,
//  string entityId,
//  int hierarchyLevel
//     );
//
//     public record Mark(
//  string type
//     );
//
//     public record NonEditableReason(
//  string reason,
//  string message
//     );
//
//     public record Parent(
//  string id,
//  string key,
//  string self,
//  Fields fields
//     );
//
//     public record Priority(
//  string self,
//  string iconUrl,
//  string name,
//  string id
//     );
//
//     public record Progress(
//  int progress,
//  int total
//     );
//
//     public record Project(
//  string self,
//  string id,
//  string key,
//  string name,
//  string projectTypeKey,
//  bool simplified,
//  AvatarUrls avatarUrls,
//  ProjectCategory projectCategory
//     );
//
//     public record ProjectCategory(
//  string self,
//  string id,
//  string description,
//  string name
//     );
//
//     public record Reporter(
//  string self,
//  string accountId,
//  AvatarUrls avatarUrls,
//  string displayName,
//  bool active,
//  string timeZone,
//  string accountType
//     );
//
//     public record Resolution(
//  string self,
//  string id,
//  string description,
//  string name
//     );
//
public record Root(
   // string expand,
   // int startAt,
   // int maxResults,
   int total,
   IReadOnlyList<JiraIssue> issues
);

public record Status(
   // string self,
   // string description,
   // string iconUrl,
   string name,
   string id
   // StatusCategory statusCategory
);
//
//     public record StatusCategory(
//  string self,
//  int id,
//  string key,
//  string colorName,
//  string name
//     );
//
//     public record Votes(
//  string self,
//  int votes,
//  bool hasVoted
//     );
//
//     public record Watches(
//  string self,
//  int watchCount,
//  bool isWatching
//     );