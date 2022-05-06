using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class PlayerNameValidationAttribute : RegularExpressionAttribute
{
    public PlayerNameValidationAttribute() : base("[a-zA-Z0-9_-]{3,20}")
    {
        ErrorMessage = "Player name must be 3-20 characters; only alphanumeric, _,- characters allowed!";
    }
}