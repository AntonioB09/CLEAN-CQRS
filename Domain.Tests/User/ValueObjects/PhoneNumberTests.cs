using Xunit;
using Domain.User.ValueObjects;
using Domain.Errors;
using Domain.Shared;

namespace Domain.Tests.User.ValueObjects
{
    public class PhoneNumberTests
    {
        [Fact]
        public void Create_ShouldReturnEmptyError_WhenPhoneNumberIsNull()
        {
            // Arrange & Act
            var result = PhoneNumber.Create(null);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.PhoneNumberErrors.Empty, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnEmptyError_WhenPhoneNumberIsEmpty()
        {
            // Arrange & Act
            var result = PhoneNumber.Create("");

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.PhoneNumberErrors.Empty, result.Error);
        }

        [Theory]
        [InlineData("12345678")]    // Too short
        [InlineData("1234567890")]   // Too long
        [InlineData("abcdefghi")]   // Invalid characters
        [InlineData("123 456 789")] // Contains spaces
        [InlineData("123-456-789")] // Contains hyphens
        [InlineData("12345678a")]   // Contains a letter
        public void Create_ShouldReturnInvalidFormatError_WhenPhoneNumberIsInvalid(string invalidPhoneNumber)
        {
            // Arrange & Act
            var result = PhoneNumber.Create(invalidPhoneNumber);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.PhoneNumberErrors.InvalidFormat, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenPhoneNumberIsValid()
        {
            // Arrange
            const string validPhoneNumber = "123456789";

            // Act
            var result = PhoneNumber.Create(validPhoneNumber);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(validPhoneNumber, result.Value.Value);
        }
        
        [Theory]
        [InlineData("000000000")]
        [InlineData("999999999")]
        [InlineData("123456789")]
        public void Create_ShouldReturnSuccess_ForValid9DigitPhoneNumbers(string validPhoneNumber)
        {
            // Arrange & Act
            var result = PhoneNumber.Create(validPhoneNumber);

            // Assert
            Assert.True(result.IsSuccess, $"Validation failed for valid phone number: {validPhoneNumber} with error: {result.Error?.Message}");
            Assert.NotNull(result.Value);
            Assert.Equal(validPhoneNumber, result.Value.Value);
        }
    }
}
