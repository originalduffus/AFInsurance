using AFInsurance.Controllers;
using AFInsurance.Models;
using AFInsurance.Models.DB;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Net;

namespace AFInsurance.Test
{
	public class CustomerTests
	{

		public Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();

		[Fact]
		public async void GetCustomerByIdReturnsCustomer()
		{
			var expected = new Customer { Id = 1 };
			customerRepositoryMock.Setup(p => p.GetById(1)).Returns(expected);
			CustomerController emp = new CustomerController(customerRepositoryMock.Object);
			var actionResult = emp.Get(1);

			var result = actionResult as OkObjectResult;
			var value = result.Value;
			Assert.Equal(value, expected);
		}

		[Fact]
		public async void GetCustomerByIdReturnsNotFound()
		{
			var expected = new Customer { Id = 1 };
			customerRepositoryMock.Setup(p => p.GetById(1)).Returns(expected);
			CustomerController emp = new CustomerController(customerRepositoryMock.Object);
			var actionResult = emp.Get(2);

			Assert.Equal(typeof(NotFoundResult), actionResult.GetType());
		}

		[Fact]
		public async Task CheckValidationBadEmail()
		{
			// Arrange
			CustomerDTO customerDto = new CustomerDTO
			{
				FirstName = "Test",
				Surname = "Test",
				PolicyReferenceNumber = "XX-123456",
				EmailAddress = "test@test.c"
			};

			var results = new List<ValidationResult>();
			bool validateAllProperties = true;

			//Act
			bool isValid = Validator.TryValidateObject(
				customerDto,
				new ValidationContext(customerDto, null, null), 
				results, 
				validateAllProperties
			);

			// Assert
			Assert.Equal(false, isValid);
		}
	}
}