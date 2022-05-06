using System.ComponentModel.DataAnnotations;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Validation;

public class RoomIdValidationAttribute : RegularExpressionAttribute
{
    public RoomIdValidationAttribute() : base("[a-zA-Z]{10}")
    {
        ErrorMessage = "Incorrect room id!";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var roomIdIsValid = base.IsValid(value);
        var gameRoomService = (IGameRoomService) validationContext
            .GetService(typeof(IGameRoomService))!;
        var roomIdExists = gameRoomService.RoomIdExists((string?)value ?? string.Empty);

        if (!roomIdIsValid)
        {
            return new ValidationResult(ErrorMessage);
        }
        
        // if value == null, [Required] attribute will fire with its error message, so no message required here
        if (value != null && !roomIdExists)
        {
            return new ValidationResult($"Room with id {value} does not exist!");
        }

        return ValidationResult.Success;
    }
}