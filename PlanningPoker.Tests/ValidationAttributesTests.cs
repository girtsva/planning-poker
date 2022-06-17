using System;
using System.Collections.Generic;
using System.Linq;
using PlanningPoker.Validation;
using Xunit;

namespace PlanningPoker.Tests;

public class ValidationAttributesTests //: TestsBase
{
    //private readonly 
    
    public ValidationAttributesTests()
    {
        
    }

    public static IEnumerable<object[]> RandomPlayerIdGenerator()
    {
        var result = new string[100];
        for (int i = 0; i < 100; i++)
        {
            result[i] = Guid.NewGuid().ToString("N").Replace("-", string.Empty).Substring(2, 10);
        }

        return result.Select(x => new []{x});
    }
    
    public static IEnumerable<object[]> RandomPlayerNameGenerator()
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
        
        var result = new string[100];
        foreach (var i in Enumerable.Range(0, 100))
        {
            var length = random.Next(3, 21);
            result[i] = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        return result.Select(x => new []{x});
    }
    
    [Theory]
    [InlineData("abcdefghijk")]
    [InlineData("ABCDEFGHIJK")]
    [InlineData("ABC")]
    [InlineData("a")]
    [InlineData("abcdefghi*")]
    [InlineData("_bcdefghij")]
    [InlineData("Abcd-fghij")]
    [InlineData("012345678")]
    public void PlayerIdValidationAttribute_IncorrectPlayerId_ReturnsFalse(string playerId)
    {
        // Arrange
        var attribute = new PlayerIdValidationAttribute();

        // Act
        var result = attribute.IsValid(playerId);

        // Assert
        Assert.False(result);
        Assert.True(attribute.ErrorMessage is "Incorrect player id!");
    }
    
    [Theory]
    [MemberData(nameof(RandomPlayerIdGenerator))]
    public void PlayerIdValidationAttribute_CorrectPlayerId_ReturnsTrue(string playerIdValue)
    {
        // Arrange
        var attribute = new PlayerIdValidationAttribute();
        
        // Assert
        Assert.True(attribute.IsValid(playerIdValue));
    }
    
    [Theory]
    [InlineData("abcdefghijklmnopqrstuvxz")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTU")]
    [InlineData("a")]
    [InlineData("abcdefghi*")]
    [InlineData(" bcdefghij")]
    [InlineData("            ")]
    [InlineData("0123\";3#$%%")]
    public void PlayerNameValidationAttribute_IncorrectPlayerName_ReturnsFalse(string playerName)
    {
        // Arrange
        var attribute = new PlayerNameValidationAttribute();

        // Act
        var result = attribute.IsValid(playerName);

        // Assert
        Assert.False(result);
        Assert.True(attribute.ErrorMessage is "Player name must be 3-20 characters; only alphanumeric, _,- characters allowed!");
    }
    
    [Theory]
    [MemberData(nameof(RandomPlayerNameGenerator))]
    public void PlayerNameValidationAttribute_CorrectPlayerName_ReturnsTrue(string playerNameValue)
    {
        // Arrange
        var attribute = new PlayerNameValidationAttribute();
        
        // Assert
        Assert.True(attribute.IsValid(playerNameValue));
    }

    [Fact]
    public void RandomPlayerNameGenerator_ShouldReturnCorrectName()
    {
        // Arrange
        

        // Act
        var names = RandomPlayerNameGenerator();

        // Assert
        Assert.True(names.Count() == 100);
        Assert.Matches("[a-zA-Z0-9_-]{3,20}", names.ToString()!);
        Assert.True(names.All(name => name.ToString()!.Length >= 3 && name.Length <= 20));
    }
    
}