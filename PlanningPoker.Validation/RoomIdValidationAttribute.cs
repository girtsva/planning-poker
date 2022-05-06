using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class RoomIdValidationAttribute : RegularExpressionAttribute
{
    public RoomIdValidationAttribute() : base("[a-zA-Z]{10}")
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ErrorMessage = null;
        var roomIdIsValid = base.IsValid(value);
        var result = ValidationResult.Success;

        if (!roomIdIsValid)
        {
            ErrorMessage = "Incorrect room id!";
        }
        else
        {
            var gameRoomService = (IGameRoomService) validationContext
                .GetService(typeof(IGameRoomService))!;
            var roomIdExists = gameRoomService.RoomIdExists((string?)value ?? string.Empty);
            
            // if value == null, [Required] attribute will fire with its error message, so no message required here
            if (ErrorMessage is null && value != null && !roomIdExists)
            {
                ErrorMessage = $"Room with id {value} does not exist!";
            }
        }
        
        if (ErrorMessage is not null)
        {
            result = new ValidationResult(ErrorMessage);
        }

        return result;
    }
}