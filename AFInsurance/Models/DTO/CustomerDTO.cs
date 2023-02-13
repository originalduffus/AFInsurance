using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AFInsurance.Models
{
    public class CustomerDTO : IValidatableObject
	{
		[Required]
		[MaxLength(50)]
		[MinLength(3)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		[MinLength(3)]
		public string Surname { get; set; }

		[Required]
		public string PolicyReferenceNumber { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public string? EmailAddress { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext context)
		{
			List<ValidationResult> results = new List<ValidationResult>();

			// First name 

			if (string.IsNullOrEmpty(FirstName))
			{
				results.Add(new ValidationResult("First name is required"));
			}

			if (FirstName.Length > 50)
			{
				results.Add(new ValidationResult("First name should be less than 50 characters"));
			}

			if (FirstName.Length < 3)
			{
				results.Add(new ValidationResult("First name should be more than 3 characters"));
			}

			// Surname 

			if (Surname.Length > 50)
			{
				results.Add(new ValidationResult("Surname should be less than 50 characters"));
			}

			if (Surname.Length < 3)
			{
				results.Add(new ValidationResult("Surname should be more than 3 characters"));
			}

			if (string.IsNullOrEmpty(Surname))
			{
				results.Add(new ValidationResult("Surname is required"));
			}

			// Policy reference number

			if (string.IsNullOrEmpty(PolicyReferenceNumber))
			{
				results.Add(new ValidationResult("Policy reference number is required"));
			}

			if (Regex.IsMatch(PolicyReferenceNumber, @"^[A-Z]{2}[-]\d{6}$") == false)
			{
				results.Add(new ValidationResult("Policy reference number should be in the format XX-999999"));
			}

			// Date of birth

			if (DateOfBirth != null)
			{
				DateTime now = DateTime.Today;
				int age = now.Year - DateOfBirth.Value.Year;
				if (DateOfBirth.Value > now.AddYears(-age))
					age--;
				if (age < 18)
				{
					results.Add(new ValidationResult("Customer must be aged 18 or over"));
				}
			}

			// Email 

			if (string.IsNullOrEmpty(EmailAddress) == false)
			{
				if (Regex.IsMatch(PolicyReferenceNumber, @"^[a-zA-Z][a-zA-Z0-9]{4,}@[a-zA-Z][a-zA-Z0-9]{2,}(.com|.co.uk)$") == false)
				{
					results.Add(new ValidationResult("Email address is in the wrong format"));
				}
			}


			return results;
		}

	}
}
