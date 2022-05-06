using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class PlayerIdValidationAttribute : RegularExpressionAttribute
{
    public string RoomId { get; set; }
    
    public PlayerIdValidationAttribute(string roomId) : base("[a-zA-Z0-9]{10}")
    {
        RoomId = roomId;
        ErrorMessage = "Incorrect player id!";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var playerIdIsValid = base.IsValid(value);
        var playerService = (IPlayerService) validationContext
            .GetService(typeof(IPlayerService))!;
        var playerIdExists = playerService.PlayerIdExists(RoomId,(string?)value ?? string.Empty);

        if (!playerIdIsValid)
        {
            return new ValidationResult(ErrorMessage);
        }

        // if value == null, [Required] attribute will fire with its error message, so no message required here
        if (value != null && !playerIdExists)
        {
            return new ValidationResult($"Player with id {value} does not exist in the room with id {RoomId}!");
        }

        return ValidationResult.Success;
    }
}