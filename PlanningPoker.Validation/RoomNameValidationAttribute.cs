using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class RoomNameValidationAttribute : RegularExpressionAttribute
{
    public RoomNameValidationAttribute() : base("[a-zA-Z0-9_-]{3,30}")
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ErrorMessage = null;
        var roomNameIsValid = base.IsValid(value);
        var result = ValidationResult.Success;

        if (!roomNameIsValid)
        {
            ErrorMessage = "Room name must be 3-30 characters; only alphanumeric, _,- characters allowed!";
        }
        else
        {
            var gameRoomService = (IGameRoomService)validationContext
                .GetService(typeof(IGameRoomService))!;
            var roomNameExists = gameRoomService.RoomNameExists((string?)value ?? string.Empty);
            
            // if value == null, [Required] attribute will fire with its error message, so no message required here
            if (ErrorMessage is null && value != null && roomNameExists)
            {
                ErrorMessage = $"Room with name {value} already exists!";
            }
        }

        if (ErrorMessage is not null)
        {
            result = new ValidationResult(ErrorMessage);
        }

        return result;
    }
}