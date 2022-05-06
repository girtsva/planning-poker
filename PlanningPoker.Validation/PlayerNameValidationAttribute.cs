using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class PlayerNameValidationAttribute : RegularExpressionAttribute
{
    public string RoomId { get; set; }
    
    public PlayerNameValidationAttribute(string roomId) : base("[a-zA-Z0-9_-]{3,20}")
    {
        RoomId = roomId;
        ErrorMessage = "Player name must be 3-20 characters; only alphanumeric, _,- characters allowed!";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var playerNameIsValid = base.IsValid(value);
        var playerService = (IPlayerService) validationContext
            .GetService(typeof(IPlayerService))!;
        var playerNameExists = playerService.PlayerNameExists(RoomId,(string?)value ?? string.Empty);

        if (!playerNameIsValid)
        {
            return new ValidationResult(ErrorMessage);
        }

        // if value == null, [Required] attribute will fire with its error message, so no message required here
        if (value != null && playerNameExists)
        {
            return new ValidationResult($"Player with name {value} already exists in the room with id {RoomId}!");
        }
        
        return ValidationResult.Success;
    }
}