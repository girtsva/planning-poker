using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class PlayerIdValidationAttribute : RegularExpressionAttribute
{
    public PlayerIdValidationAttribute() : base("[a-zA-Z0-9]{10}")
    {
        ErrorMessage = "Incorrect player id!";
    }
}