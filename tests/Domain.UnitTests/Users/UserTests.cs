using Domain.Users;
using Domain.Users.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void Create_Should_CreateUser_WhenParametersAreValid()
    {
        // Arrange
        Email email = Email.Create("test@test.com").Value;
        FirstName firstName = FirstName.Create("Firstname").Value;
        LastName lastName = LastName.Create("Lastname").Value;

        // Act
        var user = User.Create(firstName, lastName, email, "hash");

        // Assert
        user.Should().NotBeNull();
    }
}
