using Xunit;
using Domain.User.ValueObjects;
using Domain.Errors;
using Domain.Shared;

namespace Domain.Tests.User.ValueObjects
{
    public class AddressTests
    {
        private const string ValidCountry = "Valid Country";
        private const string ValidStreet = "Valid Street";
        private const string ValidCity = "Valid City";
        private const string ValidState = "Valid State";
        private const string ValidZipCode = "12345";
        private string TooLongString = new string('a', Address.MaxLength + 1);

        [Fact]
        public void Create_ShouldReturnEmptyError_WhenAnyFieldIsEmpty()
        {
            // Arrange & Act
            var result1 = Address.Create("", ValidStreet, ValidCity, ValidState, ValidZipCode);
            var result2 = Address.Create(ValidCountry, "", ValidCity, ValidState, ValidZipCode);
            var result3 = Address.Create(ValidCountry, ValidStreet, "", ValidState, ValidZipCode);
            var result4 = Address.Create(ValidCountry, ValidStreet, ValidCity, "", ValidZipCode);
            var result5 = Address.Create(ValidCountry, ValidStreet, ValidCity, ValidState, "");

            // Assert
            Assert.True(result1.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.Empty, result1.Error);
            Assert.True(result2.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.Empty, result2.Error);
            Assert.True(result3.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.Empty, result3.Error);
            Assert.True(result4.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.Empty, result4.Error);
            Assert.True(result5.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.Empty, result5.Error);
        }

        [Fact]
        public void Create_ShouldReturnCountryTooLongError_WhenCountryIsTooLong()
        {
            // Arrange & Act
            var result = Address.Create(TooLongString, ValidStreet, ValidCity, ValidState, ValidZipCode);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.CountryTooLong, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnStreetTooLongError_WhenStreetIsTooLong()
        {
            // Arrange & Act
            var result = Address.Create(ValidCountry, TooLongString, ValidCity, ValidState, ValidZipCode);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.StreetTooLong, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnCityTooLongError_WhenCityIsTooLong()
        {
            // Arrange & Act
            var result = Address.Create(ValidCountry, ValidStreet, TooLongString, ValidState, ValidZipCode);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.CityTooLong, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnStateTooLongError_WhenStateIsTooLong()
        {
            // Arrange & Act
            var result = Address.Create(ValidCountry, ValidStreet, ValidCity, TooLongString, ValidZipCode);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.StateTooLong, result.Error);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("abcde")]
        [InlineData("123456")]
        [InlineData("1234A")]
        public void Create_ShouldReturnInvalidZipCodeFormatError_WhenZipCodeIsInvalid(string invalidZipCode)
        {
            // Arrange & Act
            var result = Address.Create(ValidCountry, ValidStreet, ValidCity, ValidState, invalidZipCode);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.AddressErrors.InvalidZipCodeFormat, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenZipCodeIsValid()
        {
            // Arrange & Act
            var result = Address.Create(ValidCountry, ValidStreet, ValidCity, ValidState, ValidZipCode);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
        
        [Fact]
        public void Create_ShouldReturnSuccess_WhenAllFieldsAreValid()
        {
            // Arrange & Act
            var result = Address.Create(ValidCountry, ValidStreet, ValidCity, ValidState, ValidZipCode);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ValidCountry, result.Value.Country);
            Assert.Equal(ValidStreet, result.Value.Street);
            Assert.Equal(ValidCity, result.Value.City);
            Assert.Equal(ValidState, result.Value.State);
            Assert.Equal(ValidZipCode, result.Value.ZipCode);
        }
    }
}
