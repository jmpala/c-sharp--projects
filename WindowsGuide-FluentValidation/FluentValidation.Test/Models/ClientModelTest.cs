using FluentValidation.Demo.Model;
using Xunit;

namespace FluentValidation.Test.Model
{
    public class ClientModelTest
    {
        [Fact]
        public void Client_HasProperty_FirstName_Test()
        {
            // Arrange
            var clientDto = new ClientModel();
            var attributeName = nameof(clientDto.FirstName);

            // Act

            // Assert
            Assert.Equal("FirstName", attributeName);
        }

        [Fact]
        public void Client_HasProperty_LastName_Test()
        {
            // Arrange
            var clientDto = new ClientModel();
            var attributeName = nameof(clientDto.LastName);

            // Act

            // Assert
            Assert.Equal("LastName", attributeName);
        }

        [Fact]
        public void Client_HasProperty_Password_Test()
        {
            // Arrange
            var clientDto = new ClientModel();
            var attributeName = nameof(clientDto.Password);

            // Act

            // Assert
            Assert.Equal("Password", attributeName);
        }

        [Fact]
        public void Client_HasProperty_Email_Test()
        {
            // Arrange
            var clientDto = new ClientModel();
            var attributeName = nameof(clientDto.Email);

            // Act

            // Assert
            Assert.Equal("Email", attributeName);
        }

        [Fact]
        public void Client_HasProperty_Birthdate_Test()
        {
            // Arrange
            var clientDto = new ClientModel();
            var attributeName = nameof(clientDto.Birthdate);

            // Act

            // Assert
            Assert.Equal("Birthdate", attributeName);
        }

        [Fact]
        public void Client_HasProperty_Id_Test()
        {
            // Arrange
            var clientDto = new ClientModel();
            var attributeName = nameof(clientDto.Id);

            // Act

            // Assert
            Assert.Equal("Id", attributeName);
        }
    }
}
