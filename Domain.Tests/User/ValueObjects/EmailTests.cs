using Xunit;
using Domain.User.ValueObjects;
using Domain.Errors;
using Domain.Shared;

namespace Domain.Tests.User.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void Create_ShouldReturnEmptyError_WhenEmailIsEmptyOrWhitespace()
        {
            // Arrange & Act
            var resultEmpty = Email.Create("");
            var resultWhitespace = Email.Create("   ");

            // Assert
            Assert.True(resultEmpty.IsFailure);
            Assert.Equal(DomainErrors.EmailErrors.Empty, resultEmpty.Error);
            Assert.True(resultWhitespace.IsFailure);
            Assert.Equal(DomainErrors.EmailErrors.Empty, resultWhitespace.Error);
        }

        [Theory]
        [InlineData("test@")]
        [InlineData("@example.com")]
        [InlineData("test@example")] // Fails because domain part needs a TLD like .com
        [InlineData("test@.com")] // Fails because domain part before .com is missing
        [InlineData("plainaddress")]
        [InlineData("#@%^%#$@#$@#.com")]
        [InlineData("@example.com")]
        [InlineData("Joe Smith <email@example.com>")]
        [InlineData("email.example.com")]
        [InlineData("email@example@example.com")]
        [InlineData(".email@example.com")]
        [InlineData("email.@example.com")]
        [InlineData("email..email@example.com")]
        [InlineData("email@example.com (Joe Smith)")]
        [InlineData("email@example")]
        [InlineData("email@-example.com")]
        // [InlineData("email@example.web")] // This one might be valid depending on TLD list, but regex should catch basic structure
        [InlineData("email@111.222.333.44444")] // Invalid IP format in domain
        [InlineData("email@example..com")]
        [InlineData("Abc..123@example.com")]
        public void Create_ShouldReturnInvalidFormatError_WhenEmailFormatIsInvalid(string invalidEmail)
        {
            // Arrange & Act
            var result = Email.Create(invalidEmail);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.EmailErrors.InvalidFormat, result.Error);
        }

        [Theory]
        [InlineData("email@example.com")]
        [InlineData("firstname.lastname@example.com")]
        [InlineData("email@subdomain.example.com")]
        [InlineData("firstname+lastname@example.com")]
        [InlineData("email@123.123.123.123")] // Valid IP address format
        [InlineData("email@[123.123.123.123]")] // Valid IP address format with brackets
        [InlineData("\"email\"@example.com")]
        [InlineData("1234567890@example.com")]
        [InlineData("email@example-one.com")]
        [InlineData("_______@example.com")]
        [InlineData("email@example.name")]
        [InlineData("email@example.museum")]
        [InlineData("email@example.co.jp")]
        [InlineData("firstname-lastname@example.com")]
        public void Create_ShouldReturnSuccess_WhenEmailFormatIsValid(string validEmail)
        {
            // Arrange & Act
            var result = Email.Create(validEmail);

            // Assert
            Assert.True(result.IsSuccess, $"Validation failed for valid email: {validEmail} with error: {result.Error?.Message}");
            Assert.NotNull(result.Value);
            Assert.Equal(validEmail, result.Value.Value);
        }
    }
}
