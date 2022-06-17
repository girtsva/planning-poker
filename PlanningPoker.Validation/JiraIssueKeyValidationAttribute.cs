using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class JiraIssueKeyValidationAttribute : RegularExpressionAttribute
{
    // https://confluence.atlassian.com/adminjiraserver0820/changing-the-project-key-format-1095776846.html?searchId=YHPYX61UR
    public JiraIssueKeyValidationAttribute() : base("[a-zA-Z]+-[0-9]+")
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ErrorMessage = null;
        var issueKeyIsValid = base.IsValid(value);
        var result = ValidationResult.Success;

        if (!issueKeyIsValid)
        {
            ErrorMessage = "Incorrect JIRA issue key!";
        }
        else
        {
            var jiraClientService = (IJiraClientService) validationContext
                .GetService(typeof(IJiraClientService))!;
            var issueKeyExists = jiraClientService.JiraIssueKeyExists((string?)value ?? string.Empty);
            
            // if value == null, [Required] attribute will fire with its error message, so no message required here
            if (ErrorMessage is null && value != null && !issueKeyExists)
            {
                ErrorMessage = $"JIRA issue with key {value} does not exist or you do not have permission to see it!";
            }
        }
        
        if (ErrorMessage is not null)
        {
            result = new ValidationResult(ErrorMessage);
        }

        return result;
    }
}